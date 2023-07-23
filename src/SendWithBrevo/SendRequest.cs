using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SendWithBrevo
{
    /// <summary>
    /// Brevo send request.
    /// </summary>
    public class SendRequest
    {
        #region Public-Members

        /// <summary>
        /// Sender.
        /// </summary>
        [JsonPropertyName("sender")]
        public Sender Sender
        { 
            get
            {
                return _Sender;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(Sender));
                _Sender = value;
            }
        }

        /// <summary>
        /// Reply-to address.
        /// </summary>
        [JsonPropertyName("replyTo")]
        public Sender ReplyTo
        {
            get
            {
                if (_ReplyTo == null) return _Sender;
                return _ReplyTo;
            }
            set
            {
                _ReplyTo = value;
            }
        }

        /// <summary>
        /// Recipients on the To: line.
        /// </summary>
        [JsonPropertyName("to")]
        public List<Recipient> To
        {
            get
            {
                return _To;
            }
            set
            {
                if (value == null) _To = new List<Recipient>();
                else _To = value;
            }
        }

        /// <summary>
        /// Recipients on the CC: line.
        /// </summary>
        [JsonPropertyName("cc")]
        public List<Recipient> Cc { get; set; } = null;

        /// <summary>
        /// Recipients on the BCC: line.
        /// </summary>
        [JsonPropertyName("bcc")]
        public List<Recipient> Bcc { get; set; } = null;

        /// <summary>
        /// HTML content.
        /// </summary>
        [JsonPropertyName("htmlContent")]
        public string HtmlContent { get; set; } = null;

        /// <summary>
        /// Text content.
        /// </summary>
        [JsonPropertyName("textContent")]
        public string TextContent { get; set; } = null;

        /// <summary>
        /// Subject.
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = null;

        /// <summary>
        /// Attachments.
        /// </summary>
        [JsonPropertyName("attachment")]
        public List<Attachment> Attachments { get; set; } = null;

        /// <summary>
        /// Headers.
        /// </summary>
        [JsonPropertyName("headers")]
        public Dictionary<string, string> Headers { get; set; } = null;

        /// <summary>
        /// Parameters.
        /// </summary>
        [JsonPropertyName("params")]
        public Dictionary<string, string> Parameters { get; set; } = null;

        /// <summary>
        /// Template ID.
        /// </summary>
        [JsonPropertyName("templateId")]
        public object TemplateId { get; set; } = null;

        #endregion

        #region Private-Members

        private Sender _Sender = null;
        private Sender _ReplyTo = null;
        private List<Recipient> _To = new List<Recipient>();

        #endregion

        #region Constructors-and-Factories

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
