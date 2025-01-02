using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.Subsips.Models.SubwayStation;
using Subsips_2.Data;

namespace Subsips_2.BusinessLogic.SubwayStation;

public class SubwayStationRepository : ISubwayStationRepository
{
    private readonly Subsips_2Context _context;
    public SubwayStationRepository(Subsips_2Context context)
    {
        _context = context;
    }

    public async Task<ReturnResult<bool>> Add(string name, SubwayLines line, string? description)
    {
        _context.SubwayStations.Add(new Repository.DataModel.SubwayStation
        {
            Name = name,
            Line = (byte)line,
            Description = description,
            IsActive = false
        });


        await _context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<List<Repository.DataModel.SubwayStation>> GetAll()
    {
        return ResultFactory.GetGoodResult(_context.SubwayStations.ToList());
    }

    public async Task<ReturnResult<bool>> ChagneStatus(Guid id)
    {
        var station = _context.SubwayStations.Find(id);

        if (station is null)
            return ResultFactory.GetBadResult(new string[]
            {
                "Model is not valid"
            });

        station.IsActive = !station.IsActive;

        await _context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }
    public async Task<ReturnResult<bool>> Delete(Guid id)
    {
        var station = _context.SubwayStations.Find(id);

        if (station is null)
            return ResultFactory.GetBadResult(new string[]
            {
                "Model is not valid"
            });

        _context.SubwayStations.Remove(station);

        await _context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }
     public ReturnResult<Repository.DataModel.SubwayStation> Find(Guid id)
    {
        var res = _context.SubwayStations.Find(id);

        if (res is null)
        {
            return ResultFactory.GetBadResult<Repository.DataModel.SubwayStation>(new string[]
            {
                "Model is not valid"
            });
        }

        return ResultFactory.GetGoodResult(res);
    }



    public ReturnResult<List<SubwayStationViewModel>> GetViewModelAll()
    {
        return ResultFactory.GetGoodResult(_context.SubwayStations.Where(x => x.Cafe != null).Select(x => new SubwayStationViewModel
        {
            CafeName = x.Cafe.Name,
            CafeId = x.CafeId,
            Name = x.Name,
            StationId = x.Id
        }).ToList());
    }


}
