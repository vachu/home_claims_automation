namespace HomeClaims.Interfaces;

using HomeClaims.Core.Model;

// Interface to the external Policy Admin System
public interface IPolicyClient{
    Task<PolicyDetails> GetPolicyAsync(string policyNumber,
        CancellationToken cancellationToken);
}
