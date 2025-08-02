namespace BlazorUI;

public static class ConfigureBaseAppMessages
{
    public static ILzMessages AddBaseAppMessages(this ILzMessages lzMessages)
    {
        List<string> messages = [
            "system/{culture}/System/AuthMessages.json",
            "system/{culture}/System/BaseMessages.json",
            "system/{culture}/BaseApp/Messages.json",
            "system/{culture}/BaseApp/Images.json", // Key returns the image path
            "subtenancy/{culture}/BaseApp/Messages.json",
            "subtenancy/{culture}/BaseApp/Images.json", // Key returns the image path   
            ];
        lzMessages.MessageFiles.AddRange(messages);
        return lzMessages;
    }
}
