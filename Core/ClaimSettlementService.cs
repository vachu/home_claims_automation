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
        return await Task.FromResult(new SettlementDecision("", false, "invalid claim"));
    }
}
