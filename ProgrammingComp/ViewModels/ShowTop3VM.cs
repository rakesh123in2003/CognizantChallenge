using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammingComp.ViewModels
{
    public class ShowTop3VM
    {
        public string Name { get; set; }

        public int Submissions { get; set; }

        public string Task { get; set; }

        public List<ShowTop3VM> Top3Submissions { get; set; }

    }
}