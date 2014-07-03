using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Journeys.Application.Client.Wpf.Infrastructure.Extensions
{
    internal static class PropertyChangedEventExtensions
    {
        public static void Raise(this PropertyChangedEventHandler handler, object sender, [CallerMemberName] string propertyName = null)
        {
            if (handler == null || propertyName == null) return;
            handler(sender, new PropertyChangedEventArgs(propertyName));
        }

        public static void Raise(this PropertyChangedEventHandler handler, object sender, Expression<Func<object>> propertyExpression)
        {
            if (handler == null || propertyExpression == null) return;
            var propertyBodyExpression = propertyExpression.Body;
            var propertyAccessExpression = propertyBodyExpression.NodeType == ExpressionType.MemberAccess
                ? (MemberExpression)propertyBodyExpression
                : (MemberExpression)((UnaryExpression)propertyBodyExpression).Operand;
            handler.Raise(sender, propertyAccessExpression.Member.Name);
        }
    }
}
