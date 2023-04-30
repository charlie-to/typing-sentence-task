using System.Text.RegularExpressions;
using InputLogs.program;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace typing_sentence_task.program.scene_task.script
{
    public class InputText : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        //入力したテキスト
        public TextData textData { get; set; }

        // インプットロガーのインスタンス
        public InputsStorage inputsStorage { get; set; }
        // taskManagerのタイマー
        public TaskTimer TaskTimer;

        void Start()
        {
            textData = new TextData();
            // セーブするためのインスタンスを作成
            inputsStorage = new InputsStorage("Test1", Participant.participantId.ToString(),Participant.outputDir);
            // taskManagerのタイマーを取得
            TaskManager taskManager = GetComponent<TaskManager>();
            if (taskManager == null)
            {
                Debug.LogError("TaskManagerが見つかりません");
            }
            TaskTimer = taskManager.TaskTimer;
            if(TaskTimer == null) Debug.LogError("TaskTimerが見つかりません");
        }

        public void OnEnable()
        {
            // イベントの登録
            Keyboard.current.onTextInput += OnTextInput;
        }
        public void OnDisable()
        {
            // イベントの登録解除
            Keyboard.current.onTextInput -= OnTextInput;
        }
        public void OnTextInput(char ch)
        {
            // 入力した文字を表示
            Debug.Log($"{(int)ch:x4}");
            
            // reading time 中は入力を受け付けない
            if (!TaskTimer.IsReadTimeOver()) return;
            
            // 基本的なラテン文字なら文字を追加
            if (Regex.IsMatch(ch.ToString(), @"\p{P}\d") || ch == '\u000d' || ch == '\u007F' || ch == '\u001B' ||
                ch == '\u0020') return;
            textData.AddChar(ch);
            // InputDatumを追加
            inputsStorage.AddInputDatum(ch.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(TaskTimer.IsReadTimeOver());
            if (TaskTimer.IsReadTimeOver())
            {
                textMesh.text = textData.LinesToString();
                // enterキーで改行
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    textData.NextLine();
                    // InputDatumを追加
                    inputsStorage.AddInputDatum("Enter");
                }
                // backspaceキーで文字を削除
                if (Keyboard.current.backspaceKey.wasPressedThisFrame)
                {
                    textData.DeleteChar();
                    // InputDatumを追加
                    inputsStorage.AddInputDatum("Backspace");
                }
                // spaceキーでスペースを追加
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    textData.AddSpace();
                    // InputDatumを追加
                    inputsStorage.AddInputDatum("Space");
                }
            }
            // escapeキーでタスクを終了
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // InputDatumを追加
                // inputsStorage.AddInputDatum("Escape");
                // タスクを終了
                TaskManager.instance.EndTask();
            }
        }
    }
}