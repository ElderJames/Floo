using Floo.Core.Shared;

namespace Floo.App.Shared.Identity.User
{
    public class UserDto : BaseDto<long>
    {
        public virtual string Email { get; set; }

        public virtual string UserName { get; set; }
    }
}
