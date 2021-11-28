using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VoteApp.Domain.Contracts;
using VoteApp.Shared.Wrapper;

namespace VoteApp.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        
    }
}