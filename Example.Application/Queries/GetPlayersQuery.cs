using Example.Application.Services;
using Example.Domain;
using Example.Infrastructure;
using Example.Infrastructure.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Application.Queries
{
    public class GetPlayersQuery : IRequest<Player>, IHandler<Response<Player>>
    {
        public Expression<Func<Player, bool>> Filter { get; set; }
        public Expression<Func<Player, object>> OrderSelector { get; set; }
        public bool OrderDescending { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public async Task<Response<Player>> Handle(GenericRepository repository) =>
            await repository.GetAll(this, nameof(Player.Team));
    }
}