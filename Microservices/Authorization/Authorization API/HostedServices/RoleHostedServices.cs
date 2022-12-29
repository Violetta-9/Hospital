using Authorization.Application.Helpers;
using Microsoft.AspNetCore.Identity;


namespace Authorization_API.HostedServices
{
    public class RoleHostedServices:IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RoleHostedServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                var role = new IdentityRole(UserRoles.User);
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Doctor))
            {
                var role = new IdentityRole(UserRoles.Doctor);
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Patient))
            {
                var role = new IdentityRole(UserRoles.Patient);
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Receptionist))
            {
                var role = new IdentityRole(UserRoles.Receptionist);
                await roleManager.CreateAsync(role);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
