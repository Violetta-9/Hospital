﻿namespace Office.Application.Contracts.Outgoing;

public class AllOfficesDto
{
    public long Id { get; set; }
    public string Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public bool IsActive { get; set; }
}