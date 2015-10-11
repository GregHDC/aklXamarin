namespace Tasky.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Interfaces;
    using Models;

    public class TaskItemManager : ITaskItemManager
	{
	    private readonly ITaskRepository _repository;

		public TaskItemManager (ITaskRepository repository)
		{
		    this._repository = repository;
		}
		
		public async Task<ITaskItem> GetTask(int id)
		{
		    return await this._repository.GetTaskById(id);
		}
		
		public async Task<IList<TaskItem>> GetTasks ()
		{
            IEnumerable<TaskItem> tasks = await this._repository.GetAllTasks();
		    return tasks.ToList();
		}
		
		public async Task<int> SaveTask (ITaskItem item)
		{
		    return await this._repository.SaveTaskItem(item);
		}
		
		public async Task<int> DeleteTask(ITaskItem item)
		{
            return await this._repository.DeleteTaskItem(item);
		}
	}
}