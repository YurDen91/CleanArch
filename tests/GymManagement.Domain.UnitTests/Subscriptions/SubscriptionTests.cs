using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Subscriptions;
using TestCommon.Gyms;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        // Create subscription
        var subscription = SubscriptionFactory.CreateSubscription();

        // Create maximum number of gyms + 1
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.CreateVersion7()))
            .ToList();
        
        // Act 
        var addGymResults = gyms.ConvertAll(subscription.AddGym);
        
        // Assert
        // adding all gyms succeded, but the last one should fail
        var allButLastGymResults = addGymResults[..^1];
        allButLastGymResults.Should().AllSatisfy(addGymResults => addGymResults.Value.Should().Be(Result.Success));

        var lastGymResult = addGymResults.Last();
        lastGymResult.IsError.Should().BeTrue();
        lastGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);
    }
}