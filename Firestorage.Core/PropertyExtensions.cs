using System;
using System.Linq.Expressions;

namespace Firestorage.Core
{
    public static class PropertyExtensions
    {
        public static string GetPropertyName(this Expression<Func<object>> extension)
        {
            var memberExpression = extension.Body is UnaryExpression unaryExpression ? (MemberExpression)unaryExpression.Operand : (MemberExpression)extension.Body;
            return memberExpression.Member.Name;
        }
    }
}
