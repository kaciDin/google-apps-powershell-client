using System;
using GaPoSh.Data;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Groupssettings.v1;
using Google.Apis.Licensing.v1;
using Google.Apis.Drive.v2;
using Google.Apis.Calendar.v3;
using Google.Apis.Audit.v1;
using Google.Apis.Admin.Reports.reports_v1;
using Google.Apis.Tasks.v1;
using Google.Apis.Util;

namespace GaPoSh.Services
{
    public class Instance
    {
        public string Domain;
        public DirectoryService DirectoryService;
        public GroupssettingsService GroupSettingsService;
        public LicensingService LicensingService;
        public ReportsService ReportsService;
        public CalendarService CalendarService;
        public DriveService DriveService;
        public AuditService AuditService;
        public TasksService TasksService;

        public Instance(ServiceAuth auth)
        {
            Domain = auth.Domain;

            Console.WriteLine("Connecting to Google...");
            Console.WriteLine("Getting Authorization...");

            if (auth.ServiceUser.IsNullOrEmpty())
            {
                ServiceInstance(auth.AccountId, auth.CertPath);
            }
            else
            {
                ServiceInstance(auth.AccountId, auth.CertPath, auth.ServiceUser);
            }
        }

        public Instance(SimpleAuth auth)
        {
            Domain = auth.Domain;

            Console.WriteLine("Connecting to Google...");
            Console.WriteLine("Getting Authorization...");

            if (auth.RefreshToken.IsNullOrEmpty())
            {
                SimpleInstance(auth.ClientId, auth.ClientSecret);
            }
            else
            {
                SimpleInstance(auth.ClientId, auth.ClientSecret, auth.RefreshToken);
            }
        }

        private void ServiceInstance(string accountId, string certPath)
        {
            var auth = new ServiceOAuth(accountId, certPath);
            DirectoryService = auth.DirectoryService();
            GroupSettingsService = auth.GroupSettingsService();
            LicensingService = auth.LicensingService();
            ReportsService = auth.ReportsService();
            CalendarService = auth.CalendarService();
            DriveService = auth.DriveService();
            AuditService = auth.AuditService();
            TasksService = auth.TasksService();
        }

        private void ServiceInstance(string accountId, string certPath, string serviceUser)
        {
            var auth = new ServiceOAuth(accountId, certPath, serviceUser);
            DirectoryService = auth.DirectoryService();
            GroupSettingsService = auth.GroupSettingsService();
            LicensingService = auth.LicensingService();
            ReportsService = auth.ReportsService();
            CalendarService = auth.CalendarService();
            DriveService = auth.DriveService();
            AuditService = auth.AuditService();
            TasksService = auth.TasksService();
        }

        private void SimpleInstance(string clientId, string clientSecret)
        {
            var auth = new SimpleOAuth(clientId, clientSecret);
            DirectoryService = auth.DirectoryService();
            GroupSettingsService = auth.GroupSettingsService();
            LicensingService = auth.LicensingService();
        }

        private void SimpleInstance(string clientId, string clientSecret, string refreshToken)
        {
            var auth = new SimpleOAuth(clientId, clientSecret, refreshToken);
            DirectoryService = auth.DirectoryService();
            GroupSettingsService = auth.GroupSettingsService();
            LicensingService = auth.LicensingService();
        }
    }
}