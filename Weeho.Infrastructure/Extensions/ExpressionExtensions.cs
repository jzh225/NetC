using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Compose two expression and merge all in a new expression
        /// </summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Expression instance</param>
        /// <param name="second">Expression to merge</param>
        /// <param name="merge">Function to merge</param>
        /// <returns>New merged expressions</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        /// <summary>
        /// And operator
        /// </summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Right Expression in AND operation</param>
        /// <param name="second">Left Expression in And operation</param>
        /// <returns>New AND expression</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        /// <summary>
        /// Or operator
        /// </summary>
        /// <typeparam name="T">Type of param in expression</typeparam>
        /// <param name="first">Right expression in OR operation</param>
        /// <param name="second">Left expression in OR operation</param>
        /// <returns>New Or expressions</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }


        public static string GetMemberName(this Expression expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            return string.Empty;
        }
        public static string GetPropertyName<T>(this Expression<Func<T, object>> property)
        {
            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)lambda.Body;
                return methodCallExpression.Method.Name;
            }
            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return ((PropertyInfo)memberExpression.Member).Name;
        }
        private static string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {

            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        }



        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {

            return new ParameterRebinder(map).Visit(exp);

        }



        protected override Expression VisitParameter(ParameterExpression p)
        {

            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {

                p = replacement;

            }

            return base.VisitParameter(p);

        }

    }
}
