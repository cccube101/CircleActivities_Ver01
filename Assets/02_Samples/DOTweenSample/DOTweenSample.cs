using DG.Tweening;
using UnityEngine;

public class DOTweenSample : MonoBehaviour
{
    private enum Type
    {
        None,
        Move,
        LoopMove,
        RectMove,



    }

    // ---------------------------- SerializeField
    [SerializeField] private Type _type;
    [SerializeField] private Transform _pos;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;

    [SerializeField] private int _loopTimes;
    [SerializeField] private LoopType _loopType;

    [SerializeField] private Vector2 _rectPos;

    // ---------------------------- Field
    private bool _isTween = false;
    private Vector3 _initPos;
    private Vector2 _initRectPos;


    // ---------------------------- UnityMessage
    private void Start()
    {
        DG.Tweening.DOTween.SetTweensCapacity(tweenersCapacity: 5000, sequencesCapacity: 200);

        if (!GetComponent<RectTransform>())
        {
            _initPos = transform.position;
        }
        else
        {
            _initRectPos = GetComponent<RectTransform>().anchoredPosition;
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
