
namespace ToDoApp
{
    public class ToDoTask
    {

        public string name;
        public DateTime date;
        public string location;
        public string description;

        // Use this string to store the urgency
        public string UrgencyString {  get; set; }

        public virtual int UrgencyLevel => 1;
     

        public string Name { 
            get { return name; }
            set { name = value ?? throw new ArgumentNullException(nameof(value), "Name cannot be null"); }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value ?? throw new ArgumentNullException(nameof(value), "Description cannot be null"); }
        }


        public override string ToString()
        {
            return $"{Name} - {UrgencyString} - {Date.ToShortDateString()} - {Location} - {Description}";
        }

    }
}
