using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mathckers_.Models
{
    public class Attack
    {
        
        public Guid ID { get; set; }

        public bool Defended {get;set;}

        public string Quest {get;set;}

        public int Points {get;set;}

        [Display(Name ="Attacker")]
        public string AttackerID { get; set; }

        [ForeignKey("AttackerID")]
        public virtual Player Attacker { get; set; }

        [Display(Name = "Defender")]
        public string DefenderID { get; set; }

        [ForeignKey("DefenderID")]
        public virtual Player Defender { get; set; }
        
    }

    public class AttackViewModel
    {

        public Guid ID { get; set; }

        public bool Defended { get; set; }

        public string Quest { get; set; }

        public int Points { get; set; }

        [Display(Name = "Attacker")]
        public string AttackerID { get; set; }

        [ForeignKey("AttackerID")]
        public virtual Player Attacker { get; set; }

        [Display(Name = "Defender")]
        public string DefenderID { get; set; }

        [ForeignKey("DefenderID")]
        public virtual Player Defender { get; set; }

        public string ProblemID { get; set; }

        [ForeignKey("ProblemID")]
        public virtual ProblemModel Problem { get; set; }

        public int? Answer { get; set; }

    }
}