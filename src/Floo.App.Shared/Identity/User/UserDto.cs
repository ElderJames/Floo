using Floo.Core.Shared;

namespace Floo.App.Shared.Identity.User
{
    public class UserDto : BaseDto<long>
    {
        public string NickName { get; set; }

        public string Avatar { get; set; }

        public virtual string Email { get; set; }

        public virtual string UserName { get; set; }
    }
}
