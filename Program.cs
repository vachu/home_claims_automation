namespace HomeClaims;

using HomeClaims.Endpoints;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Get the port from the appsettings config file (default to 5000 if not provided)
        var port = builder.Configuration["ServiceConfig:Port"] ?? "5000";
        builder.WebHost.UseUrls($"http://localhost:{port}");
        
        var app = builder.Build();
        app.InitEndpoints();
        app.Run();
    }
}
