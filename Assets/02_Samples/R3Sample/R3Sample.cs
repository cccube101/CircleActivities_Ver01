using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class R3Sample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [SerializeField] private int _initHp;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _addHp;
    [SerializeField] private int _removeHp;
    [SerializeField] private float _duration;

    [SerializeField] private Button _btn;
    [SerializeField] private Button _btn_await;





    // ---------------------------- Field
    private ReactiveProperty<int> _hp = new();
    public ReadOnlyReactiveProperty<int> HP => _hp;

    private bool _canConvertingToText = true;


    // ---------------------------- UnityMessage
    private void Start()
    {
        _hp.Value = _initHp;

        PlayerEventObserver();  //  プレイヤーイベント監視
        BtnEventObserver();     //  ボタンイベント監視
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _hp.Value -= _removeHp;
        }
    }

    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod
    /// <summary>
    /// プレイヤーイベント監視
    /// </summary>
    private void PlayerEventObserver()
    {
        //  HP監視
        HP.Where(_ => _canConvertingToText)
        .Subscribe(hp =>
        {
            _text.text = hp.ToString();
        })
        .AddTo(this);
    }

    /// <summary>
    /// ボタンイベント監視
    /// </summary>
    private void BtnEventObserver()
    {
        //  OnClickと同じ挙動
        _btn.OnClickAsObservable()
        .Subscribe(_ =>
        {
            _hp.Value -= _removeHp;
        })
        .AddTo(this);

        // UniTaskを使用する場合の subscribe
        _btn_await.OnClickAsObservable()
        .SubscribeAwait(async (_, ct) =>
        {
            _hp.Value += _addHp;
            await CountText(_text, _hp.Value, _duration, ct);

        }, AwaitOperation.Drop)
        .RegisterTo(destroyCancellationToken);
    }

    /// <summary>
    /// テキストカウント
    /// </summary>
    /// <param name="text"></param>
    /// <param name="points"></param>
    /// <param name="duration"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    async UniTask CountText
        (TextMeshProUGUI text
        , int points
        , float duration
        , CancellationToken ct)
    {
        _canConvertingToText = false;

        await DOVirtual.Int(0, points, duration,
            (value) =>
            {
                text.text = value.ToString();
            })
            .SetEase(Ease.OutBack)
            .SetLink(text.gameObject)
            .SetUpdate(true)
            .ToUniTask(cancellationToken: ct);

        _canConvertingToText = true;
    }

}
