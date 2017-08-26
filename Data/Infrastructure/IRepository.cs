using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);
        T GetSingleById(int id);

        void DeleteMulti(Expression<Func<T,bool>> where);

        T GetSingeByCondition(Expression<Func<T, bool>> expression, string[] include = null);

        IEnumerable<T> GetAll(string[] include= null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> expression, string[] include = null);

        IEnumerable<T> GetMultiPagging(Expression<Func<T, bool>> filter, out int total, int page = 1, int pageSize = 50, string[] include = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}
