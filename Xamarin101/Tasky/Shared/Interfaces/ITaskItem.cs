namespace Tasky.Interfaces 
{
	public interface ITaskItem : IBusinessEntity 
    {
        string Name { get; set; }
        string Notes { get; set; }
        bool Done { get; set; }
	}
}