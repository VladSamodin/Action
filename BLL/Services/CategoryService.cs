﻿using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using ExpressionTransformer;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class CategoryService : IService<BllCategory>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalCategory> categoryRepository;

        public CategoryService(IUnitOfWork uow, IRepository<DalCategory> repository)
        {
            this.uow = uow;
            this.categoryRepository = repository;
        }

        public BllCategory Create(BllCategory bllCategory)
        {
            BllCategory newCategory = categoryRepository.Create(bllCategory.ToDalCategory()).ToBll();
            uow.Commit();
            return newCategory;
        }

        public void Delete(BllCategory bllCategory)
        {
            categoryRepository.Delete(bllCategory.ToDalCategory());
            uow.Commit();
        }

        public BllCategory Update(BllCategory bllCategory)
        {
            DalCategory oldCategory = categoryRepository.Update(bllCategory.ToDalCategory());
            //categoryRepository.Update(bllCategory.ToDalCategory());
            uow.Commit();
            return oldCategory == null ? null : oldCategory.ToBll();
        }

        public int Count()
        {
            return categoryRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllCategory, bool>> expression)
        {
            return categoryRepository.Count(ExpressionTransformer<BllCategory, DalCategory>.Transform(expression));
        }

        public IEnumerable<BllCategory> GetAll()
        {
            return categoryRepository.GetAll().Select(dalCategory => dalCategory.ToBllCategory());
        }

        public BllCategory GetById(int id)
        {
            DalCategory dalCategory = categoryRepository.GetById(id);
            return dalCategory != null ? dalCategory.ToBllCategory() : null;
        }

        public IEnumerable<BllCategory> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllCategory, bool>> expression)
        {
            return categoryRepository.GetByPredicate(ExpressionTransformer<BllCategory, DalCategory>.Transform(expression)).Select(dalCategory => dalCategory.ToBllCategory());
        }
    }
}
