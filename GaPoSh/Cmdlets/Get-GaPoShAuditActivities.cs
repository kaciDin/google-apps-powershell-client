using System;
using System.Management.Automation;
using GaPoSh.Services;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShAuditActivites")]
    public class GetGaPoShAuditActivites : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string CustomerId;

        [Parameter(Mandatory = false)]
        public string ApplicationId;

        [Parameter(Mandatory = false)]
        public string ActorEmail;

        [Parameter(Mandatory = false)]
        public string StartTime;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.AuditService.Activities.List(CustomerId, ApplicationId);

            service.MaxResults = 500;
            service.ActorEmail = String.IsNullOrEmpty(ActorEmail) ? null : ActorEmail;
            service.StartTime = String.IsNullOrEmpty(StartTime) ? null : StartTime;

            var activities = service.Execute();

            if (activities == null) return;

            WriteObject(activities);
        }
    }
}