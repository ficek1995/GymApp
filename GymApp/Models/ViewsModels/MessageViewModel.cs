using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymApp.Models.ViewsModels
{
    public class MessageViewModel
    {
        [Display(Name = "Treść")]
        public string Text { get; set; }
        public  int Id { get; set; }
    }
}