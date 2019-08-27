using Chloe;
using Chloe.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChloeDemo
{
    class MySqlDemo
    {
        static MySqlContext context = new MySqlContext(new MySqlConnectionFactory("Database='Chloe';Data Source=localhost;User ID=root;Password=sasa;CharSet=utf8;SslMode=None"));
        public static void Test()
        {
            BasicQuery();
            //JoinQuery();
            //AggregateQuery();
            //GroupQuery();
            //Insert();
            //Update();
            //Delete();
            //Method();

            ConsoleHelper.WriteLineAndReadKey();
        }


        public static void BasicQuery()
        {
            IQuery<User> q = context.Query<User>();

            q.Where(a => a.Id == 1).FirstOrDefault();
            /*
             * SELECT `Users`.`Id` AS `Id`,`Users`.`Name` AS `Name`,`Users`.`Gender` AS `Gender`,`Users`.`Age` AS `Age`,`Users`.`CityId` AS `CityId`,`Users`.`OpTime` AS `OpTime` FROM `Users` AS `Users` WHERE `Users`.`Id` = 1 LIMIT 0,1
             */

            //可以选取指定的字段
            q.Where(a => a.Id == 1).Select(a => new { a.Id, a.Name }).FirstOrDefault();
            /*
             * SELECT `Users`.`Id` AS `Id`,`Users`.`Name` AS `Name` FROM `Users` AS `Users` WHERE `Users`.`Id` = 1 LIMIT 0,1
             */

            //分页
            q.Where(a => a.Id > 0).OrderBy(a => a.Age).Skip(1).Take(999).ToList();
            /*
             * SELECT `Users`.`Id` AS `Id`,`Users`.`Name` AS `Name`,`Users`.`Gender` AS `Gender`,`Users`.`Age` AS `Age`,`Users`.`CityId` AS `CityId`,`Users`.`OpTime` AS `OpTime` FROM `Users` AS `Users` WHERE `Users`.`Id` > 0 ORDER BY `Users`.`Age` ASC LIMIT 1,999
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void JoinQuery()
        {
            IQuery<User> users = context.Query<User>();
            IQuery<City> cities = context.Query<City>();
            IQuery<Province> provinces = context.Query<Province>();

            //建立连接
            IJoiningQuery<User, City> user_city = users.InnerJoin(cities, (user, city) => user.CityId == city.Id);
            IJoiningQuery<User, City, Province> user_city_province = user_city.InnerJoin(provinces, (user, city, province) => city.ProvinceId == province.Id);

            //查出一个用户及其隶属的城市和省份的所有信息
            var view = user_city_province.Select((user, city, province) => new { User = user, City = city, Province = province }).Where(a => a.User.Id == 1).ToList();
            /*
             * SELECT `Users`.`Id` AS `Id`,`Users`.`Name` AS `Name`,`Users`.`Gender` AS `Gender`,`Users`.`Age` AS `Age`,`Users`.`CityId` AS `CityId`,`Users`.`OpTime` AS `OpTime`,`City`.`Id` AS `Id0`,`City`.`Name` AS `Name0`,`City`.`ProvinceId` AS `ProvinceId`,`Province`.`Id` AS `Id1`,`Province`.`Name` AS `Name1` FROM `Users` AS `Users` INNER JOIN `City` AS `City` ON `Users`.`CityId` = `City`.`Id` INNER JOIN `Province` AS `Province` ON `City`.`ProvinceId` = `Province`.`Id` WHERE `Users`.`Id` = 1
             */

            //也可以只获取指定的字段信息：UserId,UserName,CityName,ProvinceName
            user_city_province.Select((user, city, province) => new { UserId = user.Id, UserName = user.Name, CityName = city.Name, ProvinceName = province.Name }).Where(a => a.UserId == 1).ToList();
            /*
             * SELECT `Users`.`Id` AS `UserId`,`Users`.`Name` AS `UserName`,`City`.`Name` AS `CityName`,`Province`.`Name` AS `ProvinceName` FROM `Users` AS `Users` INNER JOIN `City` AS `City` ON `Users`.`CityId` = `City`.`Id` INNER JOIN `Province` AS `Province` ON `City`.`ProvinceId` = `Province`.`Id` WHERE `Users`.`Id` = 1
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void AggregateQuery()
        {
            IQuery<User> q = context.Query<User>();

            q.Select(a => AggregateFunctions.Count()).First();
            /*
             * SELECT COUNT(1) AS `C` FROM `Users` AS `Users` LIMIT 0,1
             */

            q.Select(a => new { Count = AggregateFunctions.Count(), LongCount = AggregateFunctions.LongCount(), Sum = AggregateFunctions.Sum(a.Age), Max = AggregateFunctions.Max(a.Age), Min = AggregateFunctions.Min(a.Age), Average = AggregateFunctions.Average(a.Age) }).First();
            /*
             * SELECT COUNT(1) AS `Count`,COUNT(1) AS `LongCount`,CAST(SUM(`Users`.`Age`) AS SIGNED) AS `Sum`,MAX(`Users`.`Age`) AS `Max`,MIN(`Users`.`Age`) AS `Min`,AVG(`Users`.`Age`) AS `Average` FROM `Users` AS `Users` LIMIT 0,1
             */

            var count = q.Count();
            /*
             * SELECT COUNT(1) AS `C` FROM `Users` AS `Users`
             */

            var longCount = q.LongCount();
            /*
             * SELECT COUNT(1) AS `C` FROM `Users` AS `Users`
             */

            var sum = q.Sum(a => a.Age);
            /*
             * SELECT CAST(SUM(`Users`.`Age`) AS SIGNED) AS `C` FROM `Users` AS `Users`
             */

            var max = q.Max(a => a.Age);
            /*
             * SELECT MAX(`Users`.`Age`) AS `C` FROM `Users` AS `Users`
             */

            var min = q.Min(a => a.Age);
            /*
             * SELECT MIN(`Users`.`Age`) AS `C` FROM `Users` AS `Users`
             */

            var avg = q.Average(a => a.Age);
            /*
             * SELECT AVG(`Users`.`Age`) AS `C` FROM `Users` AS `Users`
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void GroupQuery()
        {
            IQuery<User> q = context.Query<User>();

            IGroupingQuery<User> g = q.Where(a => a.Id > 0).GroupBy(a => a.Age);

            g = g.Having(a => a.Age > 1 && AggregateFunctions.Count() > 0);

            g.Select(a => new { a.Age, Count = AggregateFunctions.Count(), Sum = AggregateFunctions.Sum(a.Age), Max = AggregateFunctions.Max(a.Age), Min = AggregateFunctions.Min(a.Age), Avg = AggregateFunctions.Average(a.Age) }).ToList();
            /*
             * SELECT `Users`.`Age` AS `Age`,COUNT(1) AS `Count`,CAST(SUM(`Users`.`Age`) AS SIGNED) AS `Sum`,MAX(`Users`.`Age`) AS `Max`,MIN(`Users`.`Age`) AS `Min`,AVG(`Users`.`Age`) AS `Avg` FROM `Users` AS `Users` WHERE `Users`.`Id` > 0 GROUP BY `Users`.`Age` HAVING (`Users`.`Age` > 1 AND COUNT(1) > 0)
             */

            ConsoleHelper.WriteLineAndReadKey();
        }

        public static void Insert()
        {
            //返回主键 Id
            int id = (int)context.Insert<User>(() => new User() { Name = "lu", Age = 18, Gender = Gender.Man, CityId = 1, OpTime = DateTime.Now });
            /*
             * INSERT INTO `Users`(`Name`,`Age`,`Gender`,`CityId`,`OpTime`) VALUES(N'lu',18,1,1,NOW());SELECT @@IDENTITY
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
             * String ?P_0 = 'lu';
               Gender ?P_1 = Man;
               Int32 ?P_2 = 18;
               Int32 ?P_3 = 1;
               DateTime ?P_4 = '2016/8/26 18:11:26';
               INSERT INTO `Users`(`Name`,`Gender`,`Age`,`CityId`,`OpTime`) VALUES(?P_0,?P_1,?P_2,?P_3,?P_4);SELECT @@IDENTITY
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void Update()
        {
            context.Update<User>(a => a.Id == 1, a => new User() { Name = a.Name, Age = a.Age + 100, Gender = Gender.Man, OpTime = DateTime.Now });
            /*
             * UPDATE `Users` SET `Name`=`Users`.`Name`,`Age`=(`Users`.`Age` + 100),`Gender`=1,`OpTime`=NOW() WHERE `Users`.`Id` = 1
             */

            //批量更新
            //给所有女性年轻 10 岁
            context.Update<User>(a => a.Gender == Gender.Woman, a => new User() { Age = a.Age - 10, OpTime = DateTime.Now });
            /*
             * UPDATE `Users` SET `Age`=(`Users`.`Age` - 10),`OpTime`=NOW() WHERE `Users`.`Gender` = 2
             */

            User user = new User();
            user.Id = 1;
            user.Name = "lu";
            user.Age = 28;
            user.Gender = Gender.Man;
            user.OpTime = DateTime.Now;

            context.Update(user); //会更新所有映射的字段
            /*
             * String ?P_0 = 'lu';
               Gender ?P_1 = Man;
               Int32 ?P_2 = 28;
               Nullable<Int32> ?P_3 = NULL;
               DateTime ?P_4 = '2016/8/26 18:18:36';
               Int32 ?P_5 = 1;
               UPDATE `Users` SET `Name`=?P_0,`Gender`=?P_1,`Age`=?P_2,`CityId`=?P_3,`OpTime`=?P_4 WHERE `Users`.`Id` = ?P_5
             */


            /*
             * 支持只更新属性值已变的属性
             */

            context.TrackEntity(user);//在上下文中跟踪实体
            user.Name = user.Name + "1";
            context.Update(user);//这时只会更新被修改的字段
            /*
             * String ?P_0 = 'lu1';
               Int32 ?P_1 = 1;
               UPDATE `Users` SET `Name`=?P_0 WHERE `Users`.`Id` = ?P_1
             */

            ConsoleHelper.WriteLineAndReadKey();
        }
        public static void Delete()
        {
            context.Delete<User>(a => a.Id == 1);
            /*
             * DELETE `Users` FROM `Users` WHERE `Users`.`Id` = 1
             */

            //批量删除
            //删除所有不男不女的用户
            context.Delete<User>(a => a.Gender == null);
            /*
             * DELETE `Users` FROM `Users` WHERE `Users`.`Gender` IS NULL
             */

            User user = new User();
            user.Id = 1;
            context.Delete(user);
            /*
             * Int32 @P_0 = 1;
               DELETE `Users` FROM `Users` WHERE `Users`.`Id` = ?P_0
             */

            ConsoleHelper.WriteLineAndReadKey(1);
        }

        public static void Method()
        {
            IQuery<User> q = context.Query<User>();

            var space = new char[] { ' ' };

            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddDays(1);

            var ret = q.Select(a => new
            {
                Id = a.Id,

                String_Length = (int?)a.Name.Length,//LENGTH(`Users`.`Name`)
                Substring = a.Name.Substring(0),//SUBSTRING(`Users`.`Name`,0 + 1,LENGTH(`Users`.`Name`))
                Substring1 = a.Name.Substring(1),//SUBSTRING(`Users`.`Name`,1 + 1,LENGTH(`Users`.`Name`))
                Substring1_2 = a.Name.Substring(1, 2),//SUBSTRING(`Users`.`Name`,1 + 1,2)
                ToLower = a.Name.ToLower(),//LOWER(`Users`.`Name`)
                ToUpper = a.Name.ToUpper(),//UPPER(`Users`.`Name`)
                IsNullOrEmpty = string.IsNullOrEmpty(a.Name),//CASE WHEN (`Users`.`Name` IS NULL OR `Users`.`Name` = N'') THEN 1 ELSE 0 END = 1
                Contains = (bool?)a.Name.Contains("s"),//`Users`.`Name` LIKE CONCAT('%',N's','%')
                Trim = a.Name.Trim(),//TRIM(`Users`.`Name`)
                TrimStart = a.Name.TrimStart(space),//LTRIM(`Users`.`Name`)
                TrimEnd = a.Name.TrimEnd(space),//RTRIM(`Users`.`Name`)
                StartsWith = (bool?)a.Name.StartsWith("s"),//`Users`.`Name` LIKE CONCAT(N's','%')
                EndsWith = (bool?)a.Name.EndsWith("s"),//`Users`.`Name` LIKE CONCAT('%',N's')

                DiffYears = DbFunctions.DiffYears(startTime, endTime),//TIMESTAMPDIFF(YEAR,?P_0,?P_1)
                DiffMonths = DbFunctions.DiffMonths(startTime, endTime),//TIMESTAMPDIFF(MONTH,?P_0,?P_1)
                DiffDays = DbFunctions.DiffDays(startTime, endTime),//TIMESTAMPDIFF(DAY,?P_0,?P_1)
                DiffHours = DbFunctions.DiffHours(startTime, endTime),//TIMESTAMPDIFF(HOUR,?P_0,?P_1)
                DiffMinutes = DbFunctions.DiffMinutes(startTime, endTime),//TIMESTAMPDIFF(MINUTE,?P_0,?P_1)
                DiffSeconds = DbFunctions.DiffSeconds(startTime, endTime),//TIMESTAMPDIFF(SECOND,?P_0,?P_1)
                //DiffMilliseconds = DbFunctions.DiffMilliseconds(startTime, endTime),//MySql 不支持 Millisecond
                //DiffMicroseconds = DbFunctions.DiffMicroseconds(startTime, endTime),//ex

                Now = DateTime.Now,//NOW()
                UtcNow = DateTime.UtcNow,//UTC_TIMESTAMP()
                Today = DateTime.Today,//CURDATE()
                Date = DateTime.Now.Date,//DATE(NOW())
                Year = DateTime.Now.Year,//YEAR(NOW())
                Month = DateTime.Now.Month,//MONTH(NOW())
                Day = DateTime.Now.Day,//DAY(NOW())
                Hour = DateTime.Now.Hour,//HOUR(NOW())
                Minute = DateTime.Now.Minute,//MINUTE(NOW())
                Second = DateTime.Now.Second,//SECOND(NOW())
                Millisecond = DateTime.Now.Millisecond,//?P_2 AS `Millisecond`
                DayOfWeek = DateTime.Now.DayOfWeek,//(DAYOFWEEK(NOW()) - 1)

                //Byte_Parse = byte.Parse("1"),//不支持
                Int_Parse = int.Parse("1"),//CAST(N'1' AS SIGNED)
                Int16_Parse = Int16.Parse("11"),//CAST(N'11' AS SIGNED)
                Long_Parse = long.Parse("2"),//CAST(N'2' AS SIGNED)
                //Double_Parse = double.Parse("3"),//N'3' 不支持，否则可能会成为BUG
                //Float_Parse = float.Parse("4"),//N'4' 不支持，否则可能会成为BUG
                //Decimal_Parse = decimal.Parse("5"),//不支持
                Guid_Parse = Guid.Parse("D544BC4C-739E-4CD3-A3D3-7BF803FCE179"),//N'D544BC4C-739E-4CD3-A3D3-7BF803FCE179'

                Bool_Parse = bool.Parse("1"),//CAST(N'1' AS SIGNED)
                DateTime_Parse = DateTime.Parse("2014-1-1"),//CAST(N'2014-1-1' AS DATETIME)
            }).ToList();

            ConsoleHelper.WriteLineAndReadKey();
        }
    }
}
