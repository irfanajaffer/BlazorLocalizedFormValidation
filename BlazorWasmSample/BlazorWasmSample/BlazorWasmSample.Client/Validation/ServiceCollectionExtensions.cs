using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmSample.Client.Validation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClientValidation(this IServiceCollection services)
    {
        services.AddValidation(options =>
        {
            options.LocalizerProvider = (_, factory) =>
                factory.Create(typeof(ClientContactValidationMessages));
        });

        return services;
    }
}
