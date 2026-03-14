namespace HomeClaims.Core;

using HomeClaims.Interfaces;
using HomeClaims.Core.Model;

public class DummyPolicyAdminClient: IPolicyClient {
    public DummyPolicyAdminClient(string url)
    {
        // url is supposed to be the API endpoint of the (external) Policy Admin system
        // It's deliberately unused in the implementation.
    }
    
    public async Task<PolicyDetails> GetPolicyAsync(string policyNumber,
        CancellationToken cancellationToken)
    {
        // NOTE: just a dummy implementation. Real one would call the relevant API
        // of the Policy Admin System
        PolicyDetails det = policyNumber.ToLower() switch {
            "policy_1" => new PolicyDetails(10000m, true),
            "policy_2" => new PolicyDetails(10000m, false),
            _ => new PolicyDetails(0m, false)
        };
        
        cancellationToken.ThrowIfCancellationRequested();
            // NOTE: doesn't make sense in this dummy implementation but given for illustration
            
        return await Task.FromResult(det);
    }
}
