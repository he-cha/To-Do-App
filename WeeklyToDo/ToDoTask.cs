using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ToDoApp
{
    public abstract class ToDoTask
    {
        
        private string name;
        private DateTime date;
        private string location;
        private string description;
        public abstract string Urgency { get; }
        public virtual int UrgencyLevel => 1; // Default: Normal

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
            return $"{Name} - {Urgency} - {Date.ToShortDateString()} - {Location} - {Description}";
        }

    }
}
