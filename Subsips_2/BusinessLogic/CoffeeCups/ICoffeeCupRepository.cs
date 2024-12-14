using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CoffeeCup.ViewModel;
using Subsips_2.Areas.Subsips.Models.Cafe;

namespace Subsips_2.BusinessLogic.CoffeeCups;

public interface ICoffeeCupRepository
{
    public ReturnResult<List<MenuCoffeeModelView>> GetAllBasedOnStation(Guid StationId);
    public Task<ReturnResult<bool>> Add(string name, Guid cafeId, decimal price, string description, string imageUrl);
    public ReturnResult<List<CoffeeCupItemViewModel>> GetAllBasedOnCafe(Guid cafeId);
}
