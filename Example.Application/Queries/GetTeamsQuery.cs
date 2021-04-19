using Example.Application.Services;
using Example.Domain;
using Example.Infrastructure;
using Example.Infrastructure.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Application.Queries
{
    public class GetTeamsQuery : IRequest<Team>, IHandler<Response<Team>>
    {
        public Expression<Func<Team, bool>> Filter { get; set; }
        public Expression<Func<Team, object>> OrderSelector { get; set; }
        public bool OrderDescending { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public async Task<Response<Team>> Handle(GenericRepository repository) =>
            await repository.GetAll(this);
    }
}