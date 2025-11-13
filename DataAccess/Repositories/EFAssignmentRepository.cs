using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFAssignmentRepository : BaseRepository<OrderEmployee>, IAssignmentRepository
{
    public EFAssignmentRepository(DataContext context, IFilterBuilder<OrderEmployee> filterBuilder) : base(context, filterBuilder)
    {
    }
}
