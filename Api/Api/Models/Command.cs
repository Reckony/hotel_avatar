using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Command
    {
        public string Message { get; set; }

        public Command()
        {

        }

        public Command(string message)
        {
            Message = message;
        }

    }
}
