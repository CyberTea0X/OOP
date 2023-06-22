namespace StableDiffusion
{
    public class ImageProduct : IImagePromptProduct
    {
        private byte[] data;
        private string prompt;
        private uint count;
        private uint width;
        private uint height;

        public ImageProduct(byte[] data, string prompt, uint count, uint width, uint height)
        {
            this.data = data;
            this.prompt = prompt;
            this.count = count;
            this.width = width;
            this.height = height;
        }

        public uint GetCount()
        {
            return count;
        }

        public byte[] GetData()
        {
            return data;
        }

        public uint GetHeight()
        {
            return height;
        }

        public string GetPrompt()
        {
            return prompt;
        }

        public uint GetWidth()
        {
            return width;
        }
    }
}
