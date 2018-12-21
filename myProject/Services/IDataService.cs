using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Services
{
    public interface IDataService<T>
    {
        //bool Exist(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        void Create(T entity);
        T GetSingle(Func<T, bool> predicate);
        IEnumerable<T> Query(Func<T, bool> predicate);
        void Update(T entity);
        void Delete(T entity);
		IQueryable<T> GetQuery();

	}
}
