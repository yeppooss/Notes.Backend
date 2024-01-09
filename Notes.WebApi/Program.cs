using Notes.Persistence;
using Notes.Application.Commons.Mappings;
using System.Reflection;
using Notes.Application.Interfaces;
using Notes.Application;
namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });

            builder.Services.AddApplication();
            builder.Services.AddPersistence(configuration);


            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);
                }catch(Exception ex) { }
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
