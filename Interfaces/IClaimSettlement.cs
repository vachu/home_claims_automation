namespace HomeClaims.Interfaces;

using HomeClaims.Core.Model;

public interface IClaimSettlement {
    Task<SettlementDecision> ProcessClaimAsync(ClaimRequest req, CancellationToken ct);
}
