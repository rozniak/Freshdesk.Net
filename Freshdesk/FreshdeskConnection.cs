/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  John Becker (john@beckersoft.com)
 *  Oleg Shevchenko (shevchenko.oleg@outlook.com)
 *  Joseph Poh (github user jozsurf)
 *  (github user ninjacarr)
 *  (github user sloppypointless)
 *  
 *  Some web code is derived from work authored by:
 * 	Gonzalo Paniagua Javier (gonzalo@xamarin.com)
 * 	
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Reflection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Freshdesk
{
    /// <summary>
    /// Represents a connection to a Freshdesk instance, using version 2 of the Freshdesk API
    /// </summary>
    public class FreshdeskConnection
    {
        /// <summary>
        /// The encoding to use during transmission.
        /// </summary>
        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// The Regex rule to use when validating the connection hostname.
        /// </summary>
        public static readonly Regex UriRule = new Regex("^[[:alnum:]]+\\.freshdesk\\.com$");

        /// <summary>
        /// The user agent string to use during transmission.
        /// </summary>
        private const string UserAgent = "Freshdesk.NET/1.0";


        /// <summary>
        /// Gets or sets the API key to use as authentication with the Freshdesk API.
        /// </summary>
        private string ApiKey { get; set; }

        /// <summary>
        /// Gets the connected URI.
        /// </summary>
        public readonly Uri ConnectionUri;


        /// <summary>
        /// Initializes a new instance of the FreshdeskConnection class.
        /// </summary>
        /// <param name="apiKey">The API key to authenticate with.</param>
        /// <param name="connUri">The URI of the Freshdesk instance.</param>
        public FreshdeskConnection(string apiKey, Uri connUri)
        {
            ApiKey = apiKey;

            // Validate the URI
            if (connUri.Scheme != "https" ||
                !UriRule.IsMatch(connUri.Host))
                throw new ArgumentException("FreshdeskConnection.New: Invalid connection URI provided. URI must be in the form https://*.freshdesk.com.\n\nSee https://developer.freshdesk.com/api/#authentication for details.");

            ConnectionUri = connUri;

            // Force TLS 1.1 or higher. Anything lower is deprecated in the Freshdesk API as of 2016-09-30
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }


        /// <summary>
        /// Creates a new ticket on the helpdesk.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The resultant Ticket object that has been finalized on Freshdesk.</returns>
        public Ticket CreateTicket(Ticket ticket)
        {
            // TODO: Code this
            return null;
        }

        /// <summary>
        /// Delete a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>True if the ticket was deleted or does not exist.</returns>
        public bool DeleteTicket(long id)
        {
            // TODO: Code this
            return false;
        }

        /// <summary>
        /// Gets a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>A Ticket object populated with ticket data retrieved from Freshdesk if the ID was found, null otherwise.</returns>
        public Ticket GetTicketById(long id)
        {
            // TODO: Code this
            return null;
        }


        private T DoMultipartFormRequest<T>(Uri uri, object body, IEnumerable<Attachment> attachments, string propertiesArrayName, string attachmentsKey)
        {
            var json = DoMultipartFormRequest(uri, body, attachments, propertiesArrayName, attachmentsKey);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private T DoRequest<T>(Uri uri)
        {
            return DoRequest<T>(uri, "GET", null);
        }

        private T DoRequest<T>(Uri uri, string method, string body)
        {
            var json = DoRequest(uri, method, body);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private string DoRequest(Uri uri)
        {
            return DoRequest(uri, "GET", null);
        }

        private string DoRequest(Uri uri, string method, string body)
        {
            string result = string.Empty;
            WebRequest req = SetupRequest(method, uri);

            if (body != null)
            {
                byte[] bytes = Encoding.GetBytes(body);

                req.ContentLength = bytes.Length;

                using (Stream s = req.GetRequestStream())
                {
                    s.Write(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (WebResponse response = (WebResponse)req.GetResponse())
                {
                    result = GetResponseAsString(response);
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    /*
                    string json_error = GetResponseAsString(wexc.Response);
                    HttpStatusCode status_code = HttpStatusCode.BadRequest;
                    HttpWebResponse resp = wexc.Response as HttpWebResponse;
                    if (resp != null)
                        status_code = resp.StatusCode;

                    //if ((int)status_code <= 500)
                    //    throw StripeException.GetFromJSON(status_code, json_error);
                    */
                }
                throw;
            }

            return result;
        }

        private string GetAuthorizationHeader(string apiKey)
        {
            return "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(apiKey + ":" + "X"));
        }

        private static string GetResponseAsString(WebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding))
            {
                return sr.ReadToEnd();
            }
        }

        private WebRequest SetupMultipartRequest(Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            webRequest.Headers.Clear();

            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader(ApiKey);

            return webRequest;
        }

        private WebRequest SetupRequest(string method, Uri uri)
        {
            WebRequest webRequest = (WebRequest)WebRequest.Create(uri);
            webRequest.Method = method;
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest != null)
            {
                httpRequest.UserAgent = UserAgent;
            }

            webRequest.Headers["Authorization"] = GetAuthorizationHeader(ApiKey);

            switch (method)
            {
                case "POST":
                case "PUT":
                    webRequest.ContentType = "application/json";
                    break;

                case "GET":
                    ((HttpWebRequest)webRequest).AutomaticDecompression = DecompressionMethods.GZip;
                    ((HttpWebRequest)webRequest).Accept = "*/*";
                    break;
            }

            return webRequest;
        }

        private Uri UriForPath(string path, string query = null)
        {
            UriBuilder uriBuilder = new UriBuilder(ConnectionUri);
            uriBuilder.Path = path;

            if (!string.IsNullOrEmpty(query))
            {
                uriBuilder.Query = query;
            }

            return uriBuilder.Uri;
        }


        #region Shared
        
        private string DoMultipartFormRequest(Uri uri, object body, IEnumerable<Attachment> attachments, string propertiesArrayName, string attachmentsKey)
        {
            var boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            var stringsContent = GetStringsContent(body);
            var webRequest = SetupMultipartRequest(uri);

            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            
            using (var requestStream = webRequest.GetRequestStream())
            {
                foreach (var pair in stringsContent)
                {
                    WriteBoundaryBytes(requestStream, boundary, false);

                    if (pair.Key == "cc_emails")
                        WriteContentDispositionFormDataHeader(requestStream, string.Format("{0}[{1}]", "cc_emails", ""));
                    else
                        WriteContentDispositionFormDataHeader(requestStream, string.Format("{0}[{1}]", propertiesArrayName, pair.Key));

                    WriteString(requestStream, pair.Value);
                    WriteCrlf(requestStream);
                }

                foreach (var attachment in attachments)
                {
                    WriteBoundaryBytes(requestStream, boundary, false);

                    WriteContentDispositionFileHeader(requestStream, attachmentsKey,
                        attachment.FileName, /**MimeMapping.GetMimeMapping(attachment.FileName)*/ String.Empty);
                    var data = new byte[attachment.Content.Length];
                    attachment.Content.Read(data, 0, data.Length);

                    requestStream.Write(data, 0, data.Length);
                    WriteCrlf(requestStream);
                }

                WriteBoundaryBytes(requestStream, boundary, true);

                requestStream.Close();
            }

            var response = webRequest.GetResponse();
            return GetResponseAsString(response);
        }

        

        private static Dictionary<string, string> GetStringsContent(object instance) {
            if (instance == null)
                throw new ArgumentNullException("FreshdeskConnection.GetStringsContent: Parameter 'instance' cannot be null.");

            Type classType = instance.GetType();
            var properties = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in classType.GetProperties())
            {
                var propertyValue = propertyInfo.GetValue(instance, null);

                if (propertyValue == null)
                    continue;

                if (!propertyInfo.PropertyType.IsPrimitive &&
                    propertyInfo.PropertyType != typeof(decimal) &&
                    propertyInfo.PropertyType != typeof(string))
                {
                    var stringsContent = GetStringsContent(propertyValue);

                    foreach (var content in stringsContent)
                    {
                        properties.Add(content.Key, content.Value);
                    }

                    continue;
                }

                object[] attributes = propertyInfo.GetCustomAttributes(true);
                string propertyName = null;

                foreach (object attribute in attributes)
                {
                    var jsonPropertyAttribute = attribute as JsonPropertyAttribute;

                    if (jsonPropertyAttribute != null)
                    {
                        propertyName = jsonPropertyAttribute.PropertyName;
                        break;
                    }
                }

                if (propertyName == null) {
                    propertyName = propertyInfo.Name;
                }

                properties[propertyName] = propertyValue.ToString();
            }
            return properties;
        }

        private static void WriteCrlf(Stream requestStream) {
            byte[] crLf = Encoding.UTF8.GetBytes("\r\n");
            requestStream.Write(crLf, 0, crLf.Length);
        }

        private static void WriteBoundaryBytes(Stream requestStream, string b, bool isFinalBoundary) {
            string boundary = isFinalBoundary ? "--" + b + "--" : "--" + b + "\r\n";
            byte[] d = Encoding.UTF8.GetBytes(boundary);
            requestStream.Write(d, 0, d.Length);
        }

        private static void WriteContentDispositionFormDataHeader(Stream requestStream, string name) {
            string data = "Content-Disposition: form-data; name=\"" + name + "\"\r\n\r\n";
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }

        private static void WriteContentDispositionFileHeader(Stream requestStream, string name, string fileName, string contentType) {
            string data = "Content-Disposition: form-data; name=\"" + name + "\"; filename=\"" + fileName + "\"\r\n";
            data += "Content-Type: " + contentType + "\r\n\r\n";
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }

        private static void WriteString(Stream requestStream, string data) {
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }

        #endregion

        #region Customers
        /// <summary>
        /// Creates a Company
        /// </summary>
        /// <param name="createCustomerRequest"></param>
        /// <returns></returns>
        public GetCustomerResponse CreateCustomer(CreateCustomerRequest createCustomerRequest)
        {
            if (createCustomerRequest == null)
            {
                throw new ArgumentNullException("createCustomerRequest");
            }
            return DoRequest<GetCustomerResponse>(UriForPath("/customers.json"), "POST", JsonConvert.SerializeObject(createCustomerRequest));
        }
        #endregion

        #region Tickets
        


        /// <summary>
        /// Creates a Support Ticket
        /// </summary>
        /// <param name="createTicketRequest"></param>
        /// <returns></returns>
        public GetTicketResponse CreateTicket(CreateTicketRequest createTicketRequest) {
            LastError = HelpdeskError.NoError;

            if (createTicketRequest == null) {
                throw new ArgumentNullException("createTicketRequest");
            }
            return DoRequest<GetTicketResponse>(UriForPath("/helpdesk/tickets.json"), "POST", JsonConvert.SerializeObject(createTicketRequest));
        }

        /// <summary>
        /// Creates a Support Ticket with an attachment
        /// </summary>
        /// <param name="createTicketRequest"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public GetTicketResponse CreateTicketWithAttachment(CreateTicketRequest createTicketRequest, IEnumerable<Attachment> attachments) {
            LastError = HelpdeskError.NoError;

            if (createTicketRequest == null) {
                throw new ArgumentNullException("createTicketRequest");
            }
            if (attachments == null) {
                throw new ArgumentNullException("attachments");
            }

            return DoMultipartFormRequest<GetTicketResponse>(UriForPath("/helpdesk/tickets.json"), createTicketRequest, attachments, "helpdesk_ticket", "helpdesk_ticket[attachments][][resource]");
        }

        /// <summary>
        /// Gets a list of Support Tickets for a company by name
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public GetTicketListItemResponse[] GetTicketsByCompany(string companyName)
        {
            LastError = HelpdeskError.NoError;

            if (string.IsNullOrEmpty(companyName))
            {
                throw new ArgumentNullException("companyName");
            }

            return DoRequest<GetTicketListItemResponse[]>(UriForPath("/helpdesk/tickets.json", "company_name=" + companyName + "&filter_name=all_tickets"));
        }
        #endregion

        #region Users
        /// <summary>
        /// Create Contact
        /// </summary>
        /// <param name="createUserRequest"></param>
        /// <returns></returns>
        public GetUserResponse CreateUser(CreateUserRequest createUserRequest) {
            LastError = HelpdeskError.NoError;

            if (createUserRequest == null) {
                throw new ArgumentNullException("createUserRequest");
            }
            return DoRequest<GetUserResponse>(UriForPath("/contacts.json"), "POST", JsonConvert.SerializeObject(createUserRequest));
        }

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="updateUserRequest"></param>
        /// <param name="id"></param>
        public void UpdateUser(UpdateUserRequest updateUserRequest, long id) {
            LastError = HelpdeskError.NoError;

            if (updateUserRequest == null) {
                throw new ArgumentNullException("updateUserRequest");
            }
            DoRequest<string>(UriForPath(string.Format("/contacts/{0}.json", id)), "PUT", JsonConvert.SerializeObject(updateUserRequest));
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
		public IEnumerable<GetUserRequest> GetUsers() {
            LastError = HelpdeskError.NoError;

            var users = new List<GetUserRequest>();
            var page = 1;
            while (true) {
                var paginatedUsers = DoRequest<IEnumerable<GetUserRequest>>(UriForPath("/contacts.json", string.Format("page={0}", page))).ToList();
                if (paginatedUsers.Any()) {
                    users.AddRange(paginatedUsers);
                    page++;
                } else {
                    break;
                }
            }
            return users;
        }


        #endregion

        #region Time Entries
        /// <summary>
        /// Creates a Time Entry
        /// </summary>
        /// <param name="createTimeRequest"></param>
        /// <returns></returns>
        public GetTimeResponse CreateTimeEntry(CreateTimeRequest createTimeRequest, int ticket)
        {
            LastError = HelpdeskError.NoError;

            if (createTimeRequest == null)
            {
                throw new ArgumentNullException("createTimeRequest");
            }
            return DoRequest<GetTimeResponse>(UriForPath("/helpdesk/tickets/" + ticket.ToString() + "/time_sheets.json"), "POST", JsonConvert.SerializeObject(createTimeRequest));
        }


        #endregion

    }

}
