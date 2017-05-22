using System.Collections.Generic;

namespace PaderbornUniversity.SILab.Hip.Auth.Models
{
    public class Scopes
    {
        public const string CmsWebApi = "HiP-CmsWebApi";
        public const string DataStore = "HiP-DataStore";
        public const string OnlyOffice = "HiPCMS-OnlyOfficeIntegration";
        public const string FeatureToggle = "HiP-FeatureToggle";

        public static IReadOnlyCollection<string> All = new List<string>()
        {
            CmsWebApi,
            DataStore,
            OnlyOffice,
            FeatureToggle
        };
    }
}
