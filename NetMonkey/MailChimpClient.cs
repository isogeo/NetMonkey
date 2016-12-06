using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Newtonsoft.Json;

namespace NetMonkey
{

    /// <summary>Client for the <see href="http://developer.mailchimp.com/documentation/mailchimp/">MailChimp v3.0 API</see>.</summary>
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

        /// <summary>Adds the specified member to the specified list.</summary>
        /// <param name="listId">The unique id for the list.</param>
        /// <param name="member">The new member to add.</param>
        /// <returns>The new list member.</returns>
        public async Task<Model.ListMember> AddListMember(string listId, Model.ListMember member)
        {
            Debug.Assert(!string.IsNullOrEmpty(listId));
            if (string.IsNullOrEmpty(listId))
                throw new ArgumentNullException("listId");
            Debug.Assert(member!=null);
            if (member==null)
                throw new ArgumentNullException("member");

            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "lists/{0}/members",
                        listId
                    )
                )
            );

            var content = new StringContent(
                JsonConvert.SerializeObject(member, Formatting.None, _SerializerSettings),
                Encoding.UTF8,
                _JsonMediaType
            );
            using (var response = await _Client.PostAsync(uriBuilder.Uri, content))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.ListMember>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
                else
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Gets information about a list’s interest categories.</summary>
        /// <param name="listId">The unique id for the list.</param>
        /// <param name="query">The optional (but recommended) query.</param>
        /// <returns>Information about a list’s interest categories.</returns>
        public async Task<Model.InterestCategoryResults> GetInterestCategories(string listId, ResultsQuery<Model.InterestCategoryResults> query)
        {
            Debug.Assert(!string.IsNullOrEmpty(listId));
            if (string.IsNullOrEmpty(listId))
                throw new ArgumentNullException("listId");

            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "lists/{0}/interest-categories",
                        listId
                    )
                )
            );
            if (query!=null)
                uriBuilder.Query=query.ToString();

            using (var response = await _Client.GetAsync(uriBuilder.Uri))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.InterestCategoryResults>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
                else
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Gets a list of this category’s interests.</summary>
        /// <param name="listId">The unique id for the list.</param>
        /// <param name="categoryId">The unique id for the interest category.</param>
        /// <param name="query">The optional (but recommended) query.</param>
        /// <returns>The list of this category’s interests</returns>
        public async Task<Model.InterestResults> GetInterests(string listId, string categoryId, ResultsQuery<Model.InterestResults> query)
        {
            Debug.Assert(!string.IsNullOrEmpty(listId));
            if (string.IsNullOrEmpty(listId))
                throw new ArgumentNullException("listId");
            Debug.Assert(!string.IsNullOrEmpty(categoryId));
            if (string.IsNullOrEmpty(categoryId))
                throw new ArgumentNullException("categoryId");

            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "lists/{0}/interest-categories/{1}/interests",
                        listId,
                        categoryId
                    )
                )
            );
            if (query!=null)
                uriBuilder.Query=query.ToString();

            using (var response = await _Client.GetAsync(uriBuilder.Uri))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.InterestResults>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
                else
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Gets information about all lists in the account.</summary>
        /// <param name="query">The optional (but recommended) query.</param>
        /// <returns>Information about lists in the account.</returns>
        public async Task<Model.ListResults> GetLists(ResultsQuery<Model.ListResults> query)
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

        /// <summary>Gets information about a specific list member.</summary>
        /// <param name="listId">The unique id for the list.</param>
        /// <param name="address">The list member’s email address.</param>
        /// <param name="query">The optional (but recommended) query.</param>
        /// <returns>Information about a specific list member.</returns>
        public async Task<Model.ListMember> GetListMember(string listId, MailAddress address, FieldsQuery<Model.ListMember> query)
        {
            Debug.Assert(!string.IsNullOrEmpty(listId));
            if (string.IsNullOrEmpty(listId))
                throw new ArgumentNullException("listId");
            Debug.Assert(address!=null);
            if (address==null)
                throw new ArgumentNullException("address");

            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "lists/{0}/members/{1}",
                        listId,
                        address.GetMailChimpSubscriberHash()
                    )
                )
            );
            if (query!=null)
                uriBuilder.Query=query.ToString();

            using (var response = await _Client.GetAsync(uriBuilder.Uri))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.ListMember>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
                else
                    throw JsonConvert.DeserializeObject<MailChimpException>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
        }

        /// <summary>Updates information for a specific list member.</summary>
        /// <param name="listId">The unique id for the list.</param>
        /// <param name="address">The list member’s email address.</param>
        /// <param name="member">The member information to update.</param>
        /// <returns>The updated information for the list member.</returns>
        public async Task<Model.ListMember> UpdateListMember(string listId, MailAddress address, Model.ListMember member)
        {
            Debug.Assert(!string.IsNullOrEmpty(listId));
            if (string.IsNullOrEmpty(listId))
                throw new ArgumentNullException("listId");
            Debug.Assert(address!=null);
            if (address==null)
                throw new ArgumentNullException("address");
            Debug.Assert(member!=null);
            if (member==null)
                throw new ArgumentNullException("member");

            var uriBuilder = new UriBuilder(
                new Uri(
                    _Client.BaseAddress,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "lists/{0}/members/{1}",
                        listId,
                        address.GetMailChimpSubscriberHash()
                    )
                )
            );

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uriBuilder.Uri) {
                Content=new StringContent(
                    JsonConvert.SerializeObject(member, Formatting.None, _SerializerSettings),
                    Encoding.UTF8,
                    _JsonMediaType
                )
            };
            using (var response = await _Client.SendAsync(request))
                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<Model.ListMember>(await response.Content.ReadAsStringAsync(), _SerializerSettings);
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
        private const string _JsonMediaType = "application/json";
    }
}
