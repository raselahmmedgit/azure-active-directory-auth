using lab.azure_active_directory_auth.Core;
using lab.azure_active_directory_auth.Core.Identity;
using lab.azure_active_directory_auth.Helpers;
using lab.azure_active_directory_auth.JwtGenerator;
using lab.azure_active_directory_auth.Managers;
using lab.azure_active_directory_auth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab.azure_active_directory_auth.Controllers
{
    //[Authorize]
    public class AdminController : BaseController
    {
        #region Global Variable Declaration
        private readonly ILogger<AdminController> _iLogger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenManager _iTokenManager;
        private readonly IMemberManager _iMemberManager;
        #endregion

        #region Constructor
        public AdminController(ILogger<AdminController> iLogger, SignInManager<ApplicationUser> signInManager, ITokenManager iTokenManager, IMemberManager iMemberManager)
        {
            _iLogger = iLogger;
            _signInManager = signInManager;
            _iTokenManager = iTokenManager;
            _iMemberManager = iMemberManager;
        }
        #endregion

        #region Actions
        public IActionResult Index(string token)
        {
            try
            {
                var isValidateToken = _iTokenManager.IsValidateToken(token);
                if (isValidateToken)
                {
                    var adminViewModel = new AdminViewModel() { Token = token };
                    return View(adminViewModel);
                }
                else
                {
                    return View("/Home/Unauthorized");
                }
                
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }

        //[Route("Members")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Members()
        {
            try
            {
                var accessToken = Request.Headers["Authorization"];
                string token = await HttpContext.GetTokenAsync("access_token");

                var isValidateToken = _iTokenManager.IsValidateToken(token);
                if (isValidateToken)
                {
                    ViewData["Token"] = token;
                    var members = await _iMemberManager.GetMembersAsync();

                    _result = Result.Ok(MessageHelper.Success, data: members);
                    return Ok(_result);
                    //return View(members);
                }
                else
                {
                    _result = Result.Fail(MessageHelper.Fail);
                    return Ok(_result);
                    //return View("/Home/Unauthorized");
                }
                
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LoggerMessageHelper.FormateMessageForException(ex, "Members"));
                //_result = Result.Fail(MessageHelper.Fail);
                //return Json(_result);
                //return ErrorView(ex);
            }

            _result = Result.Ok(MessageHelper.Fail, "/Home/Index");
            return Ok(_result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                _iLogger.LogInformation(LoggerMessageHelper.LogFormattedMessageForRequestStart("LogOut", $"User:{User.Identity.Name}"));
                await _signInManager.SignOutAsync();
                _iLogger.LogInformation(LoggerMessageHelper.LogFormattedMessageForRequestSuccess("LogOut", $"User logged out"));

                //_result = Result.Ok(MessageHelper.LogOut);
                //return Ok(_result);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LoggerMessageHelper.FormateMessageForException(ex, "Logout"));
                //_result = Result.Fail(MessageHelper.LogOutFail);
                //return Json(_result);
            }

            //_result = Result.Ok(MessageHelper.LogOutFail, "/Home/Index");
            //return Ok(_result);
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}