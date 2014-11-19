using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Zoo.DAL.Abstract;
using Zoo.BLL.Entities;


namespace Zoo.UI.Provider
{
    public class ZooRoleProvider : RoleProvider
    {
        private IRepository<User> _db;
        private IRepository<Role> _role;

        public ZooRoleProvider()
        {
            _db = new ZooRepository<User>();
            _role = new ZooRepository<Role>();
        }
        
   public override string[] GetRolesForUser(string login)
        {
            string[] role = new string[] { };
            try
                {
                 // Получаем пользователя
                 var user =  _db.GetAll.Where(u => u.Login == login).FirstOrDefault();
                 if (user != null)
                    {
                        // получаем роль
                       // Role userRole = _db.Roles.Find(user.RoleId);
                        Role userRole = _role.GetAll.Where(u => u.Id == user.RoleId).FirstOrDefault();
                        if (userRole != null)
                        {
                            role = new string[] { userRole.Name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            return role;
        }

   public override bool IsUserInRole(string username, string roleName)
        {
           bool outputResult = false;
           try
                {
                    // Получаем пользователя
                    var user = _db.GetAll.Where(u => u.Login == username).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        Role userRole = _role.GetAll.Where(u => u.Id == user.RoleId).FirstOrDefault();
                        //сравниваем
                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            return outputResult;
        }

        #region Not implement methods
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
#endregion

    }
}