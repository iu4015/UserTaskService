using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserTasksService.Models;

namespace UserTasksService.Data
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext context;

        public TasksRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(UserTask task)
        {
            context.Add(task);
        }

        public async Task<UserTask[]> GetAllUserTasksAsync(int userId)
        {

            IQueryable<UserTask> query = from uTask in context.UserTasks
                                         where uTask.UserId == userId
                                         select uTask;
            
            return await query.ToArrayAsync();
        }


        public async Task<UserTask> GetTaskAsync(int taskId)
        {
            IQueryable<UserTask> query = from uTask in context.UserTasks
                                         where uTask.Id == taskId
                                         select uTask;
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
