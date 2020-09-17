
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Users.BLL.DataContract;

namespace Users.BLL
{
    public static class UsersBLL
    {
        public static string UserXMLPath { get; set; }

        private static XElement XElementContext
        {
            get
            {
                try
                {
                    return XElement.Load(UserXMLPath);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void SaveChanges(XElement xElementContext)
        {
            try
            {
                xElementContext.Save(UserXMLPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static UserResp CreateUser(CreateUserReq createUserReq)
        {
            try
            {
                var _xElementContext = XElementContext;

                var _newUser = new XElement("User",
                new XElement("UserId", CreateUserId()),
                new XElement("Name", createUserReq.Name),
                new XElement("Surname", createUserReq.Surname),
                new XElement("CellPhoneNumber", createUserReq.CellPhoneNumber));

                _xElementContext.Add(_newUser);

                SaveChanges(_xElementContext);

                return FillUserResp(_newUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static UserResp EditUser(EditUserReq editUserReq)
        {
            try
            {
                var _xElementContext = XElementContext;

                var _user = from item in _xElementContext.Elements("User")
                            where (string)item.Element("UserId") == editUserReq.UserId
                            select item;

                _user.FirstOrDefault().Element("Name").ReplaceNodes(editUserReq.Name);
                _user.FirstOrDefault().Element("Surname").ReplaceNodes(editUserReq.Surname);
                _user.FirstOrDefault().Element("CellPhoneNumber").ReplaceNodes(editUserReq.CellPhoneNumber);

                SaveChanges(_xElementContext);

                return FillUserResp(_user.FirstOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeleteUser(string userId)
        {
            try
            {
                var _xElementContext = XElementContext;

                var _user = from item in _xElementContext.Elements("User")
                            where (string)item.Element("UserId") == userId
                            select item;

                _user.FirstOrDefault().Remove();

                SaveChanges(_xElementContext);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static UserResp GetUserById(string userId)
        {
            try
            {
                var _user = from item in XElementContext.Elements("User")
                            where (string)item.Element("UserId") == userId
                            select item;

                return FillUserResp(_user.FirstOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<UserResp> GetAllUsers() 
        {
            try
            {
                List<UserResp> _userResps = new List<UserResp>();

                var _xElementContext = XElementContext;

                foreach (var item in _xElementContext.Elements())
                {
                    _userResps.Add(FillUserResp(item));
                }

                return _userResps;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static UserResp FillUserResp(XElement xElement)
        {
            try
            {
                if (xElement != null)
                {
                    return new UserResp()
                    {
                        UserId = xElement.Element("UserId").Value,
                        Name = xElement.Element("Name").Value,
                        Surname = xElement.Element("Surname").Value,
                        CellPhoneNumber = xElement.Element("CellPhoneNumber").Value
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CreateUserId()
        {
            try
            {
                string _userId = string.Empty;

                Random _random = new Random();

                _userId = _random.Next(10000, 2000000000).ToString();

                while (IsUserIdExisting(_userId))
                {
                    _userId = _random.Next(10000, 2000000000).ToString();
                }

                return _userId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsUserIdExisting(string userId)
        {
            try
            {
                var _user = from item in XElementContext.Elements("User")
                            where (string)item.Element("UserId") == userId
                            select item;

                return _user.FirstOrDefault() != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
