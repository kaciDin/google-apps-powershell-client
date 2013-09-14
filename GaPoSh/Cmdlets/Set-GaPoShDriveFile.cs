using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShDriveFile")]
    public class SetGaPoShDriveFile : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)] public string FileId;

        [Parameter(Mandatory = false)] public string Description;

        [Parameter(Mandatory = false)] public string MimeType;

        [Parameter(Mandatory = false)] public string Title;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var file = new File
                {
                    Description = String.IsNullOrEmpty(Description) ? null : Description,
                    Title = String.IsNullOrEmpty(Title) ? null : Title,
                    MimeType = String.IsNullOrEmpty(MimeType) ? null : MimeType
                    //TODO: Add Additional Fields to Update
                };

            var service = request.DriveService.Files.Update(file, FileId);

            var fileRequest = service.Execute();
            WriteObject(fileRequest);
        }
    }
}