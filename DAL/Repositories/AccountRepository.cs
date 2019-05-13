using DAL.Interfaces;
using ViewModel;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Transaction ValidateEmail(Registration registration)
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
                    return new Transaction { Message = "Email is free to use!", IsCompleted = true };
                }
            }
        }

        public Transaction ValidateUsername(Registration registration)
        {
            using (var db = new MyContext())
            {
                var check = db.Registrations
                     .Where(x => x.Username == registration.Username)
                     .FirstOrDefault<Registration>();

                if (check != null)
                {
                    return new Transaction { Message = "Username is already taken!", IsCompleted = false };
                }
                else
                {
                    return new Transaction { Message = "Username is free to use!", IsCompleted = true };
                }
            }
        }

        public Transaction CreateUser(UserRegistration userRegistration)
        {
            var w = new Transaction();
            var email = ValidateEmail(userRegistration.Registration);
            var userName = ValidateUsername(userRegistration.Registration);

            if (userName.IsCompleted && email.IsCompleted)
            {
                using (var context = new MyContext())
                {
                    using (DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        User u = new User();

                        u.Registration = new Registration
                        {
                            Firstname = userRegistration.Registration.Firstname,
                            Lastname = userRegistration.Registration.Lastname,
                            Username = userRegistration.Registration.Username,
                            Email = userRegistration.Registration.Email
                        };

                        u.UserAccount = new UserAccount { Password = userRegistration.UserAccount.Password };

                        context.Users.Add(u);

                        context.SaveChanges();

                        transaction.Commit();

                        w = new Transaction { Message = "Successful!", IsCompleted = true };
                    }
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
                w = new Transaction { Message = "Email and Username are already taken!", IsCompleted = false };
            }

            return w;
        }

        public Transaction ValidateAccount(User u)
        {
            var w = new Transaction();

            using (var db = new MyContext())
            {
                var check = db.Users
                    .Join(db.Registrations,
                            user => user.UserId,
                            registration => registration.RegistrationId,
                            (user, registration) => new { user, registration })
                    .Join(db.UserAccounts,
                            user1 => user1.user.UserId,
                            useraccount => useraccount.UserAccountId,
                            (user1, useraccount) => new { user1, useraccount })
                    .Where(x => x.user1.registration.Username == u.Registration.Username &&
                            x.useraccount.Password == u.UserAccount.Password)
                    .Select(x => new
                    {
                        x.user1.registration.Username,
                        x.useraccount.Password
                    })
                    .AsQueryable()
                    .ToList();

                if (check.Count != 0)
                {
                    w = new Transaction { Message = "Successful!", IsCompleted = true };
                }
                else
                {
                    w = new Transaction { Message = "Invalid Username or Password", IsCompleted = false };
                }
            }
        
            return w;
        }
    }
}
