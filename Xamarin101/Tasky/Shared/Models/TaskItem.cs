namespace Tasky.Models
{
    using Interfaces;

	public class TaskItem : BusinessEntityBase, ITaskItem
	{
		public TaskItem ()
		{
		}

		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}