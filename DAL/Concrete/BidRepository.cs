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
    public class BidRepository : IRepository<DalBid>
    {
        private readonly DbContext context;

        public BidRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalBid dalBid)
        {
            context.Set<Bid>().Add(dalBid.ToOrmBid());
        }

        public void Delete(DalBid dalBid)
        {
            Bid ormBid = context.Set<Bid>().SingleOrDefault(u => u.Id == dalBid.Id);
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            if (ormBid != null)
            {
                context.Entry<Bid>(ormBid).State = EntityState.Deleted;
                //context.Set<User>().Remove(ormUser);
            }
        }

        public void Update(DalBid dalBid)
        {
            //User ormUser = context.Set<User>().Where(u => u.Id == dalUser.Id).FirstOrDefault();
            Bid ormBid = context.Set<Bid>().SingleOrDefault(u => u.Id == dalBid.Id);
            if (ormBid != null)
            {
                ormBid.LotId = dalBid.LotId;
                ormBid.UserId = dalBid.UserId;
                ormBid.Sum = dalBid.Sum;
                ormBid.DateTime = dalBid.DateTime;
                context.Entry<Bid>(ormBid).State = EntityState.Modified;
            }
        }

        public int Count()
        {
            return context.Set<Bid>().Count();
        }

        public int Count(Expression<Func<DalBid, bool>> expression)
        {
            return context.Set<Bid>().Count(ExpressionTransformer<DalBid, Bid>.Transform(expression));
        }

        public IEnumerable<DalBid> GetAll()
        {
            return context.Set<Bid>().AsEnumerable().Select(ormBid => ormBid.ToDalBid());
        }

        public DalBid GetById(int id)
        {
            Bid ormBid = context.Set<Bid>().FirstOrDefault(u => u.Id == id);
            return ormBid != null ? ormBid.ToDalBid() : null;
        }

        IEnumerable<DalBid> IRepository<DalBid>.GetByPredicate(Expression<Func<DalBid, bool>> expression)
        {
            return context.Set<Bid>().Where(ExpressionTransformer<DalBid, Bid>.Transform(expression)).AsEnumerable().Select(u => u.ToDalBid());
        }
    }
}
