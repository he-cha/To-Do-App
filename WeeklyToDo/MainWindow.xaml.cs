using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp;

namespace WeeklyToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskManager taskManager = new TaskManager();
        public MainWindow()
        {
            
            InitializeComponent();
            ListBoxTasks.ItemsSource = taskManager.Tasks;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (UrgencyComboBox.SelectedItem is not ComboBoxItem selectedItem || selectedItem.Content is null)
            {
                MessageBox.Show("Please select an urgency level.");
                return;
            }

            string urgency = selectedItem!.Content.ToString();

            ToDoTask task = urgency switch
            {
                "Normal Urgency" => new NormalTask(),
                "Semi-Urgent" => new SemiUrgentTask(),
                "Urgent" => new UrgentTask(),
                _ => throw new ArgumentException("Invalid urgency type")
            };

            task.Name = TaskNameTextBox.Text;
            task.Date = DatePicker.SelectedDate ?? DateTime.Now;
            task.Location = LocationTextBox.Text;
            task.Description = DescriptionTextBox.Text;

            taskManager.AddTask(task);
            ListBoxTasks.Items.Refresh();
            ClearForm();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTasks.SelectedItem is ToDoTask selectedTask)
            {
                taskManager.RemoveTask(selectedTask);
                ListBoxTasks.Items.Refresh();
               
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
            ClearForm();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ListBoxTasks.SelectedIndex;

            if (index < 0)
            {
                MessageBox.Show("Please select a task to edit.");
                return;
            }

            string urgency = ((ComboBoxItem)UrgencyComboBox.SelectedItem)?.Content?.ToString() ?? "Normal Urgency";

            ToDoTask editedTask = urgency switch
            {
                "Normal Urgency" => new NormalTask(),
                "Semi-Urgent" => new SemiUrgentTask(),
                "Urgent" => new UrgentTask(),
                _ => new NormalTask()
            };

            editedTask.Name = TaskNameTextBox.Text;
            editedTask.Date = DatePicker.SelectedDate ?? DateTime.Now;
            editedTask.Location = LocationTextBox.Text;
            editedTask.Description = DescriptionTextBox.Text;

            taskManager.EditTask(index, editedTask);
            ListBoxTasks.Items.Refresh();
            ClearForm();
            
        }




        private void ClearForm()
        {
            TaskNameTextBox.Text = "";
            DatePicker.SelectedDate = null;
            LocationTextBox.Text = "";
            DescriptionTextBox.Text = "";
            UrgencyComboBox.SelectedIndex = 0;
        }

    }

}