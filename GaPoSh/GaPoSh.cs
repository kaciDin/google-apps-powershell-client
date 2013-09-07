using System.ComponentModel;
using System.Management.Automation;

namespace GaPoSh
{
    [RunInstaller(true)]
    public class GaPoSh : CustomPSSnapIn
    {
        /// <summary>
        /// Specify a description of the PowerShell snap-in.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This is a PowerShell snap-in provides commandline access to Google Apps Api's";
            }
        }

        /// <summary>
        /// Specify the localization resource information for the description.
        /// Use the format: resourceBaseName,Description.
        /// </summary>
        public override string DescriptionResource
        {
            get
            {
                return "GaPoSh,This is a PowerShell snap-in that includes the Google Apps Dot Net Client version 1.5 Beta. Please refer to https://code.google.com/p/google-api-dotnet-client/ for more information.";
            }
        }

        /// <summary>
        /// Specify the name of the PowerShell snap-in.
        /// </summary>
        public override string Name
        {
            get
            {
                return "GaPoSh";
            }
        }

        /// <summary>
        /// Specify the vendor for the PowerShell snap-in.
        /// </summary>
        public override string Vendor
        {
            get
            {
                return "Mosheldon, OpenTech Consulting";
            }
        }

        /// <summary>
        /// Specify the localization resource information for the vendor.
        /// Use the format: resourceBaseName,VendorName.
        /// </summary>
        public override string VendorResource
        {
            get
            {
                return "GaPoSh, Mosheldon";
            }
        }
    }
}
