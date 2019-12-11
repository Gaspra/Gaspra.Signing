using System;
using System.Security.Cryptography.X509Certificates;

namespace Gaspra.Signing.Options
{
    public class SecretSigningCertificateOption
    {
        public string StoreLocation { get; set; }
        public string StoreName { get; set; }
        public string CertificateSubject { get; set; }

        public X509Certificate2 GetSigningCertificate()
        {
            var certificateStore = new X509Store(
                (StoreName)Enum.Parse(typeof(StoreName), StoreName),
                (StoreLocation)Enum.Parse(typeof(StoreLocation), StoreLocation)
                );

            certificateStore.Open(OpenFlags.ReadOnly);

            X509Certificate2 certificate = null;

            foreach (var cert in certificateStore.Certificates)
            {
                if (string.Equals(
                    CertificateSubject,
                    cert.Subject,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    certificate = cert;
                }
            }

            return certificate ??
                throw new Exception($"Unable to find certificate with subject '{CertificateSubject}' in store location: '{StoreLocation}' with name '{StoreName}'");
        }
    }
}
