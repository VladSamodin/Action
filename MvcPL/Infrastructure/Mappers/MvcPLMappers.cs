using BLL.Interface.Entities;
using MvcPL.Models.User;
using MvcPL.Models.Bid;
using MvcPL.Models.Category;
using MvcPL.Models.Lot;
using System;
using System.Web.Helpers;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this BllUser bllUser)
        {
            return new UserViewModel()
            {
                Id = bllUser.Id,
                Name = bllUser.Name,
                Email = bllUser.Email,
                //Role = (Role)bllUser.RoleId
            };
        }

        public static UserViewModel ToUserViewModel(this BllUser user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public static UserEditModel ToUserEditModel(this BllUser user)
        {
            return new UserEditModel()
            {
                Name = user.Name,
                Email = user.Email
            };
        }

        public static BllUser ToBllUser(this UserCreateModel userCreateModel)
        {
            return new BllUser()
            {
                Name = userCreateModel.Name,
                Email = userCreateModel.Email,
                Password = userCreateModel.Password,
                //RoleId = (int)userCreateModel.Role
            };
        }

        public static BllBid ToBllBid(this BidCreateModel bid)
        {
            return new BllBid()
            {
                Sum = bid.Sum,
                DateTime = DateTime.Now,
                LotId = bid.LotId,
                UserId = bid.UserId
            };
        }

        public static BllCategory ToBllCategory(this CategoryCreateModel category)
        {
            return new BllCategory()
            {
                Name = category.Name
            };
        }

        public static CategoryEditModel ToCategoryEditModel(this BllCategory category)
        {
            return new CategoryEditModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static CategoryViewModel ToCategoryViewModel(this BllCategory category)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }


        public static BllLot ToBllLot(this LotCreateModel lot)
        {
            return new BllLot()
            {
                Name = lot.Name,
                Description = lot.Description,
                StartPrice = lot.StartPrice,
                StartDateTime = DateTime.Now,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                CategoryId = lot.CategoryId
            };
        }

        public static LotEditModel ToLotEditModel(this BllLot lot)
        {
            return new LotEditModel()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                CategoryId = lot.CategoryId,
                Image = lot.Image
            };
        }

        public static LotViewModel ToLotViewModel(this BllLot lot)
        {
            return new LotViewModel()
            {
                Id = lot.Id,
                Name = lot.Name,
                Description = lot.Description,
                CurrentPrice = lot.CurrentPrice,
                FinishDateTime = lot.FinishDateTime,
                Image = lot.Image,
                OwnerId = lot.OwnerId
            };
        }
    }
}