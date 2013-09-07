using System.Management.Automation;
using GaPoSh.Data;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShConnection")]
    public class NewGaPoShConnection : PSCmdlet
    {

        [Parameter(Mandatory = true)]
        public string Domain;

        [Parameter(Mandatory = true, ParameterSetName = "SimpleOAuth")]
        public string ClientId;

        [Parameter(Mandatory = true, ParameterSetName = "SimpleOAuth")]
        public string ClientSecret;

        [Parameter(Mandatory = true, ParameterSetName = "SimpleOAuth")]
        public string ClientToken;

        [Parameter(Mandatory = true, ParameterSetName = "ServiceOAuth")]
        public string ServiceEmail;

        [Parameter(Mandatory = true, ParameterSetName = "ServiceOAuth")]
        public string ServicePath;

        [Parameter(Mandatory = true, ParameterSetName = "ServiceOAuth")]
        public string ServiceUser;

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "SimpleOAuth":
                    ProcessRequest(
                        Authenticate.SimpleAccount(new SimpleAuth
                            {
                                ClientId = ClientId,
                                ClientSecret = ClientSecret,
                                RefreshToken = ClientToken,
                                Domain = Domain
                            }));
                    break;

                case "ServiceOAuth":
                    ProcessRequest(
                        Authenticate.ServiceAccount(new ServiceAuth
                            {
                                AccountId = ServiceEmail,
                                CertPath = ServicePath,
                                ServiceUser = ServiceUser,
                                Domain = Domain
                            }));
                    break;
            }
        }

        private void ProcessRequest(Instance services)
        {
            WriteObject(services);
        }

    }
}
