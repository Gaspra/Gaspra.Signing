using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSigning.Interfaces;
using SecretSigning.Options;

namespace SecretSigning
{
    public static class SecretSigningExtensions
    {
        public static IServiceCollection RegisterSecretSigningCertificateOption(
            this IServiceCollection serviceCollection,
            string storeName,
            string storeLocation,
            string certificateSubject)
        {
            serviceCollection.Configure<SecretSigningCertificateOption>(options =>
            {
                options.StoreName = storeName;
                options.StoreLocation = storeLocation;
                options.CertificateSubject = certificateSubject;
            });

            return serviceCollection;
        }

        public static IServiceCollection RegisterSecretSigningCertificateOptionFromConfiguration(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string certificateSubjectName = "SigningCertificate")
        {
            var certificateConfig = configuration.GetSection(certificateSubjectName);

            serviceCollection.Configure<SecretSigningCertificateOption>(options =>
            {
                options.StoreName = certificateConfig["StoreName"];
                options.StoreLocation = certificateConfig["StoreLocation"];
                options.CertificateSubject = certificateConfig["CertificateSubject"];
            });

            return serviceCollection;
        }

        public static IServiceCollection RegisterSigningServices(
            this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<SigningService, RsaSigning>();

            return serviceCollection;
        }
    }
}
