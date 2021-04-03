using System;
using System.Collections;
using UniRx;
using UnityEngine.Networking;

/// <summary>
/// HTTPクライアント
/// </summary>
public class HttpClientSample
{
    /// <summary>
    /// ログの発行をおこなうLoggerオブジェクト
    /// </summary>
    private readonly UniRx.Diagnostics.Logger logger;

    public HttpClientSample()
    {
        // Loggerを作成し、「どこから発行されたログであるか」を区別するための名前をつける
        // 今回はクラス名を直接Loggerの名前にしている
        logger = new UniRx.Diagnostics.Logger(typeof(HttpClientSample).Name);
    }

    private IObservable<string> GetAsync(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            // URIが未設定の場合は警告をログに出して終了
            logger.Warning("[uri] is empty.");
            return Observable.Throw<string>(new ArgumentNullException("uri"));
        }

        return Observable.FromCoroutine<string>(o => GetCoroutine(o, uri));
    }

    private IEnumerator GetCoroutine(IObserver<string> observer, string uri)
    {
        var uwr = UnityWebRequest.Get(uri);

        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
        {
            // 通信失敗時はLoggerにエラーログを流す
            // その際にstring.Formatの記法を利用することもできる
            logger.ErrorFormat("Get error / [{0}] {1}", uwr.responseCode, uwr.error);

            observer.OnError(new Exception(uwr.error));
        }
        else
        {
            observer.OnNext(uwr.downloadHandler.text);
            observer.OnCompleted();
        }
    }
}