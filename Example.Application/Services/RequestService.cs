using Example.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Example.Application.Services
{
    public class RequestService
    {
        private readonly GenericRepository repository;

        public RequestService(GenericRepository repository)
        {
            this.repository = repository;
        }

        public async Task<T> Get<T>(IHandler<T> handler) =>
            await handler.Handle(repository);

        public async Task Send(IHandler handler) =>
            await handler.Handle(repository);
    }
}