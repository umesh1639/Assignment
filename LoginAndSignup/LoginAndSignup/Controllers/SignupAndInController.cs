using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LoginAndSignup.Models;
using System.Web.Security;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace LoginAndSignup.Controllers
{
    public class SignupAndInController : Controller
    {
        private static decimal PhoneNumber=0;

        private LoginAndSignupEntities1 context;

        
        // GET: SignupAndIn
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignIn model)
        {

           // bool isUserExist = context.UserData.Any(x => x.MobileNumber == PhoneNumber);
            //sendSMS(PhoneNumber, "1234");
            if (model.OTP.ToString() == "1234" && false)
            {
                return RedirectToAction("SignUp", "SignupAndIn");
            }
            if (true)
            {
                FormsAuthentication.SetAuthCookie(PhoneNumber.ToString(), false);
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("SignUp", "SignupAndIn");
            }
        }

        private void sendSMS(string phoneNumber, string otp)
        {
            TwilioClient.Init("AC318cda7e962685e27f04a0536881b5e8", "f95b2180025c67db56abce97e635daa6");

            var message = MessageResource.Create(
                body: $"Hi there! your OTP is {otp}",
                from: new Twilio.Types.PhoneNumber("+13852090911"),
                to: new Twilio.Types.PhoneNumber("+91"+phoneNumber)
            );
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserData model)
        {
            using (var context = new LoginAndSignupEntities1())
            {
                context.UserData.Add(model);
                context.SaveChanges();
            }

            return RedirectToAction("SignIn");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "SignupAndIn");
        }

        public ActionResult GenerateOtp()
        {
            PhoneNumber = 0;
            context=new LoginAndSignupEntities1();
            return View();
        }
        [HttpPost]
        public ActionResult GenerateOtp(SignIn model)
        {
            
            bool isUserExist = context.UserData.Any(x => x.MobileNumber == model.MobileNumber);

            //sendSMS(model.MobileNumber.ToString(), "1234");
            if (isUserExist)
            {
                PhoneNumber = model.MobileNumber;
                return RedirectToAction("SignIn", "SignupAndIn");
            }
            else
            {
                return RedirectToAction("SignUp", "SignupAndIn");
            }
        }
    }
}