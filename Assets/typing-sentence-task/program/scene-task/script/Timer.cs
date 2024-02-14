using System;

namespace typing_sentence_task.program.scene_task.script
{
    public class Timer
    {
        private DateTime _startTime;
        // タスク文を読む時間
        private TimeSpan _readTime;
        // タスクを行う時間
        private TimeSpan _taskTime;

        public void SetLimitTime(int readTimeSec,int taskTimeSec)
        {
            this._readTime = new TimeSpan(0, 0, readTimeSec);
            this._taskTime = new TimeSpan(0, 0, taskTimeSec);
        }
        
        public void Start()
        {
            _startTime = DateTime.Now;
        }
        
        // readTimeの時間が経過したかどうか
        public bool IsReadTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - _startTime;
            return diff >= _readTime;
        }
        
        // 時間切れの場合はtrueを返す
        public bool IsTimeOver()
        {
            var now = DateTime.Now;
            var diff = now - _startTime;
            return diff >= _readTime + _taskTime;
        }
        // 残り時間を返す
        public double GetRemainingTime()
        {
            var now = DateTime.Now;
            var diff = now - _startTime;
            return (_taskTime+_readTime - diff).TotalSeconds;
        }
    }
}