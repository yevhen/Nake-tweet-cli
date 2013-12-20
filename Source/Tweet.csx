#load "Auth.csx"

#r "..\Bin\Meta.dll"
#r "..\Bin\Utility.dll"

#r "..\Packages\Spring.Social.Core.1.0.1\lib\net40-client\Spring.Social.Core.dll"
#r "..\Packages\Spring.Social.Twitter.2.0.0-M1\lib\net40-client\Spring.Social.Twitter.dll"

using Nake;

using System;
using Spring.Social.Twitter.Api.Impl;

[Task] public static void Default()
{
    Log.Message("Run tweet -T to see all available commands");
}

/// <summary> 
/// Shows current twitter account name
/// </summary>
[Task] public static void WhoAmI()
{
    var profile = Twitter().UserOperations
        .GetUserProfileAsync()
        .Result;

    Log.Message("@{profile.ScreenName}");
}

/// <summary> 
/// Displays last N tweets from you timeline
/// </summary>
[Task] public static void Show(int count = 20)
{
    var tweets = Twitter().TimelineOperations
                          .GetHomeTimelineAsync(count)
                          .Result;

    foreach (var tweet in tweets)
    {
        Log.Info("\nFrom: {tweet.User.Name}");
        Log.Message(tweet.Text);
    }

    Console.WriteLine();
}

/// <summary> 
/// Posts new message to twitter but reads it from stdin, enabling use with scripting
/// </summary>
[Task] public static void Read()
{
    string text;
    while ((text = Console.ReadLine()) != null)
        New(text); 
}

/// <summary> 
/// Posts new message to twitter using given text
/// </summary>
[Task] public static void New(string text = null)
{
    if (text == null)
    {
        Console.Write("\nPlease enter message to be posted: ");
        text = Console.ReadLine();
    }

    Twitter().TimelineOperations
             .UpdateStatusAsync(text)
             .Wait();

    Log.Message("DONE!");
}

static TwitterTemplate Twitter()
{
    return Api();
}