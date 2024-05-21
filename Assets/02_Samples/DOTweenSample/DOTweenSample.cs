using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenSample : MonoBehaviour
{
    private enum Type
    {
        None,
        Move,
        LoopMove,
        RectMove,
        Virtual,
        Fade,
        Color,
        //  ÇªÇÃëºÅ@Path,Text,ScaleìôÇ™Ç†ÇÈ

    }

    // ---------------------------- SerializeField
    [Header("äÓëb")]
    [SerializeField] private Type _type;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    [Header("Move")]
    [SerializeField] private Transform _pos;

    [Header("Loop")]
    [SerializeField] private int _loopTimes;
    [SerializeField] private LoopType _loopType;

    [Header("RectTransform")]
    [SerializeField] private Vector2 _rectPos;

    [Header("Virtual")]
    [SerializeField] private Slider _slider;
    [SerializeField] private float _endValue;

    [Header("Fade")]
    [SerializeField] private Image _fadeImage;

    [Header("Color")]
    [SerializeField] private Image _colorImage;
    [SerializeField] private Color _color;

    // ---------------------------- Field
    private bool _isTween = false;
    private Vector3 _initPos;
    private Vector2 _initRectPos;
    private Color _initColor;


    // ---------------------------- UnityMessage
    private void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);

        if (GetComponent<Image>()
            && _colorImage != null)
        {
            _initColor = _colorImage.color;
        }

        if (GetComponent<RectTransform>())
        {
            _initRectPos = GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            _initPos = transform.position;
        }




    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            && !_isTween)
        {
            _isTween = true;
            Tween();
        }
    }

    // ---------------------------- PublicMethod




    // ---------------------------- PrivateMethod
    /// <summary>
    /// Tween
    /// </summary>
    private void Tween()
    {
        switch (_type)
        {
            case Type.Move:
                transform.DOMove(_pos.position, _duration)
                    .SetLink(gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        Init();
                    });

                break;

            case Type.LoopMove:
                transform.DOMove(_pos.position, _duration)
                    .SetLink(gameObject)
                    .SetEase(_ease)
                    .SetLoops(_loopTimes, _loopType)
                    .OnComplete(() =>
                    {
                        Init();
                    });

                break;

            case Type.RectMove:
                GetComponent<RectTransform>().DOAnchorPos(_rectPos, _duration)
                    .SetLink(gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        RectInit();
                    });

                break;

            case Type.Virtual:
                DOVirtual.Float(_slider.value, _endValue, _duration,
                    (value) =>
                    {
                        _slider.value = value;
                    })
                    .SetLink(_slider.gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        _slider.value = 0;
                        _isTween = false;
                    });

                break;

            case Type.Fade:
                _fadeImage.DOFade(0, _duration)
                    .SetLink(_fadeImage.gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        _fadeImage.DOFade(1, _duration)
                        .SetLink(_fadeImage.gameObject)
                        .SetEase(_ease)
                        .OnComplete(() =>
                        {
                            _isTween = false;
                        });
                    });

                break;

            case Type.Color:
                _colorImage.DOColor(_color, _duration)
                    .SetLink(_colorImage.gameObject)
                    .SetEase(_ease)
                    .OnComplete(() =>
                    {
                        _colorImage.DOColor(_initColor, _duration)
                        .SetLink(_colorImage.gameObject)
                        .SetEase(_ease)
                        .OnComplete(() =>
                        {
                            _isTween = false;
                        });
                    });

                break;

        }
    }


    /// <summary>
    /// èâä˙âª
    /// </summary>
    private void Init()
    {
        transform.position = _initPos;
        _isTween = false;
    }

    /// <summary>
    /// UIèâä˙âª
    /// </summary>
    private void RectInit()
    {
        GetComponent<RectTransform>().anchoredPosition = _initRectPos;
        _isTween = false;
    }
}
