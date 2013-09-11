using System;
using System.Collections.Generic;
using System.Management.Automation;
using GaPoSh.Services;
using Google.Apis.Admin.Directory.directory_v1.Data;

namespace GaPoSh.Cmdlets
{
    [Cmdlet(VerbsCommon.Set, "GaPoShUserAddress")]
    public class SetGaPoShUserAddress : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Instance Session;

        [Parameter(Mandatory = true)]
        public string UserId;

        [Parameter(Mandatory = false)]
        public string Country;

        [Parameter(Mandatory = false)]
        public string CountryCode;

        [Parameter(Mandatory = false)]
        public string CustomType;

        [Parameter(Mandatory = false)]
        public string ExtendedAddress;

        [Parameter(Mandatory = false)]
        public string Locality;

        [Parameter(Mandatory = false)]
        public string PoBox;

        [Parameter(Mandatory = false)]
        public string PostalCode;

        [Parameter(Mandatory = false)]
        public bool? Primary;

        [Parameter(Mandatory = false)]
        public string Region;

        [Parameter(Mandatory = false)]
        public bool? SourceIsStructured;

        [Parameter(Mandatory = false)]
        public string StreetAddress;

        [Parameter(Mandatory = true)]
        public string Type;

        protected override void ProcessRecord()
        {
            ProcessRequest(Session);
        }

        private void ProcessRequest(Instance request)
        {
            try
            {
                var user = new User
                    {
                        Addresses = new List<UserAddress>
                            {
                                new UserAddress
                                    {
                                        Country = String.IsNullOrEmpty(Country) ? null : Country,
                                        CountryCode = String.IsNullOrEmpty(CountryCode) ? null : CountryCode,
                                        CustomType = String.IsNullOrEmpty(CustomType) ? null : CustomType,
                                        ExtendedAddress = String.IsNullOrEmpty(ExtendedAddress) ? null : ExtendedAddress,
                                        Locality = String.IsNullOrEmpty(Locality) ? null : Locality,
                                        PoBox = String.IsNullOrEmpty(PoBox) ? null : PoBox,
                                        Primary = Primary,
                                        Region = String.IsNullOrEmpty(Region) ? null : Region,
                                        SourceIsStructured = SourceIsStructured,
                                        StreetAddress = String.IsNullOrEmpty(StreetAddress) ? null : StreetAddress,
                                        Type = String.IsNullOrEmpty(Type) ? null : Type
                                    }
                            }
                    };

                var service = request.DirectoryService.Users.Update(user, UserId);

                service.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Update User Address!");
                Console.WriteLine("Error: " + e);
            }
        }
    }
}