namespace HomeClaims.Interfaces;

using HomeClaims.Core.Model;

public interface IClaimSettlementService {
    Task<SettlementDecision> EvaluateClaimAsync(ClaimRequest request,
        CancellationToken cancellationToken);
}
