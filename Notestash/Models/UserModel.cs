﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using NotestashUserDataAccess;
using SecurityDriven.Inferno;
using SecurityDriven.Inferno.Kdf;
using PBKDF2 = SecurityDriven.Inferno.Kdf.PBKDF2;
using SecurityDriven.Inferno.Extensions;
using static SecurityDriven.Inferno.SuiteB;
using static SecurityDriven.Inferno.Utils;


namespace Notestash.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password should be between 6 to 15 characters!")]
        public string Password { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password should be between 6 to 15 characters!")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public bool Create(UserModel objUser)
        {
            var sha384Factory = HmacFactory;
            var random = new CryptoRandom();

            byte[] derivedKey;
            string hashedPassword = null;
            string passwordText = objUser.Password;

            byte[] passwordBytes = SafeUTF8.GetBytes(passwordText);
            var salt = random.NextBytes(384 / 8);

            using (var pbkdf2 = new PBKDF2(sha384Factory, passwordBytes, salt, 256 * 1000))
                derivedKey = pbkdf2.GetBytes(384 / 8);


            using (var hmac = sha384Factory())
            {
                hmac.Key = derivedKey;
                hashedPassword = hmac.ComputeHash(passwordBytes).ToBase16();
            }

            try
            {
                tblUserData objTblUser = new tblUserData();
                objTblUser.Id = objUser.Id;
                objTblUser.FullName = objUser.FullName;
                objTblUser.Password = hashedPassword;
                objTblUser.Email = objUser.Email;
                objTblUser.Salt = salt;

                using (NotestashUserDataBaseEntities db = new NotestashUserDataBaseEntities())
                {
                    var existingUser = db.tblUserDatas.FirstOrDefault(e => e.Email.Equals(objUser.Email));

                    if (existingUser == null)
                    {
                        db.tblUserDatas.Add(objTblUser);
                        db.SaveChanges();

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