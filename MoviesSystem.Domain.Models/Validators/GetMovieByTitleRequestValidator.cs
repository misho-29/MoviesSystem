using FluentValidation;
using MoviesSystem.ExternalService.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Models.Validators
{
    public class GetMovieByTitleRequestValidator : AbstractValidator<GetMovieByTitleRequest>
    {
        public GetMovieByTitleRequestValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty();
        }
    }
}
