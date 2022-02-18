using System;
using System.Collections.Generic;
using System.Text;

namespace Wiser.API.BL.Config
{
    public class Constants
    {
        public static readonly Guid DEFAULT_GUID = new Guid("{00000000-0000-0000-0000-000000000000}");
        public const string PAYLOAD_EMPTY = "API Payload is empty";
        public const string ERROR_OCCURRED = "An error occurred";
        public static readonly DateTime WISER_TIME=DateTime.Now;
    }
}
