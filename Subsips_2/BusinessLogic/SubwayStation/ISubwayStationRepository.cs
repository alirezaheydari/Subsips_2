using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.Subsips.Models.SubwayStation;

namespace Subsips_2.BusinessLogic.SubwayStation;

public interface ISubwayStationRepository
{
    public Task<ReturnResult<bool>> Add(string name, SubwayLines line, string? description);
    public ReturnResult<List<Repository.DataModel.SubwayStation>> GetAll();
    public Task<ReturnResult<bool>> Delete(Guid id);
    public Task<ReturnResult<bool>> ChagneStatus(Guid id);
    public ReturnResult<List<SubwayStationViewModel>> GetViewModelAll();
    public ReturnResult<Repository.DataModel.SubwayStation> Find(Guid id);
}
