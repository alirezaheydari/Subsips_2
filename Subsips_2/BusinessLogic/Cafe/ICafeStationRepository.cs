using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ICafeStationRepository
{
    public ReturnResult<CafeStation> Find(Guid cafeId);
    public ReturnResult<List<CafeStationInfoViewModel>> GetCafeStationInfoViewModel();
    public ReturnResult<List<CafeStation>> GetAll();
    public Task<ReturnResult<bool>> Add(string name, string phoneNumber, Guid stationId, string description);
    public Task<ReturnResult<bool>> Update(Guid id, string name, string phoneNumber, Guid stationId, string? description);
    public Task<ReturnResult<bool>> Delete(Guid id);
}
