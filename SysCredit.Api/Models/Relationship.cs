﻿namespace SysCredit.Api.Models;

public record class Relationship : Entity
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
}
