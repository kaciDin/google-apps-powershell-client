using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShDriveFileRevisions")]
    public class GetGaPoShDriveFileRevisions : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)] public string FileId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DriveService.Revisions.List(FileId);

            var revisions = service.Execute();

            if (revisions == null) return;

            WriteObject(revisions);
        }
    }
}