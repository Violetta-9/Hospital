﻿namespace Profile.Application.Contracts.Outgoing;

public class PatientAllDTO
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
}