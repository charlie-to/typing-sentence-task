using System;
using System.Collections.Generic;
using UnityEngine;

namespace InputLogs.program
{
    // １つのタスクに対してのInputDatumを複数保持するクラス
    [Serializable]
    public class InputsStorage
    {
        // タスクの名前
        public readonly string TaskName;
        // タスクに対してのInputDatumのリスト
        // ReSharper disable once IdentifierTypo
        public List<InputDatum> inputDatas;
        // タスクの開始時間
        public DateTime startTime;
        // タスクの終了時間
        public DateTime endTime;
        // 被験者のID
        public readonly string ParticipantId;
        // タスクの状態
        public TaskState taskState = TaskState.NotYet;
        // 保存関係のクラス
        public InputStorageSaver InputStorageSaver;

        // シングルトンを実装
        public static InputsStorage instance { get; private set; }

        // コンストラクタ
        public InputsStorage(string taskName, string participantId, string outFilePath)
        {
            if (instance != null)
            {
                return;
            }
            this.TaskName = taskName;
            this.ParticipantId = participantId;
            inputDatas = new List<InputDatum>();
            InputStorageSaver = new InputStorageSaver(outFilePath);
        }

        // インスタンスの取得
        public static InputsStorage GetInstance(string taskName, string participantId, string outFilePath)
        {
            if (instance != null)
            {
                return instance;
            }
            instance = new InputsStorage(taskName, participantId, outFilePath);
            return null;
        }

        // 計測開始
        public void ReadingStart()
        {
            taskState = TaskState.Reading;
        }
        // タイピング開始
        public void TypingStart()
        {
            startTime = DateTime.Now;
            taskState = TaskState.Typing;
        }
        // 計測終了
        public void End()
        {
            Debug.Log("End Recording");
            endTime = DateTime.Now;
            taskState = TaskState.Done;
        }

        // InputDatumを追加
        public void AddInputDatum(string k)
        {
            // タスクがTyping中でない場合は追加しない
            if (taskState != TaskState.Typing)
            {
                return;
            }
            var inputDatum = new InputDatum(k, DateTime.Now);
            inputDatas.Add(inputDatum);
        }

        // 保存
        public void Save()
        {
            InputStorageSaver.Save(this);
            Debug.Log("Saved");
        }
    }

    // タスクの状態を表す列挙型
    public enum TaskState
    {
        // 未実行
        NotYet,
        // 実行中
        Reading,
        // 実行中
        Typing,
        // 実行済み
        Done
    }
}