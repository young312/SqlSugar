using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Edna.Extension.Attributes;
using Edna.Extension.Attributes.RoleHandler;
using Edna.Extension.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edna.ApiCore.Controllers
{
    /// <summary>
    /// 系统
    /// </summary>
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class SystemController : BaseApiController
    {
        /// <summary>
        ///  获取
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET","POST")]
        [Author(Roles.AdminRead)]
        public async Task<ActionResult<Object>> Get() => await SysService.SearchTest();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        public async Task<ActionResult<Object>> Login(AdminRoleViewModel ViewModel)
        {
            var claimIdentity = new ClaimsIdentity("Cookie");
            var RoleAdmin = await SysService.Login(ViewModel);
            if (RoleAdmin == null)
                return "登录失败!";
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, RoleAdmin.RolePermissionId.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, RoleAdmin.AdminName));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, RoleAdmin.HandlerRole));
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity), new AuthenticationProperties { IsPersistent = true });
            return "登录成功!";
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [Author(Roles.UserDelete)]
        public async Task<ActionResult<Object>> BatchDel() => await SysService.BatchDel();
        /// <summary>
        /// 数据恢复
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [Author(Roles.User)]
        public async Task<ActionResult<Object>> RecoveryData() => await SysService.RecoveryData();
    }
}
