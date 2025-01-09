using lab.azure_active_directory_auth.Core.Identity;
using lab.azure_active_directory_auth.Helpers;
using lab.azure_active_directory_auth.JwtGenerator;
using lab.azure_active_directory_auth.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab.azure_active_directory_auth.Components
{
    public class UserInfo: ViewComponent
    {
        #region Global Variable Declaration
        private readonly ILogger<UserInfo> _iLogger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _iTokenManager;
        #endregion

        #region Constructor
        public UserInfo(ILogger<UserInfo> iLogger, UserManager<ApplicationUser> userManager, ITokenManager iTokenManager)
        {
            _iLogger = iLogger;
            _userManager = userManager;
            _iTokenManager = iTokenManager;
        }
        #endregion

        #region Actions
        public async Task<IViewComponentResult> InvokeAsync(string token)
        {
            LoginUserViewModel loggedUserViewModel = new LoginUserViewModel();
            try
            {
                loggedUserViewModel = await GetUser(token);
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LoggerMessageHelper.FormateMessageForException(ex, "InvokeAsync"));
            }
            return View(loggedUserViewModel);
        }

        public async Task<LoginUserViewModel> GetUser(string token)
        {
            LoginUserViewModel loggedUserViewModel = new LoginUserViewModel();
            try
            {
                var tokenClaimsPrincipal = _iTokenManager.GetClaimsPrincipalByToken(token);
                var user = await _userManager.GetUserAsync(tokenClaimsPrincipal);
                if (user != null) 
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    loggedUserViewModel.UserName = user?.UserName;
                    loggedUserViewModel.RoleName = roles.FirstOrDefault();
                }
                
            }
            catch (Exception ex)
            {
                _iLogger.LogError(LoggerMessageHelper.FormateMessageForException(ex, "GetUser"));
            }
            return loggedUserViewModel;
        }
        #endregion
    }
}
