using Newtonsoft.Json;
using nscreg.Data.Constants;
using nscreg.Utilities.Attributes;
using nscreg.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace nscreg.Data.Entities
{
    /// <summary>
    ///  Class entity stat. unit
    /// </summary>
    public abstract class StatisticalUnit : IStatisticalUnit
    {
        [DataAccessCommon]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit)]
        public int RegId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public DateTime RegIdDate { get; set; }

        [DataAccessCommon]
        [Display(Order = 100, GroupName = GroupNames.StatUnitInfo)]
        [AsyncValidation(ValidationTypeEnum.StatIdUnique)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.StatIdTooltip))]
        public string StatId { get; set; }

        [Display(Order = 105, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.StatIdDateTooltip))]
        public DateTime? StatIdDate { get; set; }

        [DataAccessCommon]
        [Display(Order = 110, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.StatNameTooltip))]
        public string Name { get; set; }

        [Display(Order = 115, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ShortNameTooltip))]
        public string ShortName { get; set; }

        [SearchComponent]
        [Display(Order = 210, GroupName = GroupNames.LinkInfo)]
        public virtual int? ParentOrgLink { get; set; }

        [Display(Order = 120, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TaxRegIdTooltip))]
        public string TaxRegId { get; set; }

        [Display(Order = 125, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TaxRegDateTooltip))]
        public DateTime? TaxRegDate { get; set; }

        [Reference(LookupEnum.RegistrationReasonLookup)]
        [Display(Order = 140, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.RegistrationReasonTooltip))]
        public int? RegistrationReasonId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual RegistrationReason RegistrationReason { get; set; }

        [Display(Order = 130, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ExternalIdTooltip))]
        public string ExternalId { get; set; }

        [Display(Order = 131, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ExternalIdDateTooltip))]
        public DateTime? ExternalIdDate { get; set; }

        [Display(Order = 132, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ExternalIdTypeTooltip))]
        public string ExternalIdType { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string DataSource { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? AddressId { get; set; }

        [Display(Order = 310, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.AddressTooltip))]
        public virtual Address Address { get; set; }

        [Display(Order = 302, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.WebAddressTooltip))]
        public string WebAddress { get; set; }

        [Display(Order = 300, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TelephoneNoTooltip))]
        public string TelephoneNo { get; set; }

        [Display(Order = 301, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.EmailAddressTooltip))]
        public string EmailAddress { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? ActualAddressId { get; set; }

        [Display(Order = 320, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ActualAddressTooltip))]
        public virtual Address ActualAddress { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? PostalAddressId { get; set; }

        [Display(Order = 320, GroupName = GroupNames.ContactInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.PostalAddressTooltip))]
        public virtual Address PostalAddress { get; set; }

        [Display(Order = 890, GroupName = GroupNames.CapitalInfo)]
        public bool FreeEconZone { get; set; }

        [Display(Order = 520, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.NumOfPeopleEmpTooltip))]
        public int? NumOfPeopleEmp { get; set; }

        [Display(Order = 521, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.EmployeesTooltip))]
        public int? Employees { get; set; }

        [Display(Order = 522, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.EmployeesYearTooltip))]
        public int? EmployeesYear { get; set; }

        [Display(Order = 523, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.EmployeesDateTooltip))]
        public DateTime? EmployeesDate { get; set; }

        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TurnoverTooltip))]
        [Display(Order = 505, GroupName = GroupNames.EconomicInformation)]
        public decimal? Turnover { get; set; }

        [Display(Order = 515, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TurnoverDateTooltip))]
        public DateTime? TurnoverDate { get; set; }

        [Display(Order = 510, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.TurnoverYearTooltip))]
        public int? TurnoverYear { get; set; }

        [Display(Order = 880, GroupName = GroupNames.CapitalInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.NotesTooltip))]
        public string Notes { get; set; }

        [Display(Order = 891, GroupName = GroupNames.CapitalInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ClassifiedTooltip))]
        public bool? Classified { get; set; }

        [Display(Order = 143, GroupName = GroupNames.StatUnitInfo)]
        public DateTime? StatusDate { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        [MaxLength(25)]
        public string RefNo { get; set; }

        public virtual int? InstSectorCodeId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual SectorCode InstSectorCode { get; set; }

        public virtual int? LegalFormId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual LegalForm LegalForm { get; set; }

        [Display(Order = 710, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime RegistrationDate { get; set; }

        [Display(Order = 711, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime? LiqDate { get; set; }

        [Display(Order = 712, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public string LiqReason { get; set; }

        [Display(Order = 720, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime? SuspensionStart { get; set; }

        [Display(Order = 730, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime? SuspensionEnd { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string ReorgTypeCode { get; set; }

        [Display(Order = 740, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime? ReorgDate { get; set; }

        [SearchComponent]
        [Display(Order = 750, GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public int? ReorgReferences { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public bool IsDeleted { get; set; }

        public abstract StatUnitTypes UnitType { get; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public DateTime StartPeriod { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public DateTime EndPeriod { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual ICollection<ActivityStatisticalUnit> ActivitiesUnits { get; set; } =
            new HashSet<ActivityStatisticalUnit>();

        [NotMapped]
        [Display(Order = 400, GroupName = GroupNames.Activities)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ActivitiesTooltip))]
        public IEnumerable<Activity> Activities
        {
            get => ActivitiesUnits.Select(v => v.Activity);
            set => throw new NotImplementedException();
        }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual ICollection<PersonStatisticalUnit> PersonsUnits { get; set; } =
            new HashSet<PersonStatisticalUnit>();

        [NotMapped]
        [Display(Order = 600, GroupName = GroupNames.Persons)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.PersonsTooltip))]
        public IEnumerable<Person> Persons
        {
            get => PersonsUnits.Select(v => v.Person);
            set => throw new NotImplementedException();
        }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string UserId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public ChangeReasons ChangeReason { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string EditComment { get; set; }

        [Reference(LookupEnum.UnitSizeLookup)]
        [Display(Order = 500, GroupName = GroupNames.EconomicInformation)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.UnitSizeTooltip))]
        public int? SizeId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual UnitSize Size { get; set; }

        [Reference(LookupEnum.ForeignParticipationLookup)]
        [Display(Order = 800, GroupName = GroupNames.CapitalInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ForeignParticipationTooltip))]
        public int? ForeignParticipationId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual ForeignParticipation ForeignParticipation { get; set; }

        [Reference(LookupEnum.DataSourceClassificationLookup)]
        [Display(Order = 141, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.DataSourceClassificationTooltip))]
        public int? DataSourceClassificationId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual DataSourceClassification DataSourceClassification { get; set; }

        [Reference(LookupEnum.ReorgTypeLookup)]
        [Display(Order = 700, GroupName = GroupNames.RegistrationInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ReorgTypeTooltip))]
        public int? ReorgTypeId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual ReorgType ReorgType { get; set; }

        [Reference(LookupEnum.UnitStatusLookup)]
        [Display(Order = 142, GroupName = GroupNames.StatUnitInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.UnitStatusTooltip))]
        public int? UnitStatusId { get; set; }

        [JsonIgnore]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual UnitStatus UnitStatus { get; set; }

        [JsonIgnore]
        [Reference(LookupEnum.CountryLookup)]
        [Display(Order = 805, GroupName = GroupNames.CapitalInfo)]
        [PopupLocalizedKey(nameof(Resources.Languages.Resource.ForeignParticipationCountriesTooltip))]
        public virtual ICollection<CountryStatisticalUnit> ForeignParticipationCountriesUnits { get; set; } =
            new HashSet<CountryStatisticalUnit>();
    }
}
