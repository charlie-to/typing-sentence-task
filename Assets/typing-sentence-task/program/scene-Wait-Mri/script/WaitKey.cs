using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitKey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ５が入力されたら次のシーンに遷移
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("scene-task");
        }
    }
}
