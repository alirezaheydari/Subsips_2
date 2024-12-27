using System.ComponentModel.DataAnnotations;

namespace Repository.DataModel;

public class CafeStationAspNetUser
{
    [Key]
    public Guid Id { get; set; }

    public Guid CafeId { get; set; }
    public required string AspNetUserId { get; set; }
    public bool IsActive { get; set; }
    public bool IsOwner { get; set; }
    public CafeStation Cafe { get; set; }


    public static CafeStationAspNetUser Create(Guid cafeId, string userId, bool isActive, bool isOwner)
    {
        var res = new CafeStationAspNetUser()
        {
            AspNetUserId = userId,
            IsActive = isActive,
            IsOwner = isOwner,
            CafeId = cafeId,
            Id = Guid.NewGuid(),
        };

        return res;
    }

}
