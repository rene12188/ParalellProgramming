// See https://aka.ms/new-console-template for more information


using Exerise1;

var n = int.Parse(Environment.GetCommandLineArgs()[1]);
var thinkingTime = int.Parse(Environment.GetCommandLineArgs()[2]);
var eatingTime = int.Parse(Environment.GetCommandLineArgs()[3]);

Console.WriteLine(n);
Console.WriteLine(thinkingTime);
Console.WriteLine(eatingTime);

var restaurent = new Restaurant(n, thinkingTime, eatingTime);
restaurent.Start();