//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.DAL;
using Core.DAL.Implementations;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.QuestionModule.Repositories;

internal class QuestionRepository : BaseRepository<int, QuestionEntity>, IQuestionRepository
{
    private readonly DatabaseContext _context;
    public QuestionRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public override IQueryable<QuestionEntity> GetsQueryableNoTracker()
    {
        return _context.Questions
            .Include(q => q.QuestionOptions)
            .AsNoTracking().AsQueryable();
    }

    public async Task DeleteHardAsync(ETypeOfQuestion keyModel)
        => await _context.Questions.Where(x => x.TypeOfQuestion == keyModel)
            .ExecuteDeleteAsync();
}
