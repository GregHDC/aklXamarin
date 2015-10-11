namespace Tasky.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using SQLite;

    public class TaskRepository : ITaskRepository
    {
        private readonly SQLiteAsyncConnection _databaseConnection;

        public TaskRepository()
        {
            this._databaseConnection = new SQLiteAsyncConnection(DatabaseFilePath, true);
            this._databaseConnection.CreateTableAsync<TaskItem>().Wait();
        }

        /// <summary>
        /// Gets the database file path.
        /// </summary>
        private static string DatabaseFilePath
        {
            get
            {
                const string SqliteFilename = "TaskRepositoryDatabase.db3";
                string path = string.Empty;

#if SILVERLIGHT
	            path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#else
#if __ANDROID__
                string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
#else
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string libraryPath = System.IO.Path.Combine(documentsPath, "..", "Library", "Databases");

                if (!System.IO.Directory.Exists(libraryPath))
                {
                    System.IO.Directory.CreateDirectory(libraryPath);
                }
#endif

                path = System.IO.Path.Combine(libraryPath, SqliteFilename);
#endif
                return path;
            }
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            return await this._databaseConnection.Table<TaskItem>().ToListAsync();
        }

        public async Task<ITaskItem> GetTaskById(int id)
        {
            return await this._databaseConnection.Table<TaskItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveTaskItem(ITaskItem taskItem)
        {
            if (taskItem != null)
            {
                if (taskItem.ID == 0)
                {
                    return await this._databaseConnection.InsertAsync(taskItem);
                }
                
                return await this._databaseConnection.UpdateAsync(taskItem);
            }

            return 0;
        }

        public async Task<int> DeleteTaskItem(ITaskItem taskItem)
        {
            if (taskItem != null)
            {
                return await this._databaseConnection.DeleteAsync(taskItem);
            }

            return 0;
        }
    }
}