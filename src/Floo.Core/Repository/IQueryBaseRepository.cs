using System.Threading.Tasks;

namespace Floo.Core.Repository
{
    /// <summary>
    /// The Base Query Repository
    /// </summary>
    /// <typeparam name="T">The Type Return</typeparam>
    /// <typeparam name="I">The Type Of Id</typeparam>
    public interface IQueryBaseRepository<T> where T : class
    {
        Task<T> QueryById(long id);
    }
}