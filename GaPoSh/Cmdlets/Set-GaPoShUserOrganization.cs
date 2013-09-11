using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserOrganization")]
    public class SetGaPoShUserOrganization : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string CostCenter;

        [Parameter(Mandatory = false)]
        public string CustomType;

        [Parameter(Mandatory = false)]
        public string Department;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Domain;

        [Parameter(Mandatory = false)]
        public string Location;

        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public bool? Primary;

        [Parameter(Mandatory = false)]
        public string Symbol;

        [Parameter(Mandatory = false)]
        public string Title;

        [Parameter(Mandatory = true)]
        public string Type;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var user = new User();

                user.Organizations = new List<UserOrganization>();

                user.Organizations.Add(new UserOrganization
                    {
                        CostCenter = String.IsNullOrEmpty(CostCenter) ? null : CostCenter,
                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                        Department = String.IsNullOrEmpty(Department) ? null : Department,
                        Domain = String.IsNullOrEmpty(Domain) ? null : Domain,
                        Location = String.IsNullOrEmpty(Location) ? null : Location,
                        Name = String.IsNullOrEmpty(Name) ? null : Name,
                        Primary = Primary,
                        Symbol = String.IsNullOrEmpty(Symbol) ? null : Symbol,
                        Title = String.IsNullOrEmpty(Title) ? null : Title,
                        Type = String.IsNullOrEmpty(Type) ? null : Type
                    });

                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User Organization!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}