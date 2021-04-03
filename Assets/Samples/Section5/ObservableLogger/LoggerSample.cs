using System;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;

//Loggerの名前が衝突するためusingエイリアスディレクティブで指定
using Logger = UniRx.Diagnostics.Logger;

namespace Samples.Section5.ObservableLogger
{
    public class LoggerSample : MonoBehaviour
    {
        /// <summary>
        /// Loggerを各クラスごとに用意する
        /// 引数にそのクラス名を指定するとログ管理がしやすい
        /// </summary>
        private readonly Logger _logger = new Logger(nameof(LoggerSample));

        public void Start()
        {
            // ObservableLogger.Listener.LogToUnityDebug()を呼び出すことで、
            // 発行されたログをUnityEditorのコンソールに出力してくれるようになる
            // なお複数回呼び出すとログが重複して出力されてしまう
            // 多重実行に注意
            UniRx.Diagnostics.ObservableLogger.Listener.LogToUnityDebug();

            // ObservableLogger.ListenerはIObservable<LogEntry>
            // これを購読することで発行されたログを購読できる
            UniRx.Diagnostics.ObservableLogger.Listener
                // ログレベルでフィルタリング
                .Where(x => x.LogType == LogType.Error || x.LogType == LogType.Exception)
                // 1フレームの間に発行されたログを集約する
                .BatchFrame()
                //発行されたログをファイルに書き出す
                .Subscribe(WriteAsync)
                .AddTo(this);

            // loggerを用いて任意のLogメッセージを発行可能
            // 発行したLogメッセージは ObservableLogger.Listener より購読可能
            _logger.Debug("Debugメッセージ");
            _logger.Error("エラーが発生しました");
            _logger.Exception(new Exception("例外が発生しました"));
        }

        // ログをファイルに書き出す
        private void WriteAsync(IList<LogEntry> logs)
        {
            // LogEntry.ToString()で整形されたログメッセージを取得できる
            Observable.Start(() =>
            {
                using (var f = new FileStream("error.txt", FileMode.Append))
                using (var s = new StreamWriter(f))
                {
                    foreach (var logEntry in logs)
                    {
                        s.WriteLine(logEntry.ToString());
                    }
                }
            }).Subscribe();
        }
    }
}