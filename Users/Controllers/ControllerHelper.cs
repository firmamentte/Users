using System;
using System.Collections.Generic;
using Users.BLL.DataContract;
using Users.Models;

namespace Users.Controllers
{
    public static class ControllerHelper
    {
        public static List<UserGridModel> FillUserGridModel(List<UserResp> userResps)
        {
            try
            {
                List<UserGridModel> _userGridModels = new List<UserGridModel>();

                foreach (var item in userResps)
                {
                    _userGridModels.Add(new UserGridModel()
                    {
                        UserId = item.UserId,
                        Name = item.Name,
                        Surname = item.Surname,
                        CellPhoneNumber = item.CellPhoneNumber
                    });
                }

                return _userGridModels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UserGridModel> FillUserGridModel(UserResp userResp)
        {
            try
            {
                return new List<UserGridModel>()
                {
                    new UserGridModel()
                    {
                        UserId = userResp.UserId,
                        Name = userResp.Name,
                        Surname = userResp.Surname,
                        CellPhoneNumber = userResp.CellPhoneNumber
                    }
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static EditUserModel FillEditUserModel(UserResp userResp)
        {
            try
            {
                return new EditUserModel()
                {
                    UserId = userResp.UserId,
                    Name = userResp.Name,
                    Surname = userResp.Surname,
                    CellPhoneNumber = userResp.CellPhoneNumber
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
