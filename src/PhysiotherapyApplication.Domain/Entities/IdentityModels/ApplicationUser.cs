using Microsoft.AspNetCore.Identity;

namespace PhysiotherapyApplication.Domain.Entities.IdentityModels;

public class ApplicationUser:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
