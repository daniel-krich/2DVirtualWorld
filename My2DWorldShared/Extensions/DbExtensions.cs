using Microsoft.EntityFrameworkCore;
using My2DWorldShared.Data;
using My2DWorldShared.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public static Task<int> UpdateFieldsAsync<T>(this DbContext dbContext, T entity, params Expression<Func<T, object?>>[] fields) where T : BaseEntity
        {
            var attachedEntity = dbContext.Attach(entity);
            foreach (var field in fields)
            {
                string? fieldName = (field.Body as MemberExpression ?? ((UnaryExpression)field.Body).Operand as MemberExpression)?.Member.Name;
                if (fieldName != null)
                {
                    attachedEntity.Property(fieldName).IsModified = true;
                }
            }
            return dbContext.SaveChangesAsync();
        }

        public static Task<int> UpdateFieldsAsync<T>(this DbContext dbContext, T entity, params string[] fields) where T : BaseEntity
        {
            var attachedEntity = dbContext.Attach(entity);
            foreach (var field in fields)
            {
                attachedEntity.Property(field).IsModified = true;
            }
            return dbContext.SaveChangesAsync();
        }
    }
}
