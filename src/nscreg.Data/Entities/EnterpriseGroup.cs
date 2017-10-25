using nscreg.Data.Constants;
using nscreg.Utilities.Attributes;
using nscreg.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace nscreg.Data.Entities
{
    /// <summary>
    ///  Класс сущность группа предприятий
    /// </summary>
    public class EnterpriseGroup : IStatisticalUnit
    {
        public StatUnitTypes UnitType => StatUnitTypes.EnterpriseGroup;

        [DataAccessCommon]
        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit)]
        public int RegId { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        [NotMappedFor(ActionsEnum.Create)]
        public DateTime RegIdDate { get; set; }

        [DataAccessCommon]
        [Display(GroupName = GroupNames.StatUnitInfo)]
        public string StatId { get; set; }

        [Display(GroupName = GroupNames.StatUnitInfo)]
        public DateTime? StatIdDate { get; set; }

        [DataAccessCommon]
        [Display(GroupName = GroupNames.StatUnitInfo)]
        public string Name { get; set; }

        [Display(GroupName = GroupNames.StatUnitInfo)]
        public string ShortName { get; set; }

        [Display(Order = 110, GroupName = GroupNames.RegistrationInfo)]
        public DateTime RegistrationDate { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public string RegistrationReason { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public string TaxRegId { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public DateTime? TaxRegDate { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public string ExternalId { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public int? ExternalIdType { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public DateTime? ExternalIdDate { get; set; }

        [Display(GroupName = GroupNames.RegistrationInfo)]
        public string DataSource { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public bool IsDeleted { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? ParentId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? AddressId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? ActualAddressId { get; set; }

        [Display(Order = 100, GroupName = GroupNames.StatUnitInfo)]
        public string EntGroupType { get; set; }

        [Display(Order = 510, GroupName = GroupNames.StatUnitInfo)]
        public int? NumOfPeopleEmp { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public int? PostalAddressId { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string TelephoneNo { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string EmailAddress { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string WebAddress { get; set; }

        [NotMappedFor(ActionsEnum.Create)]
        [Display(GroupName = GroupNames.ContactInfo)]
        public DateTime? LiqDateStart { get; set; }

        [NotMappedFor(ActionsEnum.Create)]
        [Display(GroupName = GroupNames.ContactInfo)]
        public DateTime? LiqDateEnd { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string ReorgTypeCode { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public DateTime? ReorgDate { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string ReorgReferences { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public string ContactPerson { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        [Display(GroupName = GroupNames.LinkInfo)]
        public DateTime StartPeriod { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        [Display(GroupName = GroupNames.LinkInfo)]
        public DateTime EndPeriod { get; set; }

        [Display(GroupName = GroupNames.LiquidationInfo)]
        public string LiqReason { get; set; }

        [Display(GroupName = GroupNames.LiquidationInfo)]
        public string SuspensionStart { get; set; }

        [Display(GroupName = GroupNames.LiquidationInfo)]
        public string SuspensionEnd { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public int? Employees { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public int? EmployeesYear { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public DateTime? EmployeesDate { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public decimal? Turnover { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public int? TurnoverYear { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public DateTime? TurnoverDate { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public string Status { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public DateTime StatusDate { get; set; }

        [Display(GroupName = GroupNames.CapitalInfo)]
        public string Notes { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string UserId { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public ChangeReasons ChangeReason { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string EditComment { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public virtual Address Address { get; set; }

        [Display(GroupName = GroupNames.ContactInfo)]
        public virtual Address ActualAddress { get; set; }

        [Reference(LookupEnum.EnterpriseUnitLookup)]
        [Display(Order = 340, GroupName = GroupNames.LinkInfo)]
        public virtual ICollection<EnterpriseUnit> EnterpriseUnits { get; set; } = new HashSet<EnterpriseUnit>();

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual EnterpriseGroup Parrent { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public virtual ICollection<EnterpriseGroupAnalysisError> AnalysisErrors { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public string HistoryEnterpriseUnitIds { get; set; }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? RegMainActivityId
        {
            get => null;
            set { }
        }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? InstSectorCodeId
        {
            get => null;
            set { }
        }

        [NotMappedFor(ActionsEnum.Create | ActionsEnum.Edit | ActionsEnum.View)]
        public int? LegalFormId
        {
            get => null;
            set { }
        }
    }
}