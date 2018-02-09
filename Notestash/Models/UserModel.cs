using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NotestashDataAccess;

namespace Notestash.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public string Password { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public bool Create(UserModel objUser)
        {
            try
            {
                tblUser objTblUser = new tblUser();
                objTblUser.Id = objUser.Id;
                objTblUser.FullName = objUser.FullName;
                objTblUser.Password = objUser.Email;
                objTblUser.Email = objUser.Password;

                using (NoteStashDBEntities db = new NoteStashDBEntities())
                {
                    db.tblUsers.Add(objTblUser);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}