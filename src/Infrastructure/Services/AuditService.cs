using AutoMapper;
using VoteApp.Application.Interfaces.Services;
using VoteApp.Infrastructure.Models.Audit;
using VoteApp.Application.Responses.Audit;
using VoteApp.Infrastructure.Contexts;
using VoteApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using VoteApp.Application.Extensions;
using VoteApp.Infrastructure.Specifications;
using Microsoft.Extensions.Localization;

namespace VoteApp.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly BlazorHeroContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuditService> _localizer;

        public AuditService(
            IMapper mapper,
            BlazorHeroContext context,
            IStringLocalizer<AuditService> localizer)
        {
            _mapper = mapper;
            _context = context;
            _localizer = localizer;
        }

        public async Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId)
        {
            var trails = await _context.AuditTrails.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditResponse>>(trails);
            return await Result<IEnumerable<AuditResponse>>.SuccessAsync(mappedLogs);
        }
    }
}