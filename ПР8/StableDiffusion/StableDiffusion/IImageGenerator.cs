namespace StableDiffusion
{
    public interface IImageGenerator
    {
        IImagePromptProduct GenerateImage(string prompt, uint count, uint width , uint height);
    }
}
