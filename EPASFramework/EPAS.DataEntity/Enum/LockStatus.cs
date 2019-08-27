using EPAS.DataEntity.Entity.Common;
using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //锁定状态



    public enum LockStatus
    {

        [DescriptionAttribute("zh-CN,锁定;en-US,Lock;")]
        Lock = 1,
        [DescriptionAttribute("zh-CN,正常;en-US,Normal;")]
        UnLock = 0
        
    }


}
