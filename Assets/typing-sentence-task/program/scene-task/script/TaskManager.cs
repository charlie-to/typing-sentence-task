using InputLogs.program;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskManager : MonoBehaviour
    {
        // InputTextのインスタンス
        [SerializeField] private InputText inputText;
        // 課題UI
        [SerializeField] private GameObject taskUI;
        [SerializeField] Image taskUiImage;
        // タイマー
        public TaskTimer TaskTimer;

        // シングルトンを実装
        private static TaskManager instance { get; set; }

        // インスタンスがあれば削除
        private void Awake()
        {
            if (instance != null)
            {
                return;
            }
            instance = this;
            TaskTimer = new TaskTimer();
        }

        // Start is called before the first frame update
        void Start()
        {
            taskUI = GameObject.Find("Image-task");
            taskUiImage = taskUI.GetComponent<Image>();
            taskUiImage.sprite = Resources.Load<Sprite>("TaskImage/スライド1");
            // タスクUIの画像を設定
            inputText = GetComponent<InputText>();
            Debug.Log(Participant.participantId);
            // 制限時間をセット
            // TODO  タスクタイマーを作成（テスト用）
            // TODO 後で時間変更を実装
            TaskTimer.SetLimitTime(0,1);
            // タスクタイマーをスタート
            TaskTimer.Start();
            // InputStorageをリーディングに
            inputText.inputsStorage.ReadingStart();
        }

        private void Update()
        {
            // タスクの残り時間を表示する
            Debug.Log(TaskTimer.GetRemainingTime());
            // タスクの時間を管理する
            if(TaskTimer.IsReadTimeOver())
            {
                if (inputText.inputsStorage.taskState == TaskState.Reading)
                {
                    inputText.inputsStorage.TypingStart();
                }
            }
            if (TaskTimer.IsTimeOver())
            {
                if (inputText.inputsStorage.taskState == TaskState.Typing)
                {
                    Debug.Log("EndTask");
                    // 計測終了
                    inputText.inputsStorage.End();
                    inputText.inputsStorage.Save();
                    // タスク終了
                    SceneManager.LoadScene("scene-WaitMri");
                }
            }
        }
    }
}