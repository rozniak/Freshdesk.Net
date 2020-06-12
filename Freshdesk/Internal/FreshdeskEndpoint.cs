using Freshdesk.Framework;
using Freshdesk.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrlCombineLib;

namespace Freshdesk.Internal
{
    /// <summary>
    /// Provides the functionality for communicating with the Freshdesk API over
    /// HTTPS.
    /// </summary>
    internal sealed class FreshdeskEndpoint
    {
        /// <summary>
        /// The encoding to use during transmission.
        /// </summary>
        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// The timeout for <see cref="WebRequest.GetResponseAsync"/> calls made by
        /// this class.
        /// </summary>
        private const int RequestTimeout = 10000;

        /// <summary>
        /// The user agent string to use during transmission.
        /// </summary>
        private const string UserAgent = "Freshdesk.NET (1.0)";


        /// <summary>
        /// The base URI of the API endpoint.
        /// </summary>
        public Uri BaseUri { get; private set; }


        /// <summary>
        /// The key to use in the Authorization header when communicating with
        /// Freshdesk.
        /// </summary>
        private string ApiKey { get; set; }

        /// <summary>
        /// The reference to the public API wrapper that will be passed onto objects
        /// spawned by this instance.
        /// </summary>
        private FreshdeskConnection ApiWrapperRef { get; set; }


        /// <summary>
        /// Initializes a new instance of the FreshdeskEndpoint class.
        /// </summary>
        /// <param name="owner">
        /// The owning <see cref="FreshdeskConnection"/> instance.
        /// </param>
        /// <param name="apiEndpoint">
        /// The base URI for the API endpoint.
        /// </param>
        /// <param name="apiKey">
        /// The API key used to authenticate against the endpoint.
        /// </param>
        public FreshdeskEndpoint(
            FreshdeskConnection owner,
            Uri                 apiEndpoint,
            string              apiKey
        )
        {
            ApiKey        = apiKey;
            ApiWrapperRef = owner;
            BaseUri       = apiEndpoint;
        }


        /// <summary>
        /// Gets an item from Freshdesk.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="id">
        /// The ID of the item.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The item that was downloaded from Freshdesk casted as a
        /// <see cref="FreshdeskObject"/>.
        /// </returns>
        public async Task<FreshdeskObject> GetItem(
            FreshdeskObjectKind     dataType,
            long                    id,
            params FreshdeskQuery[] queries
        )
        {
            WebRequest request       = SetupRequest(
                                           HttpMethod.Get,
                                           BuildUri(
                                               BaseUri,
                                               dataType,
                                               queries,
                                               id
                                           ),
                                           ApiKey
                                       );
            string          response = await ReadWebResponse(request);
            FreshdeskObject result   = FreshdeskJson.DeserializeToType(
                                           dataType,
                                           response
                                       );

            result.Freshdesk = ApiWrapperRef;

            return result;
        }

        /// <summary>
        /// Gets items from Freshdesk.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The items that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{IFreshdeskObject}"/> collection.
        /// </returns>
        public async Task<IEnumerable<FreshdeskObject>> GetItems(
            FreshdeskObjectKind     dataType,
            params FreshdeskQuery[] queries
        )
        {
            WebRequest request = SetupRequest(
                                     HttpMethod.Get,
                                     BuildUri(
                                         BaseUri,
                                         dataType,
                                         queries
                                     ),
                                     ApiKey
                                 );
            string response    = await ReadWebResponse(request);

            return PrepareResults(
                FreshdeskJson.DeserializeToCollection(dataType, response)
            );
        }

        /// <summary>
        /// Gets items from Freshdesk.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="id">
        /// The ID of the item.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <param name="subDataType">
        /// The Freshdesk data type for sub-data, when referring to a specific parent
        /// object.
        /// </param>
        /// <returns>
        /// The items that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{IFreshdeskObject}"/> collection.
        /// </returns>
        public async Task<IEnumerable<FreshdeskObject>> GetItems(
            FreshdeskObjectKind     dataType,
            long                    id,
            FreshdeskObjectKind     subDataType,
            params FreshdeskQuery[] queries
        )
        {
            WebRequest request = SetupRequest(
                                     HttpMethod.Get,
                                     BuildUri(
                                         BaseUri,
                                         dataType,
                                         queries,
                                         id,
                                         subDataType
                                     ),
                                     ApiKey
                                 );
            string response    = await ReadWebResponse(request);

            return PrepareResults(
                FreshdeskJson.DeserializeToCollection(subDataType, response)
            );
        }


        /// <summary>
        /// Builds a <see cref="Uri"/> for a request.
        /// </summary>
        /// <param name="baseUri">
        /// The base URI of the API endpoint.
        /// </param>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="queries">
        /// The request queries.
        /// </param>
        /// <param name="id">
        /// The ID of the object, if acquiring a particular item from Freshdesk.
        /// </param>
        /// <param name="subDataType">
        /// The Freshdesk data type for sub-data, when referring to a specific parent
        /// object.
        /// </param>
        /// <returns>
        /// A <see cref="Uri"/> that forms the request based on the provided
        /// parameters.
        /// </returns>
        private Uri BuildUri(
            Uri                  baseUri,
            FreshdeskObjectKind  dataType,
            FreshdeskQuery[]     queries,
            long?                id          = null,
            FreshdeskObjectKind? subDataType = null
        )
        {
            // Construct the path component for the particular API being called
            //
            var relativePath = string.Empty;

            if (id == null)
            {
                relativePath = FreshdeskUtility.TypeToString(dataType);
            }
            else
            {
                if (subDataType == null)
                {
                    relativePath =
                        string.Format(
                            "{0}/{1}",
                            FreshdeskUtility.TypeToString(dataType),
                            id
                        );
                }
                else
                {
                    relativePath =
                        string.Format(
                            "{0}/{1}/{2}",
                            FreshdeskUtility.TypeToString(dataType),
                            id,
                            FreshdeskUtility.TypeToString(
                                (FreshdeskObjectKind) subDataType
                            )
                        );
                }
            }

            // Build the URI
            //
            var builder = new UriBuilder(baseUri.Combine(relativePath));

            builder.Query = FreshdeskQuery.ComposeAll(queries);

            return builder.Uri;
        }

        /// <summary>
        /// Prepares the objects in the result collection before they are returned to
        /// the API caller.
        /// </summary>
        /// <param name="results">
        /// The downloaded Freshdesk items.
        /// </param>
        /// <returns>
        /// The same collection passed in, with the items themselves having been
        /// prepared for returning to the public API.
        /// </returns>
        private IEnumerable<FreshdeskObject> PrepareResults(
            IEnumerable<FreshdeskObject> results
        )
        {
            foreach (FreshdeskObject item in results)
            {
                item.Freshdesk = ApiWrapperRef;
            }

            return results;
        }

        /// <summary>
        /// Acquires and reads the response to a <see cref="WebRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response from the web server.
        /// </returns>
        private async Task<string> ReadWebResponse(
            WebRequest request
        )
        {
            var res = string.Empty;

            using (var timeoutCancelToken = new CancellationTokenSource())
            {
                var responseTask = request.GetResponseAsync();
                var taskResult   = await Task.WhenAny(
                                       responseTask,
                                       Task.Delay(
                                           RequestTimeout,
                                           timeoutCancelToken.Token
                                       )
                                   );

                if (taskResult == responseTask)
                {
                    timeoutCancelToken.Cancel();

                    using (var response = (HttpWebResponse) await responseTask)
                    {
                        // We need to retrieve the transfer encoding first, Freshdesk
                        // can use 'chunked' transfer encoding, this can cause problems
                        // if we read the stream incorrectly
                        //
                        string transferEncoding =
                            response.GetResponseHeader("transfer-encoding");

                        using (Stream s = response.GetResponseStream())
                        {
                            if (transferEncoding == "chunked")
                            {
                                byte[] buffer = new byte[8192]; // Buffer 8KB at a time
                                int chunkSize = 999;
                                var sb        = new StringBuilder();

                                while (chunkSize > 0)
                                {
                                    chunkSize = s.Read(buffer, 0, buffer.Length);

                                    sb.Append(
                                        Encoding.GetString(
                                            buffer,
                                            0,
                                            chunkSize
                                        )
                                    );
                                }

                                res = sb.ToString();
                            }
                            else
                            {
                                using (var sr = new StreamReader(s, Encoding))
                                {
                                    res = sr.ReadToEnd();
                                }
                            }
                        }
                    }
                }
                else
                {
                    request.Abort();

                    var ex = new TimeoutException(
                        "The Freshdesk API call timed out."
                    );

                    ex.Data["HttpMethod"] = HttpMethod.Get.Method;
                    ex.Data["Uri"]        = request.RequestUri;

                    throw ex;
                }
            }

            return res;
        }

        /// <summary>
        /// Sets up a web request.
        /// </summary>
        /// <param name="method">
        /// The HTTP method.
        /// </param>
        /// <param name="uri">
        /// The target URI.
        /// </param>
        /// <param name="apiKey">
        /// The API key for authorizing the request.
        /// </param>
        /// <returns>
        /// A <see cref="WebRequest"/> instance that is configured with the specified
        /// parameters.
        /// </returns>
        private WebRequest SetupRequest(
            HttpMethod method,
            Uri        uri,
            string     apiKey
        )
        {
            var req = (HttpWebRequest) WebRequest.Create(uri);

            req.Headers["Authorization"] =
                string.Format(
                    "Basic {0}",
                    Convert.ToBase64String(
                        Encoding.Default.GetBytes(apiKey + ":X")
                    )
                );
            req.UserAgent                = UserAgent;

            switch (method.Method.ToUpper())
            {
                case "GET":
                    req.Accept = "*/*";
                    req.AutomaticDecompression = DecompressionMethods.GZip;
                    break;

                case "POST":
                case "PUT":
                    req.ContentType = "application/json";
                    break;

                default:
                    throw new NotImplementedException(
                        "HTTP method unsupported."
                    );
            }

            return req;
        }
    }
}
