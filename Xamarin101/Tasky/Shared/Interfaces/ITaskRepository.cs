namespace Tasky.Interfaces
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasks();
        Task<ITaskItem> GetTaskById(int id);
        Task<int> SaveTaskItem(ITaskItem taskItem);
        Task<int> DeleteTaskItem(ITaskItem taskItem);
    }
}