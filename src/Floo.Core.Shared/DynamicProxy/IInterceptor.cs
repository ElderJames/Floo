using System.Reflection;

namespace Floo.Core.Shared.DynamicProxy
{
    public interface IInterceptor
    {
        object Intercept(MethodInfo method, object[] parameters);
    }
}