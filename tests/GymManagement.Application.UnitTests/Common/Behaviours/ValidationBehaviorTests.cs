using FluentValidation;
using FluentAssertions;
using FluentValidation.Results;
using ErrorOr;
using GymManagement.Api.Common.Behaviors;
using GymManagement.Api.Gyms.Commands.CreateGym;
using MediatR;
using NSubstitute;
using TestCommon.Gyms;
using GymManagement.Domain.Gyms;


namespace GymManagement.Api.UnitTests;

public class ValidationBehaviorTests
{
    private readonly ValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _validationBehavior;
    private readonly IValidator<CreateGymCommand> _mockValidator;
    private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehaviour;
    
    public ValidationBehaviorTests()
    {
        // Create a next behaviour (mock)
        _mockNextBehaviour = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();
        
        // Create a validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();
        
        // Create a validation behaviour (SUT)
        _validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(_mockValidator);
    }
    
    [Fact]
    public async Task InvokeBehaviour_WhenValidatorResultIsValid_ShouldInvokeNextBehaviour()
    {
        // Arrange
        // Create a request
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        
        var gym = GymFactory.CreateGym();

        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehaviour.Invoke().Returns(gym);
        
        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehaviour, default);
        
        // Assert
        // Result from invoking the behavior, was the result returned by the next behaviour
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(gym);
    }
    
    [Fact]
    public async Task InvokeBehaviour_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        // Create a request
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        var validationFailure = new ValidationFailure
        {
            ErrorCode = "Name",
            ErrorMessage = "Name is required"
        };
        
        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult([validationFailure]));
        
        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehaviour, default);
        
        // Assert
        // Result from invoking the behavior, was the result returned by the next behaviour
        result.IsError.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.FirstError.Code.Should().Be(validationFailure.ErrorCode);
        result.FirstError.Description.Should().Be(validationFailure.ErrorMessage);
    }
}