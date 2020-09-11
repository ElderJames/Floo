using System.Threading.Tasks;

namespace Floo.Core.Shared.HttpProxy
{
    internal interface IServiceClient
    {
        Task SendAsync(ApiActionContext context);

        string RequestHost { get; }
    }
}