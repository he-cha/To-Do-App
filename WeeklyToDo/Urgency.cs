namespace ToDoApp 
{ 

    public class NormalTask : ToDoTask
    {
        public override int UrgencyLevel => 1;
    }
    public class SemiUrgentTask : ToDoTask
    {

        public override int UrgencyLevel => 2;
    }
    public class UrgentTask : ToDoTask
    {
        public override int UrgencyLevel => 3;
    }
}
