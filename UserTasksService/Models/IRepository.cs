using System.Threading.Tasks;

namespace UserTasksService.Models
{
    public interface IRepository
    {
        Task<IncomingUserTask> GetUserTaskAsync(int userId, int taskId);
        Task<IncomingUserTask[]> GetAllUserTasksAsync(int userId);
        void Add(UserTask task);
        Task<bool> SaveChangesAsync();
    }
}
