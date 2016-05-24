using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartManager.Interfaces
{
    public interface ICart<T>
    {
        Task<T> Get(string Id);
        Task Set(T obj, string Id);
        Task Update(T obj, string Id);
        Task Delete(string Id);
    }
}
