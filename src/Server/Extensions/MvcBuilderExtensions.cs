using System.Linq;
using VoteApp.Application.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace VoteApp.Server.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }

    }
}