﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace TechFest
{
    public static class Extensions
    {
        private const string WrongExpressionMessage = "Wrong expression\nshould be called with expression like\n() => PropertyName";

        public static string GetPropertyNameFromExpression<T>(
          this object target,
          Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(WrongExpressionMessage, "expression");
            }

            var member = memberExpression.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException(WrongExpressionMessage, "expression");
            }

            if (member.DeclaringType == null)
            {
                throw new ArgumentException(WrongExpressionMessage, "expression");
            }

            if (target != null)
            {
                if (!member.DeclaringType.GetTypeInfo().IsAssignableFrom(target.GetType().GetTypeInfo()))
                {
                    throw new ArgumentException(WrongExpressionMessage, "expression");
                }
            }

            if (member.GetMethod.IsStatic)
            {
                throw new ArgumentException(WrongExpressionMessage, "expression");
            }

            return member.Name;
        }
    }
}