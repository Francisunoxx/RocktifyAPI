using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Transaction CheckEmail(Registration registration)
        {
            using (var db = new MyContext())
            {
                var check = db.Registrations
                                     .Where(x => x.Email == registration.Email)
                                     .FirstOrDefault();

                if (check != null)
                {
                    return new Transaction { Message = "Email is already taken!", IsCompleted = false };
                }
                else
                {
                    return new Transaction { Message = "Successful!", IsCompleted = true };
                }
            }
        }

        public Transaction CheckUserName(Registration registration)
        {
            using (var db = new MyContext())
            {
                var check = db.Registrations
                     .Where(x => x.UserName == registration.UserName)
                     .FirstOrDefault<Registration>();

                if (check != null)
                {
                    return new Transaction { Message = "UserName is already taken!", IsCompleted = false };
                }
                else
                {
                    return new Transaction { Message = "Successful!", IsCompleted = true };
                }
            }
        }

        public Transaction CreateUser(Registration registration)
        {
            var userName = CheckUserName(registration);
            var email = CheckEmail(registration);
            var w = new Transaction();

            try
            {
                if (userName.IsCompleted && email.IsCompleted)
                {
                    using (var db = new MyContext())
                    {
                        db.Registrations.Add(new Registration
                        {
                            FirstName = registration.FirstName,
                            LastName = registration.LastName,
                            UserName = registration.UserName,
                            Email = registration.Email,
                            Password = registration.Password
                        });

                        db.SaveChanges();

                        w = new Transaction { Message = "Successful", IsCompleted = true };
                    }
                }
                else if (!userName.IsCompleted && email.IsCompleted)
                {
                    w = userName;
                }
                else if (userName.IsCompleted && !email.IsCompleted)
                {
                    w = email;
                }
                else if (!userName.IsCompleted && !email.IsCompleted)
                {
                    w = new Transaction { Message = "Email and UserName are already taken!", IsCompleted = false };
                }
            }
            catch (Exception ex)
            {
                w = new Transaction { Message = ex.Message, IsCompleted = false };
            }

            return w;
        }
    }

}
