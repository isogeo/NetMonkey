using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;

namespace NetMonkey
{

    internal class LoggerMessageHandler:
        DelegatingHandler
    {

        public LoggerMessageHandler() :
            base(new WebRequestHandler())
        {
            _Logger=LogManager.GetLogger<LoggerMessageHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage ret = null;

            _Logger.InfoFormat(
                CultureInfo.CurrentCulture,
                "{1} {0}",
                request.RequestUri,
                request.Method
            );

            var timer = new Stopwatch();
            timer.Start();

            ret=await base.SendAsync(request, cancellationToken);

            timer.Stop();
            if (ret!=null)
            {
                if (_Logger.IsTraceEnabled && (ret.Content!=null))
                    _Logger.TraceFormat(
                        CultureInfo.InvariantCulture,
                        "{1} {0} - {2} ({3}) {4}ms: \"{5}\"",
                        request.RequestUri,
                        request.Method,
                        ret.StatusCode,
                        (int)ret.StatusCode,
                        timer.ElapsedMilliseconds,
                        await ret.Content.ReadAsStringAsync()
                    );
                else
                    _Logger.TraceFormat(
                        CultureInfo.InvariantCulture,
                        "{1} {0} - {2} ({3}) {4}ms",
                        request.RequestUri,
                        request.Method,
                        ret.StatusCode,
                        (int)ret.StatusCode,
                        timer.ElapsedMilliseconds
                    );
            }

            return ret;
        }

        private ILog _Logger;
    }
}
