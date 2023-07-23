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
    /// Sender.
    /// </summary>
    public class Sender
    {
        #region Public-Members

        /// <summary>
        /// Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Name));
                _Name = value;
            }
        }

        /// <summary>
        /// Email address.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Email));
                _Email = value;
            }
        }

        #endregion

        #region Private-Members

        private string _Name = null;
        private string _Email = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public Sender()
        {
        }

        /// <summary>
        /// Instantiate.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="email">Email.</param>
        public Sender(string name, string email)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            _Name = name;
            _Email = email;
        }

        #endregion

        #region Public-Methods

        #endregion

        #region Private-Methods

        #endregion
    }
}
