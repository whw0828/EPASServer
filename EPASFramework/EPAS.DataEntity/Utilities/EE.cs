using EPAS.DataEntity.Utilities.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.DataEntity.Utilities
{
    /// <summary>
    /// 枚举实体中成员变量
    /// </summary>
    public class EE
    {
        //public static string Name<T>(Expression<Func<T, object[]>> enumInfo)
        //{
        //    LambdaExpression infoExp = enumInfo;



        //    NewArrayExpression body = infoExp.Body as NewArrayExpression;
        //    //  string body = infoExp.Name;

        //    return "";

        //}

        /// <summary>
        /// 返回实体类中所有成员变量的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllEntityPropertyName<T>()
        {
            Dictionary<string, string> displayItemMap = new Dictionary<string, string>();

            Type A = typeof(T);
            T obj = Activator.CreateInstance<T>();

            //待改进
            PropertyInfo[] defaultProps = obj.GetType().GetProperties();
            foreach (PropertyInfo dfProp in defaultProps)
            {
                if (dfProp != null)
                {
                    displayItemMap.Add(dfProp.Name, A.Name);
                }

            }


            return displayItemMap;


        }



        #region 获取实体类中字段的名称

        public static string Name<T>(Expression<Func<T, string>> infoExps)
        {
                MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            //  string body = infoExp.Name;

            return str;

        }

        public static string Name<T>(Expression<Func<T, int>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            //  string body = infoExp.Name;

            return str;

        }
        public static string Name<T>(Expression<Func<T, int?>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            //  string body = infoExp.Name;

            return str;

        }

        public static string Name<T>(Expression<Func<T, decimal>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }
        public static string Name<T>(Expression<Func<T, decimal?>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }

        public static string Name<T>(Expression<Func<T, DateTime>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }
        public static string Name<T>(Expression<Func<T, DateTime?>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }
        public static string Name<T>(Expression<Func<T, bool>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }
        public static string Name<T>(Expression<Func<T, bool?>> infoExps)
        {
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string str = infoExp.Member.Name;
            return str;

        }

        #endregion

        /// <summary>
        /// 拼写SQL 中Where条件 仅适用于单个实体的Where条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="infoExps"></param>
        /// <returns></returns>
        public static string Where<T>(Expression<Func<T, bool>> infoExps)
        {

           string whereSql = MsGenerateSql.DealExpress(infoExps);

            return whereSql;

        }

        public static string NameSql<T>(Expression<Func<T,object[]>> infoExps)
        {
            Type A = typeof(T);
            MemberExpression infoExp = infoExps.Body as MemberExpression;

            string endDisplayFormat = ",";


            string str = "";
            //  string body = infoExp.Name;

            NewArrayExpression body = infoExps.Body as NewArrayExpression;
            int indexOfDisplayItem = 0 ;
            for (int i = indexOfDisplayItem; i <= body.Expressions.Count - 1; i++)
            {
                int indexOfJoinType = i;

                Expression joinTypeExpression = body.Expressions[indexOfJoinType];
                if (joinTypeExpression != null)
                {
                    MemberExpression me = joinTypeExpression as MemberExpression;

                    str = str + " " + "[" + me.Expression.Type.Name + "].[" + me.Member.Name + "]" + endDisplayFormat;

                }
            }

            return str;

    }

    }

}
