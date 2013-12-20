# Nake-tweet-cli

Small tool that allows to tweet from any console. Basically it's just a showcase for [Nake](http://nake-tool.net).

CREDITS: Inspired by [node-tweet-cli](https://github.com/voronianski/node-tweet-cli)

## How to use it

1. Download zipped version from GitHub releases page and extract it somewhere
2. Add directory location to global path and reload console
3. Now run `tweet install` from console to download dependencies from NuGet
4. That's it. Start with authorizing your twitter account and finally tweeting :) 

> NOTE: You can also just clone this repository and do steps from 2 to 4.

## Hack it

Ye, you can do it. It's all just plain C# scripts, which you can find in `Source` directory. You can extend functionality with new commands very easily. Currently, Spring.Social.Twitter connector library is used. Check out it's documentation - it fully covers whole twitter's API. 

## Commands

You can get list of all available commands by running `tweet` with *-T* switch:

```bash
C:\> tweet -T

tweet login   # Logs into twitter
tweet logout  # Logs out of twitter
tweet new     # Posts new tweet using given text
tweet read    # Posts new message to twitter but reads it from stdin
tweet show    # Displays last N tweets from you timeline
tweet whoami  # Shows current twitter account name
```

### tweet login

The ``tweet login`` command manages authorization flow. It will redirect you to twitter's authorization page where you'll need to authorize application and get a PIN. Enter PIN in the terminal prompt and "voilà!" - you can now tweet from your terminal.

```bash
tweet login
```
> NOTE: You access token will be stored securely in a windows credentials store (Vault).
 
### tweet logout

The ``tweet logout`` command will un-authorize your twitter account from Nake-tweet-cli.

```bash
tweet logout
```

### tweet new

The ``tweet new`` command allows you to post tweets into your twitter account. You will be prompted to type a message text if you not specify it as parameter.

```bash
tweet new [text]
```

### tweet read

The ``tweet read`` command posts message to twitter but reads it from ``stdin``, enabling use with scripting.

```bash
echo "your tweet message" | tweet read
```
or

```bash
cat tweet.txt | tweet read
```

or

```bash
tweet read < tweet.txt
```

### tweet show

The ``tweet show`` command allows you to get latest N tweets from your twitter's timeline.

```bash
tweet show [count]
```

### tweet whoami

The ``tweet whoami`` command shows current twitter account name.

```bash
tweet whoami
```

## License

APACHE 2