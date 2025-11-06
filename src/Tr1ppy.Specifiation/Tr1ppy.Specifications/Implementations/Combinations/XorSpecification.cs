using System.Linq.Expressions;
using Tr1ppy.Specifications.Extensions;
using Tr1ppy.Specifications.Abstractions;

namespace Tr1ppy.Specifications.Implementations.Combinations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public record XorSpecification<T> : CombinedSpecification<T>
{
    public XorSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right)
    {
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override Expression<Func<T, bool>> Expression
    {
        get
        {
            var leftSide = Left.Expression.AndAlso(Right.Expression.Negate());
            var rightSide = Left.Expression.Negate().AndAlso(Right.Expression);
            
            return leftSide.OrElse(rightSide);
        }
    }
}