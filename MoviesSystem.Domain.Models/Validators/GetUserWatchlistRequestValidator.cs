using FluentValidation;
using MoviesSystem.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Validators
{
    public class GetUserWatchlistRequestValidator : AbstractValidator<GetUserWatchlistRequest>
    {
        public GetUserWatchlistRequestValidator()
        {
            RuleFor(request => request.UserId)
                .GreaterThan(0);
        }
    }
}
