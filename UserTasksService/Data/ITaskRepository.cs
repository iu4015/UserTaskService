using System.Threading.Tasks;
using UserTasksService.Models;

namespace UserTasksService.Data
{
    public interface ITaskRepository
    {
        Task<IncomingUserTask> GetUserTaskAsync(int userId, int taskId);
        Task<IncomingUserTask[]> GetAllUserTasksAsync(int userId);
        void Add(UserTask task);
        Task<bool> SaveChangesAsync();
    }
}
