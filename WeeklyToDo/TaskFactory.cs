

namespace ToDoApp
{
    public static class TaskFactory
    {
        public static ToDoTask CreateTaskFromUrgency(string urgency)
        {
            return urgency switch
            {
                "Normal Urgency" => new NormalTask { UrgencyString = urgency },
                "Semi-Urgent" => new SemiUrgentTask { UrgencyString = urgency },
                "Urgent" => new UrgentTask { UrgencyString = urgency },
                _ => new NormalTask { UrgencyString = "Normal Urgency" }
            };
        }
    }
}
