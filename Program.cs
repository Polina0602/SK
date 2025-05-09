using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelPlayground.Enums;
using SemanticKernelPlayground.Logic;
using SemanticKernelPlayground.Plugins.ModeDetection;
using SemanticKernelPlayground.Setup;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

var kernel = SetupKernel.CreateKernel(configuration);

var chatService = kernel.GetRequiredService<IChatCompletionService>();

var history = new ChatHistory();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("┌────────────────────────────────────────────┐");
Console.WriteLine("│       Welcome to Semantic Kernel AI!       │");
Console.WriteLine("└────────────────────────────────────────────┘");
Console.ResetColor();

Console.WriteLine();
Console.WriteLine("I can help you with two main things:");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Git & Release Notes");
Console.ResetColor();
Console.WriteLine("   • Generate release notes from commits");
Console.WriteLine("   • View recent repository activity");
Console.WriteLine("   • Just mention 'commits' or 'release notes'");
Console.WriteLine("   • You can specify the number of commits (e.g., \'Show me 5 commits\')");


Console.WriteLine();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Chat with AI");
Console.ResetColor();
Console.WriteLine("   • Ask me any question");
Console.WriteLine("   • Get help with coding, learning, or ideas");
Console.WriteLine("   • Just type your message to start chatting");

Console.WriteLine();
Console.Write("What would you like to do today? ");

var input = Console.ReadLine();
    if (input?.Trim().ToLower() == "exit") return;


var mode = await DetectMode.DetectModeAsync(kernel, input);

if (mode == AppMode.Commits)
    await RunCommitsMode.RunCommitsModeAsync(kernel, chatService, history, input!);
else
    await RunChatMode.RunChatModeAsync(kernel, chatService, history, input!);

