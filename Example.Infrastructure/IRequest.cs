using System;
using System.Linq.Expressions;

namespace Example.Infrastructure
{
    public interface IRequest<T>
    {
        Expression<Func<T, bool>> Filter { get; set; }
        Expression<Func<T, object>> OrderSelector { get; set; }
        bool OrderDescending { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
    }
}