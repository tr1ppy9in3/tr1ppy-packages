using Tr1ppy.Specifications.Abstractions;
using Tr1ppy.Specifications.Extensions;
using Tr1ppy.Specifications.TestsCore;

using static NUnit.Framework.Assert;

namespace Tr1ppy.Specification.Tests;

[TestFixture]
public class CompositionTests
{
    private readonly ISpecification<User> _ageSpec = new UserMustBeOverAgeSpecification(18);
    private readonly ISpecification<User> _activeSpec = new UserIsActiveSpecification();     

    private readonly User _charlie = new User { Name = "Charlie", Age = 15, IsActive = true};
    private readonly User _bob = new User { Name = "Bob", Age = 20, IsActive = true};
    private readonly User _david = new User { Name = "David", Age = 25, IsActive = false};
    private readonly User _alice = new User { Name = "Alice", Age = 30, IsActive = false};
    
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void AndAlso()
    {
        // Age > 18 && IsActive
        var composition = _ageSpec.AndAlso(_activeSpec);
        
        Multiple(() =>
        {
            That(composition.Matches(_charlie), Is.False, "Charlie: Should be False");
            That(composition.Matches(_bob), Is.True, "Bob: Should be True");
            That(composition.Matches(_david), Is.False, "David: Should be False");
            That(composition.Matches(_alice), Is.False, "Alice: Should be False");
        });
    }
    
    [Test]
    public void OrElse()
    {
        // Age > 18 || IsActive
        var composition = _ageSpec.OrElse(_activeSpec);
        
        Multiple(() =>
        {
            That(composition.Matches(_charlie), Is.True, "Charlie: Should be True");
            That(composition.Matches(_bob), Is.True, "Bob: Should be True");
            That(composition.Matches(_david), Is.True, "David: Should be True");
            That(composition.Matches(_alice), Is.True, "Alice: Should be True");
        });
    }
    
    [Test]
    public void AndNot()
    {
        // Age > 18 && !IsActive
        var composition = _ageSpec.AndNot(_activeSpec);
        
        Multiple(() =>
        {
            That(composition.Matches(_charlie), Is.False, "Charlie: Should be False");
            That(composition.Matches(_bob), Is.False, "Bob: Should be False");
            That(composition.Matches(_david), Is.True, "David: Should be True");
            That(composition.Matches(_alice), Is.True, "Alice: Should be True");
        });
    }
    
    [Test]
    public void OrNot()
    {
        // Age > 18 || !IsActive
        var composition = _ageSpec.OrNot(_activeSpec);
        
        Multiple(() =>
        {
            That(composition.Matches(_charlie), Is.False, "Charlie: Should be False");
            That(composition.Matches(_bob), Is.True, "Bob: Should be True");
            That(composition.Matches(_david), Is.True, "David: Should be True");
            That(composition.Matches(_alice), Is.True, "Alice: Should be True");
        });
    }
}