using LottoTicketGenerator.Dal;
using System.Runtime.CompilerServices;

namespace webapi.ApplicationServices
{
    public abstract class ServiceBase
    {
        public ServiceBase(
            ILogger logger
            ) {
            this.logger = logger;
        }

        public ILogger logger;
        protected T ExecuteCommand<T>(Func<EntityFrameWorkContext, T> command, [CallerMemberName] string appServiceMethodName = null)
        {
            logger.Log(LogLevel.Information, string.Format("{0}.{1}", GetType().FullName, appServiceMethodName));
            using (var context = new EntityFrameWorkContext())
            {
                var result = command(context);
                context.SaveChanges();
                return result;
            }
        }
    }
}
