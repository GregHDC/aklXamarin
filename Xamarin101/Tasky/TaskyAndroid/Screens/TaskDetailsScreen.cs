namespace TaskyAndroid.Screens 
{
    using Android.App;
    using Android.Graphics;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Shared;
    using Tasky.Interfaces;
    using Tasky.Models;

    [Activity (Label = "Task Details")]			
	public class TaskDetailsScreen : Activity 
    {
		protected ITaskItem taskItem = new TaskItem();
		protected Button cancelDeleteButton = null;
		protected EditText notesTextEdit = null;
		protected EditText nameTextEdit = null;
		protected Button saveButton = null;
		CheckBox doneCheckbox;
        private int taskID;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			View titleView = Window.FindViewById(Android.Resource.Id.Title);
			if (titleView != null) {
			  IViewParent parent = titleView.Parent;
			  if (parent != null && (parent is View)) {
			    View parentView = (View)parent;
			    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75 ,0xFF)); //38, 117 ,255
			  }
			}

			taskID = Intent.GetIntExtra("TaskID", 0);
			
			// set our layout to be the home screen
			SetContentView(Resource.Layout.TaskDetails);
			nameTextEdit = FindViewById<EditText>(Resource.Id.txtName);
			notesTextEdit = FindViewById<EditText>(Resource.Id.txtNotes);
			saveButton = FindViewById<Button>(Resource.Id.btnSave);
			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);
			
			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.btnCancelDelete);

			// button clicks 
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

        protected override async void OnResume()
        {
            base.OnResume();
            if (taskID > 0)
            {
                taskItem = await BootStrapper.Resolve<ITaskItemManager>().GetTask(taskID);
            }
            else
            {
                taskItem = BootStrapper.Resolve<ITaskItem>();
            }

            // set the cancel delete based on whether or not it's an existing task
            cancelDeleteButton.Text = (taskItem.ID == 0 ? "Cancel" : "Delete");

            // name
            nameTextEdit.Text = taskItem.Name;

            // notes
            notesTextEdit.Text = taskItem.Notes;

            // done
            doneCheckbox.Checked = taskItem.Done;
        }

        protected async void Save()
		{
			taskItem.Name = nameTextEdit.Text;
			taskItem.Notes = notesTextEdit.Text;
			taskItem.Done = doneCheckbox.Checked;
			await BootStrapper.Resolve<ITaskItemManager>().SaveTask(taskItem);
			Finish();
		}
		
		protected async void CancelDelete()
		{
			if(taskItem != null) 
            {
				await BootStrapper.Resolve<ITaskItemManager>().DeleteTask(taskItem);
			}

			Finish();
		}
	}
}