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
    public class ModerationStatusRepository : IRepository<DalModerationStatus>
    {
        private readonly DbContext context;

        public ModerationStatusRepository(DbContext uow)
        {
            this.context = uow;
        }

        public DalModerationStatus Create(DalModerationStatus dalModerationStatus)
        {
            return context.Set<ModerationStatus>().Add(dalModerationStatus.ToOrmModerationStatus()).ToDalModerationStatus();
        }

        public void Delete(DalModerationStatus dalModerationStatus)
        {
            ModerationStatus ormModerationStatus = context.Set<ModerationStatus>().SingleOrDefault(u => u.Id == dalModerationStatus.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormModerationStatus != null)
            {
                context.Entry<ModerationStatus>(ormModerationStatus).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public void Update(DalModerationStatus dalModerationStatus)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            ModerationStatus ormModerationStatus = context.Set<ModerationStatus>().SingleOrDefault(u => u.Id == dalModerationStatus.Id);
            if (ormModerationStatus != null)
            {
                ormModerationStatus.Name = dalModerationStatus.Name;
                context.Entry<ModerationStatus>(ormModerationStatus).State = EntityState.Modified;
            }
        }

        public int Count()
        {
            return context.Set<ModerationStatus>().Count();
        }

        public int Count(Expression<Func<DalModerationStatus, bool>> expression)
        {
            return context.Set<ModerationStatus>().Count(ExpressionTransformer<DalModerationStatus, ModerationStatus>.Transform(expression));
        }

        public IEnumerable<DalModerationStatus> GetAll()
        {
            return context.Set<ModerationStatus>().AsEnumerable().Select(ormModerationStatus => ormModerationStatus.ToDalModerationStatus());
        }

        public DalModerationStatus GetById(int id)
        {
            ModerationStatus ormModerationStatus = context.Set<ModerationStatus>().FirstOrDefault(u => u.Id == id);
            return ormModerationStatus != null ? ormModerationStatus.ToDalModerationStatus() : null;
        }

        IEnumerable<DalModerationStatus> IRepository<DalModerationStatus>.GetByPredicate(Expression<Func<DalModerationStatus, bool>> expression)
        {
            return context.Set<ModerationStatus>().Where(ExpressionTransformer<DalModerationStatus, ModerationStatus>.Transform(expression)).AsEnumerable().Select(u => u.ToDalModerationStatus());
        }
    }
}
