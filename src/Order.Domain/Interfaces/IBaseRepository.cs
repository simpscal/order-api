namespace Order.Domain.Interfaces;

public interface IBaseRepository
{
    public Task<int> SaveChangesAsync();
}