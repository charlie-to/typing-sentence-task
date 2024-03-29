using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace typing_sentence_task.program.general
{
    public abstract class ParticipantTask
    {
        // task name has just task name
        public string TaskName
        {
            get;
        }
        
        // 安静時間
        public int QuietSeconds => 30;
        public int ThinkingSeconds => 60;
        public int TaskSeconds => 480;
        
        public bool IsFinished
        {
            get; set;
        }

        protected  ParticipantTask(string taskName)
        {
            this.TaskName = taskName;
            IsFinished = false;
        }
    }
}

