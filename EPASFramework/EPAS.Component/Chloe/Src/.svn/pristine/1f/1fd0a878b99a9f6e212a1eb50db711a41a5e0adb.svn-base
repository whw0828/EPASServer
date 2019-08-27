using Chloe;
using Chloe.Core;
using Chloe.PostgreSQL;
using Chloe.SqlServer;
using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    //[TestClass]
    public class PostgreSQLDemo
    {
        /* WARNING: DbContext 是非线程安全的，正式使用不能设置为 static，并且用完务必要调用 Dispose 方法销毁对象 */
        static string ConnString = "User ID=postgres;Password=sa;Host=localhost;Port=5432;Database=chloe;Pooling=true;";
        static PostgreSQLContext context = new PostgreSQLContext(new PostgreSQLConnectionFactory(ConnString));

        public static void Run()
        {
            BasicQuery();
            JoinQuery();
            AggregateQuery();
            GroupQuery();
            ComplexQuery();
            Insert();
            InsertRange();
            Update();
            Delete();
            Method();
            ExecuteCommandText();
            DoWithTransaction();
            DoWithTransactionEx();

            ConsoleHelper.WriteLineAndReadKey();
        }


        public static void BasicQuery()
        {
            IQuery<User> q = context.Query<User>();

            q.Where(a => a.Id == 1).FirstOrDefault();
            /*
             * 
             */


            //可以选取指定的字段
            q.Where(a => a.Id == 1).Select(a => new { a.Id, a.Name }).FirstOrDefault();
            /*
             * 
             */


            //分页
            q.Where(a => a.Id > 0).OrderBy(a => a.Age).Skip(20).Take(10).ToList();
            /*
             * 
             */


            /* like 查询 */
            q.Where(a => a.Name.Contains("so") || a.Name.StartsWith("s") || a.Name.EndsWith("o")).ToList();
            /*
             * 
             */


            /* in 一个数组 */
            List<User> users = null;
            List<int> userIds = new List<int>() { 1, 2, 3 };
            users = q.Where(a => userIds.Contains(a.Id)).ToList(); /* list.Contains() 方法组合就会生成 in一个数组 sql 语句 */
            /*
             * 
             */


            /* in 子查询 */
            users = q.Where(a => context.Query<City>().Select(c => c.Id).ToList().Contains((int)a.CityId)).ToList(); /* IQuery<T>.ToList().Contains() 方法组合就会生成 in 子查询 sql 语句 */
            /*
             * 
             */


            /* distinct 查询 */
            q.Select(a => new { a.Name }).Distinct().ToList();
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void JoinQuery()
        {
            //建立连接
            var user_city_province = context.Query<User>()
                                    .InnerJoin<City>((user, city) => user.CityId == city.Id)
                                    .InnerJoin<Province>((user, city, province) => city.ProvinceId == province.Id);

            //查出用户及其隶属的城市和省份的所有信息
            var view = user_city_province.Select((user, city, province) => new { User = user, City = city, Province = province }).Where(a => a.User.Id > 1).ToList();
            /*
             * 
             */

            //也可以只获取指定的字段信息：UserId,UserName,CityName,ProvinceName
            user_city_province.Select((user, city, province) => new { UserId = user.Id, UserName = user.Name, CityName = city.Name, ProvinceName = province.Name }).Where(a => a.UserId > 1).ToList();
            /*
             * 
             */


            /* quick join and paging. */
            context.JoinQuery<User, City>((user, city) => new object[]
            {
                JoinType.LeftJoin, user.CityId == city.Id
            })
            .Select((user, city) => new { User = user, City = city })
            .Where(a => a.User.Id > -1)
            .OrderByDesc(a => a.User.Age)
            .TakePage(1, 20)
            .ToList();

            context.JoinQuery<User, City, Province>((user, city, province) => new object[]
            {
                JoinType.LeftJoin, user.CityId == city.Id,          /* 表 User 和 City 进行Left连接 */
                JoinType.LeftJoin, city.ProvinceId == province.Id   /* 表 City 和 Province 进行Left连接 */
            })
            .Select((user, city, province) => new { User = user, City = city, Province = province })   /* 投影成匿名对象 */
            .Where(a => a.User.Id > -1)     /* 进行条件过滤 */
            .OrderByDesc(a => a.User.Age)   /* 排序 */
            .TakePage(1, 20)                /* 分页 */
            .ToList();

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void AggregateQuery()
        {
            IQuery<User> q = context.Query<User>();

            q.Select(a => Sql.Count()).First();
            /*
             * 
             */

            q.Select(a => new { Count = Sql.Count(), LongCount = Sql.LongCount(), Sum = Sql.Sum(a.Age), Max = Sql.Max(a.Age), Min = Sql.Min(a.Age), Average = Sql.Average(a.Age) }).First();
            /*
             * 
             */

            var count = q.Count();
            /*
             * 
             */

            var longCount = q.LongCount();
            /*
             * 
             */

            var sum = q.Sum(a => a.Age);
            /*
             * 
             */

            var max = q.Max(a => a.Age);
            /*
             * 
             */

            var min = q.Min(a => a.Age);
            /*
             * 
             */

            var avg = q.Average(a => a.Age);
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void GroupQuery()
        {
            IQuery<User> q = context.Query<User>();

            IGroupingQuery<User> g = q.Where(a => a.Id > 0).GroupBy(a => a.Age);

            g = g.Having(a => a.Age > 1 && Sql.Count() > 0);

            g.Select(a => new { a.Age, Count = Sql.Count(), Sum = Sql.Sum(a.Age), Max = Sql.Max(a.Age), Min = Sql.Min(a.Age), Avg = Sql.Average(a.Age) }).ToList();
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }

        /*复杂查询*/
        public static void ComplexQuery()
        {
            /*
             * 支持 select * from Users where CityId in (1,2,3)    --in一个数组
             * 支持 select * from Users where CityId in (select Id from City)    --in子查询
             * 支持 select * from Users exists (select 1 from City where City.Id=Users.CityId)    --exists查询
             * 支持 select (select top 1 CityName from City where Users.CityId==City.Id) as CityName, Users.Id, Users.Name from Users    --select子查询
             * 支持 select 
             *            (select count(*) from Users where Users.CityId=City.Id) as UserCount,     --总数
             *            (select max(Users.Age) from Users where Users.CityId=City.Id) as MaxAge,  --最大年龄
             *            (select avg(Users.Age) from Users where Users.CityId=City.Id) as AvgAge   --平均年龄
             *      from City
             *      --统计查询
             */

            IQuery<User> userQuery = context.Query<User>();
            IQuery<City> cityQuery = context.Query<City>();

            List<User> users = null;

            /* in 一个数组 */
            List<int> userIds = new List<int>() { 1, 2, 3 };
            users = userQuery.Where(a => userIds.Contains(a.Id)).ToList();  /* list.Contains() 方法组合就会生成 in一个数组 sql 语句 */
            /*
             * 
             */


            /* in 子查询 */
            users = userQuery.Where(a => cityQuery.Select(c => c.Id).ToList().Contains((int)a.CityId)).ToList();  /* IQuery<T>.ToList().Contains() 方法组合就会生成 in 子查询 sql 语句 */
            /*
             * 
             */


            /* IQuery<T>.Any() 方法组合就会生成 exists 子查询 sql 语句 */
            users = userQuery.Where(a => cityQuery.Where(c => c.Id == a.CityId).Any()).ToList();
            /*
             * 
             */


            /* select 子查询 */
            var result = userQuery.Select(a => new
            {
                CityName = cityQuery.Where(c => c.Id == a.CityId).First().Name,
                User = a
            }).ToList();
            /*
             * 
             */


            /* 统计 */
            var statisticsResult = cityQuery.Select(a => new
            {
                UserCount = userQuery.Where(u => u.CityId == a.Id).Count(),
                MaxAge = userQuery.Where(u => u.CityId == a.Id).Max(c => c.Age),
                AvgAge = userQuery.Where(u => u.CityId == a.Id).Average(c => c.Age),
            }).ToList();
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void Insert()
        {
            //返回主键 Id
            int id = (int)context.Insert<User>(() => new User() { Name = "lu", Age = 18, Gender = Gender.Man, CityId = 1, OpTime = DateTime.Now });
            /*
             * 
             */

            User user = new User();
            user.Name = "lu";
            user.Age = 18;
            user.Gender = Gender.Man;
            user.CityId = 1;
            user.OpTime = DateTime.Now;

            //会自动将自增 Id 设置到 user 的 Id 属性上
            user = context.Insert(user);
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void InsertRange()
        {
            List<User> models = new List<User>();
            models.Add(new User() { Name = "lu", Age = 18, Gender = Gender.Woman, CityId = 1, OpTime = DateTime.Now });
            models.Add(new User() { Name = "shuxin", Age = 18, Gender = Gender.Man, CityId = 1, OpTime = DateTime.Now });

            context.InsertRange(models);

            ConsoleHelper.WriteLineAndReadKey(1);
        }
        public static void Update()
        {
            context.Update<User>(a => a.Id == 1, a => new User() { Name = a.Name, Age = a.Age + 1, Gender = Gender.Man, OpTime = DateTime.Now });
            /*
             * 
             */

            //批量更新
            //给所有女性年轻 1 岁
            context.Update<User>(a => a.Gender == Gender.Woman, a => new User() { Age = a.Age - 1, OpTime = DateTime.Now });
            /*
             * 
             */

            User user = new User();
            user.Id = 1;
            user.Name = "lu";
            user.Age = 28;
            user.Gender = Gender.Man;
            user.OpTime = DateTime.Now;

            context.Update(user); //会更新所有映射的字段
            /*
             * 
             */


            /*
             * 支持只更新属性值已变的属性
             */

            context.TrackEntity(user);//在上下文中跟踪实体
            user.Name = user.Name + "1";
            context.Update(user);//这时只会更新被修改的字段
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void Delete()
        {
            context.Delete<User>(a => a.Id == 1);
            /*
             * 
             */

            //批量删除
            //删除所有不男不女的用户
            context.Delete<User>(a => a.Gender == null);
            /*
             * 
             */

            User user = new User();
            user.Id = 1;
            context.Delete(user);
            /*
             * 
             */

            ConsoleHelper.WriteLineAndReadKey(1);
        }

        public static void Method()
        {
            IQuery<User> q = context.Query<User>();

            var space = new char[] { ' ' };

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(1);
            var result = q.Select(a => new
            {
                Id = a.Id,

                CustomFunction = DbFunctions.MyFunction(a.Id), //自定义函数

                String_Length = (int?)a.Name.Length,//
                Substring = a.Name.Substring(0),//
                Substring1 = a.Name.Substring(1),//
                Substring1_2 = a.Name.Substring(1, 2),//
                ToLower = a.Name.ToLower(),//
                ToUpper = a.Name.ToUpper(),//
                IsNullOrEmpty = string.IsNullOrEmpty(a.Name),//
                Contains = (bool?)a.Name.Contains("s"),//
                Trim = a.Name.Trim(),//
                TrimStart = a.Name.TrimStart(space),//
                TrimEnd = a.Name.TrimEnd(space),//
                StartsWith = (bool?)a.Name.StartsWith("s"),//
                EndsWith = (bool?)a.Name.EndsWith("s"),//
                Replace = a.Name.Replace("l", "L"),

                DateTimeSubtract = endTime.Subtract(startTime),

                /* pgsql does not support Sql.DiffXX methods. */
                //DiffYears = Sql.DiffYears(startTime, endTime),//DATEDIFF(YEAR,@P_0,@P_1)
                //DiffMonths = Sql.DiffMonths(startTime, endTime),//DATEDIFF(MONTH,@P_0,@P_1)
                //DiffDays = Sql.DiffDays(startTime, endTime),//DATEDIFF(DAY,@P_0,@P_1)
                //DiffHours = Sql.DiffHours(startTime, endTime),//DATEDIFF(HOUR,@P_0,@P_1)
                //DiffMinutes = Sql.DiffMinutes(startTime, endTime),//DATEDIFF(MINUTE,@P_0,@P_1)
                //DiffSeconds = Sql.DiffSeconds(startTime, endTime),//DATEDIFF(SECOND,@P_0,@P_1)
                //DiffMilliseconds = Sql.DiffMilliseconds(startTime, endTime),//DATEDIFF(MILLISECOND,@P_0,@P_1)
                //DiffMicroseconds = Sql.DiffMicroseconds(startTime, endTime),//DATEDIFF(MICROSECOND,@P_0,@P_1)  Exception

                AddYears = startTime.AddYears(1),//
                AddMonths = startTime.AddMonths(1),//
                AddDays = startTime.AddDays(1),//
                AddHours = startTime.AddHours(1),//
                AddMinutes = startTime.AddMinutes(2),//
                AddSeconds = startTime.AddSeconds(120),//
                AddMilliseconds = startTime.AddMilliseconds(20000),//

                Now = DateTime.Now,//NOW()
                //UtcNow = DateTime.UtcNow,//GETUTCDATE()
                Today = DateTime.Today,//
                Date = DateTime.Now.Date,//
                Year = DateTime.Now.Year,//
                Month = DateTime.Now.Month,//
                Day = DateTime.Now.Day,//
                Hour = DateTime.Now.Hour,//
                Minute = DateTime.Now.Minute,//
                Second = DateTime.Now.Second,//
                Millisecond = DateTime.Now.Millisecond,//
                DayOfWeek = DateTime.Now.DayOfWeek,//

                Int_Parse = int.Parse("32"),//
                Int16_Parse = Int16.Parse("16"),//
                Long_Parse = long.Parse("64"),//
                Double_Parse = double.Parse("3.123"),//
                Float_Parse = float.Parse("4.123"),//
                Decimal_Parse = decimal.Parse("5.123"),//
                //Guid_Parse = Guid.Parse("D544BC4C-739E-4CD3-A3D3-7BF803FCE179"),//

                Bool_Parse = bool.Parse("1"),//
                DateTime_Parse = DateTime.Parse("1992-1-16"),//

                B = a.Age == null ? false : a.Age > 1, //三元表达式
                CaseWhen = Case.When(a.Id > 100).Then(1).Else(0) //case when
            }).ToList();

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void ExecuteCommandText()
        {
            List<User> users = context.SqlQuery<User>("select * from Users where Age > @age", DbParam.Create("@age", 12)).ToList();

            int rowsAffected = context.Session.ExecuteNonQuery("update Users set name=@name where Id = 1", DbParam.Create("@name", "Chloe"));

            /* 
             * 执行存储过程:
             * User user = context.SqlQuery<User>("Proc_GetUser", CommandType.StoredProcedure, DbParam.Create("@id", 1)).FirstOrDefault();
             * rowsAffected = context.Session.ExecuteNonQuery("Proc_UpdateUserName", CommandType.StoredProcedure, DbParam.Create("@name", "Chloe"));
             */

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void DoWithTransactionEx()
        {
            context.UseTransaction(() =>
            {
                context.Update<User>(a => a.Id == 1, a => new User() { Name = a.Name, Age = a.Age + 1, Gender = Gender.Man, OpTime = DateTime.Now });
                context.Delete<User>(a => a.Id == 1024);
            });

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void DoWithTransaction()
        {
            context.Session.BeginTransaction();
            try
            {
                /* do some things here */
                context.Update<User>(a => a.Id == 1, a => new User() { Name = a.Name, Age = a.Age + 1, Gender = Gender.Man, OpTime = DateTime.Now });
                context.Delete<User>(a => a.Id == 1024);

                context.Session.CommitTransaction();
            }
            catch
            {
                if (context.Session.IsInTransaction)
                    context.Session.RollbackTransaction();
                throw;
            }

            ConsoleHelper.WriteLineAndReadKey();
        }

    }
}
