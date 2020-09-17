using System;
using Microsoft.AspNetCore.Mvc;
using Users.BLL;
using Users.BLL.DataContract;
using Users.Models;

namespace Users.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                PartialView("UserGrid", ControllerHelper.FillUserGridModel(UsersBLL.GetAllUsers()));

                return View("ManageUser");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            try
            {
                return PartialView();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserModel model)
        {
            try
            {
                CreateUserReq _createUserReq = new CreateUserReq()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    CellPhoneNumber = model.CellPhoneNumber
                };

                return PartialView("UserGridRow", ControllerHelper.FillUserGridModel(UsersBLL.CreateUser(_createUserReq)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult EditUser(string userId)
        {
            try
            {
                return PartialView(ControllerHelper.FillEditUserModel(UsersBLL.GetUserById(userId)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult EditUser(EditUserModel model)
        {
            try
            {
                EditUserReq _editUserReq = new EditUserReq()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Surname = model.Surname,
                    CellPhoneNumber = model.CellPhoneNumber
                };

                return PartialView("UserGridRowLine", ControllerHelper.FillUserGridModel(UsersBLL.EditUser(_editUserReq)));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public void DeleteUser(string actionValue)
        {
            try
            {
                UsersBLL.DeleteUser(actionValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
