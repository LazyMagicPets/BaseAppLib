namespace BlazorUI;

public static class ConfigureBlazorUIMessages
{
    public static ILzMessages AddBlazorUIMessages(this ILzMessages lzMessages)
    {
        // Remember that the message file folders are modified to include the culture name 
        // by the load routine.
        // Example: system/SnapsApp/Messages.json => system/SnapsApp-en-US/Messages.json
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
