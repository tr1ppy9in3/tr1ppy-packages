using System.Linq.Expressions;
using Tr1ppy.Specifications.Abstractions;
using Tr1ppy.Specifications.Extensions;

namespace Tr1ppy.Specifications.Implementations;

/// <summary>
/// 
/// </summary>
/// <param name="Specifications"></param>
/// <typeparam name="T"></typeparam>
public record AllOfSpecification<T>(IEnumerable<ISpecification<T>> Specifications) : Specification<T>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override Expression<Func<T, bool>> Expression
    {
        get
        {
            if (Specifications.Any())
                return _ => true; 
            
            var resultExpression = Specifications.First().Expression;
            
            return Specifications
                .Skip(1)
                .Aggregate(resultExpression, (current, spec) => current.AndAlso(spec.Expression));
        }
    }
}