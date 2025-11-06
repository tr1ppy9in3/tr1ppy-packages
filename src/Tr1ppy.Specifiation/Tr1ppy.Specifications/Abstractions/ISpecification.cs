using System.Linq.Expressions;

namespace Tr1ppy.Specifications.Abstractions;

/// <summary>
/// Reusable and composable business rule or query criterion.
/// </summary>
/// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the criterion defined by the specification as an Expression Tree.
    /// </summary>
    public Expression<Func<T, bool>> Expression { get; }
    
    /// <summary>
    /// Executes the specification's criterion against a single instance of the entity.
    /// </summary>
    /// <param name="object">The object of type <typeparamref name="T"/> to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the provided object satisfies the specification; 
    /// otherwise, <see langword="false"/>.
    /// </returns>
    bool Matches(T @object);
    
    /// <summary>
    /// Retrieves the compiled delegate (<see cref="Func{T, TResult}"/>) corresponding to the specification's expression.
    /// </summary>
    /// <remarks>
    /// This method is primarily used for efficient in-memory operations and is often implemented 
    /// using caching to avoid redundant expression compilation.
    /// </remarks>
    /// <returns>The compiled <see cref="Func{T, TResult}"/> delegate of the criterion.</returns>
    Func<T, bool> ToFunc();
}