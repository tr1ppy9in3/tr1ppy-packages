using System.Linq.Expressions;
using Tr1ppy.Specifications.Abstractions;
using Tr1ppy.Specifications.Extensions;

namespace Tr1ppy.Specifications.Implementations;

/// <summary>
/// 
/// </summary>
/// <param name="Original"></param>
/// <typeparam name="T"></typeparam>
public record NotSpecification<T>(Specification<T> Original) : Specification<T>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override Expression<Func<T, bool>> Expression => Original.Expression.Negate();
}