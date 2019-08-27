﻿using Chloe.Utility;
using System.Reflection;

namespace Chloe.DbExpressions
{
    public class DbOrElseExpression : DbBinaryExpression
    {
        public DbOrElseExpression(DbExpression left, DbExpression right)
            : this(left, right, null)
        {
        }
        public DbOrElseExpression(DbExpression left, DbExpression right, MethodInfo method)
            : base(DbExpressionType.OrElse, UtilConstants.TypeOfBoolean, left, right, method)
        {
        }

        public override T Accept<T>(DbExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

}
