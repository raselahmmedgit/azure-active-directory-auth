using lab.azure_active_directory_auth.Core.Identity;
using lab.azure_active_directory_auth.JwtGenerator;
using lab.azure_active_directory_auth.Managers;
using lab.azure_active_directory_auth.Repositories;
using lab.azure_active_directory_auth.Utility;
using lab.azure_active_directory_auth.Web.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lab.azure_active_directory_auth
{
    public class BootStrapper
    {
        public static void Run(WebApplicationBuilder builder)
        {
            try
            {
                // Add services to the container.
                builder.Services.AddControllersWithViews();

                //Add our Config object so it can be injected
                builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(ConnectionStrings.Name));
                builder.Services.Configure<SmsConfig>(builder.Configuration.GetSection(SmsConfig.Name));
                builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(EmailConfig.Name));
                builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.Name));
                builder.Services.Configure<AppDbConfig>(builder.Configuration.GetSection(AppDbConfig.Name));

                builder.Services.AddScoped<IMemberRepository, MemberRepository>();
                builder.Services.AddScoped<IMemberManager, MemberManager>();


                builder.Services.AddRouting(options => options.LowercaseUrls = true);

                // Add functionality to inject IOptions<T>
                builder.Services.AddOptions();
                builder.Services.AddCors();

                AppConstants.WebRootPath = builder.Environment.WebRootPath;
                AppConstants.ContentRootPath = builder.Environment.ContentRootPath;

                #region Identity

                builder.Services.Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                //    .AddEntityFrameworkStores<AppIdentityDbContext>()
                //    .AddDefaultTokenProviders();

                //builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

                //builder.Services.AddScoped<ITokenManager, TokenManager>();


                //builder.Services.ConfigureApplicationCookie(options =>
                //{
                //    // Cookie settings
                //    options.Cookie.HttpOnly = true;
                //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                //    options.LogoutPath = "/Account/Logout";
                //    options.LoginPath = "/Account/Login";
                //    options.AccessDeniedPath = "/Account/AccessDenied";
                //    options.SlidingExpiration = true;
                //});

                #endregion

                #region MemoryCache
                //builder.Services.AddDistributedMemoryCache();
                //builder.Services.AddMemoryCache();
                #endregion

                //builder.Services.AddMvc(
                //   options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
                //);
                //call this in case you need aspnet-user-authtype/aspnet-user-identity
                builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                #region JwtToken

                JwtTokenOptions jwtTokenOptions = new JwtTokenOptions();
                var jwtTokenSectiion = builder.Configuration.GetSection(JwtTokenOptions.Name);
                jwtTokenSectiion.Bind(jwtTokenOptions);
                builder.Services.Configure<JwtTokenOptions>(jwtTokenSectiion);

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidAudience = builder.Configuration["JwtToken:ValidAudience"],
                        ValidIssuer = builder.Configuration["JwtToken:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:Secret"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[CookieAuthenticationDefaults.AuthenticationScheme];
                            context.HttpContext.User = context.Principal;
                            return Task.CompletedTask;
                        },
                    };
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

                #endregion

                builder.Services.RegisterAutoMapper();

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
