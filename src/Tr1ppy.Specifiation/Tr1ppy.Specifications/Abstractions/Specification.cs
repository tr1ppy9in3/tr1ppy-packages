using System.Linq.Expressions;
using Tr1ppy.Specifications.Extensions;
using Tr1ppy.Specifications.Implementations;

namespace Tr1ppy.Specifications.Abstractions;

/// <summary>
/// Base realization of <see cref="ISpecification{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record Specification<T> : ISpecification<T>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public abstract Expression<Func<T, bool>> Expression { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual bool Matches(T @object)
    {
        return FuncInstance.Invoke(@object);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<T, bool> ToFunc()
    {
        return FuncInstance;
    }
    
    /// <summary>
    /// Instance of compiled expression as func.
    /// </summary>
    private Func<T, bool> FuncInstance => _funcInstance ??= Expression.Compile();
    private Func<T, bool>? _funcInstance;
    
    /// <summary>
    /// Creates an <see cref="ISpecification{T}"/> instance by wrapping a raw <see cref="Expression{TDelegate}"/>.
    /// </summary>
    /// <remarks>
    /// This factory method is useful for quickly turning an ad-hoc LINQ expression into a formal, reusable specification.
    /// </remarks>
    /// <param name="expression">The LINQ expression to be wrapped.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> instance.</returns>
    public static ISpecification<T> FromExpression(Expression<Func<T, bool>> expression)
        => expression.AsSpecification();
    
    /// <summary>
    /// Combines a collection of specifications using a logical OR operator (Any Of).
    /// </summary>
    /// <param name="specifications">A collection of specifications to be combined.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> instance representing the logical OR of all inputs.</returns>
    public static ISpecification<T> AnyOf(params ISpecification<T>[] specifications)
        => new AnyOfSpecification<T>(specifications);

    /// <summary>
    /// Combines a collection of raw LINQ expressions using a logical OR operator (Any Of).
    /// </summary>
    /// <param name="expressions">A collection of LINQ expressions to be combined.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> instance representing the logical OR of all inputs.</returns>
    public static ISpecification<T> AnyOf(params Expression<Func<T, bool>>[] expressions)
        => AnyOf(expressions.Select(e => e.AsSpecification()).ToArray());
    
    /// <summary>
    /// Combines a collection of specifications using a logical AND operator (All Of).
    /// </summary>
    /// <param name="specifications">A collection of specifications to be combined.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> instance representing the logical AND of all inputs.</returns>
    public static ISpecification<T> AllOf(params ISpecification<T>[] specifications)
        => new AllOfSpecification<T>(specifications);
    
    /// <summary>
    /// Combines a collection of raw LINQ expressions using a logical AND operator.
    /// </summary>
    /// <param name="expressions">A collection of LINQ expressions to be combined.</param>
    /// <returns>A new <see cref="ISpecification{T}"/> instance representing the logical AND of all inputs.</returns>
    public static ISpecification<T> AllOf(params Expression<Func<T, bool>>[] expressions)
        => AllOf(expressions.Select(e => e.AsSpecification()).ToArray());
    
    /// <summary>
    /// Defines an implicit conversion from a <see cref="Specification{T}"/>
    /// to an <see cref="Expression{TDelegate}"/>.
    /// </summary>
    /// <param name="specification">The specification to be converted.</param>
    /// <returns>The underlying <see cref="Expression{TDelegate}"/> of the specification.</returns>
    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.Expression;
    }

    /// <summary>
    /// Defines an implicit conversion from a <see cref="Specification{T}"/>
    /// to a compiled <see cref="Func{T, TResult}"/> delegate.
    /// </summary>
    /// <param name="specification">The specification to be converted.</param>
    /// <returns>The compiled <see cref="Func{T, TResult}"/> delegate of the specification.</returns>
    public static implicit operator Func<T, bool>(Specification<T> specification)
    {
        return specification.ToFunc();
    }
}
