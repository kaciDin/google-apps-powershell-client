using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShTaskList")]
    public class GetGaPoShTaskList : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.TasksService.Tasklists.List();

            service.MaxResults = "500";

            var taskLists = service.Execute();

            if (taskLists == null) return;

            Int64 totalresults = 0;
            var allTaskLists = new List<TaskList>();

            //Single Page Results
            if (String.IsNullOrEmpty(taskLists.NextPageToken))
            {
                service.PageToken = taskLists.NextPageToken;

                allTaskLists.AddRange(taskLists.Items);

                totalresults = (totalresults + taskLists.Items.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(taskLists.NextPageToken))
            {
                service.PageToken = taskLists.NextPageToken;

                allTaskLists.AddRange(taskLists.Items);

                totalresults = (totalresults + taskLists.Items.Count);

                Console.Write(totalresults + "...");
                taskLists = service.Execute();

                if (String.IsNullOrEmpty(taskLists.NextPageToken))
                {
                    service.PageToken = taskLists.NextPageToken;

                    allTaskLists.AddRange(taskLists.Items);

                    totalresults = (totalresults + taskLists.Items.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allTaskLists);
        }
    }
}