using System.Security.Cryptography.X509Certificates;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Groupssettings.v1;
using Google.Apis.Licensing.v1;
using Google.Apis.Drive.v2;
using Google.Apis.Calendar.v3;
using Google.Apis.Audit.v1;
using Google.Apis.Admin.Reports.reports_v1;
using Google.Apis.Tasks.v1;
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

        public DirectoryService DirectoryService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var scopes = Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryUser.GetStringValue() + @" " +
                         Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryGroup.GetStringValue() + @" " +
                         Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryOrgunit.GetStringValue() + @" " +
                         Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceChromeos.GetStringValue() + @" " +
                         Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceMobile.GetStringValue() + @" " +
                         Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceMobileAction.GetStringValue();

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

        public ReportsService ReportsService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://www.googleapis.com/auth/admin.reports.audit.readonly"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new ReportsService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public CalendarService CalendarService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://www.googleapis.com/auth/calendar"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new CalendarService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public DriveService DriveService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://www.googleapis.com/auth/drive"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new DriveService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public AuditService AuditService()
        {
            var certificate = new X509Certificate2(
                _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://apps-apis.google.com/a/feeds/compliance/audit/"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new AuditService((new BaseClientService.Initializer() { Authenticator = auth }));
        }

        public TasksService TasksService()
        {
            var certificate = new X509Certificate2(
               _serviceAccountCertPath, "notasecret", X509KeyStorageFlags.Exportable);

            var provider = new AssertionFlowClient(GoogleAuthenticationServer.Description, certificate)
            {
                ServiceAccountId = _serviceAccountEmail,
                Scope = "https://www.googleapis.com/auth/tasks"
            };

            if (_serviceAccountUser != string.Empty)
            {
                provider.ServiceAccountUser = _serviceAccountUser;
            }

            var auth = new OAuth2Authenticator<AssertionFlowClient>(provider, AssertionFlowClient.GetState);

            return new TasksService((new BaseClientService.Initializer() { Authenticator = auth }));
        }
    }
}