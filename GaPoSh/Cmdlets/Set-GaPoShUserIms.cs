using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserIms")]
    public class SetGaPoShUserIms : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)] public string CustomProtocol;

        [Parameter(Mandatory = false)] public string CustomType;

        [Parameter(Mandatory = true)] public string Im;

        [Parameter(Mandatory = false)] public bool? Primary;

        [Parameter(Mandatory = true)] public string Protocol;

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
                        Ims = new List<UserIm>
                            {
                                new UserIm
                                    {
                                        CustomProtocol = String.IsNullOrEmpty(CustomProtocol) ? null : CustomProtocol,
                                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                                        Im = String.IsNullOrEmpty(Im) ? null : Im,
                                        Primary = Primary,
                                        Protocol = String.IsNullOrEmpty(Protocol) ? null : Protocol,
                                        Type = String.IsNullOrEmpty(Type) ? null : Type
                                    }
                            }
                    };

                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User IMS!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}