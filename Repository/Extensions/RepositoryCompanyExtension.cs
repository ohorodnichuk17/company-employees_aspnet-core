using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryCompanyExtension
{
    public static IQueryable<Company> Sort(this IQueryable<Company> companies, string orderByQueryString)
    {
        if(string.IsNullOrWhiteSpace(orderByQueryString))
            return companies.OrderBy(c => c.Name);
        
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Company>(orderByQueryString);
        
        if(string.IsNullOrWhiteSpace(orderQuery))
            return companies.OrderBy(c => c.Name);
        
        return companies.OrderBy(orderQuery);
    }
}