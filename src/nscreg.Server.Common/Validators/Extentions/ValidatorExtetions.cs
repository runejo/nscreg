using System;
using FluentValidation;
using nscreg.Resources.Languages;
using Newtonsoft.Json;

namespace nscreg.Server.Common.Validators.Extentions
{
    /// <summary>
    /// Validation extension class
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Method validator number greater than 0 or less value
        /// </summary>
        /// <param name = "ruleBuilder"> Rule constructor </param>
        /// <param name = "compareTo"> Comparison with </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<T, int> CheckIntGreaterThan0OrLessThanValueValidator<T>(
            this IRuleBuilder<T, int> ruleBuilder,
            int compareTo = 0)
            =>
                compareTo <= 0
                    ? ruleBuilder.SetValidator(new CheckIntGreaterThanZeroOrGreaterThanValueValidator())
                        .WithMessage(nameof(Resource.IncorrectIntegerValue))
                    : ruleBuilder.SetValidator(new CheckIntGreaterThanZeroOrGreaterThanValueValidator(compareTo))
                        .WithMessage(
                            JsonConvert.SerializeObject(
                                new
                                {
                                    LocalizedKey = nameof(Resource.IncorrectIntegerValueExt),
                                    Parameters = new[] {compareTo}
                                }));

        /// <summary>
        /// The string validator method on non-empty and larger values
        /// </summary>
        /// <typeparam name = "T"> </typeparam>
        /// <param name = "ruleBuilder"> </param>
        /// <param name = "maxLength"> </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<T, string> CheckStringNotEmptyAndGreaterThanValidator<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            int maxLength)
            =>
                ruleBuilder.SetValidator(new CheckStringNotEmptyAndGreaterThanValidator(maxLength))
                    .WithMessage(JsonConvert.SerializeObject(
                        new
                        {
                            LocalizedKey = nameof(Resource.IncorrectStringValue),
                            Parameters = new[] {maxLength}
                        })
                    );

        /// <summary>
        /// Method validator of the year
        /// </summary>
        /// <param name = "ruleBuilder"> Rule constructor </param>
        /// <param name = "minYear"> The least year to start </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<T, int> Year<T>(this IRuleBuilder<T, int> ruleBuilder, int minYear = 1900)
            => ruleBuilder
                .GreaterThan(minYear)
                .Must(v => v <= DateTime.Today.Year);

        /// <summary>
        /// Message format validator method
        /// </summary>
        /// <param name = "ruleBuilder"> Rule constructor </param>
        /// <param name = "localizedKey"> Localization key </param>
        /// <param name = "parameters"> Parameter </param>
        /// <returns> </returns>
        public static IRuleBuilderOptions<TModel, TProperty> WithFormatMessage<TModel, TProperty>(
            this IRuleBuilderOptions<TModel, TProperty> ruleBuilder,
            string localizedKey,
            params object[] parameters)
            =>
                ruleBuilder.WithMessage(JsonConvert.SerializeObject(
                    new
                    {
                        LocalizedKey = localizedKey,
                        Parameters = parameters,
                    })
                );
    }
}
