using System.Linq.Expressions;
using Tr1ppy.Specifications.Abstractions;

namespace Tr1ppy.Specifications.TestsCore;

public record UserMustBeOverAgeSpecification(byte TargetAge) : Specification<User>
{
    public override Expression<Func<User, bool>> Expression => user => user.Age >= TargetAge;
}