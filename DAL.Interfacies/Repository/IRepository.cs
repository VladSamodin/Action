using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : DAL.Interface.DTO.IEntity
    {
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity e);
        int Count();
        int Count(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> expression);
    }
}