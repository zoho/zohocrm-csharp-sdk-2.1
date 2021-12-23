
using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Text;

using Com.Zoho.API.Exception;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.API.Authenticator.Store
{
    /// <summary>
    /// This class stores the user token details to the file.
    /// </summary>
    public class FileStore : TokenStore
    {
        string filePath;

        List<string> headers = new List<string>() { Constants.ID, Constants.USER_MAIL, Constants.CLIENT_ID, Constants.CLIENT_SECRET, Constants.REFRESH_TOKEN, Constants.ACCESS_TOKEN, Constants.GRANT_TOKEN, Constants.EXPIRY_TIME, Constants.REDIRECT_URL };

        /// <summary>
        /// Creates an FileStore class instance with the specified parameters.
        /// </summary>
        /// <param name="filePath">A String containing the absolute file path to store tokens.</param>
        public FileStore(string filePath)
        {
            this.filePath = filePath;

            string[] lines = null;

            if (File.Exists(this.filePath))
            {
                lines = File.ReadAllLines(this.filePath);
            }

            if (lines == null || lines.Length < 1)
            {
                using (var fileStream = new FileStream(this.filePath, FileMode.Append))
                {
                    using (var writer = new StreamWriter(fileStream))
                    {
                        writer.WriteLine(string.Join(",", headers));

                        writer.Close();
                    }

                    fileStream.Close();
                }
            }
        }

        public Token GetToken(UserSignature user, Token token)
        {
            try
            {
                var allContents = File.ReadAllLines(filePath);

                if (allContents == null || allContents.Length < 1)
                {
                    return null;
                }

                if (token is OAuthToken oauthToken)
                {
                    foreach (var line in allContents)
                    {
                        var nextRecord = line.Split(',');

                        if (CheckTokenExists(user.Email, oauthToken, nextRecord))
                        {
                            var grantToken = !string.IsNullOrEmpty(nextRecord[6]) ? nextRecord[6] : null;

                            var redirectURL = !string.IsNullOrEmpty(nextRecord[8]) ? nextRecord[8] : null;

                            oauthToken.Id = nextRecord[0];

                            oauthToken.UserMail = nextRecord[1];

                            oauthToken.ClientId = nextRecord[2];

                            oauthToken.ClientSecret = nextRecord[3];

                            oauthToken.RefreshToken = nextRecord[4];

                            oauthToken.AccessToken = nextRecord[5];

                            oauthToken.GrantToken = grantToken;

                            oauthToken.ExpiresIn = nextRecord[7];

                            oauthToken.RedirectURL = redirectURL;

                            return oauthToken;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.GET_TOKEN_FILE_ERROR, ex);
            }

            return null;
        }

        public void SaveToken(UserSignature user, Token token)
        {
            try
            {
                List<string> data = null;

                if (token is OAuthToken oauthToken)
                {
                    oauthToken.UserMail = user.Email;

                    DeleteToken(oauthToken);

                    data = new List<string>
                    {
                        oauthToken.Id,

                        user.Email,

                        oauthToken.ClientId,

                        oauthToken.ClientSecret,

                        oauthToken.RefreshToken,

                        oauthToken.AccessToken,

                        oauthToken.GrantToken,

                        oauthToken.ExpiresIn,

                        oauthToken.RedirectURL
                    };
                }

                using (var outFile = new FileStream(filePath, FileMode.Append))
                {
                    using (var writer = new StreamWriter(outFile))
                    {
                        writer.WriteLine(string.Join(",", data));

                        writer.Close();
                    }

                    outFile.Close();
                }
            }
            catch (System.Exception ex) when (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException)
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.SAVE_TOKEN_FILE_ERROR, ex);
            }
        }

        public void DeleteToken(Token token)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                if (lines == null || lines.Length < 1)
                {
                    return;
                }

                File.WriteAllText(filePath, string.Empty);

                var csvData = new StringBuilder();

                if (token is OAuthToken oauthToken)
                {
                    var allContents = lines.ToList();

                    foreach (var value in allContents)
                    {
                        var nextRecord = value.Split(',');

                        if (!CheckTokenExists(oauthToken.UserMail, oauthToken, nextRecord))
                        {
                            csvData.Append(string.Join(",", nextRecord));

                            csvData.Append("\n");
                        }
                    }
                }

                File.WriteAllText(filePath, csvData.ToString());
            }
            catch (System.Exception ex) when (!(ex is SDKException) && (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException))
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.DELETE_TOKEN_FILE_ERROR, ex);
            }
        }

        public List<Token> GetTokens()
        {
            var tokens = new List<Token>();

            try
            {
                var allContents = File.ReadAllLines(filePath);

                if (allContents == null || allContents.Length < 1)
                {
                    return null;
                }

                for (var index = 1; index < allContents.Length; index++)
                {
                    var line = allContents[index];

                    var nextRecord = line.Split(',');

                    var grantToken = !string.IsNullOrEmpty(nextRecord[6]) ? nextRecord[6] : null;

                    var redirectURL = !string.IsNullOrEmpty(nextRecord[8]) ? nextRecord[8] : null;

                    var token = new OAuthToken.Builder().ClientId(nextRecord[2]).ClientSecret(nextRecord[3]).RefreshToken(nextRecord[4]).Build();

                    token.Id = nextRecord[0];

                    token.UserMail = nextRecord[1];

                    token.AccessToken = nextRecord[5];

                    token.ExpiresIn = nextRecord[7];

                    token.RedirectURL = redirectURL;

                    token.GrantToken = grantToken;

                    tokens.Add(token);
                }
            }
            catch (System.Exception ex)
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.GET_TOKENS_FILE_ERROR, ex);
            }

            return tokens;
        }

        public void DeleteTokens()
        {
            try
            {
                File.WriteAllText(filePath, string.Empty);

                File.WriteAllText(filePath, string.Join(",", headers));
            }
            catch (System.Exception ex) when (ex is UnauthorizedAccessException || ex is DirectoryNotFoundException)
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.DELETE_TOKENS_FILE_ERROR, ex);
            }
        }

        bool CheckTokenExists(string email, OAuthToken oauthToken, string[] row)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new SDKException(Constants.USER_MAIL_NULL_ERROR, Constants.USER_MAIL_NULL_ERROR_MESSAGE);
            }

            var clientId = oauthToken.ClientId;

            var grantToken = oauthToken.GrantToken;

            var refreshToken = oauthToken.RefreshToken;

            var tokenCheck = grantToken != null ? grantToken.Equals(row[6]) : refreshToken.Equals(row[4]);

            if(row[1].Equals(email) && row[2].Equals(clientId) && tokenCheck)
            {
                return true;
            }

            return false;
        }

        public Token GetTokenById(string id, Token token)
        {
            try
            {
                var allContents = File.ReadAllLines(filePath);

                var isRowPresent = false;

                if (token is OAuthToken oauthToken)
                {
                    foreach (var line in allContents)
                    {
                        var nextRecord = line.Split(',');

                        if (nextRecord[0].Equals(id))
                        {
                            isRowPresent = true;

                            var grantToken = !string.IsNullOrEmpty(nextRecord[6]) ? nextRecord[6] : null;

                            var redirectURL = !string.IsNullOrEmpty(nextRecord[8]) ? nextRecord[8] : null;

                            oauthToken.Id = id;

                            oauthToken.UserMail = nextRecord[1];
                            
                            oauthToken.ClientId = nextRecord[2];

                            oauthToken.ClientSecret = nextRecord[3];

                            oauthToken.RefreshToken = nextRecord[4];

                            oauthToken.AccessToken = nextRecord[5];

                            oauthToken.GrantToken = grantToken;

                            oauthToken.ExpiresIn = nextRecord[7];

                            oauthToken.RedirectURL = redirectURL;

                            return oauthToken;
                        }
                    }

                    if(!isRowPresent)
                    {
                        throw new SDKException(Constants.TOKEN_STORE, Constants.GET_TOKEN_BY_ID_FILE_ERROR);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new SDKException(Constants.TOKEN_STORE, Constants.GET_TOKEN_FILE_ERROR, ex);
            }

            return null;
        }
    }
}
