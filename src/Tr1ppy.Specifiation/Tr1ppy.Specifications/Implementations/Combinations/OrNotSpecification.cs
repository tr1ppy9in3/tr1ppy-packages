using System.Linq.Expressions;
using Tr1ppy.Specifications.Extensions;
using Tr1ppy.Specifications.Abstractions;

namespace Tr1ppy.Specifications.Implementations.Combinations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public record OrNotSpecification<T>: CombinedSpecification<T>
{
    public OrNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override Expression<Func<T, bool>> Expression => Left.Expression.OrElse(Right.Expression.Negate());
}