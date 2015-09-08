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
    public class BidService : IService<BllBid>
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalBid> bidRepository;

        public BidService(IUnitOfWork uow, IRepository<DalBid> repository)
        {
            this.uow = uow;
            this.bidRepository = repository;
        }

        public void Create(BllBid bllBid)
        {
            bidRepository.Create(bllBid.ToDalBid());
            uow.Commit();
        }

        public void Delete(BllBid bllBid)
        {
            bidRepository.Delete(bllBid.ToDalBid());
            uow.Commit();
        }

        public void Update(BllBid bllBid)
        {
            bidRepository.Update(bllBid.ToDalBid());
            uow.Commit();
        }

        public int Count()
        {
            return bidRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllBid, bool>> expression)
        {
            return bidRepository.Count(ExpressionTransformer<BllBid, DalBid>.Transform(expression));
        }

        public IEnumerable<BllBid> GetAll()
        {
            return bidRepository.GetAll().Select(dalBid => dalBid.ToBllBid());
        }

        public BllBid GetById(int id)
        {
            DalBid dalBid = bidRepository.GetById(id);
            return dalBid != null ? dalBid.ToBllBid() : null;
        }

        public IEnumerable<BllBid> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllBid, bool>> expression)
        {
            return bidRepository.GetByPredicate(ExpressionTransformer<BllBid, DalBid>.Transform(expression)).Select(dalBid => dalBid.ToBllBid());
        }
    }
}
