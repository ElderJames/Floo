using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public interface IIdentityContext
    {
        long? UserId { get; }

        string UserName { get; }

        string NickName { get; }

        string Avatar { get; }

        Task GetState();
    }
}