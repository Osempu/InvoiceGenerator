using System;

namespace InvoiceGenerator.Application.Options;

public class CustomSettings
{
    public string ApplicationName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public FeatureToggle? FeatureToggle { get; set; }
}

public class OtherCustomSettings
{
    public int Settings1 { get; set; }
    public int Settings2 { get; set; }
    public int Settings3 { get; set; }
}

public class FeatureToggle
{
    public bool EnableNewFeatureX { get; set; }
    public bool EnableBetaFeatures { get; set; }
}
