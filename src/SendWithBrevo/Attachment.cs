using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SendWithBrevo
{
    /// <summary>
    /// Attachment.
    /// </summary>
    public class Attachment
    {
        #region Public-Members

        /// <summary>
        /// URL to content.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = null;

        /// <summary>
        /// Content.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = null;

        /// <summary>
        /// Filename.
        /// </summary>
        [JsonPropertyName("name")]
        public string Filename { get; set; } = null;

        #endregion

        #region Private-Members

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Attachment()
        {

        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="filename">Filename.</param>
        /// <param name="content">Content.</param>
        /// <param name="url">URL.</param>
        public Attachment(string filename, string content, string url)
        {
            if (String.IsNullOrEmpty(filename)) throw new ArgumentNullException(nameof(filename));
            if (String.IsNullOrEmpty(url) && String.IsNullOrEmpty(content)) throw new ArgumentException("Either content or URL must be supplied.");
            if (!String.IsNullOrEmpty(url) && !String.IsNullOrEmpty(content)) throw new ArgumentException("Only one of content and URL must be supplied.");

            Filename = filename;
            Url = url;
            Content = content;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
