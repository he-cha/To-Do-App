using Microsoft.Win32;
using System.ComponentModel;
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
        private ICollectionView taskView;
        public MainWindow()
        {
            
            InitializeComponent();
            ListBoxTasks.ItemsSource = taskManager.Tasks;
            taskView = CollectionViewSource.GetDefaultView(taskManager.Tasks);
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
            task.UrgencyString = urgency;

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
            editedTask.UrgencyString = urgency;

            taskManager.EditTask(index, editedTask);
            ListBoxTasks.Items.Refresh();
            ClearForm();
            
        }

        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxTasks.SelectedItem is ToDoTask selectedTask)
            {
                TaskNameTextBox.Text = selectedTask.Name;
                DatePicker.SelectedDate = selectedTask.Date;
                LocationTextBox.Text = selectedTask.Location;
                DescriptionTextBox.Text = selectedTask.Description;

                // Set urgency ComboBox
                string urgency = selectedTask switch
                {
                    NormalTask => "Normal Urgency",
                    SemiUrgentTask => "Semi-Urgent",
                    UrgentTask => "Urgent",
                    _ => "Normal Urgency"
                };

                foreach (ComboBoxItem item in UrgencyComboBox.Items)
                {
                    if (item.Content.ToString() == urgency)
                    {
                        UrgencyComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

       
        
        
        private void ShowAllTasks()
        {
            taskView.Filter = null;
            SortByUrgencyDescending();
            taskView.Refresh();
        }
        private void AllTasksButton_Click(object sender, RoutedEventArgs e) => ShowAllTasks();

        private void ShowUrgentTasks()
        {
            taskView.Filter = task => task is UrgentTask;
            taskView.Refresh();
        }
        private void UrgentButton_Click(object sender, RoutedEventArgs e) => ShowUrgentTasks();

        private void ShowDailyTasks()
        {
            taskView.Filter = task =>
            {
                if (task is ToDoTask t)
                    return t.Date.Date == DateTime.Today;
                return false;
            };
            SortByUrgencyDescending();
            taskView.Refresh();
        }
        private void DailyButton_Click(object sender, RoutedEventArgs e) => ShowDailyTasks();

        private void ShowTasksForDay(DayOfWeek day)
        {
            taskView.Filter = task =>
            {
                if (task is ToDoTask t)
                    return t.Date.DayOfWeek == day;
                return false;
            };
            SortByUrgencyDescending();
            taskView.Refresh();
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && Enum.TryParse(button.Tag.ToString(), out DayOfWeek day))
            {
                ShowTasksForDay(day);
                SortByUrgencyDescending();
            }
        }


        private void SortByUrgencyDescending()
        {
            taskView.SortDescriptions.Clear();
            taskView.SortDescriptions.Add(new SortDescription(nameof(ToDoTask.UrgencyLevel), ListSortDirection.Descending));
            taskView.Refresh();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json"
            };

            if (dialog.ShowDialog() == true)
            {
                taskManager.SaveToFile(dialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json"
            };

            if (dialog.ShowDialog() == true)
            {
                taskManager.LoadFromFile(dialog.FileName);
                ListBoxTasks.Items.Refresh();
            }
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