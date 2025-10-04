using System;
using FluentValidation;
using InvoiceGenerator.Core.Requests;
using InvoiceGenerator.Core.Validators.Address;

namespace InvoiceGenerator.Core.Validators.Customer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequestDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleForEach(x => x.Addresses)
            .SetValidator(new CreateAddressValidator())
            .When(c => c.Addresses != null && c.Addresses.Any());
    }
}
