using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class TextData
{
    // 一行の長さ
    static readonly int lineNum = 45;
    // 表示する最大の行数
    static readonly int MaxLineNum = 12;
    // 最後の一行
    public string Line { get; set; } = "";

    // 最後の一行以外を保管する配列
    public List<string> Lines { get; set; } = new List<string>(MaxLineNum);
    // 入力している文字数
    private int LetterCount { get; set; } = 0;



    //入力された文字を受け取る
    public void AddChar(char c)
    {
        //Debug.Log(Line.Length);

        // 最後の一行に文字を追加
        Line += c;
        // 入力している文字数を増やす
        LetterCount++;
        // 最後の一行が一行の長さに達したら
        if (LetterCount == lineNum)
        {
            // 最後の一行を配列に追加
            Lines.Add(Line);
            // 最後の一行を初期化
            Line = "";
            // 入力している文字数を初期化
            LetterCount = 0;
        }
    }

    // 文字を削除する
    public void DeleteChar()
    {
        // 最後の一行が空の場合
        if (LetterCount == 0)
        {
            // 最後の一行を取得
            Line = Lines[^1];
            // 最後の一行を削除
            Lines.RemoveAt(Lines.Count - 1);
            // 最後の一行の文字数を取得
            LetterCount = Line.Length;
        }
        // 最後の一行が空でない場合
        else
        {
            if (LetterCount == 0 && Lines.Count == 0)
            {
                return;
            }
            // 最後の一行の文字数を減らす
            LetterCount--;
            // 最後の一行の最後の文字を削除
            Line = Line[..^1];
        }
    }

    // 改行
    public void NextLine()
    {
        // 最後の一行を配列に追加
        Lines.Add(Line);
        // 最後の一行を初期化
        Line = "";
        // 入力している文字数を初期化
        LetterCount = 0;
    }
    public string LinesToString()
    {
        string str = "";
        // Debug.Log(Lines.Count);
        if (Lines.Count != 0)
        {
            //DisplayLineを作成
            var DisplayLines = Lines;
            if (Lines.Count > MaxLineNum - 1)
            {
                DisplayLines = DisplayLines.GetRange(Lines.Count - MaxLineNum + 1, MaxLineNum - 1);
            }
            Debug.Log(DisplayLines);

            foreach (var line in DisplayLines)
            {
                str += line.ToString();
                str += "\n";
            }
        }
        if (LetterCount != 0)
        {
            string line = Line.ToString()[..LetterCount];
            str += line;
        }
        return str;
    }
}
