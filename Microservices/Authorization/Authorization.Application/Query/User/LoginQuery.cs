using Authorization.Application.Contracts.Incoming.User;
using MediatR;

namespace Authorization.Application.Query.User
{
    public class LoginQuery:IRequest<string>
    {
        public string Email { get; set;}
        public string Password { get; set;}

        public LoginQuery(LoginDTO login)
        {
            Email=login.Email;
            Password=login.Password;
        }
    }
}
