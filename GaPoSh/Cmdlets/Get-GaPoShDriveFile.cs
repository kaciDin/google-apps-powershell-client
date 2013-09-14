using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShDriveFile")]
    public class GetGaPoShDriveFile : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DriveService.Files.List();

            service.MaxResults = 500;
            service.Projection = FilesResource.ListRequest.ProjectionEnum.FULL;

            var files = service.Execute();

            if (files == null) return;

            Int64 totalresults = 0;
            var allFiles = new List<File>();

            //Single Page Results
            if (String.IsNullOrEmpty(files.NextPageToken))
            {
                service.PageToken = files.NextPageToken;

                allFiles.AddRange(files.Items);

                totalresults = (totalresults + files.Items.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(files.NextPageToken))
            {
                service.PageToken = files.NextPageToken;

                allFiles.AddRange(files.Items);

                totalresults = (totalresults + files.Items.Count);

                Console.Write(totalresults + "...");
                files = service.Execute();

                if (String.IsNullOrEmpty(files.NextPageToken))
                {
                    service.PageToken = files.NextPageToken;

                    allFiles.AddRange(files.Items);

                    totalresults = (totalresults + files.Items.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allFiles);
        }
    }
}