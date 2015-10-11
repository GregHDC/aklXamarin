namespace Tasky.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ITaskItemManager
    {
        Task<ITaskItem> GetTask(int id);

        Task<IList<TaskItem>> GetTasks();

        Task<int> SaveTask(ITaskItem item);

        Task<int> DeleteTask(ITaskItem item);
    }
}