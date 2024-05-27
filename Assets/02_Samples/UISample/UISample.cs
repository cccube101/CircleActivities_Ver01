using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UISample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [Header("シーン遷移ボタン")]
    [SerializeField] private TextMeshProUGUI _reloadText;

    [Header("アクティブ切換えボタン")]
    [SerializeField] private GameObject _setActiveObj;
    [SerializeField] private TextMeshProUGUI _activeButtonText;

    [Header("UI移動ボタン")]
    [SerializeField] private RectTransform _moveUI;
    [SerializeField] private RectTransform[] _movePositions;
    [SerializeField] private float _duration;

    [Header("イベント選択ボタン")]
    [SerializeField] private UnityEvent[] _selectEvent;

    // ---------------------------- Field
    private static int _reloadCount = 0;

    private bool _isActiveObj = true;

    private bool _isOpenMenu = false;


    // ---------------------------- UnityMessage
    private void Start()
    {
        _reloadText.text = $"SceneReload:{_reloadCount}";   //  テキスト変更
    }



    // ---------------------------- PublicMethod
    /// <summary>
    /// シーン遷移ボタン
    /// </summary>
    public void SceneReload()
    {
        _reloadCount++; //  カウント
        _reloadText.text = $"SceneReload:{_reloadCount}";   //  テキスト変更

        var currentScene = SceneManager.GetActiveScene().buildIndex;    //  現シーンインデックス
        SceneManager.LoadScene(currentScene);   //  シーン遷移
    }

    /// <summary>
    /// アクティブ切換えボタン
    /// </summary>
    public void ObjActive()
    {
        _isActiveObj = !_isActiveObj;   //  切換え
        _activeButtonText.text = $"ObjActive:{_isActiveObj}";   //  テキスト変更
        _setActiveObj.SetActive(_isActiveObj);  //  オンオフ
    }

    /// <summary>
    /// アクティブ切換えボタン
    /// </summary>
    /// <param name="obj"></param>
    public void ObjActive(GameObject obj)
    {
        _isActiveObj = !_isActiveObj;   //  切換え

        var text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //  子オブジェクト取得
        text.text = $"ObjActive:{_isActiveObj}";   //  テキスト変更

        var activeObj = obj.transform.GetChild(1).gameObject;  //  子オブジェクト取得
        activeObj.SetActive(_isActiveObj);  //  オンオフ
    }

    /// <summary>
    /// UI移動ボタン
    /// </summary>
    public void UIMoveButton()
    {
        _isOpenMenu = !_isOpenMenu;
        if (_isOpenMenu)
        {
            Move(0);
        }
        else
        {
            Move(1);
        }

        void Move(int element)
        {
            _moveUI.DOAnchorPos(_movePositions[element].anchoredPosition, _duration)
                .SetEase(Ease.OutBack)
                .SetLink(_moveUI.gameObject)
                .SetUpdate(true);
        }
    }

    /// <summary>
    /// イベント実行
    /// </summary>
    public void UnityEventInvoke(int element)
    {
        _selectEvent[element]?.Invoke();
    }

    // ---------------------------- PrivateMethod


}
