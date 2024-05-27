using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using System.Threading;
using System.Collections.Generic;

public class UniTaskEditSample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [Header("待機")]
    [SerializeField] private float _waitTime;

    [Header("DOTween")]
    [SerializeField] private Type _type;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float _scale;

    // ---------------------------- Field


    // ---------------------------- UnityMessage
    private async void Start()
    {
        //  初期設定
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);

        //  スタート時タスク
        var startTask = StartTask(destroyCancellationToken);
        if (await startTask.SuppressCancellationThrow()) { return; }
    }


    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod
    /// <summary>
    /// スタートタスク
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask StartTask(CancellationToken ct)
    {
        //  待機
        await DelayTime(_waitTime * 2, ct);

        //  移動
        foreach (var pos in _positions)
        {
            await Move(gameObject, pos.position, _duration, _ease, ct);
        }

        //  移動　＆　拡大
        var allTasks = new List<UniTask>()
        {
            Move(gameObject, _positions[0].position, _duration, _ease, ct),
            DelayScaleTask(ct),
        };
        //  待機拡大
        async UniTask DelayScaleTask(CancellationToken ct)
        {
            await DelayTime(_duration / 2, ct);
            await ScaleTask(gameObject, _scale, _duration / 2, _ease, ct);
        }

        await UniTask.WhenAll(allTasks);
    }

    #region ------ TweenTask
    /// <summary>
    /// ディレイ
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private async UniTask DelayTime(float time, CancellationToken ct)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: ct);
    }

    /// <summary>
    /// 移動タスク
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask Move
        (GameObject obj
        , Vector3 endValue
        , float duration
        , Ease ease
        , CancellationToken ct)
    {
        await obj.transform.DOMove(endValue, duration)
            .SetEase(ease)
            .SetUpdate(true)
            .SetLink(obj)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }

    /// <summary>
    /// 拡縮タスク
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="endValue"></param>
    /// <param name="duration"></param>
    /// <param name="ease"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTask ScaleTask
        (GameObject obj
        , float endValue
        , float duration
        , Ease ease
        , CancellationToken ct)
    {
        await obj.transform.DOScale(endValue, duration)
            .SetLink(obj)
            .SetEase(ease)
            .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken: ct);
    }

    #endregion
}
