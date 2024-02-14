using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace typing_sentence_task.program.general
{
    public abstract class ParticipantTask
    {
        // task name has just task name
        private string TaskName
        {
            get;
        }
        
        // task scene name returns the name of task scene
        public string TaskSceneName => "scene-" + TaskName;

        protected  ParticipantTask(string taskName)
        {
            this.TaskName = taskName;
        }
    }
}

