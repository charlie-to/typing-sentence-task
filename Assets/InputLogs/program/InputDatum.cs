using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.InputLogs.program
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