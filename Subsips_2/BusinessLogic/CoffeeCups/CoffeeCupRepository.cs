using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CoffeeCup.ViewModel;
using Subsips_2.Areas.Subsips.Models.Cafe;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.CoffeeCups;

public class CoffeeCupRepository : ICoffeeCupRepository
{
    private readonly Subsips_2Context context;

    public CoffeeCupRepository(Subsips_2Context context)
    {
        this.context = context;
    }


    public ReturnResult<List<MenuCoffeeModelView>> GetAllBasedOnStation(Guid StationId)
    {
        var result = context.CoffeeCups.Where(x => x.Cafe.StationId == StationId).Select(x => new MenuCoffeeModelView
        {
            CoffeeDesciption = x.Description,
            CoffeeId = x.Id,
            PriceOfCoffee = x.Price,
            CoffeeName = x.Name,
            CafeId = x.CafeId,
        }).ToList();

        return ResultFactory.GetGoodResult(result);
    }
    public ReturnResult<List<CoffeeCupItemViewModel>> GetAllBasedOnCafe(Guid cafeId)
    {
        var result = context.CoffeeCups.Where(x => x.CafeId == cafeId).Select(x => new CoffeeCupItemViewModel
        {
            CoffeeId = x.Id,
            CoffeeName= x.Name,
            Price = x.Price,
        }).ToList();

        return ResultFactory.GetGoodResult(result);
    }

    public async Task<ReturnResult<bool>> Add(string name, Guid cafeId, decimal price, string description, string imageUrl)
    {
        var model = CoffeeCup.Create(name, cafeId, price, description, imageUrl);

        context.CoffeeCups.Add(model);

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }

}
