#r "..\Bin\Meta.dll"
#r "..\Bin\Utility.dll"

#r "..\Packages\Common.Logging.2.0.0\lib\2.0\Common.Logging.dll"
#r "..\Packages\Spring.Rest.1.1.1\lib\net40-client\Spring.Rest.dll"
#r "..\Packages\Spring.Social.Core.1.0.1\lib\net40-client\Spring.Social.Core.dll"
#r "..\Packages\Spring.Social.Twitter.2.0.0-M1\lib\net40-client\Spring.Social.Twitter.dll"
#r "..\Packages\CredentialManagement.1.0.1\lib\net35\CredentialManagement.dll"

using Nake;

using System;
using System.Collections.Specialized;
using System.Diagnostics;

using Spring.Social.OAuth1;
using Spring.Social.Twitter.Connect;
using Spring.Social.Twitter.Api.Impl;

using CredentialManagement;

const string consumerKey = "5x0lJ86laE9cjIIKOk9dA";
const string consumerSecret = "MPufQjef2UUQaLt0BIc525gFUPzMcVxWr62ap41Ro";

/// <summary> 
/// Logs into twitter
/// </summary>
[Task] public static void Login()
{
    if (AccessToken.Exists())
    {
        Console.Write("Twitter is already authorized. Re-login? (y/n): ");

        if (Console.ReadKey().KeyChar != 'y')
            return;
    }

    var oauth = new TwitterServiceProvider(consumerKey, consumerSecret).OAuthOperations;

    var request = oauth.FetchRequestTokenAsync("", new NameValueCollection()).Result;
    Process.Start(oauth.BuildAuthorizeUrl(request.Value, new OAuth1Parameters()));

    Console.Write("\nPlease enter PIN: ");
    var pin = Console.ReadLine();

    var authorized = new AuthorizedRequestToken(request, pin);
    AccessToken.Store(oauth.ExchangeForAccessTokenAsync(authorized, new NameValueCollection()).Result);

    Log.Message("Logged in successfully!");
}

/// <summary> 
/// Logs out of twitter
/// </summary>
[Task] public static void Logout()
{
    if (!AccessToken.Exists())
        throw new ApplicationException("Hasn't been logged in before");

    AccessToken.Delete();
}

static TwitterTemplate Api()
{
    if (!AccessToken.Exists())
        throw new ApplicationException("Please first authorize with twitter by using 'login' command!");

    var token = AccessToken.Retrieve();

    return new TwitterTemplate(
        consumerKey,
        consumerSecret,
        token.Value,
        token.Secret
    );
}

class AccessToken
{
    const string CredentialName = "Nake-tweet-cli";

    public readonly string Value;
    public readonly string Secret;

    public AccessToken(string value, string secret)
    {
        this.Value = value;
        this.Secret = secret;
    }

    public static bool Exists()
    {
        return new Credential { Target = CredentialName }.Exists();
    }

    public static AccessToken Retrieve()
    {
        var cm = new Credential { Target = CredentialName }; cm.Load();
        return new AccessToken(cm.Username, cm.Password);
    }

    public static AccessToken Store(OAuthToken oauth)
    {
        new Credential
        {
            Target = CredentialName,
            PersistanceType = PersistanceType.LocalComputer,
            Username = oauth.Value,
            Password = oauth.Secret
        }
        .Save();

        return new AccessToken(oauth.Value, oauth.Secret);
    }

    public static void Delete()
    {
        new Credential { Target = CredentialName }.Delete();
    }
}