using Microsoft.JSInterop;

namespace LzAppConsole;

public class LzConsoleInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private DotNetObjectReference<LzConsoleInterop> dotNetRef;

    public LzConsoleInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/LzAppConsole/lzconsole.js").AsTask());
        dotNetRef = DotNetObjectReference.Create(this);
    }

    public async ValueTask InitAsync(string selector = "#lzConsoleContainer")
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("initConsole", selector);
    }

    [JSInvokable]
    public void OnConsoleEvent(string message)
    {
        ConsoleEvent?.Invoke(message);
    }

    public event Action<string>? ConsoleEvent;

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }

        dotNetRef?.Dispose();
    }
}