using System.IO;
using UnityEngine;

namespace InputLogs.program
{
    // タスクの保存に必要なデータを保持するクラス
    public class InputStorageSaver
    {
        // 保存するパス
        private readonly string _relativeOutPath;
        // 保存するファイル名
        public string FileName = "test";

        // コンストラクタ
        public InputStorageSaver(string path)
        {
            this._relativeOutPath = path;
        }

        // 保存
        internal void Save(InputsStorage inputsStorage)
        {
            // ディレクトリが存在しない場合は作成する
            if (!Directory.Exists(Application.dataPath + _relativeOutPath))
            {
                Directory.CreateDirectory(Application.dataPath + _relativeOutPath);
            }
            FileInfo fi = new FileInfo(Application.dataPath + _relativeOutPath + FileName + ".csv");

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
            sw.Flush();
            sw.Close();
        }

    }
}