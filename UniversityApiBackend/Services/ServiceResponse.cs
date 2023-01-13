namespace UniversityApiBackend.Services
{
    public class ServiceResponse <T>
    {
        public T? Data { get; set; }
        public bool? Success { get; set; } = true;
        public string? Message { get; set; }
        public string? Error { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
