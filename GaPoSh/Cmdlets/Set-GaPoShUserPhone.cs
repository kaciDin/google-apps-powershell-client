using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserPhone")]
    public class SetGaPoShUserPhone : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string CustomType;

        [Parameter(Mandatory = true)]
        public bool? Primary;

        [Parameter(Mandatory = false)]
        public string Type;

        [Parameter(Mandatory = false)]
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
                        Phones = new List<UserPhone>
                            {
                                new UserPhone
                                    {
                                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                                        Primary = Primary,
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
                Console.WriteLine("Failed to Update User Phone!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}