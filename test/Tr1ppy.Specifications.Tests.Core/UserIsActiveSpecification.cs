using System.Linq.Expressions;
using Tr1ppy.Specifications.Abstractions;

namespace Tr1ppy.Specifications.TestsCore;

public record UserIsActiveSpecification : Specification<User>
{
    public override Expression<Func<User, bool>> Expression => user => user.IsActive;
}