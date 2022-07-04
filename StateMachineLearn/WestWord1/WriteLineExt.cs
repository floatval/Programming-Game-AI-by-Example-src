namespace StateMachineLearn;

public static class WriteExt
{
    /// <summary>
    /// 一般情况使用 - 背景白字体蓝
    /// </summary>
    /// <param name="content"></param>
    public static void WriteBgWhiteAndFgBlue(string content)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(content);
        Console.ResetColor();
    }

    /// <summary>
    /// 敏感信息使用 - 背景白 - 字体红
    /// </summary>
    /// <param name="content"></param>
    public static void WriteBgWhiteAndFgRed(string content)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(content);
        Console.ResetColor();
    }

    /// <summary>
    /// 警告信息使用 - 背景白，字体黄
    /// </summary>
    /// <param name="content"></param>
    public static void WriteBgWhiteAndFgYellow(string content)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(content);
        Console.ResetColor();
    }
    
}