using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Interfaces;

namespace ServiceCenter.Infrascructure.DataAccess.Repositories;

public class EFEmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EFEmployeeRepository(DataContext context, IFilterBuilder<Employee> filterBuilder) : base(context, filterBuilder)
    {
    }
}
