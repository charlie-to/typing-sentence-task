
using System.Collections;
using typing_sentence_task.program.general;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitKey : MonoBehaviour
{
    public void Start()
    {
        var task = TaskManager.GetParticipantTask();
        if (task == null)
        {
            Debug.LogError("Task is not found");
            SceneManager.LoadScene("scene-task-choice");
        }
        else
        {
            // 安静状態を指定時間維持
            if (task.IsFinished)
            {
                StartCoroutine(DelaySceneToChoice(task.QuietSeconds));
            }
            else
            {
                StartCoroutine(DelaySceneChange(task.QuietSeconds));
            }
        }
    }
    
    IEnumerator DelaySceneChange(int seconds)
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
            Application.Quit();
        }
    }
}
