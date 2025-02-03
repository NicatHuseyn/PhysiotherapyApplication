namespace PhysiotherapyApplication.Domain.Options;

public class CustomTokenOption
{
    public const string Key = "TokenOptions";

    public string Auidence { get; set; }
    public string Issure { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}
