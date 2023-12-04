using Caching.SimpleInfra.Domain.Common.Caching;
using Caching.SimpleInfra.Domain.Common.Entities;
using System.Linq.Expressions;

namespace Caching.SimpleInfra.Domain.Common.Query;

public class QuerySpecification<TEntity>(int pageSize, int pageToken)
{
    public List<Expression<Func<TEntity, bool>>> Predicate { get; } = new();
}
