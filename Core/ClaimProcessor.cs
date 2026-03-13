namespace HomeClaims.Core;

using HomeClaims.Interfaces;
using HomeClaims.Core.Model;

public class ClaimProcessor : IClaimSettlement {
    public async Task<SettlementDecision> ProcessClaimAsync(ClaimRequest req, CancellationToken ct)
    {
        IPolicyClient pc = new PolicyAdminClient("");
        return await Task.FromResult(new SettlementDecision("", false, "invalid claim"));
    }
}
