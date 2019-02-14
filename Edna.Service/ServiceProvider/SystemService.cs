using Edna.EntityCore;
using Edna.EntityCore.Model.SystemModel;
using Edna.Extension.Caches;
using Edna.Extension.ModelMapper;
using Edna.Extension.ViewModel;
using Edna.Service.IServiceProvider;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 类 描 述 ：
* 命名空间 ：Edna.Service.ServiceProvider
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Emily
* 创建时间 ：2018/11/27 14:58:55
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Emily 2018. All rights reserved.
*******************************************************************/
#endregion
namespace Edna.Service.ServiceProvider
{
    public class SystemService : SugerDbContext, ISystemService
    {
        public async Task<Object> Search()
        {
            return await Emily.Queryable<Administrator>().Where(t=>t.IsDelete==false).FirstAsync();
        }
        public async Task<AdminRoleViewModel> Login()
         {
            AdminRoleViewModel AdminRole = Emily.Queryable<Administrator, RolePermission>((t, x) => new Object[] { JoinType.Left, t.RolePermissionId == x.PrimaryId })
                .Where(t => t.Account.Equals("admin"))
                .Where(t => t.PassWord.Equals("admin"))
                .Select<AdminRoleViewModel>().First();
            await CacheFacoty.WriteCache<AdminRoleViewModel>(AdminRole, AdminRole.GetType().FullName, 1);
            return AdminRole;
        }
        public async Task<Object> BatchDel()
        {
            List<Administrator> administrator = Emily.Queryable<Administrator>().Where(t => t.IsDelete == false).ToList();
            administrator.ForEach(t => {
                t.DeleteTime =DateTime.Now;
                t.DeleteUser = "测试";
                t.DeleteUserId = Guid.NewGuid();
                t.IsDelete = true;
            });
            return await base.SoftDeletion<Administrator>(administrator);
        }
        public async Task<Object> RecoveryData()
        {
            List<Administrator> administrator = Emily.Queryable<Administrator>().Where(t => t.IsDelete == true).ToList();
            administrator.ForEach(t => {
                t.DeleteTime = null;
                t.DeleteUser = null;
                t.DeleteUserId = null;
                t.IsDelete = false;
            });
            return await base.AlterData<Administrator>(administrator);
           
        }
    }
}
