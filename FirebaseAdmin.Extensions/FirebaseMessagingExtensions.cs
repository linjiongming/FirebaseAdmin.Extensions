using Google.Apis.Json;
using System.Diagnostics.CodeAnalysis;

namespace FirebaseAdmin.Messaging;

public static class FirebaseMessagingExtensions
{
    public static async Task<string> SendAsync(this FirebaseMessaging firebaseMessaging, [NotNull] string? json, CancellationToken cancellation = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(json);
        var message = NewtonsoftJsonSerializer.Instance.Deserialize<Message>(json);
        return await firebaseMessaging.SendAsync(message, cancellation);
    }
    public static async Task<BatchResponse> SendEachAsync(this FirebaseMessaging firebaseMessaging, IEnumerable<string> jsons, CancellationToken cancellation = default)
    {
        var messages = jsons.Select(NewtonsoftJsonSerializer.Instance.Deserialize<Message>);
        return await firebaseMessaging.SendEachAsync(messages, cancellation);
    }
    public static string ToJson(this Message message) => NewtonsoftJsonSerializer.Instance.Serialize(message);
}
