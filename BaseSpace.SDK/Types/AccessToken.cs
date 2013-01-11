using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.Types
{
    public class AccessToken
    {
        public string TokenString { get; set; }
        public int ExpiresIn { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }

        public bool IsPending { get { return Error == "authorization_pending"; } }
        public bool IsAccessDenied { get { return Error == "access_denied"; } }
        public bool IsTokenGranted { get { return !String.IsNullOrEmpty(TokenString); } }
    }
}
