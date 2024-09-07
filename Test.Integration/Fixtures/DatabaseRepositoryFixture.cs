using Core.DAL;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using Core.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MDF.Test.Fixtures
{
    internal class DatabaseRepositoryFixture
    {
        private readonly DatabaseContext _context;

        public DatabaseRepositoryFixture(DatabaseContext context)
        {
            _context = context;
        }


        #region User

        public async Task<UserEntity?> UserGetWithUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(us => us.UserName == userName);
        }

        public async Task<UserEntity?> UserAddAsyc(string userName, string phone, string password)
        {
            var entity = new UserEntity()
            {
                UserName = userName,
                Phone = phone,
                Password = password,
                Otp = "",
                Age = 0,
                Education = "",
                IsMarried = false,
                LocationOfLiving = "",
                Sex = false,
                Work = ""
            };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> UserGetCountAsync()
        {
            return await _context.Users.CountAsync();
        }


        #endregion

        #region Question

        public async Task<IList<QuestionEntity>> QuestionGetsAsync(ETypeOfQuestion typeOfQuestion)
        {
            return await _context.Questions.Where(x => x.TypeOfQuestion == typeOfQuestion).ToListAsync();
        }
        #endregion
    }
}
