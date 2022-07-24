﻿namespace Giantnodes.Worker.Domain.Common
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }
    }
}
