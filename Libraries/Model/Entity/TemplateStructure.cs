using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Libraries.Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace Libraries.Model.Entity
{
    public class TemplateStructure 
    {
        public string parameterValue { get; set; }
        public string parameterName { get; set; }
        public string parameterLevel { get; set; }
        public bool parameterSkip { get; set; }
        public IList<string> parameterAction { get; set; }

    }
}