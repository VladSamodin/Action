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
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public DalRole Create(DalRole dalRole)
        {
            return context.Set<Role>().Add(dalRole.ToOrmRole()).ToDalRole();
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

        public DalRole Update(DalRole dalRole)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            Role ormRole = context.Set<Role>().SingleOrDefault(u => u.Id == dalRole.Id);
            if (ormRole != null)
            {
                DalRole oldRole = ormRole.ToDalRole();
                ormRole.Name = dalRole.Name;
                ormRole.Description = dalRole.Description;
                context.Entry<Role>(ormRole).State = EntityState.Modified;

                return oldRole;
            }
            return null;
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

        public void AddUser(DalRole dalRole, DalUser dalUser)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            Role ormRole = context.Set<Role>().SingleOrDefault(r => r.Id == dalRole.Id);
            if (ormUser == null || ormRole == null)
            {
                // Exception??
                return;
            }
            ormRole.Users.Add(ormUser);
        }

        public void RemoveUser(DalRole dalRole, DalUser dalUser)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            Role ormRole = context.Set<Role>().SingleOrDefault(r => r.Id == dalRole.Id);
            if (ormUser == null || ormRole == null)
            {
                // Exception??
                return;
            }
            // test
            ormRole.Users.Remove(ormUser);
        }

        public IEnumerable<DalUser> GetUsers(DalRole dalRole)
        {
            Role ormRole = context.Set<Role>().SingleOrDefault(u => u.Id == dalRole.Id);
            return ormRole != null ? ormRole.Users.Select(u => u.ToDalUser()) : null;
        }
    }
}
