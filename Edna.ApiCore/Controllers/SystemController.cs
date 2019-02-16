using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Edna.Extension.Attributes;
using Edna.Extension.Attributes.RoleHandler;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edna.ApiCore.Controllers
{
    /// <summary>
    /// 系统
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : BaseApiController
    {
        /// <summary>
        ///  获取
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        [Author(Roles.AdminRead)]
        public async Task<ActionResult<Object>> Get() => await SysService.SearchTest();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<Object>> Login()
        {
            var claimIdentity = new ClaimsIdentity("Cookie");
            var RoleAdmin = await SysService.Login();
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
        [HttpGet("BatchDel")]
        [AllowAnonymous]
        public async Task<ActionResult<Object>> BatchDel()=> await SysService.BatchDel();
        /// <summary>
        /// 数据恢复
        /// </summary>
        /// <returns></returns>
        [HttpGet("RecoveryData")]
        [AllowAnonymous]
        public async Task<ActionResult<Object>> RecoveryData() => await SysService.RecoveryData();
    }
}
