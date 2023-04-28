using UnityEngine;

namespace typing_sentence_task.program.scene_task.script
{
    public class TaskManager : MonoBehaviour
    {
        // InputTextのインスタンス
        [SerializeField]
        private InputText inputText;

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
        }

        // Start is called before the first frame update
        void Start()
        {
            inputText = GetComponent<InputText>();
        }

        public void EndTask()
        {
            Debug.Log("EndTask");
            inputText.inputsStorage.InputStorageSaver.Save(inputText.inputsStorage);
        }
    }
}
