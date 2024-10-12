using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.EfDbContext;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Extensions;
using FileDownLoadSystem.Entity;
using Microsoft.EntityFrameworkCore;

namespace FileDownLoadSystem.Core.BaseProvider
{
    public class BaseRepository<TModel> where TModel : BaseModel
    {
        private readonly FileDownloadSystemDbContext _defaultDbContext;

        public BaseRepository(FileDownloadSystemDbContext dbContext)
        {
            this._defaultDbContext = dbContext;
        }

        public FileDownloadSystemDbContext DbContext => _defaultDbContext;
        private DbSet<TModel> DbSet => _defaultDbContext.Set<TModel>();

        public virtual TModel FindFirst(
            Expression<Func<TModel, bool>> predicate,
            Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null)
        {
            return FindAsIQueryable(predicate, orderBy).FirstOrDefault();
        }
        public IQueryable<TModel> FindAsIQueryable(
            Expression<Func<TModel, bool>> predicate,
            Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null)
        {
            if (orderBy != null)
                // DbSet 等价于 DbContext.Set<TModel>
                return DbSet.Where(predicate).GetIQueryOrderBy(orderBy.GetExpressionToDic());
            return DbSet.Where(predicate);
        }

        public IQueryable<TModel> FindAsIQueryable()
        {
            return DbSet;
        }

        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TModel entity)
        {
            DbSet.Update(entity);
            // 这里请注意，SaveChanges()执行后返回的是受影响的行数
            return DbContext.SaveChanges();
        }
        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual int Update<T>(T entity)
            where T : BaseModel
        {
            DbContext.Set<T>().Update(entity);
            return DbContext.SaveChanges();
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TModel entity, bool isAutoSave = true)
        {
            DbSet.Add(entity);
            // 这里请注意，SaveChanges()执行后返回的是受影响的行数
            // 并不是我们所熟知RESTful风格所需要返回的实体。如果各位深受RESTful风格的影响。框架是开源的，自己修改
            if (isAutoSave)
            {
                return DbContext.SaveChanges();
            }
            return default;
        }
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TModel entity)
        {
            DbSet.Remove(entity);
            return DbContext.SaveChanges();
        }
        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

    }
}