namespace StableDiffusion.Pages
{
    public class DrawRequestModel
    {
        public string Prompt { get; set; }
        public int Count { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
