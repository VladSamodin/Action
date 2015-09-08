using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using ORM.Entities;
using ExpressionTransformer;

namespace DAL.Concrete
{
    public class CategoryRepository : IRepository<DalCategory>
    {
        private readonly DbContext context;

        public CategoryRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalCategory dalCategory)
        {
            context.Set<Category>().Add(dalCategory.ToOrmCategory());
        }

        public void Delete(DalCategory dalCategory)
        {
            Category ormCategory = context.Set<Category>().SingleOrDefault(u => u.Id == dalCategory.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormCategory != null)
            {
                context.Entry<Category>(ormCategory).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public void Update(DalCategory dalCategory)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            Category ormCategory = context.Set<Category>().SingleOrDefault(u => u.Id == dalCategory.Id);
            if (ormCategory != null)
            {
                ormCategory.Name = dalCategory.Name;
                context.Entry<Category>(ormCategory).State = EntityState.Modified;
            }
        }

        public int Count()
        {
            return context.Set<Category>().Count();
        }

        public int Count(Expression<Func<DalCategory, bool>> expression)
        {
            return context.Set<Category>().Count(ExpressionTransformer<DalCategory, Category>.Transform(expression));
        }

        public IEnumerable<DalCategory> GetAll()
        {
            return context.Set<Category>().AsEnumerable().Select(ormCategory => ormCategory.ToDalCategory());
        }

        public DalCategory GetById(int id)
        {
            Category ormCategory = context.Set<Category>().FirstOrDefault(u => u.Id == id);
            return ormCategory != null ? ormCategory.ToDalCategory() : null;
        }

        IEnumerable<DalCategory> IRepository<DalCategory>.GetByPredicate(Expression<Func<DalCategory, bool>> expression)
        {
            return context.Set<Category>().Where(ExpressionTransformer<DalCategory, Category>.Transform(expression)).AsEnumerable().Select(u => u.ToDalCategory());
        }
    }
}
