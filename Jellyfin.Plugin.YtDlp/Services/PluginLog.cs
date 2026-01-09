using System;
using System.Globalization;
using System.IO;

namespace Jellyfin.Plugin.YtDlp.Services;

/// <summary>
/// Writes log messages to the plugin log file.
/// </summary>
public static class PluginLog
{
    /// <summary>
    /// Writes a message to the plugin log file.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void Write(string message)
    {
        var logPath = GetLogPath();
        if (string.IsNullOrEmpty(logPath))
        {
            return;
        }

        try
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            File.AppendAllText(logPath, $"[{timestamp}] {message}\n");
        }
        catch
        {
            // ignore logging failures
        }
    }

    private static string? GetLogPath()
    {
        var config = Plugin.Instance?.Configuration;
        if (string.IsNullOrEmpty(config?.DownloadPath))
        {
            return null;
        }

        return Path.Combine(config.DownloadPath, "ytdlp.log");
    }
}
