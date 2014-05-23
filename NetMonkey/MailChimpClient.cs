using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetMonkey
{




    ////////////////////////////////////////////////////////////////////////////
    ///
    /// <summary>Client for the <see href="http://apidocs.mailchimp.com/api/2.0/">MailChimp v2.0 API</see>.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public class MailChimpClient:
        IDisposable
    {

        /// <summary>Creates a new instance of the <see cref="MailChimpClient" /> class.</summary>
        private MailChimpClient()
        {
            _Logger=LogManager.GetCurrentClassLogger();
        }

        /// <summary>Creates a new instance of the <see cref="MailChimpClient" /> class.</summary>
        /// <param name="apiKey">The MailChimp API key to use.</param>
        public MailChimpClient(string apiKey):
            this()
        {
            Debug.Assert(!string.IsNullOrEmpty(apiKey));
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey");

            _ApiKey=apiKey;

            var dataCenter=ApiKey.Split('-')[1];
            _Client=new HttpClient() {
                BaseAddress=new Uri(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        _BaseUri,
                        dataCenter
                    )
                )
            };
        }

        /// <summary>Finalizes the current instance.</summary>
        ~MailChimpClient()
        {
            Dispose(false);
        }

        /// <summary>Releases unmanaged resources held by the current instance.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //public async Task GetLists(object filters, int? start, int? limit, SortField? sortField, SortDirection? sortDirection)
        //{
        //    dynamic parameters=new ExpandoObject();
        //    parameters.apikey=_ApiKey;
        //    parameters.filters=filters;
        //    parameters.start=start;
        //    parameters.limit=limit;
        //    parameters.sort_field=sortField;
        //    parameters.sort_dir=sortDirection;

        //    throw new NotImplementedException();
        //}

        /// <summary>Ping the MailChimp API.</summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A message related to the current API status.</returns>
        public async Task<string> PingAsync(CancellationToken cancellationToken)
        {
            dynamic parameters=new ExpandoObject();
            parameters.apikey=_ApiKey;

            var rm=await RequestAsync(new Uri("/2.0/helper/ping.json", UriKind.Relative), parameters, cancellationToken);
            var response=JObject.Parse(await rm.Content.ReadAsStringAsync());
            return response.msg;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_Client!=null)
                {
                    _Client.CancelPendingRequests();
                    _Client.Dispose();
                }
            }

            _Client=null;
            _Logger=null;
        }


        private async Task<HttpResponseMessage> RequestAsync(Uri uri, object parameters, CancellationToken cancellationToken)
        {
            HttpContent content=null;
            if (parameters!=null)
            {
                var json=JsonConvert.SerializeObject(
                    parameters,
                    Formatting.None,
                    new JsonSerializerSettings() {
                        Formatting=Formatting.None,
                        NullValueHandling=NullValueHandling.Ignore
                    }
                );
                content=new StringContent(json, Encoding.UTF8, "application/json");
            } else
                content=new StringContent( string.Empty );

            if (_Logger.IsDebugEnabled)
            {
                var s=await content.ReadAsStringAsync();
                _Logger.DebugFormat(CultureInfo.InvariantCulture, "HTTP {1}: {0}\n{2}", uri, HttpMethod.Post, s);
            } else
                _Logger.TraceFormat(CultureInfo.InvariantCulture, "HTTP {1}: {0}", uri, HttpMethod.Post);

            var response=await _Client.PostAsync(uri, content, cancellationToken).ConfigureAwait(false);

            if (response!=null)
            {
                _Logger.DebugFormat(CultureInfo.InvariantCulture, "HTTP response: {0}", response.StatusCode);

                if (!response.IsSuccessStatusCode)
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync());
                response.EnsureSuccessStatusCode();
            }
            return response;
        }

        /// <summary>Gets the current MailChimp API key.</summary>
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
        }

        private string _ApiKey;
        private ILog _Logger;
        private HttpClient _Client;

        private const string _BaseUri="https://{0}.api.mailchimp.com/";
    }
}
