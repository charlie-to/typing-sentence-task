
using System.Collections;
using TMPro;
using typing_sentence_task.program.general;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitKey : MonoBehaviour
{
    ParticipantTask _task = TaskManager.GetParticipantTask();

    [SerializeField] private SpriteRenderer vlineRenderer;
    [SerializeField] private SpriteRenderer hlineRenderer;
    [SerializeField] private TextMeshProUGUI waitingMessage;
    public void Start()
    {
        vlineRenderer = GameObject.Find("vertical-line").GetComponent<SpriteRenderer>();
        hlineRenderer = GameObject.Find("horizontal-line").GetComponent<SpriteRenderer>();
        waitingMessage = GameObject.Find("waiting-message").GetComponent<TextMeshProUGUI>();
        
        if (_task == null)
        {
            Debug.LogError("Task is not found");
            SceneManager.LoadScene("scene-task-choice");
        }
        else
        {
            // 安静状態を指定時間維持
            if (_task.IsFinished)
            {
                vlineRenderer.color = Color.white;
                hlineRenderer.color = Color.white;
                waitingMessage.color = Color.black;
                _task.IsFinished = false;
                StartCoroutine(DelaySceneToChoice(_task.QuietSeconds));
            }
            else
            {
                Debug.Log("Waiting for MRI...");
                vlineRenderer.color = Color.black;
                hlineRenderer.color = Color.black;
                waitingMessage.color = Color.white;
            }
        }
    }

    public void OnEnable()
    {
        // キーボード入力を受け付ける
        Keyboard.current.onTextInput += OnTextInput;
    }
    public void OnDisable()
    {
        // キーボード入力を受け付けない
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char ch)
    {
        // 入力が5のときにルーチンを開始
        if (ch == '5')
        {
            Debug.Log("5 is Pressed");
            vlineRenderer.color = Color.white;
            hlineRenderer.color = Color.white;
            waitingMessage.color = Color.black;
            StartCoroutine(DelaySceneToTask(_task.QuietSeconds));
        }
    }
    
    
    // シーン遷移
    IEnumerator DelaySceneToTask(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("scene-task");
    }
    
    IEnumerator DelaySceneToChoice(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("scene-task-choice");
    }

    
    public void Update()
    {
        // 入力がescのときは終了
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("esc is Pressed");
            SceneManager.LoadScene("scene-task-choice");
        }
    }
}
