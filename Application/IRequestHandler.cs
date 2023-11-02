namespace Application;

/// <summary>
/// An abstract request handler with no result.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <remarks>Мы используем их чтобы не было мусора в контроллерах и обрабочтках эндпоинтов.</remarks>
public interface IRequestHandler<in TRequest>
{
    public Task HandleAsync(TRequest request, CancellationToken ct = default);
}

/// <summary>
/// An abstract request handler that returns a result.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <remarks>Мы используем их чтобы не было мусора в контроллерах и обрабочтках эндпоинтов.</remarks>
public interface IRequestHandler<in TRequest, TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default);
}