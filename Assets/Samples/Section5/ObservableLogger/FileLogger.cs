using System.IO;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

namespace Assets._5.LoggerSamples
{
    /// <summary>
    /// UniRx.Diagnostics.Loggerより発行されたログをファイルに書き出す
    /// </summary>
    public class FileLogger : MonoBehaviour
    {
        private void Awake()
        {
            // ExceptionまたはErrorログのみをファイルに書き出す
            ObservableLogger.Listener
                .Where(x => x.LogType == LogType.Error || x.LogType == LogType.Exception)
                .Subscribe(WriteAsync);
        }

        private void WriteAsync(LogEntry log)
        {
            Observable.Start(() => File.WriteAllText("error.log", log.ToString())).Subscribe();
        }
    }
}