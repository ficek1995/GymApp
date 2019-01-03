using GymApp.Helpers;
using GymApp.Models;
using GymApp.Services;
using GymApp.ViewsModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GymApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public partial class UsersController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersController()
        {
                
        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        // GET: Users
        public virtual ActionResult Index()
        {
            return RedirectToAction(MVC.Users.List());
        }

        public virtual ActionResult List()
        {
            var users = UserService.GetAll().MapTo<List<UserViewModel>>();
            var currentUser = users.Where(x => x.Id == User.Identity.GetUserId()).SingleOrDefault();
            if (currentUser != null)
            {
                users.Remove(currentUser);
            }
            return View(MVC.Users.Views.List, users);
        }

        // GET: Users/Details/5
        public virtual ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public virtual ActionResult Register()
        {
            return View(MVC.Users.Views.Register);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IsTrainer = model.IsTrainer,
                    Deleted = false
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (model.IsTrainer == true)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        var store = new UserStore<ApplicationUser>(context);
                        var manager = new UserManager<ApplicationUser>(store);

                        manager.AddToRole(user.Id, "Trener");
                    }

                }


                return RedirectToAction(MVC.Users.List());
            }
            return View(MVC.Users.Views.Register, model);
        }

        // GET: Users/Delete/5
        public virtual ActionResult Delete(string id)
        {
            UserService.Remove(id);
            return RedirectToAction(MVC.Users.List());
        }

    }
}
