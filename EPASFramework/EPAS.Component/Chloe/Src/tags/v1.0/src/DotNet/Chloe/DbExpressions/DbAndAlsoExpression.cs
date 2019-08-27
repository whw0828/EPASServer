using Chloe.Utility;
using System.Reflection;

namespace Chloe.DbExpressions
{
    public class DbAndAlsoExpression : DbBinaryExpression
    {
        public DbAndAlsoExpression(DbExpression left, DbExpression right)
            : this(left, right, null)
        {

        }
        public DbAndAlsoExpression(DbExpression left, DbExpression right, MethodInfo method)
            : base(DbExpressionType.AndAlso, UtilConstants.TypeOfBoolean, left, right, method)
        {

        }

        public override T Accept<T>(DbExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

}
