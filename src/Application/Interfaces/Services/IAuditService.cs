﻿using VoteApp.Application.Responses.Audit;
using VoteApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Services
{
    public interface IAuditService
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId);
    }
}