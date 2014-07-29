﻿using System;

namespace Illumina.BaseSpace.SDK.Types
{
    public class VerificationCode
    {
        // TODO: convert these "access" options to an enum
        public const string AccessBrowseGlobal = "browse global";
        public const string AccessCreateBrowseGlobal = "create global, browse global";

        public Uri VerificationUri { get; set; }
        public Uri VerificationWithCodeUri { get; set; }
        public int ExpiresIn { get; set; }
        public string UserCode { get; set; }
        public string DeviceCode { get; set; }
        public int Interval { get; set; }
    }
}
