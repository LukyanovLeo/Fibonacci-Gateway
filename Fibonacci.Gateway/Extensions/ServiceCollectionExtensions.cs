namespace Gateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsDefault(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:7777")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
