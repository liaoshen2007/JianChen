using System;

[Serializable]
public class PoemData
{
    public int PoemId;//诗词Id
    public string PoemTitle;//诗词题目
    public string AuthorName;//作者姓名
    public string CreatTime;//创造年代
    public PoemType PoemType;//诗词类型
    public string PoemContent;//诗词内容
    public string Extend;//拓展字段
}

public enum PoemType
{
    TangshiQiLv=0,
    TangshiWuLv=1,
    Songci=2,
    XianDaiShiCi=3,
}