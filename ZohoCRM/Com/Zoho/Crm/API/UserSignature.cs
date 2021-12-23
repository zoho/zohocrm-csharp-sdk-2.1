using System;

using System.Text.RegularExpressions;

using Com.Zoho.API.Exception;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json.Linq;

namespace Com.Zoho.Crm.API
{
    /// <summary>
    /// This class represents the CRM user email.
    /// </summary>
    public class UserSignature
    {
        string email;

        /// <summary>
        /// Creates an User class instance with the specified user email.
        /// </summary>
        /// <param name="email">A String containing the CRM user email</param>
        public UserSignature(string email)
        {
            var isEmail = Regex.IsMatch(email, Constants.EMAIL_REGEX, RegexOptions.IgnoreCase);

            if (!isEmail)
            {
                var error = new JObject
                {
                    { Constants.FIELD, Constants.EMAIL },
                    { Constants.EXPECTED_TYPE, Constants.EMAIL }
                };

                throw new SDKException(Constants.USER_SIGNATURE_ERROR, error);
            }

            this.email = email;
        }

        /// <summary>
        /// This is a getter method to get user email.
        /// </summary>
        /// <returns>A String representing the CRM user email.</returns>
        public string Email
        {
            get
            {
                return email;
            }
        }
    }
}
