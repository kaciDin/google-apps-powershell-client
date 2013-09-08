using System;
using System.Diagnostics;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
using Google.Apis.Groupssettings.v1;
using Google.Apis.Licensing.v1;
using Google.Apis.Services;
using Google.Apis.Util;

namespace GaPoSh.Services
{
    internal class SimpleOAuth
    {
        private static string _refreshToken;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SimpleOAuth(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public SimpleOAuth(string clientId, string clientSecret, string refreshToken)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _refreshToken = refreshToken;
        }

        private NativeApplicationClient InitializeProvider()
        {
            return new NativeApplicationClient(GoogleAuthenticationServer.Description)
            {
                ClientIdentifier = _clientId,
                ClientSecret = _clientSecret
            };
        }

        public DirectoryService DirectoryService()
        {
            var provider = InitializeProvider();
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);

            return new DirectoryService(new BaseClientService.Initializer { Authenticator = auth });
        }

        public GroupssettingsService GroupSettingsService()
        {
            var provider = InitializeProvider();
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);
            return new GroupssettingsService(new BaseClientService.Initializer { Authenticator = auth });
        }

        public LicensingService LicensingService()
        {
            var provider = InitializeProvider();
            var auth = new OAuth2Authenticator<NativeApplicationClient>(provider, GetAuthorization);
            return new LicensingService(new BaseClientService.Initializer { Authenticator = auth });
        }

        private static IAuthorizationState GetAuthorization(NativeApplicationClient arg)
        {
            // Get the auth URL:
            IAuthorizationState state =
                new AuthorizationState(
                    new[]
                        {
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryUser.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryUserAlias.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryOrgunit.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryGroupMember.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceChromeos.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceMobile.GetStringValue(),
                            Google.Apis.Admin.Directory.directory_v1.DirectoryService.Scopes.AdminDirectoryDeviceMobileAction.GetStringValue(),
                            GroupssettingsService.Scopes.AppsGroupsSettings.GetStringValue() });

            state.Callback = new Uri(NativeApplicationClient.OutOfBandCallbackUrl);

            if (!String.IsNullOrWhiteSpace(_refreshToken))
            {
                state.RefreshToken = _refreshToken;

                if (arg.RefreshToken(state))
                    return state;
            }

            var authUri = arg.RequestUserAuthorization(state);

            // Request authorization from the user (by opening a browser window):
            Process.Start(authUri.ToString());
            Console.Write("  Authorization Code: ");
            var authCode = Console.ReadLine();
            Console.WriteLine();

            // Retrieve the access token by using the authorization code:
            var result = arg.ProcessUserAuthorization(authCode, state);
            Console.WriteLine("Refresh Token: " + result.RefreshToken);

            return result;
        }
    }
}