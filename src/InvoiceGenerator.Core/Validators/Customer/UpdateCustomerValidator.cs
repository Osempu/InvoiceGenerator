using System;
using FluentValidation;
using InvoiceGenerator.Core.Requests.CustomerRequests;
using InvoiceGenerator.Core.Validators.Address;

namespace InvoiceGenerator.Core.Validators.Customer;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequestDto>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(250);
        
        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleForEach(x => x.Address)
            .SetValidator(new UpdateAddressValidator())
            .When(x => x.Address != null && x.Address.Any());
    }
}
