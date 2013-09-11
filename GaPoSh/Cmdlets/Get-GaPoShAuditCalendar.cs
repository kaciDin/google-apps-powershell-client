using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Calendar.v3.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "GaPoShCalendar")]
    public class GetGaPoShCalendar : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            var service = request.CalendarService.CalendarList.List();

            service.MaxResults = 500;

            var calendars = service.Execute();

            if (calendars == null) return;

            Int64 totalresults = 0;
            var allCalendars = new List<CalendarListEntry>();

            //Single Page Results
            if (String.IsNullOrEmpty(calendars.NextPageToken))
            {
                service.PageToken = calendars.NextPageToken;

                allCalendars.AddRange(calendars.Items);

                totalresults = (totalresults + calendars.Items.Count);

                Console.Write(totalresults + "...");
            }

            //Multiple Page Results
            while (!String.IsNullOrEmpty(calendars.NextPageToken))
            {
                service.PageToken = calendars.NextPageToken;

                allCalendars.AddRange(calendars.Items);

                totalresults = (totalresults + calendars.Items.Count);

                Console.Write(totalresults + "...");
                calendars = service.Execute();

                if (String.IsNullOrEmpty(calendars.NextPageToken))
                {
                    service.PageToken = calendars.NextPageToken;

                    allCalendars.AddRange(calendars.Items);

                    totalresults = (totalresults + calendars.Items.Count);

                    Console.Write(totalresults + "...");
                }
            }

            WriteObject(allCalendars);
        }
    }
}