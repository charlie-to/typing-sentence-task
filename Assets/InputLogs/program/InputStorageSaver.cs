using System.IO;
using UnityEngine;

namespace InputLogs.program
{
    // タスクの保存に必要なデータを保持するクラス
    public class InputStorageSaver
    {
        // 保存するパス
        private readonly string _relativeOutPath;

        // コンストラクタ
        public InputStorageSaver(string path)
        {
            this._relativeOutPath = path;
        }

        // 保存
        internal void Save(InputsStorage inputsStorage)
        {
            Debug.Log(Directory.GetCurrentDirectory());
            // ディレクトリが存在しない場合は作成する
            if (!Directory.Exists(Directory.GetCurrentDirectory() + _relativeOutPath))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + _relativeOutPath);
            }
            FileInfo fi = new FileInfo(Directory.GetCurrentDirectory() + _relativeOutPath + inputsStorage.TaskName + ".csv");

            // 書き込み
            var sw = fi.AppendText();
            // メタデータを書き込み
            sw.WriteLine("Participant ID:, "+inputsStorage.ParticipantId);
            sw.WriteLine("Task Name:, "+inputsStorage.TaskName);
            // 開始時刻と終了時刻を書き込み
            sw.WriteLine("Task Start:, "+inputsStorage.startTime);
            sw.WriteLine("Task End:, "+inputsStorage.endTime);
            // Dividerを書き込み
            sw.WriteLine("------,------");
            sw.WriteLine("Key, Time");
            
            //インプットデータを書き込み
            foreach (InputDatum inputDatum in inputsStorage.inputDatas)
            {
                sw.WriteLine(inputDatum.key + "," + $"{inputDatum.time:yyyy/MM/dd HH:mm:ss.fff}");
            }
            // 終了を書き込み
            sw.WriteLine("End,------");
            // ファイルを閉じる
            sw.Flush();
            sw.Close();
        }

    }
}