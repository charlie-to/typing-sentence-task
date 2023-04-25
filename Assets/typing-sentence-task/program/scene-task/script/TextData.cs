using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class TextData
{
    // 一行の長さ
    static readonly int lineNum = 30;
    // 最後の一行
    public char[] Line { get; set; } = new char[lineNum];

    // 最後の一行以外を保管する配列
    public List<char[]> Lines { get; set; }
}
