using System.Collections.Generic;
using UnityEngine;


public class TextData
{
    // 一行の長さ
    static readonly int LineNum = 60;
    // 表示する最大の行数
    static readonly int MaxLineNum = 10;
    // 最後の一行
    public string line { get; set; } = "";

    // 最後の一行以外を保管する配列
    public List<string> lines { get; set; } = new List<string>(MaxLineNum);
    // 入力している文字数
    private int letterCount { get; set; } = 0;
    // Line中の最後の空白の位置
    private int lastSpaceIndex { get; set; } = 0;



    //入力された文字を受け取る
    public void AddChar(char c)
    {
        //Debug.Log(Line.Length);

        // 最後の一行に文字を追加
        line += c;
        // 入力している文字数を増やす
        letterCount++;
        // 最後の一行が一行の長さに達したら
        if (letterCount >= LineNum)
        {
            // 最後の一行の最後の空白までを配列に追加
            lines.Add(line[..(lastSpaceIndex - 1)]);
            // 空白以降は残す
            line = line[(lastSpaceIndex)..];
            // 入力している文字数は空白以降の文字数にする
            letterCount -= lastSpaceIndex;
            Debug.Log(letterCount);
            // 最後の一行の最後の空白の位置を初期化
            lastSpaceIndex = 0;
        }
    }
    public void AddSpace()
    {
        line += " ";
        letterCount++;
        lastSpaceIndex = letterCount;
        Debug.Log(lastSpaceIndex);
        if (letterCount >= LineNum)
        {
            // 最後の一行を配列に追加
            lines.Add(line[..(lastSpaceIndex - 1)]);
            // 最後の一行を初期化
            line = "";
            // 入力している文字数を初期化
            letterCount = 0;
            // 最後の一行の最後の空白の位置を初期化
            lastSpaceIndex = 0;
        }
    }

    // 文字を削除する
    public void DeleteChar()
    {
        // 最後の一行が空の場合
        if (letterCount == 0)
        {
            // 最後の一行を取得
            line = lines[^1];
            // 最後の一行を削除
            lines.RemoveAt(lines.Count - 1);
            // 最後の一行の文字数を取得
            letterCount = line.Length;
            // 最後の一行の最後の空白の位置を見つける
            lastSpaceIndex = line.LastIndexOf(" ");
        }
        // 最後の一行が空でない場合
        else
        {
            if (letterCount == 0 && lines.Count == 0)
            {
                return;
            }
            // 最後の一行の文字数を減らす
            letterCount--;
            // 最後の一行の最後の文字を削除
            line = line[..^1];
            // 最後の一行の最後の空白の位置を見つける
            lastSpaceIndex = line.LastIndexOf(" ");
        }
    }

        // 改行
        public void NextLine()
        {
            // 最後の一行を配列に追加
            lines.Add(line);
            // 最後の一行を初期化
            line = "";
            // 入力している文字数を初期化
            letterCount = 0;
        }

        public string LinesToString()
        {
            string str = "";
            // Debug.Log(lines.Count);
            if (lines.Count != 0)
            {
                //DisplayLineを作成
                var displayLines = lines;
                if (lines.Count > MaxLineNum - 1)
                {
                    displayLines = displayLines.GetRange(lines.Count - MaxLineNum + 1, MaxLineNum - 1);
                }

                foreach (var line in displayLines)
                {
                    str += line;
                    str += "\n";
                }
            }

            if (letterCount != 0)
            {
                string line = this.line[..letterCount];
                str += line;
            }

            return str;
        }
}