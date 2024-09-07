//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.DAL;
using Core.DAL.Implementations;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.Repositories;

internal class QuestionAnswerRepository : BaseRepository<int, QuestionAnswerEntity>, IQuestionAnswerRepository
{
    public QuestionAnswerRepository(DatabaseContext context) : base(context)
    {
    }
}
