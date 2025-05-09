using System;
using System.IO;
using System.Linq;
using Microsoft.SemanticKernel;
using SemanticKernelPlayground.Plugins.ModeDetection;

namespace SemanticKernelPlayground;

internal static class AddPlugins
{
    public static void Register(IKernelBuilder builder)
    {
        var baseDir = AppContext.BaseDirectory;

        try
        {
            builder.Plugins.AddFromType<Plugins.GitPlugin.GitPlugin>("Git");
            builder.Plugins.AddFromType<Plugins.ModeDetection.ModeDetectionPlugin>("ModeDetection");
            builder.Plugins.AddFromType<Plugins.ReleaseNotes.ReleaseNotesPlugin>("ReleaseNotes");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering plugins: {ex.Message}");
        }
      
    }
}

