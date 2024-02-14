using InputLogs.program;
using TMPro;
using typing_sentence_task.program.general;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskEventManager : MonoBehaviour
    {
        // InputTextのインスタンス
        [SerializeField] private InputText inputText;
        // 課題UI
        [SerializeField] private GameObject taskUI;
        [SerializeField] RawImage taskUiImage;
        // シンキングタイムUI
        [SerializeField] private TextMeshProUGUI thinkingTimeText;
        // タイマー
        public Timer Timer;
        
        // タスク
        private ParticipantTask _task;

        // シングルトンを実装
        private static TaskEventManager Instance { get; set; }

        // インスタンスがあれば削除
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            Timer = new Timer();
        }

        // Start is called before the first frame update
        void Start()
        {
            taskUI = GameObject.Find("image-task");
            taskUiImage = taskUI.GetComponent<RawImage>();
            thinkingTimeText = GameObject.Find("thinking-time").GetComponent<TextMeshProUGUI>();
            // タスクUIの画像を設定
            if (TaskManager.NextTaskName == null)
            {
                Debug.LogError("TaskManager.NextTaskNameがnullです");
                taskUiImage.texture = Resources.Load<Texture>("TaskImage/Error");
            }else
            {
                taskUiImage.texture = Resources.Load<Texture>("TaskImage/"+TaskManager.NextTaskName);
            }
            taskUiImage.FixAspect();
            
            // 入力エリア
            inputText = GetComponent<InputText>();
            
            // タスクを取得
            _task = TaskManager.GetParticipantTask();
            // 制限時間をセット
            Timer.SetLimitTime(_task.ThinkingSeconds,_task.TaskSeconds);
            // タスクタイマーをスタート
            Timer.Start();
            // InputStorageをリーディングに
            inputText.InputsStorage.ReadingStart();
        }

        private void Update()
        {
            // タスクの残り時間を表示する
            Debug.Log(Timer.GetRemainingTime());
            // タスクの時間を管理する
            if(Timer.IsReadTimeOver())
            {
                if (inputText.InputsStorage.taskState == TaskState.Reading)
                {
                    inputText.InputsStorage.TypingStart();
                    thinkingTimeText.text = "";
                }
            }
            if (Timer.IsTimeOver())
            {
                if (inputText.InputsStorage.taskState == TaskState.Typing)
                {
                    Debug.Log("EndTask");
                    // 計測終了
                    inputText.InputsStorage.End();
                    inputText.InputsStorage.Save();
                    // タスク終了
                    _task.IsFinished = true;
                    SceneManager.LoadScene("scene-WaitMri");
                }
            }
        }
    }
}