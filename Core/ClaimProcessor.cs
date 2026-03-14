namespace HomeClaims.Core;

using HomeClaims.Interfaces;
using HomeClaims.Core.Model;

public class ClaimProcessor : IClaimSettlement {
    private IPolicyClient policyAdminClient;
    
    public ClaimProcessor(IPolicyClient policyAdminClient)
    {
        this.policyAdminClient = policyAdminClient;
    }
    
    public async Task<SettlementDecision> EvaluateClaimAsync(ClaimRequest request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult(new SettlementDecision("", false, "invalid claim"));
    }
}
