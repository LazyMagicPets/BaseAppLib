namespace BlazorUI;

public class WindowSize
{
    public int Width { get; set; }   
    public int Height { get; set; }  
    public bool IsLandscape => Width > Height;
    public bool IsPortrait => Width < Height;

}
