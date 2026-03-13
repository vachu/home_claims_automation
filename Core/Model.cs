namespace HomeClaims.Core.Model;

public record ClaimRequest(string PolicyNumber,
    decimal ClaimAmount,
    int PropertyAgeYears);

public record SettlementDecision(string PolicyNumber,
    bool IsApproved,
    string StatusReason);

public record PolicyDetails(decimal CoverageLimit, bool IsActive);

