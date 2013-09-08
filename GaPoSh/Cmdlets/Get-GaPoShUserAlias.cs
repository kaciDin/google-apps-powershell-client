using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShUserAlias")]
    public class GetGaPoShUserAlias : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public bool DomainOnly;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DirectoryService.Users.Aliases.List(UserId);

            var aliases = service.Execute();

            if (aliases == null) return;

            var allAliases = new List<Alias>();

            allAliases.AddRange(aliases.AliasesValue);
            Console.WriteLine(aliases.AliasesValue.Count + "...");

            WriteObject(allAliases);
        }
    }
}