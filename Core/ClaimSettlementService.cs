namespace HomeClaims.Core;

using HomeClaims.Interfaces;
using HomeClaims.Core.Model;

public class ClaimSettlementService : IClaimSettlementService {
    private IPolicyClient policyAdminClient;
    
    public ClaimSettlementService(IPolicyClient policyAdminClient)
    {
        this.policyAdminClient = policyAdminClient;
    }
    
    public async Task<SettlementDecision> EvaluateClaimAsync(ClaimRequest request,
        CancellationToken cancellationToken)
    {
        if (policyAdminClient == null) {
            return await Task.FromResult(
                    new SettlementDecision("", false, "Policy Admin System inaccessible")
                );
        }
        
        // Create a "watchdog" task that completes when the token is cancelled
        Task cancellationTask = Task.Delay(-1, cancellationToken);
        
        // Supposedly a long-running Task
        Task<PolicyDetails> policyDetailsTask =
            policyAdminClient.GetPolicyAsync(request.PolicyNumber, cancellationToken);
        
        var completedTask = Task.WhenAny(policyDetailsTask, cancellationTask);
        if (completedTask == cancellationTask) {
            throw new OperationCanceledException(cancellationToken);
        }
        
        PolicyDetails det = await policyDetailsTask; // fetch Policy Details
        var statusReason = det.IsActive ? "" : "Policy is inactive";

        bool isWithinSmartSettleThreshold = det.IsActive;
        if (isWithinSmartSettleThreshold) {
            isWithinSmartSettleThreshold =
                request.PropertyAgeYears <= 30 && request.ClaimAmount < 3000m;
            if (!isWithinSmartSettleThreshold) {
                isWithinSmartSettleThreshold =
                    request.PropertyAgeYears > 30 && request.ClaimAmount < 1000m;
            }
            
            if (!isWithinSmartSettleThreshold)
                statusReason = "Smart Settle Threshold breached";
        }
        
        return await Task.FromResult(new SettlementDecision(
                                            request.PolicyNumber,
                                            isWithinSmartSettleThreshold,
                                            statusReason
                                        )
                                     );
    }
}
