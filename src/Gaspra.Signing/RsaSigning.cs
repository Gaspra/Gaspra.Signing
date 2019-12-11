using Gaspra.Signing.Interfaces;
using Gaspra.Signing.Options;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Gaspra.Signing
{
    public class RsaSigning : SigningService
    {
        private readonly X509Certificate2 certificate;
        private readonly RSAEncryptionPadding encryptionPadding;
        public RsaSigning(IOptionsMonitor<SecretSigningCertificateOption> optionsMonitor)
        {
            certificate = optionsMonitor.CurrentValue.GetSigningCertificate();
            encryptionPadding = RSAEncryptionPadding.OaepSHA256;
        }

        public byte[] Decrypt(byte[] data)
        {
            using (RSA rsa = certificate.GetRSAPrivateKey())
            {
                return rsa.Encrypt(data, encryptionPadding);
            }
        }

        public string Decrypt(string data)
        {
            using (RSA rsa = certificate.GetRSAPrivateKey())
            {
                var dataBytes = Convert.FromBase64String(data);

                return Encoding.UTF8.GetString(rsa.Decrypt(dataBytes, encryptionPadding));
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            using (RSA rsa = certificate.GetRSAPublicKey())
            {
                return rsa.Encrypt(data, encryptionPadding);
            }
        }

        public string Encrypt(string data)
        {
            using (RSA rsa = certificate.GetRSAPublicKey())
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);

                return Convert.ToBase64String(rsa.Encrypt(dataBytes, encryptionPadding));
            }
        }
    }


}
