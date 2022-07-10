﻿using FluentValidation;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries.GetDirectoryContents
{
    public class GetDirectoryContentsValidator : AbstractValidator<GetDirectoryContents>
    {
        public GetDirectoryContentsValidator()
        {
            RuleFor(p => p.Directory)
                .NotEmpty();
        }
    }
}
