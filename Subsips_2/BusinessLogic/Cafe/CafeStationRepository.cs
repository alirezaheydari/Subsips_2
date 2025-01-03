using Microsoft.IdentityModel.Tokens;
using Repository.DataModel;
using Repository.Helper;
using Repository.Helper.Validations;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;
using Subsips_2.Data;
using static System.Collections.Specialized.BitVector32;

namespace Subsips_2.BusinessLogic.Cafe;

public class CafeStationRepository : ICafeStationRepository
{
    private readonly Subsips_2Context context;

    public CafeStationRepository(Subsips_2Context context)
    {
        this.context = context;
    }
    public ReturnResult<List<CafeStation>> GetAll()
    {
        return ResultFactory.GetGoodResult(context.CafeStations.Select(x => new CafeStation
        {
            Description = x.Description,
            Name = x.Name,
            PhoneNumber = x.PhoneNumber,
            Id = x.Id,
            StationId = x.StationId,
            Station = x.Station,
        }).ToList());
    }
    public ReturnResult<CafeStation> Find(Guid cafeId)
    {
        var cafe = context.CafeStations.Find(cafeId);
        if (cafe is null)
            ResultFactory.GetBadResult(new string[]
            {
                "Model is not valid"
            });
        return ResultFactory.GetGoodResult(cafe);
    }
    public async Task<ReturnResult<bool>> Add(string name, string phoneNumber, Guid stationId, string? description)
    {
        var newCafe = CafeStation.Create(name, phoneNumber, stationId, description ?? string.Empty);

        context.CafeStations.Add(newCafe);

        var station = context.SubwayStations.Find(stationId);

        station.CafeId = newCafe.Id;

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }
    public async Task<ReturnResult<bool>> Delete(Guid id)
    {
        var cafe = context.CafeStations.Find(id);

        if (cafe is null)
            return ResultFactory.GetGoodResult();

        context.CafeStations.Remove(cafe);

        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }
    public async Task<ReturnResult<bool>> Update(Guid id, string name, string phoneNumber, Guid stationId, string? description)
    {
        var cafe = context.CafeStations.Find(id);

        if (cafe is null)
            return ResultFactory.GetBadResult(new string[]
            {
                "Model is not found"
            });


        if (!name.IsNullOrEmpty())
            cafe.Name = name;
        if (!description.IsNullOrEmpty())
            cafe.Description = description;
        if (!phoneNumber.IsNullOrEmpty())
        {
            phoneNumber = PhoneNumberValidation.GetPhoneNumberWithoutZero(phoneNumber);
            if (PhoneNumberValidation.IsValid(phoneNumber))
                cafe.PhoneNumber = phoneNumber;
        }
        if (stationId != Guid.Empty)
        {
            cafe.StationId = stationId;

            var station = context.SubwayStations.Find(stationId);

            station.CafeId = id;
        }
        await context.SaveChangesAsync();

        return ResultFactory.GetGoodResult();
    }
    public ReturnResult<List<CafeStationInfoViewModel>> GetCafeStationInfoViewModel()
    {
        var res = context.CafeStations.Select(x => new CafeStationInfoViewModel
        {
            CafeId = x.Id,
            CafeName = x.Name,
            StationName = x.Station.Name,
        }).ToList();


        return ResultFactory.GetGoodResult(res);
    }
}
