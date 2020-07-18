using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Mvc
{
    public class BaseController : Controller
    {
        public User _user;
        public Carts _cart;

        public BaseController()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                //string UserID = User.Claims.FirstOrDefault(a =>
                //                                a.Type.Equals("UserID", StringComparison.InvariantCultureIgnoreCase)).Value;

                //DB To Set User, Cart
                _user = new User();
                //Get Cart From Redis
                _cart = new Carts()
                {
                    CartItems = new List<Item>(),
                    CartId = -1
                };
            }
            catch
            {
                context.Result = RedirectToAction("Error", "Error");
            }
        }
    }
}