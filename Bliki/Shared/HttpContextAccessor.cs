using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Shared
{
    public class BlikiHttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlikiHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext Context => _httpContextAccessor.HttpContext;
    }
}
