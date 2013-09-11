using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserAdmin")]
    public class SetGaPoShUserAdmin : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)]
        public bool? Status;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var admin = new UserMakeAdmin
                    {
                        Status = Status
                    };

                var service = request.DirectoryService.Users.MakeAdmin(admin, UserId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Set User Admin!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}