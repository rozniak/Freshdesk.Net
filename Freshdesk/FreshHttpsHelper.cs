/*
 * Freshdesk.FreshHttpHelper -- Freshdesk HTTPS Communication Subsystem
 *
 * This source-code is part of the Freshdesk API for C# library by Rory Fewell (rozniak) of Oddmatics for Agile ICT for Education Ltd.:
 * <<https://oddmatics.uk>>
 * <<http://www.agileict.co.uk>>
 * 	
 * Copyright (C) 2017 Oddmatics
 * 	
 * Sharing, editing and general licence term information can be found inside of the "LICENSE.MD" file that should be located in the root of this project's directory structure.
 */

using Freshdesk.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk
{
    /// <summary>
    /// Provides methods for communicating with Freshdesk over HTTPS.
    /// </summary>
    internal static class FreshHttpsHelper
    {
        /// <summary>
        /// The encoding to use during transmission.
        /// </summary>
        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// The user agent string to use during transmission.
        /// </summary>
        private const string UserAgent = "Freshdesk.NET/1.0";


        /// <summary>
        /// Sets the key to use in the authorization header when talking to Freshdesk.
        /// </summary>
        public static string AuthorizationKey
        {
            get { throw new FieldAccessException("FreshHttpHelper.AuthorizationKey.get: Not allowed to get this field. It is public write-only."); }
            set { _AuthorizationKey = value; }
        }
        private static string _AuthorizationKey;

        
        public static T DoMultipartFormRequest<T>(Uri uri, object body, IEnumerable<Attachment> attachments, string propertiesArrayName, string attachmentsKey)
        {
            var json = DoMultipartFormRequest(uri, body, attachments, propertiesArrayName, attachmentsKey);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string DoMultipartFormRequest(Uri uri, object body, IEnumerable<Attachment> attachments, string propertiesArrayName, string attachmentsKey)
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

        /// <summary>
        /// Performs a standard HTTP GET request to the specified URI and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The Type to deserialize the response into.</typeparam>
        /// <param name="uri">The URI of the target.</param>
        /// <returns>The response from the remote host, deserialized into the specified Type, cast as an object.</returns>
        public static async Task<object> DoRequest<T>(Uri uri)
        {
            return await DoRequest<T>(uri, "GET", null);
        }

        /// <summary>
        /// Performs a HTTP request to the specified URI and deserializes the response.
        /// </summary>
        /// <typeparam name="T">The Type to deserialize the response into.</typeparam>
        /// <param name="uri">The URI of the target.</param>
        /// <param name="method">The HTTP request method to use.</param>
        /// <param name="body">The request body.</param>
        /// <returns>The response from the remote host, deserialized into the specified Type, cast as an object.</returns>
        public static async Task<object> DoRequest<T>(Uri uri, string method, string body)
        {
            var json = await DoRequest(uri, method, body);
            Type genericType = typeof(T);

            // TODO: Replace this tower of IF statements
            //
            if (genericType == typeof(Ticket))
                return new Ticket(json);
            else if (genericType == typeof(IList<Ticket>))
            {
                JArray arr = JArray.Parse(json);
                var resultTickets = new List<Ticket>();

                foreach (JObject obj in arr.Children<JObject>())
                {
                    resultTickets.Add(new Ticket(obj));
                }

                return resultTickets.AsReadOnly();
            }
            else if (genericType == typeof(Company))
                return new Company(json);
            else if (genericType == typeof(IList<Company>))
            {
                JArray arr = JArray.Parse(json);
                var resultCompanies = new List<Company>();

                foreach (JObject obj in arr.Children<JObject>())
                {
                    resultCompanies.Add(new Company(obj));
                }

                return resultCompanies.AsReadOnly();
            }
            else if (genericType == typeof(Contact))
                return new Contact(json);
            else if (genericType == typeof(IList<Contact>))
            {
                JArray arr = JArray.Parse(json);
                var resultContacts = new List<Contact>();

                foreach (JObject obj in arr.Children<JObject>())
                {
                    resultContacts.Add(new Contact(obj));
                }

                return resultContacts.AsReadOnly();
            }
            else if (genericType == typeof(TicketTimeEntry))
                return new TicketTimeEntry(json);
            else if (genericType == typeof(IList<TicketTimeEntry>))
            {
                JArray arr = JArray.Parse(json);
                var resultTimeEntries = new List<TicketTimeEntry>();

                foreach (JObject obj in arr.Children<JObject>())
                {
                    resultTimeEntries.Add(new TicketTimeEntry(obj));
                }

                return resultTimeEntries.AsReadOnly();
            }
            else if (genericType == typeof(Agent))
                return new Agent(json);
            else if (genericType == typeof(IList<Agent>))
            {
                JArray arr = JArray.Parse(json);
                var resultAgents = new List<Agent>();

                foreach (JObject obj in arr.Children<JObject>())
                {
                    resultAgents.Add(new Agent(obj));
                }

                return resultAgents.AsReadOnly();
            }

            throw new NotSupportedException("FreshHttpsHelper.DoRequest<T>: Type '" + genericType.Name + "' is not supported for deserialization.");
        }

        /// <summary>
        /// Performs a standard HTTP GET request to the specified URI.
        /// </summary>
        /// <param name="uri">The URI of the target.</param>
        /// <returns>The response from the remote host.</returns>
        public static async Task<string> DoRequest(Uri uri)
        {
            return await DoRequest(uri, "GET", null);
        }

        /// <summary>
        /// Performs a HTTP request to the specified URI.
        /// </summary>
        /// <param name="uri">The URI of the target.</param>
        /// <param name="method">The HTTP request method to use.</param>
        /// <param name="body">The request body.</param>
        /// <returns>The response from the remote host.</returns>
        public static async Task<string> DoRequest(Uri uri, string method, string body)
        {
            string result = string.Empty;
            WebRequest request = await SetupRequest(method, uri);

            if (body != null)
            {
                byte[] data = Encoding.GetBytes(body);

                request.ContentLength = data.Length;

                // Begin the async request
                using (Stream s = await request.GetRequestStreamAsync())
                {
                    s.Write(data, 0, data.Length);
                }
            }

            using (WebResponse response = await request.GetResponseAsync())
            {
                result = GetResponseAsString(response);
            }

            return result;
        }
        
        /// <summary>
        /// Builds a URI using the specified parameters.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="path">The URI path.</param>
        /// <param name="query">The URI query string.</param>
        /// <returns>The final constructed Uri object.</returns>
        public static Uri UriForPath(Uri baseUri, string path, string query = null)
        {
            UriBuilder uriBuilder = new UriBuilder(baseUri);
            uriBuilder.Path = path;

            if (!string.IsNullOrEmpty(query))
            {
                uriBuilder.Query = query;
            }

            return uriBuilder.Uri;
        }


        private static async Task<string> GetAuthorizationHeader()
        {
            if (_AuthorizationKey == null)
                throw new NullReferenceException("FreshHttpHelper.GetAuthorizationHeader: No authorization key has been set.");

            return "Basic " + await Task.Run(() => Convert.ToBase64String(Encoding.Default.GetBytes(_AuthorizationKey + ":" + "X")));
        }

        private static string GetResponseAsString(WebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding))
            {
                return sr.ReadToEnd();
            }
        }

        private static Dictionary<string, string> GetStringsContent(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException("FreshHttpHelper.GetStringsContent: Parameter 'instance' cannot be null.");

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

                if (propertyName == null)
                {
                    propertyName = propertyInfo.Name;
                }

                properties[propertyName] = propertyValue.ToString();
            }
            return properties;
        }

        private static WebRequest SetupMultipartRequest(Uri uri)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            webRequest.Headers.Clear();

            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader().Result;

            return webRequest;
        }

        private static async Task<WebRequest> SetupRequest(string method, Uri uri)
        {
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = method;
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;

            if (httpRequest != null)
            {
                httpRequest.UserAgent = UserAgent;
            }

            webRequest.Headers["Authorization"] =  await GetAuthorizationHeader();

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

        private static void WriteBoundaryBytes(Stream requestStream, string b, bool isFinalBoundary)
        {
            string boundary = isFinalBoundary ? "--" + b + "--" : "--" + b + "\r\n";
            byte[] d = Encoding.UTF8.GetBytes(boundary);
            requestStream.Write(d, 0, d.Length);
        }

        private static void WriteContentDispositionFormDataHeader(Stream requestStream, string name)
        {
            string data = "Content-Disposition: form-data; name=\"" + name + "\"\r\n\r\n";
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }

        private static void WriteContentDispositionFileHeader(Stream requestStream, string name, string fileName, string contentType)
        {
            string data = "Content-Disposition: form-data; name=\"" + name + "\"; filename=\"" + fileName + "\"\r\n";
            data += "Content-Type: " + contentType + "\r\n\r\n";
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }

        private static void WriteCrlf(Stream requestStream)
        {
            byte[] crLf = Encoding.UTF8.GetBytes("\r\n");
            requestStream.Write(crLf, 0, crLf.Length);
        }

        private static void WriteString(Stream requestStream, string data)
        {
            byte[] b = Encoding.UTF8.GetBytes(data);
            requestStream.Write(b, 0, b.Length);
        }
    }
}
