using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.CoffeeCup;
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
        }).OrderBy(x => x.PriceOfCoffee).ToList();

        return ResultFactory.GetGoodResult(result);
    }
    public ReturnResult<List<CoffeeCupItemViewModel>> GetAllBasedOnCafe(Guid cafeId)
    {
        var result = context.CoffeeCups.Where(x => x.CafeId == cafeId).Select(x => new CoffeeCupItemViewModel
        {
            CoffeeId = x.Id,
            CoffeeName = x.Name,
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

    public ReturnResult<CoffeeConfirmationInfo> FindCoffeeAndCafeInfo(Guid coffeeId)
    {
        var res = context.CoffeeCups.Where(x => x.Id == coffeeId)?.Select(x => new CoffeeConfirmationInfo
        {
            CafeName = x.Cafe.Name,
            CoffeeName = x.Name,
            CafePhoneNumber = x.Cafe.PhoneNumber,
            StationName = x.Cafe.Station.Name,
            Price = x.Price
        }).FirstOrDefault();
        if (res is null)
            ResultFactory.GetBadResult(new string[]
            {
                "Model is not valid"
            });

        return ResultFactory.GetGoodResult(res);
    }

    public ReturnResult<AddCoffeeCupRequestModel> Find(Guid coffeeId)
    {
        var coffee = context.CoffeeCups.Find(coffeeId);



        return ResultFactory.GetGoodResult(new AddCoffeeCupRequestModel() 
        { 
            Name = coffee.Name,
            Description = coffee.Description,
            CoffeeId = coffeeId,
            Price = coffee.Price,
        });
    }
    public async Task<ReturnResult<bool>> Update(AddCoffeeCupRequestModel model)
    {
        var coffee = context.CoffeeCups.Find(model.CoffeeId);



        coffee.Name = model.Name;
        coffee.Description = model.Description;
        coffee.Price = model.Price;


        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();

    }

}
