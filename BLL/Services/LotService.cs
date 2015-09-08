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
    public class LotService: ILotService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalLot> lotRepository;
        private readonly IService<BllBid> bidService;

        public LotService(IUnitOfWork uow, IRepository<DalLot> repository, IService<BllBid> bidService)
        {
            this.uow = uow;
            this.lotRepository = repository;
            this.bidService = bidService;
        }

        public void Create(BllLot bllLot)
        {
            lotRepository.Create(bllLot.ToDalLot());
            uow.Commit();
        }

        public void Delete(BllLot bllLot)
        {
            lotRepository.Delete(bllLot.ToDalLot());
            uow.Commit();
        }

        public void Update(BllLot bllLot)
        {
            lotRepository.Update(bllLot.ToDalLot());
            uow.Commit();
        }

        public int Count()
        {
            return lotRepository.Count();
        }

        public int Count(System.Linq.Expressions.Expression<System.Func<BllLot, bool>> expression)
        {
            return lotRepository.Count(ExpressionTransformer<BllLot, DalLot>.Transform(expression));
        }

        public IEnumerable<BllLot> GetAll()
        {
            return lotRepository.GetAll().Select(dalLot => setCurrentPrice(dalLot.ToBllLot()));
        }

        public BllLot GetById(int id)
        {
            DalLot dalLot = lotRepository.GetById(id);
            return dalLot != null ? setCurrentPrice(dalLot.ToBllLot()) : null;
        }

        public IEnumerable<BllLot> GetByPredicate(System.Linq.Expressions.Expression<System.Func<BllLot, bool>> expression)
        {
            return lotRepository.GetByPredicate(ExpressionTransformer<BllLot, DalLot>.Transform(expression)).Select(dalLot => setCurrentPrice(dalLot.ToBllLot()));
        }

        public IEnumerable<BllLot> GetByCategoryIdOrOwnerId(int? categoryId, int? ownerId)
        {
            return GetByPredicate(lot => (categoryId.HasValue && lot.CategoryId == categoryId) || (ownerId.HasValue && lot.OwnerId == ownerId));
        }

        private BllLot setCurrentPrice(BllLot lot)
        {
            BllBid bllLastBid = bidService.GetByPredicate(bid => bid.LotId == lot.Id).OrderBy(bid => bid.DateTime).LastOrDefault();
            if (bllLastBid == null)
            {
                lot.CurrentPrice = lot.StartPrice;
            }
            else
            {
                lot.CurrentPrice = bllLastBid.Sum;
            }

            return lot;
        }
    }
}