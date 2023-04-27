using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        inputText.saveTaskInputData.Save(inputText.inputsStorage);
    }
}
