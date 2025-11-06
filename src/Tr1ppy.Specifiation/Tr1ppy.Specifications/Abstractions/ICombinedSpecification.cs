namespace Tr1ppy.Specifications.Abstractions;

/// <summary>
/// Defines a specification composed of two other specifications via a logical operator 
/// (e.g., AND, OR, XOR).
/// </summary>
/// <typeparam name="T">The type of entity to which the specification applies.</typeparam>
public interface ICombinedSpecification<T> : ISpecification<T>
{
    /// <summary>
    /// Gets the left-hand operand of the logical composition.
    /// </summary>
    ISpecification<T> Left { get; }
    
    /// <summary>
    /// Gets the right-hand operand of the logical composition.
    /// </summary>
    ISpecification<T> Right { get; }
}