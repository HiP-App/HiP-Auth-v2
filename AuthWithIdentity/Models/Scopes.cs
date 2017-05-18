using System.Collections.Generic;

namespace PaderbornUniversity.SILab.Hip.Auth.Models
{
    public class Scopes
    {
        public const string CmsWebApi = "HiP-CmsWebApi";
        public const string CmsAngularapp = "HiP-CmsAngularApp";
        public const string TokenGenerator = "HiP-TokenGenerator";
        public const string DataStore = "HiP-DataStore";
        public const string OnlyOffice = "HiPCMS-OnlyOfficeIntegration";
        public const string FeatureToggle = "HiP-FeatureToggle";

        public static IReadOnlyCollection<string> All = new List<string>()
        {
            CmsWebApi,
            CmsAngularapp,
            TokenGenerator,
            DataStore,
            OnlyOffice,
            FeatureToggle
        };
    }
}
