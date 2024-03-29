﻿using Microsoft.AspNetCore.Http;
using Profile.Application.Contracts.Enum;

namespace Profile.Application.Contracts.Incoming;

public class AddPhotoDTO
{
    public string AccountId { get; set; }
    public IFormFile File { get; set; }
    public long EntityType { get; set; }
    public SubjectUpdate SubjectUpdate { get; set; }
}