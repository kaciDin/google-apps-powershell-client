using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserEmail")]
    public class SetGaPoShUserEmail : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)]
        public string Address;

        [Parameter(Mandatory = false)] public string CustomType;

        [Parameter(Mandatory = false)] public bool? Primary;

        [Parameter(Mandatory = true)] public string Type;

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
                        Emails = new List<UserEmail>
                            {
                                new UserEmail
                                    {
                                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                                        Primary = Primary,
                                        Type = String.IsNullOrEmpty(Type) ? null : Type,
                                        Address = String.IsNullOrEmpty(Address) ? null : Address
                                    }
                            }
                    };

                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User Email!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}