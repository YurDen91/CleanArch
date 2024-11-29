namespace GymManagement.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangeAsync();
}