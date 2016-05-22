using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;

using CartManager.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CartManager.Implementation
{
    public class Cart<T> : ICart<T>
    {
        private readonly IDistributedCache _cache;
        
        public Cart(IDistributedCache cache)
        {
            _cache = cache;
        }

        #region Public Methods
        /// <summary>
        /// Deletes the object cotained in key ID
        /// </summary>
        /// <param name="Id">Key of the object</param>
        public async void Delete(string Id)
        {
            try
            {
                await _cache.RemoveAsync(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Retrives an object
        /// </summary>
        /// <param name="Id">Id of the key</param>
        /// <returns>Object</returns>
        public async Task<T> Get(string Id)
        {
            try
            {
                var obj = await _cache.GetAsync(Id);
                var result = ByteToObj(obj);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Sets an object to certain key
        /// </summary>
        /// <param name="obj">Object to be stored</param>
        /// <param name="Id">Id that represents that object</param>
        public async void Set(T obj, string Id)
        {
            try
            {
                var array = ObjToByte(obj);
                await _cache.SetAsync(Id, array);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates an object
        /// </summary>
        /// <param name="obj">new object</param>
        /// <param name="Id">Id of the object that needs to be updated</param>
        public async void Update(T obj, string Id)
        {
            try
            {
                var tempCart = await Get(Id);
                tempCart = obj;
                Delete(Id);
                Set(obj, Id);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        #endregion

        #region Private Methods
        private T ByteToObj(byte[] array)
        {
            var memeStream = new MemoryStream();
            var formatter = new BinaryFormatter();

            memeStream.Write(array, 0, array.Length);
            memeStream.Seek(0, SeekOrigin.Begin);

            var result = (T)formatter.Deserialize(memeStream);

            return result;
        }

        private byte[] ObjToByte(T obj)
        {
            var formatter = new BinaryFormatter();
            var memStream = new MemoryStream();

            if(obj != null)
            {
                formatter.Serialize(memStream, obj);
            }

            return memStream.ToArray();
        }
        #endregion
    }
}
