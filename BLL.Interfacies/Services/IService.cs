﻿using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BLL.Interface.Services
{
    public interface IService<TEntity> where TEntity : IEntity
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