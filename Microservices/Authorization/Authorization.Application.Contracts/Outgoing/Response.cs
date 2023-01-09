namespace Authorization.Application.Contracts.Outgoing;

public class Response
{
    public bool IsSuccess { get; set; }
    public static Response Success => new() { IsSuccess = true };
    public static Response Error => new() { IsSuccess = false };
}