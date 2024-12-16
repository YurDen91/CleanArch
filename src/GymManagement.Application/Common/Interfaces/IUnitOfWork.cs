namespace GymManagement.Api.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}