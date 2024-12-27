using Repository.DataModel;
using Repository.Helper;
using Subsips_2.Areas.CPanel.Models.Admin.ModelView;

namespace Subsips_2.BusinessLogic.Cafe;

public interface ICafeStationRepository
{
    public ReturnResult<CafeStation> Find(Guid cafeId);
    public ReturnResult<List<CafeStationInfoViewModel>> GetCafeStationInfoViewModel();
}
