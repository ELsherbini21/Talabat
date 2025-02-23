﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Services.Contract
{
    public interface IAuthService
    {
        // take user that i will generate token For him .
        Task<string> CreateTokenAsync(ApplicationUser appUser);
    }
}
