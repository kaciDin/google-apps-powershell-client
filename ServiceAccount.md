# Service Account #

  1. Go to https://code.google.com/apis/console
  1. Click Services Tab, Enable the following services
    * Admin SDK
    * Audit API
    * Calendar API
    * Drive API
    * Drive SDK
    * Enterprise Licensing API
    * Groups Settings API
    * Tasks API
  1. Click API Access Tab
    * Click Create Another client ID
    * Service Account
    * Download Private Key (You'll need this saved locally, password is not important)
    * Make note of ServiceID and Email
  1. Go to https://admin.google.com
    * Go to security settings
    * Manage third party OAuth Client access
    * Use the ServiceID and add the following scopes
```
         https://www.googleapis.com/auth/admin.directory.device.chromeos, 
         https://www.googleapis.com/auth/admin.directory.device.mobile,
         https://www.googleapis.com/auth/admin.directory.device.mobile.action, 
         https://www.googleapis.com/auth/admin.directory.group,
         https://www.googleapis.com/auth/admin.directory.orgunit,
         https://www.googleapis.com/auth/admin.directory.user,
         https://www.googleapis.com/auth/apps.groups.settings,
         https://www.googleapis.com/auth/apps.licensing,
         https://www.googleapis.com/auth/admin.reports.audit.readonly,
         https://www.googleapis.com/auth/calendar,
         https://www.googleapis.com/auth/drive,
         https://apps-apis.google.com/a/feeds/compliance/audit/,
         https://www.googleapis.com/auth/tasks
```

### References ###
[Drive SDK Service Account](https://developers.google.com/drive/service-accounts)