﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Floo.App.Shared.Identity.User
{
    public interface IUserService
    {
        public Task<UserDto> FindByNameAsync(string userName);
        public Task<UserDto> FindByIdAsync(long id, CancellationToken cancellation);
    }
}
