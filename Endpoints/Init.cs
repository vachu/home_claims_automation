namespace HomeClaims.Endpoints;

using HomeClaims.Core;
using HomeClaims.Core.Model;
using HomeClaims.Interfaces;

public static class AppEndpoints {
    private static string policyAdminUrl = "";
    
    public static void InitEndpoints(this IEndpointRouteBuilder app, string url)
    {
        policyAdminUrl = url;        
        
        // A simple GET endpoint returning a plain text
        app.MapGet("/", () => "Home Claims Automation Service!");

        // A simple GET endpoint returning a JSON object
        app.MapGet("/status", () => new { 
            System = "Home Claims Automation Service",
            Version = "v0.0.1",
            Os = "Linux Mint 22.3", 
            Framework = ".NET 10", 
            Status = "Online" 
        });
        
        // The MAIN Endpoint for this exercise
        app.MapPost("/claim-settlement", SettleClaim);
    }
    
    private static async Task<IResult> SettleClaim(ClaimRequest req, CancellationToken ct)
    {
        IPolicyClient cli = new DummyPolicyAdminClient(policyAdminUrl);
        IClaimSettlementService settlement = new ClaimSettlementService(cli);
        
        // Create a "watchdog" task that completes when the token is cancelled
        Task cancellationTask = Task.Delay(-1, ct);
        
        // Supposedly a long-running Task
        Task<SettlementDecision> settlementTask = settlement.EvaluateClaimAsync(req, ct);
        
        var completedTask = Task.WhenAny(settlementTask, cancellationTask);
        if (completedTask == cancellationTask) {
            throw new OperationCanceledException(ct);
        }
        return Results.Ok(await settlementTask);
    }
}
