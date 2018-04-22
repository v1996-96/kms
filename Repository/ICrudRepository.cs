using System.Collections.Generic;
using System.Threading.Tasks;
using kms.Models;

namespace kms.Repository
{
    public interface ICrudRepository<T> where T : BaseModel
    {
        Task Add(T item);
        Task Remove(int id);
        Task Update(T item);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetList();
    }
}
