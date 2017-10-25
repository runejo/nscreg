﻿using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using nscreg.Server.Common.Validators;

namespace nscreg.Server.Common.Models.StatUnits.Create
{
    public class EnterpriseUnitCreateM : StatUnitModelBase
    {
        public int? EntGroupId { get; set; }
        [DataType(DataType.Date)]
        public DateTime EntGroupIdDate { get; set; }
        public bool Commercial { get; set; }
        public int? InstSectorCodeId { get; set; }
        public string TotalCapital { get; set; }
        public string MunCapitalShare { get; set; }
        public string StateCapitalShare { get; set; }
        public string PrivCapitalShare { get; set; }
        public string ForeignCapitalShare { get; set; }
        public string ForeignCapitalCurrency { get; set; }
        public string EntGroupRole { get; set; }
        public int[] LegalUnits { get; set; }
    }

    public class EnterpriseUnitCreateMValidator : StatUnitModelBaseValidator<EnterpriseUnitCreateM>
    {
        public EnterpriseUnitCreateMValidator()
        {
            RuleFor(x => x.LegalUnits)
                .Must(x => x != null && x.Length != 0)
                .WithMessage(Resources.Languages.Resource.ChooseAtLeastOne);
        }
    }
}