using Common.UnitOfWork;
using Core.IDBModule.Repositories;
using Core.QuestionModule.Repositories;
using Core.SymptomsModule.Repositories;
using Core.UserManagement.Repositories;

namespace Core.DAL
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuestionAnswerRepository QuestionAnswerRepository { get; }
        IIDBRepository IDBRepository { get; }
        ISymptomRepository SymptomRepository { get; }
    }
}
