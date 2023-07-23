![alt tag](https://raw.githubusercontent.com/jchristn/SendWithBrevo/master/assets/icon.ico)

# SendWithBrevo

[![NuGet Version](https://img.shields.io/nuget/v/SendWithBrevo.svg?style=flat)](https://www.nuget.org/packages/SendWithBrevo/) [![NuGet](https://img.shields.io/nuget/dt/SendWithBrevo.svg)](https://www.nuget.org/packages/SendWithBrevo) 

A simple C# class library to help simplify sending emails using Brevo.

## New in v1.0.x

- Initial release

## Test Apps

A test project is included which will help you exercise the class library.
 
## Examples

```csharp
using SendWithBrevo;

BrevoClient client = new BrevoClient("[your API key]");

await client.SendAsync(
	new Sender("[my name]", "[my email]"),
	new List<Recipient> { new Recipient("[their name]", "[their email]") },
	"Email subject",
	"Email body",
	false,  // true if body is HTML
	);
```

Full signature for ```SendAsync```:
```csharp
public async Task<bool> SendAsync(
    Sender sender,
    List<Recipient> to,
    string subject,
    string content,
    bool isHtml = false,
    List<Recipient> cc = null,
    List<Recipient> bcc = null,
    Sender replyTo = null,
    Dictionary<string, string> headers = null,
    Dictionary<string, string> parameters = null,
    List<Attachment> attachments = null,
    object templateId = null,
    CancellationToken token = default
    )
```

## Version History

Please refer to CHANGELOG.md for version history.
