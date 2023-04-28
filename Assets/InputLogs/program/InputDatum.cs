using System;

namespace InputLogs.program
{
    // 入力したキーと時間を保持するクラス
    [Serializable]
    public class InputDatum
    {
        public string key;
        public DateTime time;

        public InputDatum(string key, DateTime time)
        {
            this.key = key;
            this.time = time;
        }
    }
}