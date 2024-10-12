using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FileDownLoadSystem.Core.EfDbContext;
using FileDownLoadSystem.Core.Enums;
using FileDownLoadSystem.Entity;

namespace FileDownLoadSystem.Core.BaseProvider
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        FileDownloadSystemDbContext DbContext { get; }
        int Delete(TModel entity);
        IQueryable<TModel> FindAsIQueryable();
        IQueryable<TModel> FindAsIQueryable(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null);
        TModel FindFirst(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, Dictionary<object, QueryOrderBy>>> orderBy = null);
        int Insert(TModel entity, bool isAutoSave = true);
        int SaveChanges();
        int Update(TModel entity);
        int Update<T>(T entity) where T : BaseModel;
    }
}