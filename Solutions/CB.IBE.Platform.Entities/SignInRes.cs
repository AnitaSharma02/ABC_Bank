using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CB.IBE.Platform.Masters.Entities;

namespace CB.IBE.Platform.Entities
{
    public class SignInRes
    {
        public string SessionId { get; set; }
        public RefererDetails RefererDetails { get; set; }
    }
}
