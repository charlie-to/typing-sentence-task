using System.IO;
using UnityEngine;

namespace InputLogs.program
{
    // タスクの保存に必要なデータを保持するクラス
    public class InputStorageSaver
    {
        // 保存するパス
        private string _relativeOutPath;
        // 保存するファイル名
        public string FileName = "test";

        // コンストラクタ
        public InputStorageSaver(string path)
        {
            this._relativeOutPath = path;
        }

        // 保存
        public void Save(InputsStorage inputsStorage)
        {
            // // InputStorageがDoneでない場合は保存しない
            // if (inputsStorage.taskState != TaskState.Done)
            // {
            //     return;
            // }
            // ファイル名を設定
            // ディレクトリが存在しない場合は作成する
            if (!Directory.Exists(Application.dataPath + _relativeOutPath))
            {
                Directory.CreateDirectory(Application.dataPath + _relativeOutPath);
            }
            FileInfo fi = new FileInfo(Application.dataPath + _relativeOutPath + FileName + ".csv");

            // 書き込み
            var sw = fi.AppendText();
            foreach (InputDatum inputDatum in inputsStorage.inputDatas)
            {
                sw.WriteLine(inputDatum.key + "," + $"{inputDatum.time:yyyy/MM/dd HH:mm:ss.fff}");
            }
            sw.Flush();
            sw.Close();
        }

    }
}