using FluentValidation;
using MoviesSystem.Domain.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Validators
{
    public class AddWatchlistItemRequestForUserValidator : AbstractValidator<AddWatchlistItemRequestForUser>
    {
        public AddWatchlistItemRequestForUserValidator()
        {
            RuleFor(request => request.UserId)
                .GreaterThan(0);
        }
    }
}
