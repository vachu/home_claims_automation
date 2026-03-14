using HomeClaims.Core;
using HomeClaims.Core.Model;
using HomeClaims.Interfaces;
using Xunit;

public class ClaimSettlementServiceTests
{
    private class MockPolicyClient : IPolicyClient
    {
        public Task<PolicyDetails> GetPolicyAsync(string policyNumber, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PolicyDetails(2999m, true));
        }
    }

    [Fact]
    public async Task EvaluateClaimAsync_ReturnsApproved_WhenPolicyIsValid()
    {
        var policyClient = new MockPolicyClient();
        var service = new ClaimSettlementService(policyClient);
        var request = new ClaimRequest("policy_1", 1000, 20);
        var result = await service.EvaluateClaimAsync(request, CancellationToken.None);
        Assert.True(result.IsApproved);
        Assert.Equal("POL123", result.PolicyNumber);
    }

    [Fact]
    public async Task EvaluateClaimAsync_ReturnsDenied_WhenSmartThresholdIsExceeded()
    {
        var policyClient = new MockPolicyClient();
        var service = new ClaimSettlementService(policyClient);
        var request = new ClaimRequest("policy_1", 10000, 20);
        var result = await service.EvaluateClaimAsync(request, CancellationToken.None);
        Assert.False(result.IsApproved);
        Assert.Equal("Policy Admin System inaccessible", result.StatusReason);
    }

    [Fact]
    public async Task EvaluateClaimAsync_Cancels_WhenTokenIsCancelled()
    {
        var policyClient = new MockPolicyClient();
        var service = new ClaimSettlementService(policyClient);
        var request = new ClaimRequest("policy_1", 1000, 20);
        var cts = new CancellationTokenSource();
        cts.Cancel();
        await Assert.ThrowsAsync<TaskCanceledException>(async () =>
        {
            await service.EvaluateClaimAsync(request, cts.Token);
        });
    }
}
