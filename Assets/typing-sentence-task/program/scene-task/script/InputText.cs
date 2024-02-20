using System.Text.RegularExpressions;
using InputLogs.program;
using TMPro;
using typing_sentence_task.program.general;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace typing_sentence_task.program.scene_task.script
{
    public class InputText : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        //入力したテキスト
        public TextData TextData { get; set; }

        // インプットロガーのインスタンス
        public InputsStorage InputsStorage { get; set; }
        // taskManagerのタイマー
        public Timer Timer;

        private ParticipantTask _task = TaskManager.GetParticipantTask();

        private void Awake()
        {
            // セーブするためのインスタンスを作成
            InputsStorage = new InputsStorage(_task.TaskName, Participant.participantId.ToString(),Participant.outputDir);
        }

        void Start()
        {
            TextData = new TextData();
            textMesh = GameObject.Find("text-input").GetComponent<TextMeshProUGUI>();
            // taskManagerのタイマーを取得
            TaskEventManager taskEventManager = GetComponent<TaskEventManager>();
            if (taskEventManager == null)
            {
                Debug.LogError("TaskManagerが見つかりません");
            }
            Timer = taskEventManager.Timer;
            if(Timer == null) Debug.LogError("TaskTimerが見つかりません");
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
            if (!Timer.IsReadTimeOver()) return;
            
            // 基本的なラテン文字なら文字を追加
            if (Regex.IsMatch(ch.ToString(), @"\p{P}\d") || ch == '\u000d' || ch == '\u007F' || ch == '\u001B' ||
                ch == '\u0020'|| ch=='\u0008') return;
            TextData.AddChar(ch);
            // InputDatumを追加
            InputsStorage.AddInputDatum(ch.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Timer.IsReadTimeOver());
            if (Timer.IsReadTimeOver())
            {
                textMesh.text = TextData.LinesToString();
                // enterキーで改行
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    TextData.NextLine();
                    // InputDatumを追加
                    InputsStorage.AddInputDatum("Enter");
                }
                // backspaceキーで文字を削除
                if (Keyboard.current.backspaceKey.wasPressedThisFrame)
                {
                    TextData.DeleteChar();
                    // InputDatumを追加
                    InputsStorage.AddInputDatum("Backspace");
                }
                // spaceキーでスペースを追加
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    TextData.AddSpace();
                    // InputDatumを追加
                    InputsStorage.AddInputDatum("Space");
                }
            }
            // escapeキーでタスクを終了
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // InputDatumを追加
                // inputsStorage.AddInputDatum("Escape");
                // 計測を終了
                InputsStorage.End();
                InputsStorage.Save();
                // タスクを終了
                SceneManager.LoadScene("scene-WaitMri");
            }
        }
    }
}