using Client.Controllers.V1.DPS;
using Client.Models;

namespace Client.Services;

public interface IBlindBoxService : IBaseService<BlindBox, Guid, VwBlindBoxDisplay>
{
    List<ItemEntity> SelectByBlindBox(byte? sortBy);
}