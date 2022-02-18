﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wiser.API.BL.Config;

namespace Wiser_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SystemRoles.SuperAdmin)]
    public class TestController : ControllerBase
    {
        [HttpGet, Route("/testrequest")]
        public string Testrequest()
        {
            return "hello";
        }
    }
}
