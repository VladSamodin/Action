using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : DAL.Interface.DTO.IEntity
    {
        TEntity Create(TEntity e);
        void Delete(TEntity e);
        TEntity Update(TEntity e);
        int Count();
        int Count(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> expression);
    }
}