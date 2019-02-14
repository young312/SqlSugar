using Edna.Configuration;
using Edna.Extension.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.Service.IServiceProvider
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/27 14:58:41
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************
 //----------------------------------------------------------------*/
#endregion
namespace Edna.Service.IServiceProvider
{
    public interface ISystemService : IService
    {
        Task<Object> Search();
        Task<AdminRoleViewModel> Login();
        Task<Object> BatchDel();
        Task<Object> RecoveryData();
    }
}
