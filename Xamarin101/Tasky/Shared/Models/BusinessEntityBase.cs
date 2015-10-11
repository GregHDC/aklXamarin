namespace Tasky.Models
{
    using Interfaces;
    using SQLite;

	public class BusinessEntityBase : IBusinessEntity
    {
		public BusinessEntityBase ()
		{
		}
		
		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
	}
}