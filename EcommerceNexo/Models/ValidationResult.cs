namespace EcommerceNexo.Models
{
    public class ValidationResult
    {
        public bool Success {  get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}
