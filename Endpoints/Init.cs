namespace HomeClaims.Endpoints;

using HomeClaims.Core.Model;

public static class AppEndpoints {
    public static void InitEndpoints(this IEndpointRouteBuilder app)
    {
        // A simple GET endpoint returning a plain text
        app.MapGet("/", () => "Home Claims Automation Service!");

        // A simple GET endpoint returning a JSON object
        app.MapGet("/status", () => new { 
            System = "Home Claims Automation",
            Version = "v0.0.1",
            Os = "Linux Mint 22.3", 
            Framework = ".NET 10", 
            Status = "Online" 
        });
        
        app.MapPost("/claim-settlement", SettleClaim);
    }
    
    private static async Task<IResult> SettleClaim(ClaimRequest req, CancellationToken ct)
    {
        return await Task.FromResult(Results.Created($"Claim received - {req}", req));
        //return await Task.FromResult(Results.BadRequest($"Cannot process Claim - {req}"));
    }
}
