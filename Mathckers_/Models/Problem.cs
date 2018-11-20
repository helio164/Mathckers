using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Mathckers_.Models
{
    public class ProblemModel
    {
        public string id { get; set; }

        public string question { get; set; }

        public string[] choices { get; set; }

        public int correct_choice { get; set; }

        public string instruction { get; set; }

        public string category { get; set; }

        public string topic { get; set; }

        public string difficulty { get; set; }
        
    }
}