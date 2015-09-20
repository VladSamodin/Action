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
    public class LotRepository : IRepository<DalLot>
    {
        private readonly DbContext context;

        public LotRepository(DbContext uow)
        {
            this.context = uow;
        }

        public DalLot Create(DalLot dalLot)
        {
            return context.Set<Lot>().Add(dalLot.ToOrmLot()).ToDalLot();
        }

        public void Delete(DalLot dalLot)
        {
            Lot ormLot = context.Set<Lot>().SingleOrDefault(u => u.Id == dalLot.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormLot != null)
            {
                context.Entry<Lot>(ormLot).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public void Update(DalLot dalLot)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            Lot ormLot = context.Set<Lot>().SingleOrDefault(u => u.Id == dalLot.Id);
            if (ormLot != null)
            {
                ormLot.Name               = dalLot.Name;
                ormLot.Description        = dalLot.Description;
                ormLot.OwnerId            = dalLot.OwnerId;
                ormLot.Sold               = dalLot.Sold;
                ormLot.StartDateTime      = dalLot.StartDateTime;
                ormLot.FinishDateTime     = dalLot.FinishDateTime;
                ormLot.StartPrice         = dalLot.StartPrice;
                // TODO: Добавить, если будет время
                //ormLot.RedemptionPrice    = dalLot.RedemptionPrice;
                ormLot.CategoryId         = dalLot.CategoryId;
                ormLot.Image              = dalLot.Image;
                ormLot.ModeratorId        = dalLot.ModeratorId;
                ormLot.ModerationStatusId = dalLot.ModerationStatusId;
                ormLot.ModerationDateTime = dalLot.ModerationDateTime;
                ormLot.ModeratorMessage   = dalLot.ModeratorMessage;
                
                context.Entry<Lot>(ormLot).State = EntityState.Modified;
            }
        }

        public int Count()
        {
            return context.Set<Lot>().Count();
        }

        public int Count(Expression<Func<DalLot, bool>> expression)
        {
            return context.Set<Lot>().Count(ExpressionTransformer<DalLot, Lot>.Transform(expression));
        }

        public IEnumerable<DalLot> GetAll()
        {
            return context.Set<Lot>().AsEnumerable().Select(ormLot => ormLot.ToDalLot());
        }

        public DalLot GetById(int id)
        {
            Lot ormLot = context.Set<Lot>().FirstOrDefault(u => u.Id == id);
            return ormLot != null ? ormLot.ToDalLot() : null;
        }

        IEnumerable<DalLot> IRepository<DalLot>.GetByPredicate(Expression<Func<DalLot, bool>> expression)
        {
            return context.Set<Lot>().Where(ExpressionTransformer<DalLot, Lot>.Transform(expression)).AsEnumerable().Select(u => u.ToDalLot());
        }
    }
}
