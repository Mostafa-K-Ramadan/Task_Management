using Application.DTOs;
using FluentValidation;

namespace Application.TaskOb
{
        public class TaskValidator : AbstractValidator<TaskDTO>
        {
            public TaskValidator()
            {
                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Description is Empty")
                    .MaximumLength(1024).WithMessage("Length of description should be less than 1024");

                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Title is Empty")
                    .MaximumLength(1024).WithMessage("Length of title should be less than 160");

                RuleFor(x => x.Date)
                    .NotEmpty().WithMessage("Date is Empty")
                    .Must(BeValidDate).WithMessage("Operation Date is required")
                    .GreaterThan(DateTime.Now).WithMessage("Operation Date must be in the future");
            }

            private bool BeValidDate(DateTime date)
            {
                return !date.Equals(default(DateTime));
            }
        }
}


