using Floo.App.Shared.Identity.User;
using Floo.Core.Shared;
using Floo.Core.Shared.Utils;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Floo.Core.Entities.Identity.Users
{
    public class UserService : IUserService
    {
        IEntityStorage<User> _userStorage;
        UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, IEntityStorage<User> userStorage)
        {
            _userManager = userManager;
            _userStorage = userStorage;
        }

        public async Task<UserDto> GetByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return Mapper.Map<User, UserDto>(user);
        }
    }
}
