using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShTask")]
    public class GetGaPoShTask : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)] public string TaskListId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.TasksService.Tasks.List(TaskListId);

            service.MaxResults = "500";

            var tasks = service.Execute();

            if (tasks == null) return;

            Int64 totalresults = 0;
            var allTaskLists = new List<Task>();

            //Single Page Results
            if (String.IsNullOrEmpty(tasks.NextPageToken))
            {
                service.PageToken = tasks.NextPageToken;

                allTaskLists.AddRange(tasks.Items);

                totalresults = (totalresults + tasks.Items.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(tasks.NextPageToken))
            {
                service.PageToken = tasks.NextPageToken;

                allTaskLists.AddRange(tasks.Items);

                totalresults = (totalresults + tasks.Items.Count);

                Console.Write(totalresults + "...");
                tasks = service.Execute();

                if (String.IsNullOrEmpty(tasks.NextPageToken))
                {
                    service.PageToken = tasks.NextPageToken;

                    allTaskLists.AddRange(tasks.Items);

                    totalresults = (totalresults + tasks.Items.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allTaskLists);
        }
    }
}