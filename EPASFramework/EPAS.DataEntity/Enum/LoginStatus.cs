using System.ComponentModel;

namespace EPAS.DataEntity.Enum
{

    //登录状态
    public enum LoginStatus
    {
     
        [DescriptionAttribute("zh-CN,用户不能为空!;en-US,Users are not allowed to be empty!;")]
        UserIsNull = 1,
        [DescriptionAttribute("zh-CN,密码不能为空!;en-US,Password are not allowed to be empty!;")]
        PasswordIsNull = 2,
        [DescriptionAttribute("zh-CN,用户不存在!;en-US,User does not exist!;")]
        UserNotExist = 3,
        [DescriptionAttribute("zh-CN,密码错误!;en-US,Password error!;")]
        PasswordError = 4,
        [DescriptionAttribute("zh-CN,登录成功!;en-US,Success!;")]
        Sucess = 5,
        [DescriptionAttribute("zh-CN,登录失败!;en-US,Logon failure!;")]
        Failed = 6,
    }
}
