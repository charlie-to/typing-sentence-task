using System.Collections.Generic;

namespace typing_sentence_task.program.general
{
    public static class TaskManager
    {
        public static string NextTaskName
        {
            get;
            set;
        }
        
        public static List<ParticipantTask> TaskList
        {
            get;
        }
        
        static TaskManager()
        {
            TaskList = new List<ParticipantTask>
            {
                new TaskPractice(),
                new Task1(),
                new Task2()
                
            };
        }
    }
}


