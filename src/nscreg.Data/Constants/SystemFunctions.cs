using nscreg.Utilities.Attributes;

namespace nscreg.Data.Constants
{
    /// <summary>
    /// Константы системных функций
    /// </summary>
    public enum SystemFunctions
    {
        // account
        [AllowedTo(DefaultRoleNames.Employee, DefaultRoleNames.ExternalUser)]
        AccountView = 0,

        [AllowedTo(DefaultRoleNames.Employee, DefaultRoleNames.ExternalUser)]
        AccountEdit = 1,

        // roles
        RoleView = 2,
        RoleCreate = 3,
        RoleEdit = 4,
        RoleDelete = 5,

        // users
        UserView = 6,
        UserCreate = 7,
        UserEdit = 8,
        UserDelete = 9,

        // stat. units
        [AllowedTo(DefaultRoleNames.Employee, DefaultRoleNames.ExternalUser)]
        StatUnitView = 10,

        [AllowedTo(DefaultRoleNames.Employee)]
        StatUnitCreate = 11,

        [AllowedTo(DefaultRoleNames.Employee)]
        StatUnitEdit = 12,

        [AllowedTo(DefaultRoleNames.Employee)]
        StatUnitDelete = 13,

        // regions
        RegionsView = 17,
        RegionsCreate = 18,
        RegionsEdit = 19,
        RegionsDelete = 20,

        // address
        AddressView = 25,
        AddressCreate = 26,
        AddressEdit = 27,
        AddressDelete = 28,

        // links
        LinksView = 29,
        LinksCreate = 30,
        LinksDelete = 31,

        // data sources
        DataSourcesView = 32,
        DataSourcesCreate = 33,
        DataSourcesEdit = 34,
        DataSourcesDelete = 35,

        // data source queues
        DataSourcesQueueView = 36,
        DataSourcesQueueLogView = 37,
        DataSourcesQueueLogEdit = 38,
        DataSourcesQueueAdd = 39,
        DataSourcesQueueDelete = 51,
        DataSourcesQueueLogDelete = 52,

        // Analysis
        StatUnitAnalysis = 40,
        AnalysisQueueView = 41,
        AnalysisQueueAdd = 42,
        AnalysisQueueLogView = 43,
        AnalysisQueueLogUpdate = 44,

        //Sample Frames
        SampleFramesCreate = 45,
        SampleFramesEdit = 46,
        SampleFramesDelete = 47,
        SampleFramesView = 48,
        SampleFramesPreview = 49,

        Reports = 50,
    }
}
