using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "The base class will take care of that")]
        public LoggerMessageHandler() :
            base(new WebRequestHandler())
        {
            _Logger=LogManager.GetLogger<LoggerMessageHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage ret = null;

            if ((request.Content!=null) && (_Logger.IsTraceEnabled))
            {
                // Necessary as the content will be read multiple times
                await request.Content.LoadIntoBufferAsync()
                    .ConfigureAwait(false);
                _Logger.InfoFormat(
                    CultureInfo.CurrentCulture,
                    "{1} {0}: \"{2}\"",
                    request.RequestUri,
                    request.Method,
                    await request.Content.ReadAsStringAsync()
                        .ConfigureAwait(false)
                );
            } else
                _Logger.InfoFormat(
                    CultureInfo.CurrentCulture,
                    "{1} {0}",
                    request.RequestUri,
                    request.Method
                );

            var timer = new Stopwatch();
            timer.Start();

            ret=await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            timer.Stop();
            if (ret!=null)
            {
                // The following code seems to create a deadlock in some situations
                /*if (ret.Content!=null)
                {
                    // Necessary as the content will be read multiple times
                    await ret.Content.LoadIntoBufferAsync()
                        .ConfigureAwait(false);
                    _Logger.TraceFormat(
                        CultureInfo.InvariantCulture,
                        "{1} {0} - {2} ({3}) {4}ms: \"{5}\"",
                        request.RequestUri,
                        request.Method,
                        ret.StatusCode,
                        (int)ret.StatusCode,
                        timer.ElapsedMilliseconds,
                        await ret.Content.ReadAsStringAsync()
                            .ConfigureAwait(false)
                    );
                } else*/
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
