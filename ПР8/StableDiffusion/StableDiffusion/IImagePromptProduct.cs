namespace StableDiffusion
{
    public interface IImagePromptProduct
    {
        byte[] GetData();
        string GetPrompt();
        uint GetCount();
        uint GetWidth();
        uint GetHeight();
    }
}
