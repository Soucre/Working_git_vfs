﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PhimHang.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PhimHang.Controllers
{
    [Authorize]
    public class FrontAccountController : Controller
    {
        public FrontAccountController()
            : this(new UserManager<UserCustom>(new CustomUserStore()))
        {
        }
        public FrontAccountController(UserManager<UserCustom> userManager)
        {
            UserManager = userManager;
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public UserManager<UserCustom> UserManager { get; private set; }
        private OFrontTEntities frontDb;
        private VfsCustomerServiceEntities customerDb;
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                bool signInSucess =  await SignInFrontAsync(model.UserName, model.Password);
                
                if (signInSucess)
                {
                    //var user = await UserManager.FindAsync(model.UserName, model.Password);
                    var user = new UserCustom { UserName = model.UserName, PasswordHash = model.Password };
                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);
                        using (customerDb = new VfsCustomerServiceEntities())
                        {
                            #region kiem tra khach hang VIP
                            var customer = await customerDb.Customers.FirstOrDefaultAsync(cs => cs.CustomerId == user.UserName);               
                            if (customer.VType == true)
                            {
                                Helper.SetCookieOfVIP();
                            }
                            #endregion

                            #region thong ke khach hang

                            var customerlog = await customerDb.CustomerLogs.FirstOrDefaultAsync(cl => cl.CustomerId == user.UserName);
                            if (customerlog == null)
                            {
                                // insert log
                                customerlog = new CustomerLog();
                                customerlog.CreateDate = DateTime.Now;
                                customerlog.CustomerId = user.UserName;
                                customerlog.Total_Download = 0;
                                customerlog.Total_Login = 1;
                                customerDb.CustomerLogs.Add(customerlog);                                
                            }
                            else
                            {
                                //update log                                
                                customerlog.Total_Download +=1;
                                customerlog.Total_Login +=1;
                                customerDb.Entry(customerlog).State = EntityState.Modified;

                            }
                            await customerDb.SaveChangesAsync(); // save database
                            #endregion
                        }
                        return RedirectToLocal(returnUrl);
                    }
                }
                
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private async Task SignInAsync(UserCustom user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private async Task<bool> SignInFrontAsync(string userName, string passWord)
        {
            using (frontDb = new OFrontTEntities())
            {

                /// chuyen password sang ma hoa MD5
                /// 
                string passwordMD5 = Helper.MD5Hash(passWord);
                // tim trong co so du lieu ung voi user name va password
                var user = await frontDb.FrontUsers.FirstOrDefaultAsync(fu => fu.UserName == userName && fu.Password == passwordMD5);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Helper.ReleaseCookieOfFace();
            return RedirectToAction("Index", "Home");
        }
    }
}
