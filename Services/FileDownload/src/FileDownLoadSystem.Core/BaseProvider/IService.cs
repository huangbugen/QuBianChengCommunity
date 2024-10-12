using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Core.Untity;
using FileDownLoadSystem.Entity;
using FileDownLoadSystem.Entity.Core;

namespace FileDownLoadSystem.Core.BaseProvider
{
    public interface IService<TModel>
        where TModel : BaseModel
    {
       WebResponseContent Add<TDetail>(TModel entity, List<TDetail> list = null) where TDetail : BaseModel;
        WebResponseContent AddEntity(TModel entity);
        TModel FindFirst(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null);
        WebResponseContent Update(SaveModel saveModel);
        WebResponseContent UpdateEntity<TDetail>(SaveModel saveModel, PropertyInfo mainKeyProperty, PropertyInfo detailKeyInfo, object keyDefaultVal) where TDetail : BaseModel, new();
   

    }
}