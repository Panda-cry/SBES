using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class SecurityException
    {
        private string message;
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
