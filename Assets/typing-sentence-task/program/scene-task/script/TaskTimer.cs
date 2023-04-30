using System;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskTimer
    {
        public DateTime StartTime;
        // タスク文を読む時間
        public TimeSpan ReadTime;
        // タスクを行う時間
        public TimeSpan TaskTime;

        public TaskTimer()
        {
            
        }
        
        public void SetLimitTime(int readTimeMin,int taskTimeMin)
        {
            this.ReadTime = new TimeSpan(0, readTimeMin, 0);
            this.TaskTime = new TimeSpan(0, taskTimeMin, 0);
        }
        
        public void Start()
        {
            StartTime = DateTime.Now;
        }
        
        // readTimeの時間が経過したかどうか
        public bool IsReadTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - StartTime;
            return diff >= ReadTime;
        }
        
        // 時間切れの場合はtrueを返す
        public bool IsTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - StartTime;
            return diff >= ReadTime + TaskTime;
        }
        // 残り時間を返す
        public double GetRemainingTime()
        {
            var now = DateTime.Now;
            var diff = now - StartTime;
            return (TaskTime+ReadTime - diff).TotalSeconds;
        }
    }
}