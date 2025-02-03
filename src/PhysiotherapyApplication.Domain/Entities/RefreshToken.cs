using PhysiotherapyApplication.Domain.Entities.Common;

namespace PhysiotherapyApplication.Domain.Entities;

public class RefreshToken:BaseEntity
{
    public string ApplicationUserId { get; set; }
    public string UserRefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}
