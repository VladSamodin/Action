using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ILotService : IService<BllLot>
    {
        IEnumerable<BllLot> GetByCategoryIdOrOwnerId(int? categoryId, int? ownerId);
    }
}
