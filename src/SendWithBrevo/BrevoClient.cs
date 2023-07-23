using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestWrapper;

namespace SendWithBrevo
{
    /// <summary>
    /// Brevo client.
    /// </summary>
    public class BrevoClient
    {
        #region Public-Members

        /// <summary>
        /// Method to invoke to send log messages.
        /// </summary>
        public Action<string> Logger { get; set; } = null;

        /// <summary>
        /// Boolean to indicate whether request body should be included in log messages.
        /// </summary>
        public bool LogRequests { get; set; } = false;

        /// <summary>
        /// Boolean to indicate whether response body should be included in log messages.
        /// </summary>
        public bool LogResponses { get; set; } = true;

        /// <summary>
        /// Endpoint URL, e.g. https://api.brevo.com/v3/smtp/email
        /// </summary>
        public string Endpoint
        {
            get
            {
                return _Endpoint;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Endpoint));

                while (value.EndsWith("/")) value = value.Substring(0, (value.Length - 1));

                _EndpointUri = new Uri(value);
                _Endpoint = value;
            }
        }

        /// <summary>
        /// Brevo API key.
        /// </summary>
        public string ApiKey
        {
            get
            {
                return _ApiKey;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(ApiKey));

                _ApiKey = value;
            }
        }

        /// <summary>
        /// Serialization helper.
        /// </summary>
        public SerializationHelper Serializer
        {
            get
            {
                return _Serializer;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Serializer));
                _Serializer = value;
            }
        }

        #endregion

        #region Private-Members

        private string _Header = "[BrevoClient] ";
        private string _Endpoint = "https://api.brevo.com/v3/smtp/email";
        private Uri _EndpointUri = null;
        private string _ApiKey = null;
        private SerializationHelper _Serializer = new SerializationHelper();
        
        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="apiKey">Brevo API key.</param>
        /// <param name="endpoint">Endpoint URL, e.g. https://api.brevo.com/v3/smtp/email</param>
        public BrevoClient(
            string apiKey,
            string endpoint = "https://api.brevo.com/v3/smtp/email"
            )
        {
            if (String.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            if (String.IsNullOrEmpty(endpoint)) throw new ArgumentNullException(nameof(endpoint));

            _ApiKey = apiKey;
            _Endpoint = endpoint;
        }

        #endregion

        #region Public-Methods

        /// <summary>
        /// Send an email.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="to">List of recipients.</param>
        /// <param name="subject">Subject.</param>
        /// <param name="content">Message body.</param>
        /// <param name="isHtml">Boolean indicating if body is HTML.</param>
        /// <param name="cc">List of carbon copy recipients, or null.</param>
        /// <param name="bcc">List of blind carbon copy recipients, or null.</param>
        /// <param name="replyTo">Reply to, or sender if null.</param>
        /// <param name="headers">Additional headers.</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <param name="attachments">Attachments.</param>
        /// <param name="templateId">Template ID, if any.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Boolean indicating success.</returns>
        public async Task<bool> SendAsync(
            Sender sender,
            List<Recipient> to,
            string subject,
            string content,
            bool isHtml = false,
            List<Recipient> cc = null,
            List<Recipient> bcc = null,
            Sender replyTo = null,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null,
            List<Attachment> attachments = null,
            object templateId = null,
            CancellationToken token = default
            )
        {
            if (sender == null) throw new ArgumentNullException(nameof(sender));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (to.Count < 1) throw new ArgumentException("List of recipients must contain at least one entry.");
            if (String.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));
            if (String.IsNullOrEmpty(content)) throw new ArgumentNullException(nameof(content));

            if (cc != null && cc.Count < 1) cc = null;
            if (bcc != null && bcc.Count < 1) bcc = null;
            if (replyTo == null) replyTo = sender;
            if (headers != null && headers.Count < 1) headers = null;
            if (parameters != null && parameters.Count < 1) parameters = null;
            if (attachments != null && attachments.Count < 1) attachments = null;

            SendRequest sr = new SendRequest
            {
                Sender = sender,
                To = to,
                Subject = subject,
                Cc = cc,
                Bcc = bcc,
                ReplyTo = (replyTo != null ? replyTo : sender),
                Headers = headers,
                Parameters = parameters,
                Attachments = attachments,
                TemplateId = templateId
            };

            if (isHtml) sr.HtmlContent = content;
            else sr.TextContent = content;

            RestRequest req = new RestRequest(_Endpoint, HttpMethod.Post);
            req.ContentType = "application/json";

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("Accept", "application/json");
            nvc.Add("api-key", _ApiKey);

            req.Headers = nvc;

            string body = _Serializer.SerializeJson(sr, true);
            if (LogRequests) Logger?.Invoke(body);

            RestResponse resp = await req.SendAsync(body, token);
            if (resp != null && resp.StatusCode >= 200 && resp.StatusCode <= 299)
            {
                Logger?.Invoke(_Header + "message sent successfully, status " + resp.StatusCode);
                if (LogResponses) Logger?.Invoke(resp.DataAsString);
                return true;
            }
            else if (resp == null)
            {
                Logger?.Invoke(_Header + "unable to retrieve a response from " + _Endpoint);
                if (LogResponses) Logger?.Invoke(resp.DataAsString);
                return false;
            }
            else
            {
                Logger?.Invoke(_Header + "non-success status returned, status " + resp.StatusCode);
                return false;
            }
        }

        #endregion

        #region Private-Methods

        #endregion
    }
}