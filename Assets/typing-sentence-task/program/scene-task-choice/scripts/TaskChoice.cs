using System.Collections;
using System.Collections.Generic;
using typing_sentence_task.program.scene_task.script;
using UnityEngine;
using UnityEngine.SceneManagement;
using TaskManager = typing_sentence_task.program.general.TaskManager;

public class TaskChoice : MonoBehaviour
{
    
    // Go Task1 when pushed button_task1
    public void GoPracticeScene()
    {
        TaskManager.NextTaskName = "TaskPractice";
        SceneManager.LoadScene("scene-WaitMri");
    }
    
    public void GoTask1Scene()
    {
        TaskManager.NextTaskName = "Task1";
        SceneManager.LoadScene("scene-WaitMri");
    }
    
    public void GoTask2Scene()
    {
        TaskManager.NextTaskName = "Task2";
        SceneManager.LoadScene("scene-WaitMri");
    }
    
    public void GoTask3Scene()
    {
        TaskManager.NextTaskName = "Task3";
        SceneManager.LoadScene("scene-WaitMri");
    }
    
    public void GoTask4Scene()
    {
        TaskManager.NextTaskName = "Task4";
        SceneManager.LoadScene("scene-WaitMri");
    }
}
