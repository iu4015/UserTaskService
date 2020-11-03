using System.Threading.Tasks;
using UserTasksService.Models;

namespace UserTasksService.Data
{
    public interface ITasksRepository
    {
        Task<UserTask> GetTaskAsync(int taskId);
        Task<UserTask[]> GetAllUserTasksAsync(int userId);
        void Add(UserTask task);
        Task<bool> SaveChangesAsync();
    }
}
