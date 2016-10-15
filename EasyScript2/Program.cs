using System;
using System.IO;
using Easily.ES;

/// <summary>
/// @author Easily
/// </summary>
public static class Program {

    private static void Main() {
        Console.WriteLine("Hello World!");
        var scanner = new Scanner(File.ReadAllText("C:/Program Files (x86)/Microsoft Visual Studio 14.0/Common7/IDE/WebTemplates/MVC/CSharp/1033/Spav5.0/Scripts/app/_run.js"));
    }

}

