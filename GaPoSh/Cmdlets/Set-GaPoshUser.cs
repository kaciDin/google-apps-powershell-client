using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUser")]
    public class SetGaPoShUser : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string PrimaryEmail;

        [Parameter(Mandatory = false)]
        public string GivenName;

        [Parameter(Mandatory = false)]
        public string FamilyName;

        [Parameter(Mandatory = false)]
        public bool? Suspended;

        [Parameter(Mandatory = false)]
        public string Password;

        [Parameter(Mandatory = false)]
        public string HashFunction;

        [Parameter(Mandatory = false)]
        public bool? ChangePasswordAtNextLogon;

        [Parameter(Mandatory = false)]
        public bool? IpWhiteListed;

        [Parameter(Mandatory = false)]
        public string OrgUnitPath;

        [Parameter(Mandatory = false)]
        public bool? IncludeInGlobalAddressList;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var names = new UserName
                    {
                        FamilyName = String.IsNullOrEmpty(FamilyName) ? null : FamilyName,
                        GivenName = String.IsNullOrEmpty(GivenName) ? null : GivenName
                    };

                var user = new User
                    {
                        PrimaryEmail = String.IsNullOrEmpty(PrimaryEmail) ? null : PrimaryEmail,
                        Name = names,
                        Suspended = Suspended,
                        Password = String.IsNullOrEmpty(Password) ? null : Password,
                        HashFunction = String.IsNullOrEmpty(HashFunction) ? null : HashFunction,
                        ChangePasswordAtNextLogin = ChangePasswordAtNextLogon,
                        IpWhitelisted = IpWhiteListed,
                        OrgUnitPath = String.IsNullOrEmpty(OrgUnitPath) ? null : OrgUnitPath,
                        IncludeInGlobalAddressList = IncludeInGlobalAddressList
                    };

                var service = request.DirectoryService.Users.Update(user, UserId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}