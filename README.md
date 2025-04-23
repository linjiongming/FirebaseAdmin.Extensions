# FirebaseAdmin.Extensions



## Usage

```csharp
using Microsoft.Extensions.DependencyInjection;

services.AddGoogleCredential("projectid-firebase-adminsdk-12345-6789abcdef.json").AddFirebaseMessaging();
```

```csharp
using FirebaseAdmin.Messaging;

ctor(FirebaseMessaging firebaseMessaging)

var msg = new Message
{
    Token = "eX1_..._wSkAkajO6T1Mnd5lWml2r24kZQrDuBI",
    Notification = new Notification()
    {
        Title = "title",
        Body = "content"
    },
    Data = new Dictionary<string, string>
    {
        { "key1", "value1" },
        { "key2", "value2" }
    }
};

await firebaseMessaging.SendAsync(msg, cancellation);

//var json = msg.ToJson();

//await firebaseMessaging.SendAsync(json, cancellation);
```
