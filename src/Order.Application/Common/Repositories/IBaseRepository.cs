namespace Order.Application.Common.Repositories;

public interface IBaseRepository
{
    public Task<int> SaveChangesAsync();
}