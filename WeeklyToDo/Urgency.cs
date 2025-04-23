using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class NormalTask : ToDoTask
    {
        public override string Urgency => "Normal Urgency";
    }
    public class SemiUrgentTask : ToDoTask
    {
        public override string Urgency => "Semi Urgent";
    }
    public class UrgentTask : ToDoTask
    {
        public override string Urgency => "Urgent";
    }
}
