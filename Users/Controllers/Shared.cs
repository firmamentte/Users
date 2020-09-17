using System;
using Microsoft.AspNetCore.Mvc;
using Users.Models.Shared;

namespace Users.Controllers
{
    public class Shared : Controller
    {
        [HttpGet]
        public ActionResult YesNo(string actionController, string actionMethod, string actionValue, string yesNoMessage)
        {
            try
            {
                return PartialView("_YesNo", new YesNoModel()
                {
                    ActionController = actionController,
                    ActionMethod = actionMethod,
                    ActionValue = actionValue,
                    YesNoMessage = yesNoMessage
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Ok(string okMessage, string messageSymbol)
        {
            return PartialView("_Ok", new OkModel() { OkMessage = okMessage, MessageSymbol = messageSymbol });
        }
    }
}
