using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserRelation")]
    public class SetGaPoShUserRelation : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string CustomType;

        [Parameter(Mandatory = true)]
        public string Type;

        [Parameter(Mandatory = true)]
        public string Value;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var user = new User
                    {
                        Relations = new List<UserRelation>
                            {
                                new UserRelation
                                    {
                                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                                        Type = String.IsNullOrEmpty(Type) ? null : Type,
                                        Value = String.IsNullOrEmpty(Value) ? null : Value
                                    }
                            }
                    };

                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User Relation!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}