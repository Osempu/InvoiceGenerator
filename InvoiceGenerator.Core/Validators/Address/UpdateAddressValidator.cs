using System;
using FluentValidation;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Requests.AddressRequests;

namespace InvoiceGenerator.Core.Validators.Address;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressRequestDto>
{
    public UpdateAddressValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
            
        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(250);
        
        RuleFor(x => x.Street2)
            .MaximumLength(250);

        RuleFor(x => x.PostalCode)
            .MaximumLength(30);
        
        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(250);
        
        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(250);
        
        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.AddressType)
            .NotEmpty()
            .Must(type => AddressType.validAddressTypes.Contains(type));

        RuleFor(x => x.CustomerId)
            .NotNull();
    }
}
