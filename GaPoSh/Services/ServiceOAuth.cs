using System.Security.Cryptography.X509Certificates;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Groupssettings.v1;
using Google.Apis.Licensing.v1;
using Google.Apis.Services;
using Google.Apis.Util;

namespace GaPoSh.Services
{
    public class ServiceOAuth
    {
        private readonly string _serviceAccountEmail;
        private readonly string _serviceAccountCertPath;
        private readonly string _serviceAccountUser;

        public ServiceOAuth(string serviceAccountEmail, string serviceAccountCertPath)
        {
            _serviceAccountEmail = serviceAccountEmail;
            _serviceAccountCertPath = serviceAccountCertPath;
        }

        public ServiceOAuth(string serviceAccountEmail, string serviceAccountCertPath, string serviceAccountUser)
        {
            _serviceAccountEmail = serviceAccountEmail;
            _serviceAccountCertPath = serviceAccountCertPath;
            _serviceAccountUser = serviceAccountUser;
        }

        public DirectoryService BuildDirectoryService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var scopes = DirectoryService.Scopes.AdminDirectoryUser.GetStringValue() + @" " +
                         DirectoryService.Scopes.AdminDirectoryGroup.GetStringValue() + @" " +
                         DirectoryService.Scopes.AdminDirectoryOrgunit.GetStringValue() + @" " +
                         DirectoryService.Scopes.AdminDirectoryDeviceChromeos.GetStringValue() + @" " +
                         DirectoryService.Scopes.AdminDirectoryDeviceMobile.GetStringValue() + @" " +
                         DirectoryService.Scopes.AdminDirectoryDeviceMobileAction.GetStringValue();

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = @scopes
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new DirectoryService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public GroupssettingsService GroupSettingsService()
        {
            var certificate = new X509Certificate2(
                 _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = GroupssettingsService.Scopes.AppsGroupsSettings.GetStringValue()
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new GroupssettingsService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public LicensingService LicensingService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://www.googleapis.com/auth/apps.licensing"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new LicensingService((new BaseClientService.Initializer() { Authenticator = auth }));
        }
    }
}