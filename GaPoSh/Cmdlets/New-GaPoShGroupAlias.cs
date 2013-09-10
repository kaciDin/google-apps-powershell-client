using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShGroupAlias")]
    public class NewGaPoShGroupAlias : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

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

                var service = request.DirectoryService.Groups.Aliases.Insert(alias, GroupId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Add Group Alias!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}