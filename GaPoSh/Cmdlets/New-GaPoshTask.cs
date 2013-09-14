using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Tasks.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.New, "GaPoShUserTask")]
    public class NewGaPoShTask : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public string Notes;

        [Parameter(Mandatory = false)] public string Due;

        [Parameter(Mandatory = false)] public string TaskListId;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var task = new Task
                    {
                        Title = Title,
                        Notes = Notes,
                        Due = Due
                    };

                var service = request.TasksService.Tasks.Insert(task, TaskListId);

                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Create Task!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}