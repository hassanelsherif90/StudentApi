namespace StudentApi
{
    public class attachmentOptions
    {
        public required string AllowedExtensions { get; set; }
        public int MaxSizeInMegaBytes { get; set; }
        public bool EnableCompression { get; set; }
    }
}
