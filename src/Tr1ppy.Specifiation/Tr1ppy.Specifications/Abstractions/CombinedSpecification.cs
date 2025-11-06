namespace Tr1ppy.Specifications.Abstractions;

/// <summary>
/// Base realization of <see cref="CombinedSpecification{T}"/>
/// </summary>
/// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
public abstract record CombinedSpecification<T> : Specification<T>, ICombinedSpecification<T>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ISpecification<T> Left { get; }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ISpecification<T> Right { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CombinedSpecification{T}"/> record.
    /// </summary>
    /// <param name="left">The left-hand specification (operand).</param>
    /// <param name="right">The right-hand specification (operand).</param>
    protected CombinedSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        Left = left;
        Right = right;
    }
}