using System;
using System.Linq;
using System.Threading.Tasks;
using Libraries.Service.IApplicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SiteMaster.Helper;
namespace SiteMaster.Middleware
{
    public class SessionRelay
    {
        private readonly RequestDelegate _next;
        public SessionRelay(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("Home/Logout");
                await context.Response.WriteAsync("You're not logged in.");
                return;
            }
           // var sessionId = serviceProvider.GetRequiredService<ISessionContext>();
            
            string _sessionIPAdress = string.Empty;
            string _sessionBrowserInfo = string.Empty;
            string _CurrentUserID = string.Empty;
            string _sessionUserID = string.Empty;
            string _sessionTimestamp = string.Empty;
            string _sessionGUID = string.Empty;
            string _encryptedString = string.Empty;
            if (context.Session != null)
            {
                if (context.Request.Cookies["AuthToken"] != null)
                { 
                     _encryptedString = context.Request.Cookies["AuthToken"];

                    //var userSessionvalue = await _sessionservice.GetSessionAsync(sessionId.sessionId);
                    //if (userSessionvalue==null)
                    //{
                    //    InvalidateSession(context);
                    //    await context.Response.WriteAsync("Dear User, Your session has expired. Error 401 Invalid Session");
                    //    return;
                    //}
                    if (!string.IsNullOrEmpty(_encryptedString))
                    {                         
                        byte[] _encodedAsBytes = System.Convert.FromBase64String(_encryptedString);
                        string _decryptedString = System.Text.ASCIIEncoding.ASCII.GetString(_encodedAsBytes);
                        char[] _separator = new char[] { '^' };
                        if (!string.IsNullOrEmpty(_decryptedString))
                        {
                            string[] _splitStrings = _decryptedString.Split(_separator);
                            if (_splitStrings.Length > 0)
                            {
                                _sessionUserID = _splitStrings[0];
                                _sessionTimestamp = _splitStrings[1];
                                _sessionGUID = _splitStrings[3];
                                if (string.IsNullOrEmpty(context.Session.GetString("sessionGUID")))
                                {
                                    context.Session.SetString("sessionGUID", _sessionGUID);
                                }
                                string[] _userBrowserInfo = _splitStrings[2].Split('~');
                                if (_userBrowserInfo.Length > 0)
                                {
                                    _sessionBrowserInfo = _userBrowserInfo[0];
                                    _sessionIPAdress = _userBrowserInfo[1];
                                }
                            }
                        }

                        string _currentuseripAddress = string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"].FirstOrDefault())
                            ? context.Connection.RemoteIpAddress.ToString()
                            : context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                        System.Net.IPAddress result;
                        if (!System.Net.IPAddress.TryParse(_currentuseripAddress, out result))
                        {
                            result = System.Net.IPAddress.None;
                        }

                        string _currentBrowserInfo = context.Request.Headers["User-Agent"].ToString();
                        _CurrentUserID = context.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;

                        // Validate timestamp
                        if (DateTime.TryParse(_sessionTimestamp, out DateTime tokenTimestamp))
                        {
                            // Set your desired token validity period here
                            TimeSpan tokenValidity = TimeSpan.FromMinutes(30);
                            if (DateTime.Now - tokenTimestamp > tokenValidity)
                            {
                                InvalidateSession(context);
                                await context.Response.WriteAsync("Dear User, Your session has expired. Error 401 Invalid Session");
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(_sessionIPAdress))
                        {
                            if (context.Session.GetString("sessionGUID") != _sessionGUID | _sessionIPAdress != _currentuseripAddress || _sessionBrowserInfo != _currentBrowserInfo || _sessionUserID != _CurrentUserID)
                            {
                                InvalidateSession(context);
                                await context.Response.WriteAsync("Dear User, You're not logged in. Error 401 Invalid Session");
                                return;
                            }
                        }
                        else
                        {
                            InvalidateSession(context);
                            await context.Response.WriteAsync("Dear User, You're not logged in. Error 401 Invalid Session");
                            return;
                        }
                    }
                    else
                    {
                        InvalidateSession(context);
                        await context.Response.WriteAsync("Dear User, You're not logged in. Error 401 Invalid Session");
                        return;
                    }
                }
                else
                {
                    InvalidateSession(context);
                    await context.Response.WriteAsync("Dear User, You're not logged in. Error 401 Invalid Session");
                    return;
                }
            }
            else
            {
                InvalidateSession(context);
                await context.Response.WriteAsync("Dear User, You're not logged in. Error 401 Invalid Session");
                return;
            }

            await _next.Invoke(context);
        }

        private void InvalidateSession(HttpContext context)
        {
            context.Session.Clear();
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddSeconds(-30)
            };
            context.Response.Cookies.Append(".AspNetCore.Session", Guid.NewGuid().ToString(), options);
            options.Expires = DateTime.Now.AddSeconds(-30);
            context.Response.Cookies.Append("AuthToken", "", options);
            context.Response.StatusCode = 401; // Unauthorized
        }
        private void RedirectToLogin(HttpContext context)
        {
            context.Response.StatusCode = 302; // Redirect status code
            context.Response.Headers["Location"] = "/Home/Logout"; // Login page URL
        }
    }
}
