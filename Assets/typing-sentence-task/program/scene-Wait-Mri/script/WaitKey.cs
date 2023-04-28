using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaitKey : MonoBehaviour
{
    public void OnEnable()
    {
        // イベントの登録
        Keyboard.current.onTextInput += OnTextInput;
    }
    public void OnDisable()
    {
        // イベントの登録解除
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char ch)
    {
        // 入力が５の時にシーンを遷移
        if (ch == '5')
        {
            Debug.Log("5 is Pressed");
            SceneManager.LoadScene("scene-task");
        }
    }
}
