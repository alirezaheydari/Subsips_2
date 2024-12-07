using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.Subsips.Models.SubwayStation;

namespace Subsips_2.BusinessLogic.SubwayStation;

public interface ISubwayStationRepository
{
    public Task<ReturnResult<bool>> Add(string name, SubwayLines line, string description, Guid cafeId);
    public ReturnResult<List<Repository.DataModel.SubwayStation>> GetAll();
    public ReturnResult<List<SubwayStationViewModel>> GetViewModelAll();
}
