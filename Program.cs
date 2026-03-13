namespace home_claims_automation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Get the port from the appsettings config file (default to 5000 if not provided)
        var port = builder.Configuration["ServiceConfig:Port"] ?? "5000";

        // 2. Tell the Kestrel server to use this port
        builder.WebHost.UseUrls($"http://localhost:{port}");
        
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        // A simple GET endpoint returning a JSON object
        app.MapGet("/status", () => new { 
            System = "Home Claims Automation",
            Version = "v0.0.1",
            Os = "Linux Mint 22.3", 
            Framework = ".NET 10", 
            Status = "Online" 
        });

        app.Run();
    }
}
