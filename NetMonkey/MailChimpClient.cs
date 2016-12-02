using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// <summary>Client for the <see href="http://developer.mailchimp.com/documentation/mailchimp/">MailChimp v3.0 API</see>.</summary>
    ///
    ////////////////////////////////////////////////////////////////////////////

    public class MailChimpClient:
        IDisposable
    {

        private MailChimpClient()
        { }

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
            _Client=new HttpClient(new LoggerMessageHandler()) {
                BaseAddress=new Uri(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        _BaseUri,
                        dataCenter
                    )
                )
            };
            _Client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Concat(Guid.NewGuid().ToString("n", CultureInfo.InvariantCulture), ":", ApiKey)
                    )
                )
            );
            _SerializerSettings=new JsonSerializerSettings() {
                ContractResolver=new Model.Serialization.MailChimpJsonContractResolver(),
                Formatting=Formatting.None,
                NullValueHandling=NullValueHandling.Ignore,
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

        /// <summary>Get information about all lists in the account.</summary>
        /// <param name="query">The query.</param>
        /// <returns>Information about lists in the account.</returns>
        public async Task<Model.ListResults> GetLists(ListQuery query)
        {
            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    "lists"
                )
            );
            if (query!=null)
                uriBuilder.Query=query.ToString();

            using (var response = await _Client.GetAsync(uriBuilder.Uri))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.ListResults>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
                else
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
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
                var json=JsonConvert.SerializeObject(parameters, Formatting.None, _SerializerSettings);
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
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
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
        private JsonSerializerSettings _SerializerSettings;

        private const string _BaseUri="https://{0}.api.mailchimp.com/3.0/";
    }
}
