﻿using nscreg.Utilities.Enums;
using System;
using FluentValidation;
using nscreg.Data.Constants;
using nscreg.Resources.Languages;

namespace nscreg.Server.Models.DataSources
{
    public class SearchQueryM
    {
        public string Wildcard { get; set; }
        public int Restriction { get; set; } = 0;
        public int Priority { get; set; } = 0;
        public int AllowedOperations { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public OrderRule OrderByValue { get; private set; } = OrderRule.Asc;
        public string OrderBy
        {
            set
            {
                OrderRule parsed;
                if (Enum.TryParse(value, out parsed))
                    OrderByValue = parsed;
            }
        }
    }

    // ReSharper disable once ArrangeTypeModifiers
    class SearchQueryMValidator : AbstractValidator<SearchQueryM>
    {
        public SearchQueryMValidator()
        {

            RuleFor(x => x.Restriction)
                .Must(x => x == 0 || Enum.IsDefined(typeof(StatUnitTypes), x))
                .WithMessage(nameof(Resource.BadDataSourceRestrictionSearch));

            RuleFor(x => x.Priority)
                .Must(x => x == 0 || Enum.IsDefined(typeof(DataSourcePriority), x))
                .WithMessage(nameof(Resource.BadDataSourcePrioritySearch));

            RuleFor(x => x.AllowedOperations)
                .Must(x => x == 0 || Enum.IsDefined(typeof(DataSourceAllowedOperation), x))
                .WithMessage(nameof(Resource.BadDataSourceAllowedOperationsSearch));

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0)
                .WithMessage(nameof(Resource.PageError));

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage(nameof(Resource.PageSizeError));
        }
    }
}