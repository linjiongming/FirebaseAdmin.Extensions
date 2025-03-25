using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection;

public static class FirebaseAdminServiceCollectionExtensions
{
    public static IServiceCollection AddGoogleCredential(this IServiceCollection services, [NotNull] string? credentialPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(credentialPath);
        if (!Path.IsPathRooted(credentialPath))
        {
            credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, credentialPath);
        }
        services.AddSingleton(GoogleCredential.FromFile(credentialPath));
        return services;
    }
    public static IServiceCollection AddFirebaseApp(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var credential = provider.GetRequiredService<GoogleCredential>();
            var options = new AppOptions
            {
                Credential = credential,
                ProjectId = ((ServiceAccountCredential)credential.UnderlyingCredential).ProjectId,
            };
            return FirebaseApp.Create(options, AppDomain.CurrentDomain.FriendlyName);
        });
        return services;
    }
    public static IServiceCollection AddFirebaseMessaging(this IServiceCollection services)
    {
        if (!services.Any(x => x.ServiceType == typeof(FirebaseApp)))
        {
            services.AddFirebaseApp();
        }
        services.AddSingleton(provider => FirebaseMessaging.GetMessaging(provider.GetRequiredService<FirebaseApp>()));
        return services;
    }
}
