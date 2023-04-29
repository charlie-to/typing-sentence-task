using UnityEngine;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskManager : MonoBehaviour
    {
        // InputTextのインスタンス
        [SerializeField] private InputText inputText;

        // タイマー
        public TaskTimer _taskTimer;

        // シングルトンを実装
        public static TaskManager Instance { get; private set; }

        // インスタンスがあれば削除
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
            // TODO  タスクタイマーを作成（テスト用）
            // TODO 後で時間変更を実装
            _taskTimer = new TaskTimer(1, 1);
        }

        // Start is called before the first frame update
        void Start()
        {
            inputText = GetComponent<InputText>();
            // タスクタイマーをスタート
            _taskTimer.Start();
        }

        private void Update()
        {
            // タスクの残り時間を表示する
            Debug.Log(_taskTimer.GetRemainingTime());
            // タスクの時間を管理する
            if (_taskTimer.IsTimeOver())
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