
namespace Authorization.Application.Contracts.Outgoing
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public static Response Success => new Response() { IsSuccess = true };
        public static Response Error => new Response() { IsSuccess = false };
    }
}
