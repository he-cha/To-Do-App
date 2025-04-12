using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyToDo
{
    abstract class Task
    {
        private string name;
        private DateTime date;
        private string location;
        private string description;
        private string urgency;

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
        public string Urgency
        {
            get { return urgency; }
            set { urgency = value; }
        }
    }
}
