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

            _SerializerSettings=new JsonSerializerSettings() {
                ContractResolver=new Serialization.MailChimpJsonContractResolver(),
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

        #region Lists Related
        /// <summary>Get all the information for particular members of a list.</summary>
        /// <typeparam name="TMergeVariables"></typeparam>
        /// <param name="id">The list id to connect to. Get by calling <see cref="ListAsync" />.</param>
        /// <param name="emails">The emails to find.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The information for the members of the list.</returns>
        public async Task<MemberInfoResult<TMergeVariables>> MemberInfoAsync<TMergeVariables>(string id, IEnumerable<Email> emails, CancellationToken cancellationToken)
            where TMergeVariables:
                MergeVariables
        {
            dynamic parameters=new ExpandoObject();
            parameters.apikey=_ApiKey;
            parameters.id=id;
            parameters.emails=emails;

            var rm=await RequestAsync(new Uri("/2.0/lists/member-info.json", UriKind.Relative), parameters, cancellationToken);
            return JsonConvert.DeserializeObject<MemberInfoResult<TMergeVariables>>(await rm.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Retrieve all of the lists defined for your user account.</summary>
        /// <param name="filters">Filters to apply to this query</param>
        /// <param name="start">Control paging of lists, start results at this list number.</param>
        /// <param name="limit">Control paging of lists, number of lists to return with each call.</param>
        /// <param name="sortField">The field used to sort results.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The lists defined for your user account.</returns>
        public async Task<ListResult> ListAsync(CancellationToken cancellationToken, ListFilter filters=null, int? start=null, int? limit=null, SortField? sortField=null, SortDirection? sortDirection=null)
        {
            dynamic parameters=new ExpandoObject();
            parameters.apikey=_ApiKey;
            if (filters!=null)
                parameters.filters=filters;
            if (start.HasValue)
                parameters.start=start.Value;
            if (limit.HasValue)
                parameters.limit=limit.Value;
            if (sortField.HasValue)
                parameters.sort_field=sortField.Value;
            if (sortDirection.HasValue)
                parameters.sort_dir=sortDirection.Value;

            var rm=await RequestAsync(new Uri("/2.0/lists/list.json", UriKind.Relative), parameters, cancellationToken);
            return JsonConvert.DeserializeObject<ListResult>(await rm.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Subscribe the provided email to a list.</summary>
        /// <param name="id">The list id to connect to. Get by calling <see cref="ListAsync" />.</param>
        /// <param name="email">The email to subscribe.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <param name="mergeVars">Optional merges for the email (FNAME, LNAME, <see href="http://kb.mailchimp.com/article/where-can-i-find-my-lists-merge-tags">etc</see>.).</param>
        /// <param name="emailType">Email type preference for the email.</param>
        /// <param name="doubleOptIn">Flag to control whether a double opt-in confirmation message is sent.</param>
        /// <param name="updateExisting">Flag to control whether existing subscribers should be updated instead of throwing an error.</param>
        /// <param name="replaceInterests">Flag to determine whether we replace the interest groups with the groups
        /// provided or we add the provided groups to the member's interest groups.</param>
        /// <param name="sendWelcome">If your <paramref name="doubleOptIn" /> is false and this is true, we will send your lists Welcome email
        /// if this subscribe succeeds - this will *not* fire if we end up updating an existing subscriber. If <paramref name="doubleOptIn" /> is true, this has no effect.</param>
        /// <returns>The email information.</returns>
        public async Task<Email> SubscribeAsync<TMergeVariables>(string id, Email email, CancellationToken cancellationToken, TMergeVariables mergeVars=null, EmailType? emailType=null, bool? doubleOptIn=null, bool? updateExisting=null, bool? replaceInterests=null, bool? sendWelcome=null)
            where TMergeVariables:
                MergeVariables
        {
            dynamic parameters=new ExpandoObject();
            parameters.apikey=_ApiKey;
            parameters.id=id;
            parameters.email=email;
            if (mergeVars!=null)
                parameters.merge_vars=mergeVars;
            if (emailType.HasValue)
                parameters.email_type=emailType.Value;
            if (doubleOptIn.HasValue)
                parameters.double_optin=doubleOptIn.Value;
            if (updateExisting.HasValue)
                parameters.update_existing=updateExisting.Value;
            if (replaceInterests.HasValue)
                parameters.replace_interests=replaceInterests.Value;
            if (sendWelcome.HasValue)
                parameters.send_welcome=sendWelcome.Value;

            var rm=await RequestAsync(new Uri("/2.0/lists/subscribe.json", UriKind.Relative), parameters, cancellationToken);
            return JsonConvert.DeserializeObject<Email>(await rm.Content.ReadAsStringAsync(), _SerializerSettings);
        }
        #endregion

        #region Helper Related
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
        #endregion

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

        private const string _BaseUri="https://{0}.api.mailchimp.com/";
    }
}
