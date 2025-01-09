using lab.azure_active_directory_auth.Utility;
using lab.azure_active_directory_auth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab.azure_active_directory_auth.Controllers
{
    public class HomeController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public IActionResult Authorized()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public IActionResult Unauthorized()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            try
            {
                LoginViewModel model = new LoginViewModel();
                model.ReturnUrl = returnUrl;
                return View(model);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        public IActionResult Register(string returnUrl = null)
        {
            try
            {
                RegisterViewModel model = new RegisterViewModel();
                model.ReturnUrl = returnUrl;
                model.RoleName = AppConstants.AppRoleName.Member.ToString();
                return View(model);
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}