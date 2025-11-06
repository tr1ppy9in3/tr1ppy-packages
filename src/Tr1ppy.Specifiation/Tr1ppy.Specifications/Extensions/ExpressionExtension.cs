using System.Linq.Expressions;

using Tr1ppy.Specifications.Abstractions;
using Tr1ppy.Specifications.Implementations;

namespace Tr1ppy.Specifications.Extensions;

internal static class ExpressionExtension
{
    internal static Expression<Func<T, bool>> Negate<T>(this Expression<Func<T, bool>> expression)
    {
        var negated = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
    }
    
    internal static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> leftExpression, 
        Expression<Func<T, bool>> rightExpression
    )
    {
        return leftExpression.Combine(rightExpression, Expression.AndAlso);
    }
    
    internal static Expression<Func<T, bool>> OrElse<T>(
        this Expression<Func<T, bool>> leftExpression, 
        Expression<Func<T, bool>> rightExpression
    )
    {
        return leftExpression.Combine(rightExpression, Expression.OrElse);
    }
    
    private static Expression<T> Combine<T>(
        this Expression<T> leftExpression, 
        Expression<T> rightExpression, 
        Func<Expression, Expression, Expression> mergeOperation
    )
    {
        var parameterMap = leftExpression.Parameters
            .Select((leftParam, i) => new { Left = leftParam, Right = rightExpression.Parameters[i] })
            .ToDictionary(p => p.Right, p => p.Left);
        
        var rightBodyRebound = OwnExpressionVisitor.ReplaceParameters(parameterMap, rightExpression.Body);
        return Expression.Lambda<T>(
            mergeOperation(leftExpression.Body, rightBodyRebound), 
            leftExpression.Parameters
        );
    }
    
    internal static ISpecification<T> AsSpecification<T>(this Expression<Func<T, bool>> expression)
        => new ExpressionWrapperSpecification<T>(expression);
}


file class OwnExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> map) : ExpressionVisitor
{
    private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterMap = map
        ?? new Dictionary<ParameterExpression, ParameterExpression>();
    
    internal static Expression ReplaceParameters(
        Dictionary<ParameterExpression, ParameterExpression> parameterMap, 
        Expression expression
    )
    {
        return new OwnExpressionVisitor(parameterMap).Visit(expression);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Expression VisitParameter(ParameterExpression p)
    {
        return base.VisitParameter(_parameterMap.GetValueOrDefault(p, p));
    }
}