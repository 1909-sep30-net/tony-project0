using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourStoreWeb.Models
{
    public class BaseViewModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public BaseViewModel([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseViewModel(BaseViewModel other)
        {
            _httpContextAccessor = other._httpContextAccessor;
        }


    }
}
