﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserExternalId")]
    public class SetGaPoShUserExternalId : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = true)]
        public string Value;

        [Parameter(Mandatory = true)]
        public string Type;

        [Parameter(Mandatory = false)]
        public string CustomType;

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
                        ExternalIds = new List<UserExternalId>
                            {
                                new UserExternalId
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
                Console.WriteLine("Failed to Update User External Id!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}