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
    //public class UserRepository : IRepository<DalUser>
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        //public DalUser Create(DalUser dalUser)
        public DalUser Create(DalUser dalUser)
        {
            return context.Set<User>().Add(dalUser.ToOrmUser()).ToDalUser();
        }

        public void Delete(DalUser dalUser)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormUser != null)
            {
                context.Entry<User>(ormUser).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public DalUser Update(DalUser dalUser)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            if (ormUser != null)
            {
                DalUser oldUser = ormUser.ToDalUser();
                ormUser.Name = dalUser.Name;
                ormUser.Email = dalUser.Email;
                ormUser.Password = dalUser.Password;
                context.Entry<User>(ormUser).State = EntityState.Modified;
                return dalUser;
            }
            return null;
        }

        public int Count()
        {
            return context.Set<User>().Count();
        }

        public int Count(Expression<Func<DalUser, bool>> expression)
        {
            return context.Set<User>().Count(ExpressionTransformer<DalUser, User>.Transform(expression));
        }

        public IEnumerable<DalUser> GetAll()
        {
            //return context.Set<User>().Select(ormUser => ormUser.ToDalUser());
            return context.Set<User>().AsEnumerable().Select(ormUser => ormUser.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            User ormUser = context.Set<User>().FirstOrDefault(u => u.Id == id);
            return ormUser != null ? ormUser.ToDalUser() : null;
        }

        IEnumerable<DalUser> IRepository<DalUser>.GetByPredicate(Expression<Func<DalUser, bool>> expression)
        {
            return context.Set<User>().Where(ExpressionTransformer<DalUser, User>.Transform(expression)).AsEnumerable().Select(u => u.ToDalUser());
        }

        public void AddRole(DalUser dalUser, DalRole dalRole)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            Role ormRole = context.Set<Role>().SingleOrDefault(r => r.Id == dalRole.Id);
            if (ormUser == null || ormRole == null)
            {
                // Exception??
                return;
            }
            ormUser.Roles.Add(ormRole);
        }

        public void RemoveRole(DalUser dalUser, DalRole dalRole)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            Role ormRole = context.Set<Role>().SingleOrDefault(r => r.Id == dalRole.Id);
            if (ormUser == null || ormRole == null)
            {
                // Exception??
                return;
            }
            // test
            ormUser.Roles.Remove(ormRole);
        }

        public IEnumerable<DalRole> GetRoles(DalUser dalUser)
        {
            User ormUser = context.Set<User>().SingleOrDefault(u => u.Id == dalUser.Id);
            return ormUser != null ? ormUser.Roles.Select(r => r.ToDalRole()) : null;
        }
    }
}