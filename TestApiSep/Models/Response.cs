using TestApiSep.DTOs;

namespace TestApiSep.Models
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public RegistroDto? Data { get; set; }
    }
}
