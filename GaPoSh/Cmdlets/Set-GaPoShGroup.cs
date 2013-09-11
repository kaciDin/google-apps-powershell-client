using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShGroup")]
    public class SetGaPoShGroup : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Email;

        [Parameter(Mandatory = false)]
        public string Name;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var group = new Group
                    {
                        Description = String.IsNullOrEmpty(Description) ? null : Description,
                        Email = String.IsNullOrEmpty(Email) ? null : Email,
                        Name = String.IsNullOrEmpty(Name) ? null : Name
                    };

                var service = request.DirectoryService.Groups.Update(group, GroupId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Group!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}