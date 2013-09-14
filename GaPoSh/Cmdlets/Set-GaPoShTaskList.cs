using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShTaskList")]
    public class SetGaPoShTaskList : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)] public string Name;

        [Parameter(Mandatory = true)]
        public string Id;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var taskList = new TaskList
                {
                    Title = Name,
                };
            
            try
            {
            var service = request.TasksService.Tasklists.Update(taskList, Id);


            var tasksRequest = service.Execute();

            WriteObject(tasksRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Task List!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}