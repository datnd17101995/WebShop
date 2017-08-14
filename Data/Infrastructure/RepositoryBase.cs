using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class RepositoryBase<T> where T:class
    {
        #region properties
        private ShopDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        } 

        protected ShopDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = new ShopDbContext());
            }
        }

        public RepositoryBase(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
            dbSet = dbContext.Set<T>();
        }
        #endregion

        #region implement
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            //dbcontext chứa các đối tượng để biết xem nó có đồng bộ vs csdl hay ko.
            //khi sửa thì trạng thái của nó là modified 
            //khi gọi savechange thì nó sẽ thực hiện modified
            //khi đối tượng bị sửa mà chỉ muốn cập nhật những thuộc tính bị sửa Attach
            dbSet.Attach(entity);
            //trạng thái của hành động sang sửa.
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void DeleteMulti(Expression<Func<T,bool>> where)
        {
            IEnumerable<T> query =  dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in query)
                dbSet.Remove(obj);
        }

        public T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string[] includes)
        {
            return dbSet.Where(where).ToList();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> where, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var obj in includes.Skip(1))
                    query = query.Include(obj);
                return query.FirstOrDefault(where);
            }
            return dbContext.Set<T>().FirstOrDefault(where);
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).Count();
        }

        public IEnumerable<T> GetAll(string[] includes)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var obj in includes.Skip(1))
                    query = query.Include(obj);
                return query.AsEnumerable();
            }
            return dbContext.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> GetMulti(Expression<Func<T, bool>> where, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var obj in includes.Skip(1))
                    query = query.Include(obj);
                return query.Where<T>(where).AsEnumerable();
            }
            return dbContext.Set<T>().Where(where).AsEnumerable();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? dbContext.Set<T>().Where<T>(predicate).AsQueryable() : dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsEnumerable();
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count<T>(predicate) > 0;
        }
        #endregion
    }
}
