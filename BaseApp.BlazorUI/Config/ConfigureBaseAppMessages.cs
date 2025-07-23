namespace BlazorUI;

public static class ConfigureBaseAppMessages
{
    public static ILzMessages AddBaseAppMessages(this ILzMessages lzMessages)
    {
        List<string> messages = [
            ];
        lzMessages.MessageFiles.AddRange(messages);
        return lzMessages;
    }
}
