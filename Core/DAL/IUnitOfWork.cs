using Common.UnitOfWork;
using Core.QuestionModule.Repositories;
using Core.UserManagement.Repositories;

namespace Core.DAL
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuestionAnswerRepository QuestionAnswerRepository { get; }
    }
}
