using System;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Groupssettings.v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShGroupSettings")]
    public class SetGaPoShGroupSettings : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string GroupId;

        [Parameter(Mandatory = false)]
        public string AllowExternalMembers;

        [Parameter(Mandatory = false)]
        public string AllowGoogleCommunication;

        [Parameter(Mandatory = false)]
        public string AllowWebPosting;

        [Parameter(Mandatory = false)]
        public string ArchiveOnly;

        [Parameter(Mandatory = false)]
        public string CustomReplyTo;

        [Parameter(Mandatory = false)]
        public string DefaultMessageDenyNotificationText;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public string Email;

        [Parameter(Mandatory = false)]
        public string IncludeInGlobalAddressList;

        [Parameter(Mandatory = false)]
        public string IsArchived;

        [Parameter(Mandatory = false)]
        public int? MaxMessageBytes;

        [Parameter(Mandatory = false)]
        public string MembersCanPostAsTheGroup;

        [Parameter(Mandatory = false)]
        public string MessageDisplayFont;

        [Parameter(Mandatory = false)]
        public string MessageModerationLevel;

        [Parameter(Mandatory = false)]
        public string SendMessageDenyNotification;

        [Parameter(Mandatory = false)]
        public string ShowInGroupDirectory;

        [Parameter(Mandatory = false)]
        public string SpamModerationLevel;

        [Parameter(Mandatory = false)]
        public string Name;

        [Parameter(Mandatory = false)]
        public string ReplyTo;

        [Parameter(Mandatory = false)]
        public string PrimaryLanguage;

        [Parameter(Mandatory = false)]
        public string WhoCanPostMessage;

        [Parameter(Mandatory = false)]
        public string WhoCanInvite;

        [Parameter(Mandatory = false)]
        public string WhoCanJoin;

        [Parameter(Mandatory = false)]
        public string WhoCanViewGroup;

        [Parameter(Mandatory = false)]
        public string WhoCanViewMembership;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var groups = new Groups
                    {
                        AllowExternalMembers = String.IsNullOrEmpty(AllowExternalMembers) ? null : AllowExternalMembers,
                        AllowGoogleCommunication = String.IsNullOrEmpty(AllowGoogleCommunication) ? null : AllowGoogleCommunication,
                        AllowWebPosting = String.IsNullOrEmpty(AllowWebPosting) ? null : AllowWebPosting,
                        ArchiveOnly = String.IsNullOrEmpty(ArchiveOnly) ? null : ArchiveOnly,
                        CustomReplyTo = String.IsNullOrEmpty(CustomReplyTo) ? null : CustomReplyTo,
                        DefaultMessageDenyNotificationText = String.IsNullOrEmpty(DefaultMessageDenyNotificationText) ? null : DefaultMessageDenyNotificationText,
                        Description = String.IsNullOrEmpty(Description) ? null : Description,
                        Email = String.IsNullOrEmpty(Email) ? null : Email,
                        IncludeInGlobalAddressList = String.IsNullOrEmpty(IncludeInGlobalAddressList) ? null : IncludeInGlobalAddressList,
                        IsArchived = String.IsNullOrEmpty(IsArchived) ? null : IsArchived,
                        MaxMessageBytes = MaxMessageBytes ?? null,
                        MembersCanPostAsTheGroup = String.IsNullOrEmpty(MembersCanPostAsTheGroup) ? null : MembersCanPostAsTheGroup,
                        MessageDisplayFont = String.IsNullOrEmpty(MessageDisplayFont) ? null : MessageDisplayFont,
                        MessageModerationLevel = String.IsNullOrEmpty(MessageModerationLevel) ? null : MessageModerationLevel,
                        SendMessageDenyNotification = String.IsNullOrEmpty(SendMessageDenyNotification) ? null : SendMessageDenyNotification,
                        ShowInGroupDirectory = String.IsNullOrEmpty(ShowInGroupDirectory) ? null : ShowInGroupDirectory,
                        SpamModerationLevel = String.IsNullOrEmpty(SpamModerationLevel) ? null : SpamModerationLevel,
                        Name = String.IsNullOrEmpty(Name) ? null : Name,
                        ReplyTo = String.IsNullOrEmpty(ReplyTo) ? null : ReplyTo,
                        PrimaryLanguage = String.IsNullOrEmpty(PrimaryLanguage) ? null : PrimaryLanguage,
                        WhoCanPostMessage = String.IsNullOrEmpty(WhoCanPostMessage) ? null : WhoCanPostMessage,
                        WhoCanInvite = String.IsNullOrEmpty(WhoCanInvite) ? null : WhoCanInvite,
                        WhoCanJoin = String.IsNullOrEmpty(WhoCanJoin) ? null : WhoCanJoin,
                        WhoCanViewGroup = String.IsNullOrEmpty(WhoCanViewGroup) ? null : WhoCanViewGroup,
                        WhoCanViewMembership = String.IsNullOrEmpty(WhoCanViewMembership) ? null : WhoCanViewMembership
                    };

                var service = request.GroupSettingsService.Groups.Update(groups, GroupId);
                WriteObject(service.Execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update Group Settings!");
                Console.WriteLine("Error: " + e);
                WriteObject(false);
            }
        }
    }
}