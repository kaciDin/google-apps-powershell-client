using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShTask")]
    public class SetGaPoShTask : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)] public string Name;

        [Parameter(Mandatory = false)] public string Notes;

        [Parameter(Mandatory = false)] public string Due;

        [Parameter(Mandatory = true)]
        public string TaskListId;

        [Parameter(Mandatory = true)] public string TaskId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var taskList = new Task
                {
                    Title = Name,
                    Notes = Notes,
                    Due = Due
                };

            try
            {
                var service = request.TasksService.Tasks.Update(taskList, TaskListId, TaskId);


                var tasksRequest = service.Execute();

                WriteObject(tasksRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Task!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}