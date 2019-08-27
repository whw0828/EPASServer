using EPAS.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.License
{
    class EPASLicense
    {
        /// <summary>
        /// 配置数据库连接信息
        /// </summary>
        /// <param name="connectStr"></param>
        public static void SetLicenseConnectStr(string connectStr)
        {
            DbConnectionHelper.LicenseConnectStr = connectStr;
        }
    }
}
