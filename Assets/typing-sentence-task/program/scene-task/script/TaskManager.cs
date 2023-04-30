using InputLogs.program;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskManager : MonoBehaviour
    {
        // InputTextのインスタンス
        [SerializeField] private InputText inputText;

        // タイマー
        public TaskTimer TaskTimer;

        // シングルトンを実装
        public static TaskManager instance { get; private set; }

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