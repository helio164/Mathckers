using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Mathckers_.Models
{
    public class Player
    {
                
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "XP")]
        public int XP { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

        [Display(Name = "Coins")]
        public int Coins { get; set; }

        [Key]        
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
        
        public int AvatarID { get; set; }

        [ForeignKey("AvatarID")]
        public virtual PlayerAvatar Avatar { get; set; }

    }

    public class PlayerAvatar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Avatar { get; set; }

        public int Price { get; set; }

        public int PowerA { get; set; }

        public int PowerD { get; set; }
    }


    public class PlayerLoginViewModel
    {
        [Key]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "XP")]
        public int XP { get; set; }

        [Display(Name = "Coins")]
        public int Coins { get; set; }
    }
}