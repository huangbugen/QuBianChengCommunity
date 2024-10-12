using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Extensions;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity;
using FileDownLoadSystem.Entity.AttributeManager;
using FileDownLoadSystem.Entity.Core;
using FileDownLoadSystem.Entity.FileInfo;
using Microsoft.EntityFrameworkCore;

namespace FileDownLoadSystem.Core.BaseProvider
{
    public class BaseService<TModel, TRepository>
        where TModel : BaseModel
        where TRepository : IRepository<TModel>
    {
        protected TRepository _repository;
        protected WebResponseContent Response { get; set; }
        public BaseService(TRepository repository)
        {
            this._repository = repository;
            Response = new(true);
        }

        public TModel FindFirst(Expression<Func<TModel, bool>> predicate,
            Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null)
        {
            return _repository.FindFirst(predicate, orderBy);
        }

        public virtual WebResponseContent AddEntity(TModel entity)
        {
            return Add<TModel>(entity, null);
        }
        public WebResponseContent Add<TDetail>(TModel entity, List<TDetail> list = null)
            where TDetail : BaseModel
        {
            _repository.Insert(entity);
            // 保存明细
            if (list != null && list.Count > 0)
            {
                list.ForEach(x =>
                {
                    _repository.DbContext.Entry<TDetail>(x).State = EntityState.Added;
                });
                _repository.DbContext.SaveChanges();
            }
            Response.OK(ResponseType.SaveSuccess);

            return Response;
        }

        public WebResponseContent UpdateEntity<TDetail>(
            SaveModel saveModel,
            PropertyInfo mainKeyProperty,
            PropertyInfo detailKeyInfo,
            object keyDefaultVal)
            where TDetail : BaseModel, new()
        {
            var mainEntity = saveModel.MainData?.DicToEntity<TModel>();
            var detailList = saveModel.DetailData.DicToList<TDetail>();
            // 需要处理的集合
            List<TDetail> addList = new();
            List<TDetail> editList = new();
            List<object> delKeys = new();

            foreach (var detail in detailList)
            {
                object keyValue = detailKeyInfo.GetValue(detail);
                if(keyDefaultVal.Equals(keyValue))
                {
                    // 设置新增的主表值
                    if(detailKeyInfo.PropertyType == typeof(Guid))
                    {
                        detailKeyInfo.SetValue(detail,Guid.NewGuid());
                    }
                    addList.Add(detail);
                }
                else
                {
                    editList.Add(detail);
                }
            }
            // 删除
            if(saveModel.DelKeys != null && saveModel.DelKeys.Count >0)
            {
                delKeys = saveModel.DelKeys.Select(q=>q.ChangeType(detailKeyInfo.PropertyType))
                    .Where(x=>x!=null).ToList();
            }
            // 执行保存
            _repository.Update(mainEntity);
            editList.ForEach(x=>{
                _repository.Update<TDetail>(x);
            });

            addList.ForEach(x=>{
                _repository.DbContext.Entry<TDetail>(x).State = EntityState.Added;
            });

            delKeys.ForEach(x=>{
                TDetail delT = Activator.CreateInstance<TDetail>();
                detailKeyInfo.SetValue(delT, x);
                _repository.DbContext.Entry<TDetail>(delT).State = EntityState.Deleted;
            });
            _repository.DbContext.SaveChanges();
            Response.OK(ResponseType.SaveSuccess);
            return Response;
        }

        public WebResponseContent Update(SaveModel saveModel)
        {
            var type = typeof(TModel);
            var mainKeyProperty = type.GetKeyProperty();

            Type detailType = null;
            if (saveModel.DetailData != null)
            {
                saveModel.DetailData = saveModel.DetailData == null
                    ? new List<Dictionary<string, object>>()
                    : saveModel.DetailData.Where(x => x.Count > 0).ToList();

                detailType = GetRealDetailType();
            }

            if (mainKeyProperty == null
                || !saveModel.MainData.ContainsKey(mainKeyProperty.Name)
                || saveModel.MainData[mainKeyProperty.Name] == null)
            {
                return Response.Error(ResponseType.NoKey);
            }

            if (detailType == null)
            {
                TModel mainEntity = saveModel.MainData.DicToEntity<TModel>();
                _repository.Update(mainEntity);
                Response.OK(ResponseType.SaveSuccess);
                Response.Data = new { data = mainEntity };
                return Response;
            }
            var ForeignKeyName = typeof(TModel).GetCustomAttribute<EntityAttribute>()?.ForeignKeyName;
            saveModel.DetailData = saveModel.DetailData.Where(x => x.Count > 0).ToList();
            PropertyInfo detailKeyInfo = detailType.GetKeyProperty();
            object keyDefaultVal = mainKeyProperty.PropertyType.Assembly.CreateInstance(mainKeyProperty.PropertyType.FullName);
            object detailKeydefaultValue = detailKeyInfo.PropertyType.Assembly.CreateInstance(detailKeyInfo.PropertyType.FullName);
            foreach (Dictionary<string, object> dic in saveModel.DetailData)
            {
                // 如果当前子表不包含主表的外键则动态添加，并附默认值，可用于判断是否是新增的数据
                // 如果子表主键不存在也要Add
                if (!dic.ContainsKey(detailKeyInfo.Name))
                {
                    dic.Add(detailKeyInfo.Name, detailKeydefaultValue);
                    if (dic.ContainsKey(ForeignKeyName))
                    {
                        dic[ForeignKeyName] = saveModel.MainData[mainKeyProperty.Name];
                    }
                    else
                    {
                        dic.Add(ForeignKeyName, saveModel.MainData[mainKeyProperty.Name]);
                    }
                    continue;
                }
                // 这个健壮性是有必要的，比如主键是string
                if (dic[detailKeyInfo.Name] == null)
                {
                    return Response.Error(ResponseType.NoKey);
                }

                string detailKeyVal = dic[detailKeyInfo.Name].ToString();
                if (detailKeyVal != detailKeydefaultValue.ToString() && (
                    !dic.ContainsKey(ForeignKeyName) || dic[ForeignKeyName] == null
                    || dic[ForeignKeyName].ToString() == keyDefaultVal.ToString()
                ))
                {
                    return Response.Error(mainKeyProperty.Name + "是必须的");
                }

            }


            return this.GetType().GetMethod("UpdateEntity")
            .MakeGenericMethod(detailType)
            .Invoke(this, new object[] { saveModel, mainKeyProperty, detailKeyInfo, keyDefaultVal })
            as WebResponseContent;
        }
        
        private Type? GetRealDetailType()
        {
            return typeof(TModel).GetCustomAttribute<EntityAttribute>()?.DetailTable?[0];
        }

        public virtual TModel GetData(Expression<Func<TModel,bool>> query)
        {
            return _repository.FindFirst(query);
        }

        public virtual IEnumerable<TModel> GetDataList(Expression<Func<TModel,bool>> query, int skipCount, int maxResultCount = 1000)
        {
            return _repository.FindAsIQueryable().Where(query).Skip(skipCount).Take(maxResultCount).ToList();
        }
        public virtual IEnumerable<TModel> GetDataList(int skipCount, int maxResultCount = 1000)
        {
            return _repository.FindAsIQueryable().Skip(skipCount).Take(maxResultCount).ToList();
        }
        // 删除一条数据
        public virtual int Delete(TModel model)
        {
            return _repository.Delete(model);
        }
        public virtual int Delete(object id)
        {
            var keyType = id.GetType().GetTypeInfo();
            var modelKeyType = typeof(TModel).GetKeyProperty().PropertyType;
            if(keyType == modelKeyType)
            {
                TModel model = Activator.CreateInstance<TModel>();
                typeof(TModel).GetKeyProperty().SetValue(model, id);
                return _repository.Delete(model);
            }
            return 0;
        }
    }
}