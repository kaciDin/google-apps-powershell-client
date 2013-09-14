using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShUserTaskList")]
    public class NewGaPoShTaskList : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string Name;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var taskList = new TaskList
                    {
                        Title = Name
                    };

                var service = request.TasksService.Tasklists.Insert(taskList);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Create Task List!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}