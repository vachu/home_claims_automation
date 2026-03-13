namespace HomeClaims.Interfaces;

using HomeClaims.Core.Model;

public interface IClaimRepository{
    Task SaveDecisionAsync(
        SettlementDecision decision,
        CancellationToken cancellationToken);
}
