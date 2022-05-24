using Microsoft.EntityFrameworkCore;
using My2DWorldShared.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Extensions
{
    public static class DbExtensions
    {
        public static IQueryable<T> FindMany<T>(this DbSet<T> dbset, IEnumerable<int?> keys)
            where T : BaseEntity => dbset.Where(x => keys.Contains(x.Id));

        public static IQueryable<T> FindMany<T>(this DbSet<T> dbset, params int?[] keys)
            where T : BaseEntity => dbset.Where(x => keys.Contains(x.Id));
    }
}
