using System.Linq.Expressions;
using Tr1ppy.Specifications.Abstractions;

namespace Tr1ppy.Specifications.Implementations;

public record ExpressionWrapperSpecification<T>(Expression<Func<T, bool>> Wrapped) : Specification<T>
{
    public override Expression<Func<T, bool>> Expression { get; } = Wrapped;
}   