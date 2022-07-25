using System.IO;
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using CSharpFunctionalExtensions;

namespace ZohoCRM.SDK_2_1.Core;

public class ClientId : SimpleValueObject<string>
{
    public ClientId(string value) : base(value)
    {
    }
}

public class ClientSecret : SimpleValueObject<string>
{
    public ClientSecret(string value) : base(value)
    {
    }
}

public class GrantToken : SimpleValueObject<string>
{
    public GrantToken(string value) : base(value)
    {
    }
}

public class SdkLogFileName : SimpleValueObject<string>
{
    public SdkLogFileName(string value) : base(value)
    {
    }
}

public class UserEmail : SimpleValueObject<string>
{
    public UserEmail(string value) : base(value)
    {
    }
}

public class ResourcesDirectory : SimpleValueObject<string>
{
    public ResourcesDirectory(string value) : base(value)
    {
    }
}

public class TokenStorePath : SimpleValueObject<string>
{
    public TokenStorePath(string value) : base(value)
    {
    }
}

public static class Initialize
{
    public static bool IsInitialized { get; private set; }

    public static void SdkInitialize(ClientId clientId, ClientSecret clientSecret, GrantToken grantToken, SdkLogFileName sdkLogFileName, UserEmail userEmail, ResourcesDirectory resourceDirectoryName,
        TokenStorePath tokenStorePath)
    {
        if (IsInitialized) return;
        /*
    * Create an instance of Logger Class that requires the following
    * Level -> Level of the log messages to be logged. Can be configured by typing Levels "." and choose any level from the list displayed.
    * FilePath -> Absolute file path, where messages need to be logged.
    */
        var logger = new Logger.Builder()
            .Level(Logger.Levels.ALL)
            .FilePath(sdkLogFileName)
            .Build();

        //Create an UserSignature instance that takes user Email as parameter
        var user = new UserSignature(userEmail);

        /*
    * Configure the environment
    * which is of the pattern Domain.Environment
    * Available Domains: USDataCenter, EUDataCenter, INDataCenter, CNDataCenter, AUDataCenter
    * Available Environments: PRODUCTION, DEVELOPER, SANDBOX
    */
        var environment = USDataCenter.PRODUCTION;

        /*
    * Create a Token instance
    * ClientId -> OAuth client id.
    * ClientSecret -> OAuth client secret.
    * GrantToken -> GRANT token.
    * RedirectURL -> OAuth redirect URL.
    */
        var token = new OAuthToken.Builder()
            .ClientId(clientId)
            .ClientSecret(clientSecret)
            .GrantToken(grantToken)
            //.RedirectURL("redirectURL")
            .Build();

        /*
    * TokenStore can be any of the following
    * DB Persistence - Create an instance of DBStore
    * File Persistence - Create an instance of FileStore
    * Custom Persistence - Create an instance of CustomStore
    */

        /*
    * Create an instance of DBStore.
    * Host -> DataBase host name. Default "localhost"
    * DatabaseName -> DataBase name. Default "zohooauth"
    * UserName -> DataBase user name. Default "root"
    * Password -> DataBase password. Default ""
    * PortNumber -> DataBase port number. Default "3306"
    * TableName -> Table Name. Default value "oauthtoken"
    */
        //TokenStore tokenstore = new DBStore.Builder().Build();

        TokenStore tokenstore = new FileStore(tokenStorePath); //"tokenstore.path");
        // DBStore.Builder()
        // .Host("hostName")
        // .DatabaseName("dataBaseName")
        // .TableName("tableName")
        // .UserName("userName")
        // .Password("password")
        // .PortNumber("portNumber")
        // .Build();

        // TokenStore tokenstore = new FileStore("absolute_file_path");

        /*
    * autoRefreshFields
    * if true - all the modules' fields will be auto-refreshed in the background, every    hour.
    * if false - the fields will not be auto-refreshed in the background. The user can manually delete the file(s) or refresh the fields using methods from ModuleFieldsHandler(com.zoho.crm.api.util.ModuleFieldsHandler)
    *
    * pickListValidation
    * if true - value for any picklist field will be validated with the available values.
    * if false - value for any picklist field will not be validated, resulting in creation of a new value.
    */
        var config = new SDKConfig.Builder()
            .AutoRefreshFields(false)
            .PickListValidation(false)
            .Timeout(10)
            .Build();

        // var resourcePath = "csharpsdk-application";

        // if (!Directory.Exists(resourcePath))
        Directory.CreateDirectory(resourceDirectoryName);

        // /**
        // * Create an instance of RequestProxy class that takes the following parameters
        // * Host -> Host
        // * Port -> Port Number
        // * User -> User Name
        // * Password -> Password
        // * UserDomain -> User Domain
        // */
        // var requestProxy = new RequestProxy.Builder()
        // .Host("proxyHost")
        // .Port(proxyPort)
        // .User("proxyUser")
        // .Password("password")
        // .UserDomain("userDomain")
        // .Build();

        /*
    * The initialize method of Initializer class that takes the following arguments
    * User -> UserSignature instance
    * Environment -> Environment instance
    * Token -> Token instance
    * Store -> TokenStore instance
    * SDKConfig -> SDKConfig instance
    * ResourcePath -> resourcePath -A String
    * Logger -> Logger instance
    * RequestProxy -> RequestProxy instance
    */

        // Set the following in InitializeBuilder
        new Initializer.Builder()
            .User(user)
            .Environment(environment)
            .Token(token)
            .Store(tokenstore)
            .SDKConfig(config)
            .ResourcePath(resourceDirectoryName)
            .Logger(logger)
            // .RequestProxy(requestProxy)
            .Initialize();

        IsInitialized = true;
    }
}