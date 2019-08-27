using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChloeDemo
{
    public enum Gender
    {
        Man = 1,
        Woman
    }

    [TableAttribute("Users")]
    public class User
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrementAttribute]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public int? CityId { get; set; }
        public DateTime? OpTime { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
    }

    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
