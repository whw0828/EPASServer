using EPAS.DataEntity.Entity.Common;
using EPAS.Interface.PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.PluginManager
{
    /// <summary>
    /// 使用单例（单态）设计模式  数据插件管理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PluginManager<T>
    {

        private static readonly PluginManager<T> instance = new PluginManager<T>();
        private static Dictionary<DataPluginName, IDataPlugin<T>> dpMap = new Dictionary<DataPluginName, IDataPlugin<T>>();

        private static DataPluginName currentPluginName = DataPluginName.DefaultDataPlugin;

        static PluginManager()
        {

        }
        private  PluginManager()
        {
            
        }

        public static PluginManager<T> Instance
        {
            get
            {
                return instance;
            }
        }


        public  void AddPlugin(DataPluginName pluginName,IDataPlugin<T> dp)
        {
            currentPluginName = pluginName;
            if (!dpMap.ContainsKey(currentPluginName))
            {
                dpMap.Add(pluginName, dp);
            }
               
        }

        public  void SetCurrentDataPlugin(DataPluginName pluginName)
        {
            currentPluginName= pluginName;
        }

        public  IDataPlugin<T> GetCurrentDataPlugin()
        {
            if(dpMap.Count>0)
            {
                if(dpMap.ContainsKey(currentPluginName))
                {
                    IDataPlugin<T> dp = dpMap[currentPluginName];
                    return dp;
                }              
            }

            return null;
        }

        public IDataPlugin<T> GetDataPlugin(DataPluginName pluginName)
        {
            if (dpMap.Count > 0)
            {
                if (dpMap.ContainsKey(pluginName))
                {
                    IDataPlugin<T> dp = dpMap[pluginName];
                    return dp;
                }
            }

            return null;
        }



    }
}
