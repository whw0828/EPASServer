using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace EPAS.DataEntity.Utilities.SQLServer
{

    public  class MsGenerateSql
    {
       static MsGenerateSql sqlObject = new MsGenerateSql();


        static Dictionary<string, string> displayItemMap = new Dictionary<string, string>();
        public static string endDisplayFormat = ",";

        public static string spaceChar = "  ";

        public static string whereSql = "";//Sql Where条件

        public static MSSQL msSQL = null;
        public static MsGenerateSql SelectFromTable<T>()
        {
            Type Root = typeof(T);

            msSQL = new MSSQL();

            msSQL.displayItem = "";
            msSQL.whereSql = "";
            msSQL.rootTable = "";
            msSQL.headCmd = "";

            displayItemMap.Clear();

            msSQL.rootTable= Root.Name;

            Dictionary<string, string> tempMap = EE.GetAllEntityPropertyName<T>();

            foreach (var temp in tempMap)
            {
                if (!displayItemMap.ContainsKey(temp.Key))
                {
                    displayItemMap.Add(temp.Key, temp.Value);
                }
            }

            return sqlObject;
        }

  


        StringBuilder sql = new StringBuilder();

        public static List<JoinRelationship> jrsList= new List<JoinRelationship>();

        
        private static void DisplayItem<T>()
        {
            Type A = typeof(T);

            displayItemMap.Add("", A.Name);
        }

        private static MsGenerateSql Join<T1, T2>(JoinType joinType, string OnCondition)
        {

            Type A = typeof(T1);
            Type B = typeof(T2);

            JoinRelationship item = new JoinRelationship();

            item.JoinTableName = A.Name;//Join 表的名称
            item.OnCondition = OnCondition;//Join条件
            item.JoinTableAsName = item.JoinTableName;//Join表的别名
            item.joinType = joinType;//Join类型 例如 Left Join ，Right Join ,Inner Join 

            if (jrsList.Find(x => x.JoinTableName == item.JoinTableName) != null)
            {
                item.JoinTableAsName = item.JoinTableAsName + "COPY";//防止表别名重复
            }

            Dictionary<string, string> tempMap= EE.GetAllEntityPropertyName<T1>();

            foreach(var temp in tempMap)
            {
                if(!displayItemMap.ContainsKey(temp.Key))
                {
                    displayItemMap.Add(temp.Key, temp.Value);
                }
             
            }
            

            jrsList.Add(item);

            return sqlObject;
        }

        #region where 
        public MsGenerateSql Where<T>(Expression<Func<T, bool>> infoExps)
        {
          
            whereSql = DealExpress(infoExps);

            return sqlObject;

        }


        public static string DealExpress(Expression exp)
        {
            if (exp is LambdaExpression)
            {
                LambdaExpression l_exp = exp as LambdaExpression;
                return DealExpress(l_exp.Body);
            }
            if (exp is BinaryExpression)
            {
                return DealBinaryExpression(exp as BinaryExpression);
            }
            if (exp is MemberExpression)
            {
                return DealMemberExpression(exp as MemberExpression);
            }
            if (exp is ConstantExpression)
            {
                return DealConstantExpression(exp as ConstantExpression);
            }
            if (exp is UnaryExpression)
            {
                return DealUnaryExpression(exp as UnaryExpression);
            }
            return "";
        }
          public static string DealUnaryExpression(UnaryExpression exp)
          {
              return DealExpress(exp.Operand);
          }
          public static string DealConstantExpression(ConstantExpression exp)
          {
              object vaule = exp.Value;
              string v_str = string.Empty;
              if (vaule == null)
              {
                  return "NULL";
              }
              if (vaule is string)
              {
                  v_str = string.Format("'{0}'", vaule.ToString());
              }
              else if (vaule is DateTime)
              {
                  DateTime time = (DateTime)vaule;
                  v_str = string.Format("'{0}'", time.ToString("yyyy-MM-dd HH:mm:ss"));
              }
              else
              {
                  v_str = vaule.ToString();
              }
              return v_str;
          }

        private static object GetCFExpressionValue(MemberExpression exp)
        {
            // expression is ConstantExpression or FieldExpression
            if (exp.Expression is ConstantExpression)
            {
                return (((ConstantExpression)exp.Expression).Value)
                        .GetType()
                        .GetField(exp.Member.Name)
                        .GetValue(((ConstantExpression)exp.Expression).Value);
            }
            else if (exp.Expression is MemberExpression)
            {
                return GetCFExpressionValue((MemberExpression)exp.Expression);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public static string DealBinaryExpression(BinaryExpression exp)
          {
           
              string left = DealExpress(exp.Left);
              string oper = GetOperStr(exp.NodeType);
           
            string right = DealExpress(exp.Right);
              if (right == "NULL")
              {
                  if (oper == "=")
                  {
                      oper = " is ";
                  }
                  else
                  {
                      oper = " is not ";
                  }
              }
              return left + oper + right;
          }
          public static string DealMemberExpression(MemberExpression exp)
          {
            string strExp = "";
            try
            {
               //处理表达式中变量
                strExp = GetCFExpressionValue(exp).ToString();
                strExp = string.Format("'{0}'", strExp);
            }
            catch
            {
                strExp = "[" + exp.Expression.Type.Name + "].[" + exp.Member.Name + "]";
            }
          
            return strExp;
          }
          public static string GetOperStr(ExpressionType e_type)
          {
              switch (e_type)
              {
                  case ExpressionType.OrElse: return " OR ";
                  case ExpressionType.Or: return "|";
                  case ExpressionType.AndAlso: return " AND ";
                  case ExpressionType.And: return "&";
                  case ExpressionType.GreaterThan: return ">";
                  case ExpressionType.GreaterThanOrEqual: return ">=";
                  case ExpressionType.LessThan: return "<";
                  case ExpressionType.LessThanOrEqual: return "<=";
                  case ExpressionType.NotEqual: return "<>";
                  case ExpressionType.Add: return "+";
                  case ExpressionType.Subtract: return "-";
                  case ExpressionType.Multiply: return "*";
                  case ExpressionType.Divide: return "/";
                  case ExpressionType.Modulo: return "%";
                 case ExpressionType.Equal: return "=";
             }
             return "";
         }
        #endregion


        public MsGenerateSql Distinct()
        {
            msSQL.headCmd = msSQL.headCmd + spaceChar+"DISTINCT" + spaceChar;
            return sqlObject;
        }

        //public MsGenerateSql LeftJoin<T1,T2>(Expression<Func<T1, T2, object[]>> infoExps)
        //{
        //    NewArrayExpression body = infoExps.Body as NewArrayExpression;


        //    //for (int i = 0; i < infoExps.Parameters.Count - 1; i++)
        //    //{
        //    //    /*
        //    //     * 0  0
        //    //     * 1  2
        //    //     * 2  4
        //    //     * 3  6
        //    //     * ...
        //    //     */
        //    //    int indexOfJoinType = i * 1;

        //    //    Expression joinTypeExpression = body.Expressions[indexOfJoinType];


        //    //    MemberExpression infoExp = joinTypeExpression as MemberExpression;
        //    //}

        //    Expression A = body.Expressions[0];
        //    MemberExpression infoExp = A as MemberExpression;


        //    string fieldA = infoExp.Member.Name;

        //    Expression B = body.Expressions[1];
        //    infoExp = B as MemberExpression;

        //    string fieldB = infoExp.Member.Name;

        //    if(body.Expressions.Count>2 && displayItem==".*")
        //    {
        //        displayItem = "";
        //    }

        //    int indexOfDisplayItem = 2 ;

        //   for(int i= indexOfDisplayItem; i<= body.Expressions.Count-1;i++)
        //    {
        //        int indexOfJoinType = i;

        //         Expression joinTypeExpression = body.Expressions[indexOfJoinType];
        //        if(joinTypeExpression!=null)
        //        {
        //            MemberExpression me = joinTypeExpression as MemberExpression;

        //            displayItem = displayItem+" "+"[" + me.Expression.Type.Name + "].[" + me.Member.Name + "]" + endDisplayFormat;

        //        }
        //    }


        //    return Join<T1, T2>(JoinType.LeftJoin, fieldA, fieldB);

        //}


        public MsGenerateSql LeftJoin<T1, T2>(Expression<Func<T1, T2, bool>> infoExps)
        {

            string OnCondition = DealExpress(infoExps);

            return Join<T1,T2>(JoinType.LeftJoin, OnCondition);

        }

        public MsGenerateSql AutoMapDisplayItem<T>()
        {
            Dictionary<string, string> tempMap = EE.GetAllEntityPropertyName<T>();

            foreach (var temp in tempMap)
            {
                if(displayItemMap.ContainsKey(temp.Key))
                {
                    msSQL.displayItem = msSQL.displayItem+" " + "[" + displayItemMap[temp.Key] + "].[" + temp.Key + "]" + endDisplayFormat;
                }
            }

            return sqlObject;
        }

        public MsGenerateSql ManualMapDisplayItem(object[] fields)
        {
            //msSQL.displayItem = "";
            foreach (var item in fields)
            {
                msSQL.displayItem = msSQL.displayItem + " " +item;
            }
           
            return sqlObject;
        }

        public MsGenerateSql ManualMapDisplayItem(string item)
        {
            msSQL.displayItem = msSQL.displayItem + " " + item ;       
            return sqlObject;
        }
        public  string GenerateSql()
        {
            StringBuilder sql = new StringBuilder();

            //去掉显示字段中多余的',' 否则SQL语法错误
            if (msSQL.displayItem!=null)
            {
                msSQL.displayItem = msSQL.displayItem.Trim();
                msSQL.displayItem = msSQL.displayItem.Substring(0, msSQL.displayItem.Length - endDisplayFormat.Length);
            }else
            {
                msSQL.displayItem = spaceChar + "*"+ spaceChar;
            }
            

            sql.Append("select " + msSQL.headCmd + msSQL.displayItem + " from [");

            sql.Append(msSQL.rootTable + "] AS ["+ msSQL.rootTable + "]");


            //联合查询表
            foreach (var item in jrsList)
            {

               if (item.joinType==JoinType.LeftJoin)
                {
            
                    string str = "\n Left Join [" + item.JoinTableName + "] AS [" + item.JoinTableAsName + "] on " + item.OnCondition;
                    sql.Append(str);
                    sql.Append("\r");
                }
            }

            //查询条件
            sql.Append("\r");
            sql.Append(" where "+whereSql);//Where查询条件

            //清空SQL
            jrsList.Clear();
            return sql.ToString();
        }
    }
}
