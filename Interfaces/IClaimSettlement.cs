namespace HomeClaims.Interfaces;

using HomeClaims.Core.Model;

public interface IClaimSettlement {
    Task<SettlementDecision> EvaluateClaimAsync(ClaimRequest request,
        CancellationToken cancellationToken);
}
