using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abp.UI;
using TestProject.Models;

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

            findPropExpression = Expression.Property(findPropExpression, propName);
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

        //private bool CheckIfPropExist(string propNameExp, Type findPropExpType)
        //{
        //    var props = findPropExpType.GetProperties();
        //    foreach (var prop in props)
        //    {
        //        if (prop.Name == propNameExp)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public Expression<Func<TEntity, bool>> GetWhereLambda<TEntity>(Expression binaryExp,
            ParameterExpression parameter)
        {
            var lambdaExp = Expression.Lambda<Func<TEntity, bool>>(binaryExp, parameter);
            return lambdaExp;
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

        public Expression FilterRuleNested<TEntity>(ParameterExpression param, List<Rules> input, string condition)
        {
            Expression result;
            if (condition == "and")
            {
                result = Expression.Constant(true, typeof(bool));
            }
            else
            {
                result = Expression.Constant(false, typeof(bool));
            }
            
            
            foreach (var rul in input)
            {
                Expression prperty = Expression.Property(param, rul.Property);
                var type = prperty.Type;
                var convertValue = Convert.ChangeType(rul.Value, type);
                var constant = Expression.Constant(convertValue);

                BinaryExpression binary;
                switch (convertValue)
                {
                    case string _:
                        binary = GetBinaryExpressionForString(rul.Operator, prperty, constant);
                        break;
                    case int _:
                        binary = GetBinaryExpressionForInt(rul.Operator, prperty, constant);
                        break;
                    default:
                        throw new UserFriendlyException($"Neocekivani tip vrijednosti '{type.Name}'");
                }

                switch (condition)
                {
                    case "and":
                        result = Expression.AndAlso(result, binary);
                        break;
                    case "or":
                        result = Expression.OrElse(result, binary);
                        break;
                    default:
                        throw new UserFriendlyException($"Neocekivani tip vrijednosti '{type.Name}'");
                }

                if (rul.Condition == null)
                {
                    continue;
                }

                switch (condition)
                {
                    case "and":
                        result = Expression.AndAlso(result, FilterRuleNested<TEntity>(param, rul.Rule, rul.Condition));
                        break;
                    case "or":
                        result = Expression.OrElse(result, FilterRuleNested<TEntity>(param, rul.Rule, rul.Condition));
                        break;
                    default:
                        throw new UserFriendlyException($"Neocekivani tip vrijednosti '{type.Name}'");
                }
                
            }

            
            return result;
        }


    }
}
