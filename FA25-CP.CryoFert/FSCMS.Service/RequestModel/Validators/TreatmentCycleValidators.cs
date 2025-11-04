using FluentValidation;

namespace FSCMS.Service.RequestModel.Validators
{
    public class CreateTreatmentCycleRequestValidator : AbstractValidator<CreateTreatmentCycleRequest>
    {
        public CreateTreatmentCycleRequestValidator()
        {
            RuleFor(x => x.TreatmentId).NotEmpty();
            RuleFor(x => x.CycleName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.CycleNumber).InclusiveBetween(1, 100);
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).When(x => x.EndDate.HasValue);
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
        }
    }

    public class UpdateTreatmentCycleRequestValidator : AbstractValidator<UpdateTreatmentCycleRequest>
    {
        public UpdateTreatmentCycleRequestValidator()
        {
            RuleFor(x => x.CycleName).MaximumLength(200).When(x => x.CycleName != null);
            RuleFor(x => x.CycleNumber).InclusiveBetween(1, 100).When(x => x.CycleNumber.HasValue);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate!.Value).When(x => x.EndDate.HasValue && x.StartDate.HasValue);
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
        }
    }

    public class StartTreatmentCycleRequestValidator : AbstractValidator<StartTreatmentCycleRequest>
    {
        public StartTreatmentCycleRequestValidator()
        {
            // optional fields; no strict rules
        }
    }

    public class CompleteTreatmentCycleRequestValidator : AbstractValidator<CompleteTreatmentCycleRequest>
    {
        public CompleteTreatmentCycleRequestValidator()
        {
            RuleFor(x => x.Outcome).NotEmpty();
        }
    }

    public class CancelTreatmentCycleRequestValidator : AbstractValidator<CancelTreatmentCycleRequest>
    {
        public CancelTreatmentCycleRequestValidator()
        {
            RuleFor(x => x.Reason).NotEmpty();
        }
    }
}


