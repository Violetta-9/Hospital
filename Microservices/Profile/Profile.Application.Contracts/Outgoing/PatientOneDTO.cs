﻿using Authorization.Data_Domain.Models;

namespace Profile.Application.Contracts.Outgoing;

public class PatientOneDTO
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string PhotoUrl { get; set; }
    public string DocumentAbsolutUrl { get; set; }
    public List<ResultForPatient> Results { get; set; }
}