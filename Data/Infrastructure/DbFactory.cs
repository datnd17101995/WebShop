using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class DbFactory : Disposable,IDbFactory
    {
        private ShopDbContext dbContext;
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
        public ShopDbContext Init()
        {
            return dbContext?? (dbContext = new ShopDbContext());
        }
    }
}
