using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Drive.v2.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShDriveChanges")]
    public class GetGaPoShDriveChanges : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = false)] public bool? IncludeDeleted;

        [Parameter(Mandatory = false)] public bool? IncludeSubscribed;

        [Parameter(Mandatory = false)] public string StartChangeId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.DriveService.Changes.List();

            service.MaxResults = 1000;
            service.IncludeDeleted = IncludeDeleted;
            service.IncludeSubscribed = IncludeSubscribed;
            service.StartChangeId = String.IsNullOrEmpty(StartChangeId) ? null : StartChangeId;

            var changes = service.Execute();

            if (changes == null) return;

            Int64 totalresults = 0;
            var allChanges = new List<Change>();

            //Single Page Results
            if (String.IsNullOrEmpty(changes.NextPageToken))
            {
                service.PageToken = changes.NextPageToken;

                allChanges.AddRange(changes.Items);

                totalresults = (totalresults + changes.Items.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(changes.NextPageToken))
            {
                service.PageToken = changes.NextPageToken;

                allChanges.AddRange(changes.Items);

                totalresults = (totalresults + changes.Items.Count);

                Console.Write(totalresults + "...");
                changes = service.Execute();

                if (String.IsNullOrEmpty(changes.NextPageToken))
                {
                    service.PageToken = changes.NextPageToken;

                    allChanges.AddRange(changes.Items);

                    totalresults = (totalresults + changes.Items.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allChanges);
        }
    }
}