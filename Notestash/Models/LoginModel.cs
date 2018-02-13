using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NotestashDataAccess;

namespace Notestash.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool Check(LoginModel objUser)
        {
            try
            {
                using (NoteStashDBEntities db = new NoteStashDBEntities())
                {
                    var userCredentials =
                        db.tblUsers.FirstOrDefault(e => e.Email.Equals(objUser.Email) && e.Password.Equals(objUser.Password));

                    if (userCredentials != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}