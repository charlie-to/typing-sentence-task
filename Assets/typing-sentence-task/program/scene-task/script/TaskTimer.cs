using System;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskTimer
    {
        public DateTime startTime;
        // タスク文を読む時間
        public TimeSpan readTime;
        // タスクを行う時間
        public TimeSpan taskTime;
        
        public TaskTimer(int readTime_min,int taskTime_min)
        {
            this.readTime = new TimeSpan(0, readTime_min, 0);
            this.taskTime = new TimeSpan(0, taskTime_min, 0);
        }
        
        public void Start()
        {
            startTime = DateTime.Now;
        }
        
        // readTimeの時間が経過したかどうか
        public bool IsReadTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - startTime;
            return diff >= readTime;
        }
        
        // 時間切れの場合はtrueを返す
        public bool IsTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - startTime;
            return diff >= readTime + taskTime;
        }
        // 残り時間を返す
        public double GetRemainingTime()
        {
            var now = DateTime.Now;
            var diff = now - startTime;
            return (taskTime+readTime - diff).TotalSeconds;
        }
    }
}