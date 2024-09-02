namespace Api.Commons.Middlewares
{
    public class JobRunnerMiddleware
    {
        private readonly RequestDelegate _next;
        public JobRunnerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)//, IJobModule jobModule)
        {
            //jobModule.RunRecurringTasks().GetAwaiter().GetResult();
            await _next(context);
        }
    }
}