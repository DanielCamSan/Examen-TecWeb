namespace TecWebFest.Repositories
{
    public interface IStageRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
