using Application.Interfaces.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Di
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(config=>config.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IAdminService, AdminService>()
                .AddScoped<ICourseService, CourseService>()
                .AddScoped<IEnrollmentService, EnrollementService>()
                .AddScoped<IInstructorService, InstructorService>()
                .AddScoped<ISyllabusService, SyllabusService>();
            return services;
        }
    }
}
