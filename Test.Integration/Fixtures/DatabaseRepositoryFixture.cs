using Core.DAL;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using Core.SymptomsModule.Abstractions.Enums;
using Core.SymptomsModule.Entities;
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

        public async Task QuestionAddAsync(string title,
            ETypeOfQuestion typeOfQuestion,
            IEnumerable<(string name, int value)> options)
        {
            await _context.Questions.AddAsync(new QuestionEntity
            {
                Title = title,
                TypeOfQuestion = typeOfQuestion,
                QuestionOptions = options.Select(x => new QuestionOption
                {
                    Name = x.name,
                    Value = x.value
                }).ToList()
            });
            await _context.SaveChangesAsync();
        }
        public async Task<IList<QuestionEntity>> QuestionGetsAsync(ETypeOfQuestion typeOfQuestion)
        {
            return await _context.Questions.Where(x => x.TypeOfQuestion == typeOfQuestion).ToListAsync();
        }


        public async Task<QuestionAnswerEntity> QuestionAnswerGetAsync(
            string userName, int questionId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return await _context.QuestionAnswers.SingleOrDefaultAsync(x => x.QuestionId == questionId && x.UserId == user.Id);
        }
        #endregion

        #region Symptoms

        public Task<List<SymptomEntity>> SymptomGetAsync(string userName)
        {
            return _context.Symptoms
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName).ToListAsync();
        }

        public async Task SymptomAddAsync(string userName,
            ETypeOfSymptoms typeOfSymptom, int value,
            DateTime? dtCreate = null)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            await _context.Symptoms.AddAsync(
               new SymptomEntity
               {
                   UserId = user.Id,
                   TypeOfSymptom = typeOfSymptom,
                   Value = value,
                   DateTimeOfUpdate = dtCreate ?? DateTime.Now
               });
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
