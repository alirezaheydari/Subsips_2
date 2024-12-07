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

    public async Task<ReturnResult<bool>> Add(string name, SubwayLines line, string description, Guid cafeId)
    {
        _context.SubwayStations.Add(new Repository.DataModel.SubwayStation
        {
            Name = name,
            Line = (byte)line,
            CafeId = cafeId,
            Description = description
        });


        await _context.SaveChangesAsync();


        return ResultFactory.GetGoodResult();
    }

    public ReturnResult<List<Repository.DataModel.SubwayStation>> GetAll()
    {
        return ResultFactory.GetGoodResult(_context.SubwayStations.ToList());
    }



    public ReturnResult<List<SubwayStationViewModel>> GetViewModelAll()
    {
        return ResultFactory.GetGoodResult(_context.SubwayStations.Select(x => new SubwayStationViewModel
        {
            CafeName = x.Cafe.Name,
            Name = x.Name,
            StationId = x.Id
        }).ToList());
    }


}
