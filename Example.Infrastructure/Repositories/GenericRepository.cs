using Example.Domain;
using Example.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Example.Infrastructure.Repositories
{
    public class GenericRepository
    {
        private readonly IDbContextFactory<ExampleContext> contextFactory;

        public GenericRepository(IDbContextFactory<ExampleContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Response<T>> GetAll<T>(IRequest<T> request, params string[] includeProperties) where T : class, IHasId
        {
            ExampleContext context = contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();
            foreach (string property in includeProperties)
                query = query.Include(property);
            if (request.Filter != null)
                query = query.Where(request.Filter);
            if (request.OrderSelector != null)
            {
                if (request.OrderDescending)
                    query = query.OrderByDescending(request.OrderSelector);
                else
                    query = query.OrderBy(request.OrderSelector);
            }
            else
                query = query.OrderBy(e => e.Id);
            int totalCount = query.Count();
            if (request.PageSize > 0 && request.Page > 0)
            {
                int skip = (request.Page - 1) * request.PageSize;
                query = query.Skip(skip).Take(request.PageSize);
            }
            Response<T> response = new()
            {
                Data = await query.ToListAsync(),
                TotalCount = totalCount
            };
            await context.DisposeAsync();
            return response;
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> expression) where T : class, IHasId
        {
            ExampleContext context = contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();
            T entity = await query.FirstOrDefaultAsync(expression);
            await context.DisposeAsync();
            return entity;
        }

        public async Task<T> Get<T>(int id) where T : class, IHasId =>
            await Get<T>(e => e.Id == id);

        public async Task Create<T>(T entity) where T : class, IHasId
        {
            ExampleContext context = contextFactory.CreateDbContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            await context.DisposeAsync();
        }

        public async Task Update<T>(T entity) where T : class, IHasId
        {
            ExampleContext context = contextFactory.CreateDbContext();
            context.Update(entity);
            await context.SaveChangesAsync();
            await context.DisposeAsync();
        }

        public async Task Delete<T>(T entity) where T : class, IHasId
        {
            ExampleContext context = contextFactory.CreateDbContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
            await context.DisposeAsync();
        }
    }
}