using FluentValidation;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).MaximumLength(100).WithMessage("Name must not exceed 200 characters.");
    }
}