using LottoTicketGenerator.Dal;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace webapi.Controllers
{
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public ControllerBase(ILogger logger)
        {
            this.Logger = logger;
        }

        ILogger Logger;

        public void LogMethod([CallerMemberName] string controllerMethodName = null)
        {
            this.Logger.LogInformation(string.Format("{0}.{1}", GetType().FullName, controllerMethodName));
        }

        public T ReadDto<T>(Func<EntityFrameWorkContext, T> query)
        {
            using (var context = new EntityFrameWorkContext())
            {
                return query(context);
            }
        }
        
        public T[] ReadDtos<T>(Func<EntityFrameWorkContext, T[]> query)
        {
            using (var context = new EntityFrameWorkContext())
            {
                return query(context);
            }
        }
    }
}
