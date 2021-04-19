using Example.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Example.Application.Services
{
    public interface IHandler<T>
    {
        Task<T> Handle(GenericRepository repository);
    }

    public interface IHandler
    {
        Task Handle(GenericRepository repository);
    }
}
