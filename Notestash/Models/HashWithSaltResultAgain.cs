using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notestash.Models
{
    public class HashWithSaltResultAgain
    {
        public string Salt { get; }
        public string Digest { get; set; }

        public HashWithSaltResultAgain(string salt, string digest)
        {
            Salt = salt;
            Digest = digest;
        }
    }
}