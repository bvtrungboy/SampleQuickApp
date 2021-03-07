using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickApp.ViewModels
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class AdvertisementViewModellValidator : AbstractValidator<AdvertisementViewModel>
    {
        public AdvertisementViewModellValidator()
        {
            RuleFor(register => register.Name).NotEmpty().WithMessage("Advertisement name cannot be empty");
            RuleFor(register => register.StartDate).NotEmpty().WithMessage("StartDate cannot be empty");
            RuleFor(register => register.EndDate).NotEmpty().WithMessage("EndDate cannot be empty");
        }
    }
}
