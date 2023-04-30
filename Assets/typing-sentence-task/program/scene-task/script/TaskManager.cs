using UnityEngine;

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
        }

        private void Update()
        {
            // タスクの残り時間を表示する
            Debug.Log(TaskTimer.GetRemainingTime());
            // タスクの時間を管理する
            if (TaskTimer.IsTimeOver())
            {
                EndTask();
            }
        }

        public void EndTask()
        {
            Debug.Log("EndTask");
            inputText.inputsStorage.InputStorageSaver.Save(inputText.inputsStorage);
        }
    }
}