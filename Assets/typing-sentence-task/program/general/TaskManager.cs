using System.Collections.Generic;
using System.Linq;

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
                new Task2(),
                new Task3(),
                new Task4()
            };
        }
        
        public static ParticipantTask GetParticipantTask()
        {
            return TaskList.FirstOrDefault(task => task.TaskName == NextTaskName);
        }
    }
}


