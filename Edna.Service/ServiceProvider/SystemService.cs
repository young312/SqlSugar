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
        public async Task<Object> InsertTest()
        {
            AdminRoleViewModel ViewModel = new AdminRoleViewModel
            {
                Account = "Admin",
                PassWord = "Admin"
            };
            Administrator Admin = ViewModel.AutoMapper<Administrator>();
            List<Administrator> LA = new List<Administrator> { Admin };
            return await base.InsertData<Administrator>(LA);
        }
        public async Task<Object> SearchTest()
        {
            return await Emily.Queryable<Administrator>().Where(t => t.IsDelete == false).FirstAsync();
        }
        public async Task<Object> UpdateTest()
        {
            List<Administrator> administrator = Emily.Queryable<Administrator>().Where(t => t.IsDelete == false).ToList();
            return await base.AlterData<Administrator>(administrator, DbReturnTypes.AlterCols, null, t => new { t.Account, t.PassWord }, t => t.PrimaryId != Guid.Empty);
        }
        public async Task<Object> BatchDel()
        {
            List<Administrator> administrator = Emily.Queryable<Administrator>().Where(t => t.IsDelete == false).ToList();
            return await base.AlterData<Administrator>(administrator, DbReturnTypes.AlterSoft);
        }
        public async Task<Object> RecoveryData()
        {
            List<Administrator> administrator = Emily.Queryable<Administrator>().Where(t => t.IsDelete == true).ToList();
            return await base.AlterData<Administrator>(administrator, DbReturnTypes.AlterSoft, false);

        }
        public async Task<Object> RemoveTest() {
            List<Administrator> administrator = Emily.Queryable<Administrator>().ToList();
            return await base.RemoveData<Administrator>(administrator);
        }
        public async Task<AdminRoleViewModel> Login(AdminRoleViewModel ViewModel)
        {
            AdminRoleViewModel AdminRole = Emily.Queryable<Administrator, RolePermission>((t, x) => new Object[] { JoinType.Left, t.RolePermissionId == x.PrimaryId })
                .Where(t => t.Account.Equals(ViewModel.Account))
                .Where(t => t.PassWord.Equals(ViewModel.PassWord))
                .Select<AdminRoleViewModel>().First();
            if (AdminRole != null)
                await CacheFacoty.WriteCache<AdminRoleViewModel>(AdminRole, AdminRole.GetType().FullName, 1);
            return AdminRole;
        }
    }
}
