using System;

using Com.Zoho.API.Authenticator;

using Com.Zoho.API.Authenticator.Store;

using Com.Zoho.Crm.API;

using Com.Zoho.Crm.API.Dc;

using Com.Zoho.Crm.API.Logger;

using Newtonsoft.Json;

using static Com.Zoho.API.Authenticator.OAuthToken;

using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

using SDKInitializer = Com.Zoho.Crm.API.Initializer;

namespace Com.Zoho.Crm.Sample.Initializer
{
    public class Initialize
    {
		public static void SDKInitialize()
		{
			/*
			 * Create an instance of Logger Class that takes two parameters
			 * 1 -> Level of the log messages to be logged. Can be configured by typing Levels "." and choose any level from the list displayed.
			 * 2 -> Absolute file path, where messages need to be logged.
			 */
			Logger logger = new Logger.Builder()
				.Level(Logger.Levels.ALL)
				.FilePath("/Users/Documents/csharp_sdk_log.log")
				.Build();
		
		    //Create an UserSignature instance that takes user Email as parameter
		    UserSignature user = new UserSignature("abc@zoho.com");

			/*
		     * Configure the environment
		     * which is of the pattern Domain.Environment
		     * Available Domains: USDataCenter, EUDataCenter, INDataCenter, CNDataCenter, AUDataCenter
		     * Available Environments: PRODUCTION, DEVELOPER, SANDBOX
		     */
			Environment environment = USDataCenter.PRODUCTION;

            /*
		     * Create a Token instance
		     * 1 -> OAuth client id.
		     * 2 -> OAuth client secret.
		     * 3 -> OAuth redirect URL.
		     * 4 -> REFRESH/GRANT token.
		     * 5 -> token type.
		     */
            Token token = new OAuthToken.Builder()
                //.Id("csharp_abc_us_prd_aec1")
                .ClientId("ClientId")
                .ClientSecret("ClientSecret")
                .GrantToken("GrantToken")
                .RefreshToken("RefreshToken")
                .RedirectURL("RedirectURL")
				.AccessToken("AccessToken")
                .Build();

            /*
		     * Create an instance of TokenStore.
		     * 1 -> DataBase host name. Default "localhost"
		     * 2 -> DataBase name. Default "zohooauth"
		     * 3 -> DataBase user name. Default "root"
		     * 4 -> DataBase password. Default ""
		     * 5 -> DataBase port number. Default "3306"
		     */
            //TokenStore tokenstore = new DBStore.Builder().Build();

        //    TokenStore tokenstore = new DBStore.Builder()
        //      .Host("Host")
		// 		.DatabaseName("DatabaseName")
        //      .TableName("TableName")
		// 		.UserName("UserName")
        //      .Password("Password")
		// 		.PortNumber("PortNumber")
        //      .Build();

            TokenStore tokenstore = new FileStore("/Users/Documents/csharp_sdk_token.txt");

            /*
            * autoRefreshFields
            * if true - all the modules' fields will be auto-refreshed in the background, every    hour.
            * if false - the fields will not be auto-refreshed in the background. The user can manually delete the file(s) or refresh the fields using methods from ModuleFieldsHandler(com.zoho.crm.api.util.ModuleFieldsHandler)
            * 
            * pickListValidation
            * if true - value for any picklist field will be validated with the available values.
            * if false - value for any picklist field will not be validated, resulting in creation of a new value.
            */
            SDKConfig config = new SDKConfig.Builder()
			.AutoRefreshFields(true)
			.PickListValidation(false)
			.Build();

			string resourcePath = "/Users/Documents";

            /**
			 * Create an instance of RequestProxy class that takes the following parameters
			 * 1 -> Host
			 * 2 -> Port Number
			 * 3 -> User Name
			 * 4 -> Password
			 * 5 -> User Domain
			 */
            RequestProxy requestProxy = new RequestProxy.Builder()
				.Host("Host")
				.Port(Port)
				.User("User")
				.Password("")
				.UserDomain("UserDomain")
				.Build();
            /*
		     * Call static initialize method of Initializer class that takes the arguments
		     * 1 -> UserSignature instance
		     * 2 -> Environment instance
		     * 3 -> Token instance
		     * 4 -> TokenStore instance
		     * 5 -> SDKConnfig 
		     * 6 -> The path containing the absolute directory path to store user specific JSON files containing module fields information.
		     * 7 -> Logger instance
		     * 8 -> RequestProxy instance
		     */
            // The following are the available initialize methods
            new SDKInitializer.Builder()
				.User(user)
				.Environment(environment)
				.Token(token)
				.Store(tokenstore)
				.SDKConfig(config)
				.ResourcePath(resourcePath)
				.Logger(logger)
				.Initialize();

            // foreach (Token token1 in ((DBStore)tokenstore).GetTokens())
            // {
            //    OAuthToken authToken = (OAuthToken)token1;

            //    Console.WriteLine(JsonConvert.SerializeObject(authToken));
            // }
        }
	}
}
