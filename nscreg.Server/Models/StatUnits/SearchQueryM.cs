﻿using FluentValidation;
using nscreg.Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using nscreg.Resources.Languages;

namespace nscreg.Server.Models.StatUnits
{
    public class SearchQueryM : IValidatableObject
    {
        public string Wildcard { get; set; }
        public StatUnitTypes? Type { get; set; }
        public bool IncludeLiquidated { get; set; } = false;
        public decimal? EmployeesNumberFrom { get; set; }
        public decimal? EmployeesNumberTo { get; set; }
        public decimal? TurnoverFrom { get; set; }
        public decimal? TurnoverTo { get; set; }
        public string TerritorialCode { get; set; }
        public DateTime? LastChangeFrom { get; set; }
        public DateTime? LastChangeTo { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 12;

        public IEnumerable<ValidationResult> Validate(
                System.ComponentModel.DataAnnotations.ValidationContext validationContext)
            => new Validator()
                .Validate(this)
                .Errors
                .Select(x => new ValidationResult(x.ErrorMessage, new[] {x.PropertyName}));

        private class Validator : AbstractValidator<SearchQueryM>
        {
            public Validator()
            {
                RuleFor(x => x.Page)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(nameof(Resource.PageError));

                RuleFor(x => x.PageSize)
                    .GreaterThan(0)
                    .WithMessage(nameof(Resource.PageSizeError));

                RuleFor(x => x.LastChangeFrom)
                    .LessThanOrEqualTo(x => x.LastChangeTo)
                    .When(x => x.LastChangeFrom.HasValue && x.LastChangeTo.HasValue)
                    .WithMessage(nameof(Resource.LastChangeFromError));

                RuleFor(x => x.LastChangeTo)
                    .GreaterThanOrEqualTo(x => x.LastChangeFrom)
                    .When(x => x.LastChangeFrom.HasValue && x.LastChangeTo.HasValue)
                    .WithMessage(nameof(Resource.LastChangeToError));

                RuleFor(x => x.EmployeesNumberFrom)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.EmployeesNumberFrom.HasValue)
                    .WithMessage(nameof(Resource.EmployeesNumberFromErrorNegative));

                RuleFor(x => x.EmployeesNumberFrom)
                    .LessThanOrEqualTo(x => x.EmployeesNumberTo)
                    .When(x => x.EmployeesNumberFrom.HasValue && x.EmployeesNumberTo.HasValue)
                    .WithMessage(nameof(Resource.EmployeesNumberFromErrorLarge));

                RuleFor(x => x.EmployeesNumberTo)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.EmployeesNumberTo.HasValue)
                    .WithMessage(nameof(Resource.EmployeesNumberToErrorNegative));

                RuleFor(x => x.EmployeesNumberTo)
                    .LessThanOrEqualTo(x => x.EmployeesNumberFrom)
                    .When(x => x.EmployeesNumberFrom.HasValue && x.EmployeesNumberTo.HasValue)
                    .WithMessage(nameof(Resource.EmployeesNumberToErrorLess));

                RuleFor(x => x.TurnoverFrom)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.TurnoverFrom.HasValue)
                    .WithMessage(nameof(Resource.TurnoverFromErrorNegative));

                RuleFor(x => x.TurnoverFrom)
                    .LessThanOrEqualTo(x => x.TurnoverTo)
                    .When(x => x.TurnoverFrom.HasValue && x.TurnoverTo.HasValue)
                    .WithMessage(nameof(Resource.TurnoverFromErrorLarger));

                RuleFor(x => x.TurnoverTo)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.TurnoverTo.HasValue)
                    .WithMessage(nameof(Resource.TurnoverToErrorNegative));

                RuleFor(x => x.TurnoverTo)
                    .GreaterThanOrEqualTo(x => x.TurnoverFrom)
                    .When(x => x.TurnoverFrom.HasValue && x.TurnoverTo.HasValue)
                    .WithMessage(nameof(Resource.TurnoverToErrorLess));
            }
        }
    }
}