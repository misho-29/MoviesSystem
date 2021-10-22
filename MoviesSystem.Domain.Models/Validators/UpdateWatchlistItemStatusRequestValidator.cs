using FluentValidation;
using MoviesSystem.Domain.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Validators
{
    public class UpdateWatchlistItemStatusRequestValidator : AbstractValidator<UpdateWatchlistItemStatusRequest>
    {
        public UpdateWatchlistItemStatusRequestValidator()
        {
            RuleFor(requetst => requetst.UserId)
                .GreaterThan(0);
        }
    }
}
