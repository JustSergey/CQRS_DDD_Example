using Example.Application.Services;
using Example.Domain;
using Example.Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Example.Application.Commands
{
    public class AddPlayerCommand : IHandler
    {
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int TeamId { get; set; }

        public Player GetPlayer() => new()
        {
            Name = Name,
            TeamId = TeamId
        };

        public async Task Handle(GenericRepository repository) =>
            await repository.Create(GetPlayer());
    }
}