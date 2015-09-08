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
    public class RoleRepository : IRepository<DalRole>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalRole dalRole)
        {
            context.Set<Role>().Add(dalRole.ToOrmRole());
        }

        public void Delete(DalRole dalRole)
        {
            Role ormRole = context.Set<Role>().SingleOrDefault(u => u.Id == dalRole.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormRole != null)
            {
                context.Entry<Role>(ormRole).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public void Update(DalRole dalRole)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            Role ormRole = context.Set<Role>().SingleOrDefault(u => u.Id == dalRole.Id);
            if (ormRole != null)
            {

                ormRole.Name = dalRole.Name;
                ormRole.Description = dalRole.Description;
                context.Entry<Role>(ormRole).State = EntityState.Modified;
            }
        }

        public int Count()
        {
            return context.Set<Role>().Count();
        }

        public int Count(Expression<Func<DalRole, bool>> expression)
        {
            return context.Set<Role>().Count(ExpressionTransformer<DalRole, Role>.Transform(expression));
        }

        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().AsEnumerable().Select(ormRole => ormRole.ToDalRole());
        }

        public DalRole GetById(int id)
        {
            Role ormRole = context.Set<Role>().FirstOrDefault(u => u.Id == id);
            return ormRole != null ? ormRole.ToDalRole() : null;
        }

        IEnumerable<DalRole> IRepository<DalRole>.GetByPredicate(Expression<Func<DalRole, bool>> expression)
        {
            return context.Set<Role>().Where(ExpressionTransformer<DalRole, Role>.Transform(expression)).AsEnumerable().Select(u => u.ToDalRole());
        }
    }
}
