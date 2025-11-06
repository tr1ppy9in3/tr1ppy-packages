using System.Linq.Expressions;

using Tr1ppy.Specifications.Abstractions;
using Tr1ppy.Specifications.Implementations;
using Tr1ppy.Specifications.Implementations.Combinations;

namespace Tr1ppy.Specifications.Extensions;

/// <summary>
/// Extensions for combine specification.
/// </summary>
public static class SpecificationExtensions
{
    /// <summary>
    /// Creates a new specification that logically negates the result of the current specification (logical NOT).
    /// </summary>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <param name="specification">The specification to be negated.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the negation.</returns>
    public static Specification<T> Negate<T>(this Specification<T> specification)
        => new NotSpecification<T>(specification);
    
    /// <summary>
    /// Combines two specifications using the logical AND operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the AND composition.</returns>
    public static ISpecification<T> AndAlso<T>(this ISpecification<T> left, ISpecification<T> right) 
        => new AndSpecification<T>(left, right);

    /// <summary>
    /// Combines a specification with a raw LINQ expression using the logical AND operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand. </param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the AND composition.</returns>
    public static ISpecification<T> AndAlso<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => AndAlso(left, right.AsSpecification());
    
    /// <summary>
    /// Combines two specifications using the logical OR operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the OR composition.</returns>
    public static ISpecification<T> OrElse<T>(this ISpecification<T> left, ISpecification<T> right)
        => new OrSpecification<T>(left, right);
    
    /// <summary>
    /// Combines a specification with a raw LINQ expression using the logical OR operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the OR composition.</returns>
    public static ISpecification<T> OrElse<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => new OrSpecification<T>(left, right.AsSpecification());
    
    /// <summary>
    /// Combines two specifications using the logical AND NOT operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the AND NOT composition.</returns>
    public static ISpecification<T> AndNot<T>(this ISpecification<T> left, ISpecification<T> right)
        => new AndNotSpecification<T>(left, right);
    
    /// <summary>
    /// Combines a specification with a raw LINQ expression using the logical AND NOT operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the AND NOT composition.</returns>
    public static ISpecification<T> AndNot<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => new AndNotSpecification<T>(left, right.AsSpecification());
    
    /// <summary>
    /// Combines two specifications using the logical OR NOT operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the OR NOT composition.</returns>
    public static ISpecification<T> OrNot<T>(this ISpecification<T> left, ISpecification<T> right)
        => new OrNotSpecification<T>(left, right);
    
    /// <summary>
    /// Combines a specification with a raw LINQ expression using the logical OR NOT operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the OR NOT composition.</returns>
    public static ISpecification<T> OrNot<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => new OrNotSpecification<T>(left, right.AsSpecification());

    /// <summary>
    /// Combines two specifications using the logical Exclusive OR operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the XOR composition.</returns>
    public static ISpecification<T> Xor<T>(this ISpecification<T> left, ISpecification<T> right)
        => new XorSpecification<T>(left, right);
    
    /// <summary>
    /// Combines a specification with a raw LINQ expression using the logical Exclusive OR operator.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the XOR composition.</returns>
    public static ISpecification<T> Xor<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => new XorSpecification<T>(left, right.AsSpecification());
    
    /// <summary>
    /// Defines a specification representing logical implication.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The specification to be user as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the implication.</returns>
    public static ISpecification<T> Implies<T>(this ISpecification<T> left, ISpecification<T> right)
        => new ImpliesSpecification<T>(left, right);
    
    /// <summary>
    /// Defines a specification representing logical implication with a raw LINQ expression.
    /// </summary>
    /// <param name="left">The specification to be used as the left operand.</param>
    /// <param name="right">The raw LINQ expression to be used as the right operand.</param>
    /// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
    /// <returns>A new <see cref="ISpecification{T}"/> representing the implication.</returns>
    public static ISpecification<T> Implies<T>(this ISpecification<T> left, Expression<Func<T, bool>> right)
        => new ImpliesSpecification<T>(left, right.AsSpecification());
}

