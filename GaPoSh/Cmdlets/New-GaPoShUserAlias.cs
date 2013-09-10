using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShUserAlias")]
    public class NewGaPoShUserAlias : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)]
        public string Alias;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var alias = new Alias
                    {
                        AliasValue = Alias
                    };

                var service = request.DirectoryService.Users.Aliases.Insert(alias, UserId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Add User Alias!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}