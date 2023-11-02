using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    /// <summary>
    /// Adds all implementations of <see cref="IRequestHandler{TRequest}"/>
    /// and <see cref="IRequestHandler{TRequest, TResult}"/> to
    /// this <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddRequestHandlers(this IServiceCollection services) => services.Scan(scan => scan
        .FromAssembliesOf(typeof(DependencyInjection))
        .AddClasses(c => c.AssignableToAny(typeof(IRequestHandler<>), typeof(IRequestHandler<,>)))
        .AsSelfWithInterfaces()
        .WithScopedLifetime());
}