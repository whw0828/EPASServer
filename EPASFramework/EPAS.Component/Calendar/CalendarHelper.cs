using EPAS.Component.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Component.Calendar
{

    public class APSCalendar
    {

        /// <summary>
        /// 年度
        /// </summary>
        [DisplayName("年度")]
        public int dtYear
        {
            get;
            set;
        }
        /// <summary>
        /// 月
        /// </summary>
        [DisplayName("月")]
        public int dtMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 日
        /// </summary>
        [DisplayName("日")]
        public int dtDay
        {
            get;
            set;
        }

        /// <summary>
        /// 星期一 - 星期天
        /// </summary>
        [DisplayName("星期")]
        public int iDayOfWeek
        {
            get;
            set;
        }

        /// <summary>
        /// 日期
        /// </summary>
        [DisplayName("日期")]
        public DateTime dt
        {
            get;
            set;
        }

        /// <summary>
        /// 作业日期
        /// </summary>
        [DisplayName("作业日期")]
        public bool IsRest
        {
            get;
            set;
        }

        /// <summary>
        /// 一天作业合计秒
        /// </summary>
        [DisplayName("一天作业合计秒")]
        public int DayWorkSeconds
        {
            get;
            set;
        }


        /// <summary>
        /// 日出勤时间段
        /// </summary>
        [DisplayName("日出勤时间段")]
        public List<APSAttendanceSlot> ascList
        {
            get;
            set;
        }

       

    }

    //出勤时间

    public class APSAttendanceTime
    {

        /// <summary>
        /// 小时
        /// </summary>
        [DisplayName("小时")]
        public int hour
        {
            get;
            set;
        }
        /// <summary>
        /// 分钟
        /// </summary>
        [DisplayName("分钟")]
        public int minute
        {
            get;
            set;
        }
        /// <summary>
        /// 秒
        /// </summary>
        [DisplayName("秒")]
        public int second
        {
            get;
            set;
        }     

    }


    public class APSAttendanceSlot
    {

        /// <summary>
        /// 开始时刻
        /// </summary>
        [DisplayName("开始时刻")]
        public APSAttendanceTime startSlot
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时刻
        /// </summary>
        [DisplayName("结束时刻")]
        public APSAttendanceTime endSlot
        {
            get;
            set;
        }

        /// <summary>
        /// 时间段合计秒
        /// </summary>
        [DisplayName("时间段合计秒")]
        public int SlotSeconds
        {
            get;
            set;
        }

        /// <summary>
        /// 是否跨天
        /// </summary>
        [DisplayName("是否跨天")]
        public bool IsCrossDay
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 生产日历计算
    /// </summary>
    public class CalendarHelper
    {
        public static string ALLResourceCode = SplitHelper.SplitStar.ToString();//star
        public static string ALLResourceName = SplitHelper.SplitStar.ToString();


        /// <summary>
        /// 计算两个时刻之间的时间差 返回值单位：秒
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int TimeSlotDiff(APSAttendanceTime start, APSAttendanceTime end)
        {
            int totalSecond = 0;
            int dayHours = 24;//一天24小时
            if(end.hour==0)
            {
                end.hour = dayHours;
            }

            //8:00-12:00;13:00-17:00情况 不跨天
            if(start.hour<=end.hour)
            {
                totalSecond = (end.hour - start.hour) * 3600 + (end.minute - start.minute) * 60;//单位秒
            }
            //20:00-8:00
            if (start.hour > end.hour)
            {
                totalSecond = (end.hour+ dayHours + - start.hour) * 3600 + (end.minute - start.minute) * 60;//单位秒
            }

            return totalSecond;
        }

        /// <summary>
        /// 出勤模式换算成时间
        /// </summary>
        /// <param name="mode">8:00-12:00;13:00-17:00</param>
        /// <returns></returns>
        public static List<APSAttendanceSlot> AttendanceModeTransformTime(string mode)
        {
            List<APSAttendanceSlot> itemList = new List<APSAttendanceSlot>();

            string[] lines = mode.Split(SplitHelper.SplitSemicolon);

            string[] TimeSlots;
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    TimeSlots=line.Split(SplitHelper.SplitHyphen);//8:00  12:00

                    if (TimeSlots!=null)
                    {
                        APSAttendanceSlot item = new APSAttendanceSlot();

                        //TimeSlots  中索引含义 0-小时 1-分钟
                        //开始时刻 TimeSlots[0]代表8:00
                        if (!string.IsNullOrWhiteSpace(TimeSlots[0]))
                        {
                            string[] slot = TimeSlots[0].Split(SplitHelper.SplitColon);//8 00

                            APSAttendanceTime start = new APSAttendanceTime();

                            start.hour = int.Parse(slot[0]);
                            start.minute = int.Parse(slot[1]);
        
                            item.startSlot = start;
                        }

                        //结束时刻 TimeSlots[1]代表 12:00
                        if (!string.IsNullOrWhiteSpace(TimeSlots[1]))
                        {
                            string[] slot = TimeSlots[1].Split(SplitHelper.SplitColon);//12 00

                            APSAttendanceTime end = new APSAttendanceTime();

                            end.hour = int.Parse(slot[0]);
                            end.minute = int.Parse(slot[1]);

                            item.endSlot = end;

                            item.SlotSeconds = TimeSlotDiff(item.startSlot, item.endSlot);
                        }

                        itemList.Add(item);
                    }
                }

            }

            return itemList;

        }

        /// <summary>
        /// 判断当前日期是否工作时间，精确到秒
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="wdList"></param>
        /// <returns></returns>
        public static bool IsWorkTime(DateTime dt, List<APSCalendar> wdList)
        {
            bool result = false;
           
            //首先判断是否工作日
            APSCalendar item = wdList.Find(x => x.dtYear == dt.Year && x.dtMonth == dt.Month && x.dtDay == dt.Day);
            if (item != null)
            {
                List<APSAttendanceSlot> acsList = item.ascList;

                //判断是否工作时间
                foreach (var acs in acsList)
                {
                    if(acs.startSlot.hour<acs.endSlot.hour)
                    {
                        //非跨天
                        if (acs.startSlot.hour <= dt.Hour &&  dt.Hour< acs.endSlot.hour)
                        {
                            result = true;
                        }
                    }

                    else if (acs.startSlot.hour >= acs.endSlot.hour)
                    {
                        //跨天
                        //前半夜
                        if (acs.startSlot.hour < dt.Hour && dt.Hour<=24)
                        {

                            result = true;
                        }
                        //后半夜
                        if (0 < dt.Hour && dt.Hour < acs.endSlot.hour)
                        {

                            result = true;
                        }
                    }


                }
            }
            
            return result;
        }
        /// <summary>
        /// 获取最终APS时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="span"></param>
        /// <param name="workList"></param>
        /// <param name="Forward"></param>
        /// <returns></returns>
        public static DateTime GetAPSWorkTime(DateTime dateTime, decimal span, List<APSCalendar> workList,bool Forward=true)
        {
            DateTime dt = DateTime.Parse(dateTime.ToString());
            int step = -1;//从当前时间向前推移

            if(!Forward)
            {
                step = 1;//从当前时间向后推移
            }
            int totalStep = 0;
            while(true)
            {
                dt = dt.AddSeconds(step);
      
                if(IsWorkTime(dt, workList))
                {
                    totalStep++;
                }             
                if(totalStep>=span)
                {
                    break;
                }
            }

            return dt;
        }
     
        /// <summary>
        /// 返回一年的可以作业的时间
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="WorkWeekValue">工作星期</param>
        /// <param name="NotWorkDates">节假日</param>
        /// <param name="overtime">加班日期</param>
        /// <returns></returns>
        public static List<APSCalendar> CalendarTransformWorkDate(int year, string WorkWeekValue,string mode,string NotWorkDates="",string overtime ="")
        {
            List<APSCalendar> itemList = new List<APSCalendar>();

            // WorkWeekValue 格式 1;2;3;4;5;6;  表示星期一、星期二、星期三、星期五、星期六上班

            string[] lines = WorkWeekValue.Split(SplitHelper.SplitSemicolon);

            List<int> weeks = new List<int>();
            foreach (string line in lines)
            {
                if(!string.IsNullOrWhiteSpace(line))
                {
                    weeks.Add(int.Parse(line));
                }
                
            }

            List<APSCalendar> dtNotWorkDates = new List<APSCalendar>();//节假日期

            List<APSCalendar> dtOverDates = new List<APSCalendar>();//加班日期

            #region 节假日
            if (!string.IsNullOrWhiteSpace(NotWorkDates))
            {
                string[] lineNotWorkDates = NotWorkDates.Split(SplitHelper.SplitSemicolon);

                foreach (string line in lineNotWorkDates)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    DateTime dt = DateTime.Parse(line);
                    int dayofWeek = (int)dt.DayOfWeek;
                    APSCalendar item = new APSCalendar();
                    item.dt = dt;
                    item.dtYear = dt.Year;
                    item.dtMonth = dt.Month;
                    item.dtDay = dt.Day;
                    item.iDayOfWeek = dayofWeek;
                    item.IsRest = true;
                    dtNotWorkDates.Add(item);
                }
            }
            #endregion

            #region 加班日期
            if (!string.IsNullOrWhiteSpace(overtime))
            {
                string[] lineovertime = overtime.Split(SplitHelper.SplitSemicolon);

                foreach (string line in lineovertime)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    DateTime dt = DateTime.Parse(line);
                    int dayofWeek = (int)dt.DayOfWeek;
                    APSCalendar item = new APSCalendar();
                    item.dt = dt;
                    item.dtYear = dt.Year;
                    item.dtMonth = dt.Month;
                    item.dtDay = dt.Day;
                    item.iDayOfWeek = dayofWeek;
                    item.IsRest = false;
                    dtOverDates.Add(item);
                }
            }
            #endregion


            for (int month=1;month<=12;month++)
            {
                int monthDays = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= monthDays;day++)
                {

                    DateTime dt = new DateTime(year, month,day);


                   
                    int dayofWeek = (int)dt.DayOfWeek;
                    if(weeks.Contains(dayofWeek))//去掉工厂允许的正常休息时间(例如星期六 星期天)
                    {
                        if(dtNotWorkDates.Find(x=>x.dtYear==dt.Year && x.dtMonth==dt.Month && x.dtDay==dt.Day)!=null)
                        {
                            //日期是节假日 无法作业（去掉工厂执行的国家法定节假日）
                            continue;
                        }
                        APSCalendar item = new APSCalendar();
                        item.dt = dt;
                        item.dtYear = year;
                        item.dtMonth = month;
                        item.dtDay = day;
                        item.iDayOfWeek = dayofWeek;
                        item.IsRest = false;
                        itemList.Add(item);
                    }                
                    
                }
            }

            if(dtOverDates.Count>0)
            {
                //补录加班时间
                foreach(var item in dtOverDates)
                {
                    DateTime dt = item.dt;
                    if (itemList.Find(x => x.dtYear == dt.Year && x.dtMonth == dt.Month && x.dtDay == dt.Day) == null)
                    {
                        itemList.Add(item);
                    }
                }
           
            }

            #region 追加日作业中时间段
            List<APSAttendanceSlot>  ascList=AttendanceModeTransformTime(mode);
            if(ascList != null && ascList.Count>0)
            {
                foreach (var itemNew in itemList)
                {
                    itemNew.DayWorkSeconds = ascList.Sum(x => x.SlotSeconds);

                    itemNew.ascList = ascList;
                }
            }
           
            #endregion

            return itemList;
        }

        /// <summary>
        /// 返回一年的非作业的时间
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="WorkWeekValue">工作星期</param>
        /// <param name="NotWorkDates">节假日</param>
        /// <param name="overtime">加班日期</param>
        /// <returns></returns>
        public static List<APSCalendar> CalendarTransformRestDate(int year, string WorkWeekValue, string NotWorkDates = "", string overtime = "")
        {
            List<APSCalendar> itemRestList = new List<APSCalendar>();//非作业时间
            List<APSCalendar> itemList = CalendarTransformWorkDate(year, WorkWeekValue, NotWorkDates, overtime);//获取一年内的作业时间

           
            for (int month = 1; month <= 12; month++)
            {
                int monthDays = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= monthDays; day++)
                {

                    DateTime dt = new DateTime(year, month, day);

                    int dayofWeek = (int)dt.DayOfWeek;
                    if (itemList.Find(x => x.dtYear == dt.Year && x.dtMonth == dt.Month && x.dtDay == dt.Day) == null)
                    {
                        //作业时间以外的日期 认为是休息时间
                        APSCalendar item = new APSCalendar();
                        item.dt = dt;
                        item.dtYear = year;
                        item.dtMonth = month;
                        item.dtDay = day;
                        item.iDayOfWeek = dayofWeek;
                        item.IsRest = true;
                        itemRestList.Add(item);
                    }

                }
            }

            return itemRestList;
        }
    }
}
