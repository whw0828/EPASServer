
namespace Chloe.DbExpressions
{
    public enum DbExpressionType
    {
        And = 1,
        AndAlso,
        Or,
        OrElse,
        Equal,
        NotEqual,
        Not,
        Convert,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Add,
        Subtract,
        Multiply,
        Divide,
        Constant,
        CaseWhen,
        MemberAccess,
        Call,

        Table,
        ColumnAccess,

        Parameter,
        FromTable,
        JoinTable,
        Aggregate,

        SqlQuery,
        SubQuery,
        Insert,
        Update,
        Delete,

    }
}
