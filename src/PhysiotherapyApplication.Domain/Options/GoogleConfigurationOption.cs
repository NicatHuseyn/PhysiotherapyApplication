namespace PhysiotherapyApplication.Domain.Options;

public class GoogleConfigurationOption
{
    public const string Key = "Google";

    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
