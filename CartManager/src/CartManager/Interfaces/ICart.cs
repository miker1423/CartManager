using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartManager.Interfaces
{
    public interface ICart<T>
    {
        Task<T> Get(string Id);
        void Set(T obj, string Id);
        void Update(T obj, string Id);
        void Delete(string Id);
    }
}
