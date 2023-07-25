﻿namespace SysCredit.Api.DataTransferObject;

public record class GuarantorDataTransferObject
{
    public long GuarantorId { get; set; }

    public string? Identification { get; set; }

    public string? Name { get; set; } 

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Neighborhood { get; set; }

    public string? BussinessType { get; set; }

    public string? BussinessAddress { get; set; }

    public string? Phone { get; set; }

    public string? Relationship { get; set; }
}