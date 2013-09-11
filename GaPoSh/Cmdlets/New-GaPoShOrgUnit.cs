using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShOrgUnit")]
    public class NewGaPoShOrgUnit : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public bool? BlockInheritance;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string OrgUnitPath;

        [Parameter(Mandatory = true)]
        public string ParentOrgUnitPath;

        [Parameter(Mandatory = true)]
        public string Name;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var orgUnit = new OrgUnit
                    {
                        BlockInheritance = BlockInheritance,
                        Description = String.IsNullOrEmpty(Description) ? null : Description,
                        OrgUnitPath = String.IsNullOrEmpty(OrgUnitPath) ? null : OrgUnitPath,
                        ParentOrgUnitPath = String.IsNullOrEmpty(ParentOrgUnitPath) ? null : ParentOrgUnitPath,
                        Name = String.IsNullOrEmpty(Name) ? null : Name
                    };

                var service = request.DirectoryService.Orgunits.Insert(orgUnit, "my_customer");

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Add OrgUnit!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}