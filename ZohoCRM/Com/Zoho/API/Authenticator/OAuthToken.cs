using System;

using System.Collections.Generic;

using System.IO;

using System.Net;

using System.Text;

using Com.Zoho.API.Authenticator.Store;

using Com.Zoho.API.Exception;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.Logger;

using Com.Zoho.Crm.API.Util;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;

namespace Com.Zoho.API.Authenticator
{
    /// <summary>
    /// This class gets the tokens and checks the expiry time.
    /// </summary>
    public class OAuthToken : Token
    {
        public class Builder
        {
            string clientId;

            string clientSecret;

            string redirectURL;

            string refreshToken;

            string grantToken;

            string accessToken;

            string id;

            public Builder Id(string id)
            {
                this.id = id;

                return this;
            }

            public Builder ClientId(string clientId)
            {
                Utility.AssertNotNull(clientId, Constants.TOKEN_ERROR, Constants.CLIENT_ID_NULL_ERROR_MESSAGE);

                this.clientId = clientId;

                return this;
            }

            public Builder ClientSecret(string clientSecret)
            {
                Utility.AssertNotNull(clientSecret, Constants.TOKEN_ERROR, Constants.CLIENT_SECRET_NULL_ERROR_MESSAGE);

                this.clientSecret = clientSecret;

                return this;
            }

            public Builder RedirectURL(string redirectURL)
            {
                this.redirectURL = redirectURL;

                return this;
            }

            public Builder RefreshToken(string refreshToken)
            {
                this.refreshToken = refreshToken;

                return this;
            }

            public Builder GrantToken(string grantToken)
            {
                this.grantToken = grantToken;

                return this;
            }

            public Builder AccessToken(string accessToken)
            {
                this.accessToken = accessToken;

                return this;
            }

            public OAuthToken Build()
            {
                if (grantToken == null && refreshToken == null && id == null && accessToken == null)
                {
                    throw new SDKException(Constants.MANDATORY_VALUE_ERROR, Constants.MANDATORY_KEY_ERROR + "-" + JsonConvert.SerializeObject(Constants.OAUTH_MANDATORY_KEYS));
                }

                return new OAuthToken(clientId, clientSecret, grantToken, refreshToken, redirectURL, id, accessToken);
            }
        }

        string clientID;

        string clientSecret;

        string redirectURL;

        string grantToken;

        string refreshToken;

        string accessToken;

        string expiresIn;

        string userMail;

        string id;

        /// <summary>
        /// This is a getter method to get OAuth client id.
        /// </summary>
        /// <returns>A string representing the OAuth client id.</returns>
        public string ClientId
        {
            get
            {
                return clientID;
            }
            set
            {
                clientID = value;
            }
        }

        /// <summary>
        /// This is a getter method to get OAuth client secret.
        /// </summary>
        /// <returns>A string representing the OAuth client secret.</returns>
        public string ClientSecret
        {
            get
            {
                return clientSecret;
            }
            set
            {
                clientSecret = value;
            }
        }

        /// <summary>
        /// This is a getter method to get OAuth redirect URL.
        /// </summary>
        /// <returns>A string representing the OAuth redirect URL.</returns>
        public string RedirectURL
        {
            get
            {
                return redirectURL;
            }
            set
            {
                redirectURL = value;
            }
        }

        /// <summary>
        /// This is a getter method to get grant token.
        /// </summary>
        /// <returns>A string representing the grant token.</returns>
        public string GrantToken
        {
            get
            {
                return grantToken;
            }
            set
            {
                grantToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>A string containing the refresh token.</value>
        /// <returns>A string representing the refresh token.</returns>
        public string RefreshToken
        {
            get
            {
                return refreshToken;
            }
            set
            {
                refreshToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>A string containing the access token.</value>
        /// <returns>A string representing the access token.</returns>
        public string AccessToken
        {
            get
            {
                return accessToken;
            }
            set
            {
                accessToken = value;
            }
        }

        /// <summary>
        /// Gets or sets the token expire time.
        /// </summary>
        /// <value>A string containing the token expire time.</value>
        /// <returns>A string representing the token expire time.</returns>
        public string ExpiresIn
        {
            get
            {
                return expiresIn;
            }
            set
            {
                expiresIn = value;
            }
        }

        public string UserMail
        {
            get
            {
                return userMail;
            }
            set
            {
                userMail = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void Authenticate(APIHTTPConnector urlConnection)
        {
            lock (this)
            {
                try
                {
                    var initializer = Initializer.GetInitializer();

                    var store = initializer.Store;

                    var user = initializer.User;

                    OAuthToken oauthToken = null;

                    if (accessToken == null)
                    {
                        if (id != null)
                        {
                            oauthToken = (OAuthToken)store.GetTokenById(id, this);
                        }
                        else
                        {
                            oauthToken = (OAuthToken)store.GetToken(user, this);
                        }
                    }
                    else
                    {
                        oauthToken = this;
                    }

                    var token = "";

                    if (oauthToken == null)//first time
                    {
                        token = refreshToken != null ? RefreshAccessToken(user, store).AccessToken : GenerateAccessToken(user, store).AccessToken;
                    }
                    else if (oauthToken.ExpiresIn != null && GetExpiryLapseInMillis(oauthToken.ExpiresIn) < 5L)//access token will expire in next 5 seconds or less
                    {
                        SDKLogger.LogInfo(Constants.REFRESH_TOKEN_MESSAGE);

                        token = oauthToken.RefreshAccessToken(user, store).AccessToken;
                    }
                    else
                    {
                        token = oauthToken.AccessToken;
                    }

                    urlConnection.AddHeader(Constants.AUTHORIZATION, Constants.OAUTH_HEADER_PREFIX + token);

                }
                catch (System.Exception ex) when (!(ex is SDKException))
                {
                    throw new SDKException(ex);
                }
            }
        }

        string GetResponseFromServer(Dictionary<string, string> requestParams)
        {
            try
            {
                var USER_AGENT = Constants.USER_AGENT;

                var url = Initializer.GetInitializer().Environment.GetAccountsUrl();

                var request = (HttpWebRequest)WebRequest.Create(url);

                string urlParameters = null;

                if (requestParams != null && requestParams.Count != 0)
                {
                    foreach (var param in requestParams)
                    {
                        if (urlParameters == null)
                        {
                            urlParameters = param.Key + "=" + param.Value;
                        }
                        else
                        {
                            urlParameters += "&" + param.Key + "=" + param.Value;
                        }
                    }
                }

                request.UserAgent = USER_AGENT;

                var data = Encoding.UTF8.GetBytes(urlParameters);

                request.ContentType = Constants.URL_ENCODEED;

                request.ContentLength = data.Length;

                request.Method = Constants.REQUEST_METHOD_POST;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                return new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (System.Exception ex)
            {
                throw new SDKException(ex);
            }
        }

        OAuthToken RefreshAccessToken(UserSignature user, TokenStore store)
        {
            try
            {
                var requestParams = new Dictionary<string, string>
                {
                    { Constants.CLIENT_ID, ClientId },
                    { Constants.CLIENT_SECRET, ClientSecret },
                    { Constants.GRANT_TYPE, Constants.REFRESH_TOKEN },
                    { Constants.REFRESH_TOKEN, RefreshToken }
                };

                var response = GetResponseFromServer(requestParams);

                ParseResponse(response);

                if(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                {
                    GenerateId();
                }

                store.SaveToken(user, this);
            }
            catch (System.Exception ex) when (!(ex is SDKException))
            {
                throw new SDKException(Constants.SAVE_TOKEN_ERROR, ex);
            }

            return this;
        }


        OAuthToken GenerateAccessToken(UserSignature user,TokenStore store)
        {
            try
            {
                var requestParams = new Dictionary<string, string>
                {
                    { Constants.CLIENT_ID, ClientId },
                    { Constants.CLIENT_SECRET, ClientSecret }
                };

                if (RedirectURL != null)
                {
                    requestParams.Add(Constants.REDIRECT_URI, RedirectURL);
                }

                requestParams.Add(Constants.GRANT_TYPE, Constants.GRANT_TYPE_AUTH_CODE);

                requestParams.Add(Constants.CODE, GrantToken);

                var response = GetResponseFromServer(requestParams);

                ParseResponse(response);
                
                GenerateId();

                store.SaveToken(user, this);
            }
            catch (System.Exception ex) when (!(ex is SDKException))
            {
                throw new SDKException(Constants.SAVE_TOKEN_ERROR, ex);
            }

            return this;
        }

        OAuthToken ParseResponse(string response)
        {
            var responseJSON = JObject.Parse(response);

            if (!responseJSON.ContainsKey(Constants.ACCESS_TOKEN))
            {
                throw new SDKException(Constants.INVALID_TOKEN_ERROR, (responseJSON.ContainsKey(Constants.ERROR_KEY))? responseJSON[Constants.ERROR_KEY].ToString() : Constants.NO_ACCESS_TOKEN_ERROR);
            }

            AccessToken = (string)responseJSON[Constants.ACCESS_TOKEN];

            ExpiresIn = GetTokenExpiresIn(responseJSON).ToString();

            if (responseJSON.ContainsKey(Constants.REFRESH_TOKEN))
            {
                refreshToken = (string)responseJSON[Constants.REFRESH_TOKEN];
            }

            return this;
        }

        long GetTokenExpiresIn(JObject response)
        {
            var expiresInTime = response.ContainsKey(Constants.EXPIRES_IN_SEC) ? Convert.ToInt64(response[Constants.EXPIRES_IN]) : Convert.ToInt64(response[Constants.EXPIRES_IN]) * 1000;

            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + expiresInTime - 600000;
        }

        public long GetExpiryLapseInMillis(string ExpiryTime)
        {
            var time = Convert.ToInt64(ExpiryTime) - (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            return time;
        }

        /// <summary>
        /// The method to remove the current token from the Store.
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            try
            {
                Initializer.GetInitializer().Store.DeleteToken(this);

                return true;
            }
            catch (System.Exception ex) when (!(ex is SDKException))
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates an OAuthToken class instance with the specified parameters.
        /// </summary>
        /// <param name="clientID">A string containing the OAuth client id.</param>
        /// <param name="clientSecret">A string containing the OAuth client secret.</param>
        /// <param name="token">A string containing the REFRESH/GRANT token.</param>
        /// <param name="type">A enum containing the given token type.</param>
        /// <param name="redirectURL">A string containing the OAuth redirect URL.</param>
        OAuthToken(string clientID, string clientSecret, string grantToken, string refreshToken, string redirectURL, string id, string accessToken)
        {
            this.clientID = clientID;

            this.clientSecret = clientSecret;

            this.grantToken = grantToken;

            this.refreshToken = refreshToken;

            this.redirectURL = redirectURL;

            this.accessToken = accessToken;

            this.id = id;
        }

        void GenerateId()
        {
            var builder = new StringBuilder();

            var email = Initializer.GetInitializer().User.Email;

            builder.Append(Constants.CSHARP).Append(email.Substring(0, email.IndexOf("@"))).Append("_");

            builder.Append(Initializer.GetInitializer().Environment.GetName()).Append("_");

            builder.Append(refreshToken.Substring(refreshToken.Length - 4));

            id = builder.ToString();
        }
    }
}
