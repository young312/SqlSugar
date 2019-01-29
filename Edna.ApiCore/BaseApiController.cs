using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edna.Extension.AutofacIoc;
using Edna.Service.IServiceProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Edna.ApiCore
{
    public class BaseApiController : ControllerBase
    {
        public ISystemService SysService = AutofocManage.CreateInstance().Resolve<ISystemService>();
    }
}