using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TaskManagerCLI
{
    internal class CustomTask
    {
        public ulong Id { get; set;}
        public string Name { get; set;}
        public string Description { get; set;}
        public DateTime CreatedATime { get; set;}
        public DateTime UpdatedATime { get; set;}
        
        public int status { get; set;}

    }

}
