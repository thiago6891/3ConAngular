using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRSApi.Data.Interfaces
{
    public interface IRepository<T>
    {
        T Obter(int id);

        IQueryable<T> Query();

        void Create(T entity);

        void Delete(int id);

        void Delete(T entity);

        void Update(T entity);

        int Count();
    }
}