using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace Assets.InputLogs.program
{
    // タスクの保存に必要なデータを保持するクラス
    public class SaveTaskInputData
    {
        // 保存するパス
        public string relative_path = "/Out/Logs/";
        // 保存するファイル名
        public string file_name;

        // コンストラクタ
        public SaveTaskInputData(string path)
        {
            this.relative_path = path;
        }

        // 保存
        public void Save(InputsStorage inputsStorage)
        {
            // // InputStrageがDoneでない場合は保存しない
            // if (inputsStorage.taskState != TaskState.Done)
            // {
            //     return;
            // }
            // ファイル名を設定
            StreamWriter sw;
            // ディレクトリが存在しない場合は作成する
            if (!Directory.Exists(Application.dataPath + relative_path))
            {
                Directory.CreateDirectory(Application.dataPath + relative_path);
            }
            FileInfo fi = new FileInfo(Application.dataPath + relative_path + file_name + ".csv");

            // 書き込み
            sw = fi.AppendText();
            foreach (InputDatum inputDatum in inputsStorage.inputDatas)
            {
                sw.WriteLine(inputDatum.key + "," + String.Format("{0:yyyy/MM/dd HH:mm:ss.fff}", inputDatum.time));
            }
            sw.Flush();
            sw.Close();
        }

    }
}