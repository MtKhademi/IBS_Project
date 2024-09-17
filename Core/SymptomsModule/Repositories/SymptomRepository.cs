using Core.DAL;
using Core.DAL.Implementations;
using Core.SymptomsModule.Entities;

namespace Core.SymptomsModule.Repositories;

internal class SymptomRepository : BaseRepository<int, SymptomEntity>, ISymptomRepository
{
    private readonly DatabaseContext _context;
    public SymptomRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }


}
