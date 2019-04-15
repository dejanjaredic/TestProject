using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Abp.UI;

namespace TestProject.Dto.QueryInfoDtos
{
    public class QueryInfo
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string SearchText { get; set; }
        public List<Sorters> Sorters { get; set; } = new List<Sorters>();
        public List<string> SearchProperties { get; set; } = new List<string>();
        public Filter Filter { get; set; }

        public BinaryExpression GetWhereExp<TEntity>(ParameterExpression par, string op, string propName, string value)
        {
            Expression findPropExpression = par;

            string[] propNamesExp = propName.Split(".");
            foreach (var propNameExp in propNamesExp)
            {
                Type findPropExpType = findPropExpression.Type;
                if (!CheckIfPropExist(propNameExp, findPropExpType))
                {
                    throw new UserFriendlyException("Property ne postoji");
                }

                findPropExpression = Expression.Property(findPropExpression, propNameExp);
            }

            var type = findPropExpression.Type;
            var convertValue = Convert.ChangeType(value, type);
            var constant = Expression.Constant(convertValue);

            BinaryExpression binary;
            switch (convertValue)
            {
                case string _:
                    binary = GetBinaryExpressionForString(op, findPropExpression, constant);
                    break;
                case int _:
                    binary = GetBinaryExpressionForInt(op, findPropExpression, constant);
                    break;
                default:
                    throw new UserFriendlyException($"Neocekivani tip vrijednosti '{type.Name}'");
            }

            return binary;
        }

        private bool CheckIfPropExist(string propNameExp, Type findPropExpType)
        {
            var props = findPropExpType.GetProperties();
            foreach (var prop in props)
            {
                if (prop.Name == propNameExp)
                {
                    return true;
                }
            }

            return false;
        }

        public BinaryExpression GetBinaryExpressionForInt(string op, Expression propExpression,
            ConstantExpression constant)
        {
            switch (op)
            {
                case "gt":
                    return Expression.GreaterThan(propExpression, constant);
                case "lt":
                    return Expression.LessThan(propExpression, constant);
                case "gte":
                    return Expression.GreaterThanOrEqual(propExpression, constant);
                case "lte":
                    return Expression.LessThanOrEqual(propExpression, constant);
                case "eq":
                    return Expression.Equal(propExpression, constant);
                default:
                    throw new UserFriendlyException($"Neocekivani operator {op}");
            }
        }

        public BinaryExpression GetBinaryExpressionForString(string op, Expression propExpression,
            ConstantExpression constant)
        {
            var trueExpression = Expression.Constant(true, typeof(bool));
            BinaryExpression bin;
            switch (op)
            {
                case "eq":
                    return Expression.Equal(propExpression, constant);
                case "ct":
                    MethodInfo methodInfo = typeof(string).GetMethod("Contains", new[] {typeof(string)});
                    var contains = Expression.Call(propExpression, methodInfo, constant);
                    bin = Expression.Equal(contains, trueExpression);
                    break;
                default:
                    throw new UserFriendlyException($"Neocekivani operator {op}");
            }

            return bin;
        }

        public Expression<Func<TEntity, object>> OrderThings<TEntity>(string propName, string direction)
        {
            var type = typeof(TEntity);
            var prop = type.GetProperty(propName);
            var param = Expression.Parameter(type);
            var access = Expression.Property(param, prop);
            var convert = Expression.Convert(access, typeof(object));
            var finalExp = Expression.Lambda<Func<TEntity, object>>(convert, param);
            return finalExp;
        }


    }
}
