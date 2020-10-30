using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace UserTasksService.Models
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(UserTask task)
        {
            context.Add(task);
        }

        public async Task<IncomingUserTask[]> GetAllUserTasksAsync(int userId)
        {

            IQueryable<IncomingUserTask> query = from uTask in context.UserTasks
                                                 where uTask.UserId == userId
                                                 select new IncomingUserTask
                                                 {
                                                     TaskNumber = uTask.TaskDate.ToString("yyyyMMdd") + "-" + string.Format("{0:0000}", uTask.Number),
                                                     Date = uTask.Date,
                                                     Status = uTask.Status,
                                                     Comment = uTask.Comment
                                                 };

            return await query.ToArrayAsync();
        }


        public async Task<IncomingUserTask> GetUserTaskAsync(int userId, int taskId)
        {
            IQueryable<IncomingUserTask> query = from uTask in context.UserTasks
                                                 where uTask.UserId == userId && uTask.Id == taskId
                                                 select new IncomingUserTask
                                                 {
                                                     TaskNumber = uTask.TaskDate.ToString("yyyyMMdd") + "-" + string.Format("{0:0000}", uTask.Number),
                                                     Date = uTask.Date,
                                                     Status = uTask.Status,
                                                     Comment = uTask.Comment
                                                 };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
