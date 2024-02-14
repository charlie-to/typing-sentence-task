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
            
            // タスクUIの画像を設定
            inputText = GetComponent<InputText>();
            Debug.Log(Participant.participantId);
            // 制限時間をセット
            // TODO  タスクタイマーを作成（テスト用）
            // TODO 後で時間変更を実装
            Timer.SetLimitTime(10,60);
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
                    SceneManager.LoadScene("scene-WaitMri");
                }
            }
        }
    }
}