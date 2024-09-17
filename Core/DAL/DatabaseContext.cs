//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.IDBModule.Entities;
using Core.QuestionModule.Entities;
using Core.SymptomsModule.Entities;
using Core.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<QuestionAnswerEntity> QuestionAnswers { get; set; }

    public DbSet<IDBEntity> IDBs { get; set; }
    public DbSet<SymptomEntity> Symptoms { get; set; }

}
