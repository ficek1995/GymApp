using GymApp.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymApp.Models.ViewsModels
{
    public class UsersListViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<UserViewModel> Trainers { get; set; }
    }
}