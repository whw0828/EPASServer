using Chloe;
using Chloe.Core;
using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    //[TestClass]
    public class MsSqlDemo
    {
        static readonly int ID = 9999;

        public static void Test()
        {
            //QueryTest();
            //PredicateTest();
            //JoinQueryTest();
            //InsertTest();
            //UpdateTest();
            MsSqlDemo.MethodTest();

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void PredicateTest()
        {
            object ret = null;
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            var q = context.Query<User>();

            List<int> ids = new List<int>();
            ids.Add(1);
            ids.Add(2);
            ids.Add(2);

            string name = "lu";
            string nullString = null;
            //name = null;
            bool b = false;
            bool b1 = true;


            ret = q.Where(a => true).ToList();
            ret = q.Where(a => a.Id == MsSqlDemo.ID).ToList();
            ret = q.Where(a => a.Id == MsSqlDemo.ID || a.Id > 1).ToList();
            ret = q.Where(a => a.Id == 1 && a.Name == name && a.Name == null && a.Name == nullString && a.Id == MsSqlDemo.ID).ToList();
            ret = q.Where(a => ids.Contains(a.Id)).ToList();
            ret = q.Where(a => !b == (a.Id > 0)).ToList();

            ret = q.Where(a => a.Id > 0).Where(a => a.Id == 1).ToList();
            ret = q.Where(a => !(a.Id > 10)).ToList();
            ret = q.Where(a => !(a.Name == name)).ToList();
            ret = q.Where(a => a.Name != name).ToList();
            ret = q.Where(a => a.Name == name).ToList();

            ret = q.Where(a => (a.Name == name) == (a.Id > 0)).ToList();
            ret = q.Where(a => a.Name == (a.Name ?? name)).ToList();
            ret = q.Where(a => (a.Age == null ? 0 : 1) == 1).ToList();

            ret = q.Select(a => b & b1).ToList();
            ret = q.Select(a => a.Id & 1).ToList();
            ret = q.Select(a => new { Id = a.Id, And = a.Id & 1, And1 = a.Id & 1 & a.Id, Or = a.Id | 1, B = b & b1 }).ToList();
            var xxx = q.ToList().Select(a => new { Id = a.Id, And = a.Id & 1, And1 = a.Id & 1 & a.Id, Or = a.Id | 1 }).ToList();

            ret = q.Where(a => b & true).ToList();
            ret = q.Where(a => b | true).ToList();
            ret = q.Where(a => b || true).ToList();

            ret = q.Where(a => b & b1).ToList();
            ret = q.Where(a => b | b1).ToList();
            ret = q.Where(a => b || b1).ToList();

            Console.WriteLine(1);
            Console.ReadKey();
        }
        //[TestMethod]
        public static void QueryTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            context.PagingMode = PagingMode.OFFSET_FETCH;

            var q = context.Query<User>();

            object ret = null;
            q.Where(a => a.Id > 0).FirstOrDefault();
            ret = q.Where(a => a.Id > 0).ToList();
            ret = q.Where(a => a.Id > 0).OrderBy(a => a.Id).ToList();
            ret = q.Where(a => a.Id > 0).OrderBy(a => a.Id).ThenByDesc(a => a.Age).ToList();
            ret = q.Where(a => a.Id > 0).Skip(1).ToList();
            ret = q.Where(a => a.Id > 0).Take(999).ToList();
            ret = q.Where(a => a.Id > 0).Take(999).OrderBy(a => a.Age).ToList();
            ret = q.Where(a => a.Id > 0).Skip(1).Take(999).ToList();
            ret = q.Where(a => a.Id > 0).OrderBy(a => a.Id).ThenByDesc(a => a.Age).Skip(1).Take(999).ToList();
            ret = q.Where(a => a.Id > 0).OrderBy(a => a.Id).ThenByDesc(a => a.Age).Skip(1).Take(999).Where(a => a.Id > -100).ToList();

            ret = q.Select(a => new { Name1 = a.Name, Name2 = a.Name }).ToList();


            Console.WriteLine(1);
            Console.ReadKey();
        }
        public static void MethodTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            var q = context.Query<User>();

            var space = new char[] { ' ' };
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(1);
            var xxxx = q.Select(a => new
            {
                Id = a.Id,

                String_Length = a.Name.Length,
                Substring = a.Name.Substring(0),
                Substring1 = a.Name.Substring(1),
                Substring1_2 = a.Name.Substring(1, 2),
                ToLower = a.Name.ToLower(),
                ToUpper = a.Name.ToUpper(),
                IsNullOrEmpty = string.IsNullOrEmpty(a.Name),
                Contains = (bool?)a.Name.Contains("s"),
                Trim = a.Name.Trim(),
                TrimStart = a.Name.TrimStart(space),
                TrimEnd = a.Name.TrimEnd(space),
                StartsWith = (bool?)a.Name.StartsWith("s"),
                EndsWith = (bool?)a.Name.EndsWith("s"),

                DiffYears = DbFunctions.DiffYears(startTime, endTime),
                DiffMonths = DbFunctions.DiffMonths(startTime, endTime),
                DiffDays = DbFunctions.DiffDays(startTime, endTime),
                DiffHours = DbFunctions.DiffHours(startTime, endTime),
                DiffMinutes = DbFunctions.DiffMinutes(startTime, endTime),
                DiffSeconds = DbFunctions.DiffSeconds(startTime, endTime),
                DiffMilliseconds = DbFunctions.DiffMilliseconds(startTime, endTime),
                //DiffMicroseconds = DbFunctions.DiffMicroseconds(startTime, endTime),//Exception

                /* No longer support method 'DateTime.Subtract(DateTime d)', instead of using 'DbFunctions.DiffXX' */
                SubtractTotalDays = endTime.Subtract(startTime).TotalDays,
                SubtractTotalHours = endTime.Subtract(startTime).TotalHours,
                SubtractTotalMinutes = endTime.Subtract(startTime).TotalMinutes,
                SubtractTotalSeconds = endTime.Subtract(startTime).TotalSeconds,
                SubtractTotalMilliseconds = endTime.Subtract(startTime).TotalMilliseconds,

                AddYears = startTime.AddYears(1),//DATEADD(YEAR,1,@P_0)
                AddMonths = startTime.AddMonths(1),//DATEADD(MONTH,1,@P_0)
                AddDays = startTime.AddDays(1),//DATEADD(DAY,1,@P_0)
                AddHours = startTime.AddHours(1),//DATEADD(HOUR,1,@P_0)
                AddMinutes = startTime.AddMinutes(2),//DATEADD(MINUTE,2,@P_0)
                AddSeconds = startTime.AddSeconds(120),//DATEADD(SECOND,120,@P_0)
                AddMilliseconds = startTime.AddMilliseconds(20000),//DATEADD(MILLISECOND,20000,@P_0)

                Now = DateTime.Now,
                UtcNow = DateTime.UtcNow,
                Today = DateTime.Today,
                Date = DateTime.Now.Date,
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month,
                Day = DateTime.Now.Day,
                Hour = DateTime.Now.Hour,
                Minute = DateTime.Now.Minute,
                Second = DateTime.Now.Second,
                Millisecond = DateTime.Now.Millisecond,
                DayOfWeek = DateTime.Now.DayOfWeek,

                Int_Parse = int.Parse("1"),
                Int16_Parse = Int16.Parse("11"),
                Long_Parse = long.Parse("2"),
                Double_Parse = double.Parse("3"),
                Float_Parse = float.Parse("4"),
                //Decimal_Parse = decimal.Parse("5"),//'Decimal.Parse(string s)' is not supported now,because we don't know the precision and scale information.
                Guid_Parse = Guid.Parse("D544BC4C-739E-4CD3-A3D3-7BF803FCE179"),

                Bool_Parse = bool.Parse("1"),
                DateTime_Parse = DateTime.Parse("2014-1-1"),

                B = a.Age == null ? false : a.Age > 1,
            }).ToList();

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void SqlQueryTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            object ret = null;

            var users = context.SqlQuery<User>("select Id as id,Name as name,'asdsd' as Name,ByteArray from Users where Name=@name", DbParam.Create("@name", "lu1"));

            var list = users.ToList();

            ret = context.SqlQuery<int?>("select Id from Users").ToList();

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void CTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            IQuery<User> q = context.Query<User>();

            //var users = q.Select(a => new User() { U = a }).ToList();

            //var r = q.Select(a => new { Id = a.Id, Id1 = a.Id }).ToList();

            //var r1 = q.Where(a => a.Id > 0).OrderBy(a => a.Id).Skip(1).Take(100).ToList();

            //var r2 = q.Where(a => a.Id > 0).OrderBy(a => a.Id).Skip(1).Take(100).Select(a => new { a.Id, a.Name }).ToList();

            var r3 = q.Select(a => new { B = a.Id > 5 }).ToList();

            Console.WriteLine(1);
        }
        public static void MappingTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            IQuery<User> q = context.Query<User>();

            string n = "";
            Expression<Func<User, bool>> p = a => a.Name == n;

            //q.Where(a => a.Name == n);
            var r = q.ToList();

            Console.WriteLine(1);
        }
        public static void JoinQueryTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            IQuery<User> q = context.Query<User>();
            IQuery<User> q1 = context.Query<User>();
            IQuery<User> q2 = context.Query<User>();

            object ret = null;
            ret = q.InnerJoin(context.Query<User>(), (a, b) => a.Id == b.Id).Select((a, b) => new { A = a, B = b }).ToList();

            ret = q.InnerJoin(context.Query<User>().Select(a => new { a.Id, a.Name }), (a, b) => a.Id == b.Id).Select((a, b) => new { A = a, B = b }).ToList();

            ret = q.LeftJoin(context.Query<User>().Select(a => new { a.Id, a.Name }), (a, b) => a.Id == b.Id + 1).Select((a, b) => new { A = a, B = b }).ToList();

            ret = q.RightJoin(context.Query<User>().Where(a => a.Id <= 20).Select(a => new { a.Id, a.Name }), (a, b) => a.Id == b.Id + 1).Select((a, b) => new { A = a, B = b }).ToList();

            ret = q.InnerJoin(q1, (a, b) => a.Id == b.Id).InnerJoin(q2, (a, b, c) => a.Name == c.Name).Select((a, b, c) => new { A = a, B = b, C = c }).ToList();

            ret = q.InnerJoin(q1, (a, b) => a.Id == b.Id).LeftJoin(q2, (a, b, c) => a.Name == c.Name).RightJoin(q, (a, b, c, d) => a.Id == d.Id + 1).Select((a, b, c, d) => new { A = a, B = b, C = c, D = d }).ToList();

            ret = q.InnerJoin(q1, (a, b) => a.Id == b.Id).InnerJoin(q2, (a, b, c) => a.Name == c.Name).RightJoin(q, (a, b, c, d) => a.Id == d.Id + 1).Select((a, b, c, d) => new { A = a, D = d }).ToList();

            ret = q.InnerJoin(q1, (a, b) => a.Id == b.Id).InnerJoin(q2.Where(a => a.Id > 0).Select(a => a.Id), (a, b, c) => a.Id == c).RightJoin(q, (a, b, c, d) => a.Id == d.Id + 1).Select((a, b, c, d) => new { a, C = (int?)c }).ToList();

            ret = q.InnerJoin(q1, (a, b) => a.Id == b.Id).LeftJoin(q2.Where(a => a.Id > 0).Select(a => a.Id), (a, b, c) => a.Id == c).FullJoin(q, (a, b, c, d) => a.Id == d.Id + 1).Select((a, b, c, d) => new { a, C = (int?)c }).ToList();

            q.InnerJoin(q1, (a, b) => a.Id == b.Id).Select((a, b) => new { a, b }).Where(a => a.a.Id > 0).ToList();

            Console.WriteLine(1);
        }
        public static void GroupQueryTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            object ret = null;

            var q = context.Query<User>();

            var r = q.GroupBy(a => a.Id).Having(a => a.Id > 1).Select(a => new { a.Id, Count = AggregateFunctions.Count(), Sum = AggregateFunctions.Sum(a.Id), Max = AggregateFunctions.Max(a.Id), Min = AggregateFunctions.Min(a.Id), Avg = AggregateFunctions.Average(a.Id) }).ToList();

            q.GroupBy(a => a.Age).Having(a => a.Age > 1).Select(a => new { a.Age, Count = AggregateFunctions.Count(), Sum = AggregateFunctions.Sum(a.Age), Max = AggregateFunctions.Max(a.Age), Min = AggregateFunctions.Min(a.Age), Avg = AggregateFunctions.Average(a.Age) }).ToList();

            var r1 = q.GroupBy(a => a.Age).Having(a => AggregateFunctions.Count() > 0).Select(a => new { a.Age, Count = AggregateFunctions.Count(), Sum = AggregateFunctions.Sum(a.Age), Max = AggregateFunctions.Max(a.Age), Min = AggregateFunctions.Min(a.Age), Avg = AggregateFunctions.Average(a.Age) }).ToList();

            var g = q.GroupBy(a => a.Gender);
            //g = g.ThenBy(a => a.Name);
            //g = g.Having(a => a.Id > 0);
            //g = g.Having(a => a.Name.Length > 0);
            var gq = g.Select(a => new { Count = AggregateFunctions.Count() });

            //gq = gq.Skip(1);
            //gq = gq.Take(100);
            //gq = gq.Where(a => a > -1);

            ret = gq.ToList();
            var c = gq.Count();

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void AggregateFunctionTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            var q = context.Query<User>();
            q = q.Where(a => a.Id > 0);
            var xxx = q.Select(a => AggregateFunctions.Count()).First();
            q.Select(a => new { Count = AggregateFunctions.Count(), LongCount = AggregateFunctions.LongCount(), Sum = AggregateFunctions.Sum(a.Age), Max = AggregateFunctions.Max(a.Age), Min = AggregateFunctions.Min(a.Age), Average = AggregateFunctions.Average(a.Age) }).First();
            var count = q.Count();
            var longCount = q.LongCount();
            var sum = q.Sum(a => a.Age);
            var max = q.Max(a => a.Age);
            var min = q.Min(a => a.Age);
            var avg = q.Average(a => a.Age);

            Console.WriteLine(1);
        }

        public static void InsertTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            User user = new User();
            user.Id = 10;
            user.Name = "88888";
            user.Age = 21;
            user.Gender = Gender.Man;

            var id = context.Insert<User>(() => new User() { Name = user.Name, Age = user.Age, Gender = Gender.Man, OpTime = DateTime.Now });

            //var id = context.Insert<User>(() => new User() { Name = "lu", NickName = "so", Age = 18, Gender = Gender.Man, OpTime = DateTime.Now });

            //var users = context.Query<User>().Where(a => a.Name == null).ToList();

            //user = context.Query<User>().Where(a => a.Id == (int)id).First();

            user.OpTime = DateTime.Now;
            var user1 = context.Insert(user);

            context.Insert(new User() { Name = "lu", Age = 18, Gender = Gender.Man, OpTime = DateTime.Now });

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void UpdateTest()
        {
            List<string> names = new List<string>();
            names.Add("so");
            names.Add(null);

            string name = "lu1";
            string stringNull = null;
            int? intNull = null;
            DateTime? dateTimeNull = null;
            //name = null;

            int r = -1;

            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            r = context.Update<User>(a => new User() { Name = a.Name, Age = a.Age + 100, Gender = Gender.Man, OpTime = DateTime.Now }, a => a.Name == name);

            r = context.Update<User>(a => new User() { Name = stringNull, Age = intNull, Gender = null, OpTime = dateTimeNull }, a => false);

            User user = new User() { Id = 1, Name = "lu", Age = 18, Gender = Gender.Man };
            user.Id = 2;
            user.Name = "shuxin";
            user.Age = 28;
            user.Gender = Gender.Man;
            //user.OpTime = DateTime.Now;

            object o = user;
            r = context.Update(o);

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void TrackingTest()
        {
            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);

            object ret = null;

            var q = context.Query<User>();
            q = q.AsTracking();

            User user = q.First();
            ret = context.Update(user);

            Console.WriteLine(ret);

            context.TrackEntity(user);
            user.Name = user.Name + "1";
            user.Age = user.Age;
            user.Gender = null;
            ret = context.Update(user);

            Console.WriteLine(ret);

            ret = context.Update(user);
            Console.WriteLine(ret);

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void DeleteTest()
        {
            string name = "so2";
            //name = null;

            MsSqlContext context = new MsSqlContext(DbHelper.ConnectionString);
            int r = -1;
            int? age = null;

            //r = context.Delete<User>(a => a.Gender == Gender.Man);
            r = context.Delete<User>(a => a.Age == r);
            //r = context.Delete<User>(a => a.Gender == null);
            //r = context.Delete<User>(a => a.Age == age);
            //r = context.Delete<User>(a => age == a.Age);

            User user = new User();
            user.Id = 6;
            r = context.Delete(user);
            context.Delete(new User() { Id = 1 });
            Console.WriteLine(1);
        }
    }
}
