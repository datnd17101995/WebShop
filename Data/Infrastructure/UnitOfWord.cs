using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public class UnitOfWord : IUnitOfWord
    {
        private readonly IDbFactory _dbFactory;
        private ShopDbContext dbContext;

        public UnitOfWord(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public ShopDbContext DbContext
        {
            get
            {
                return dbContext ?? (dbContext = new ShopDbContext());
            }
        }
        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
