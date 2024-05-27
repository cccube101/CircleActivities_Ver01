using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UISample : MonoBehaviour
{
    // ---------------------------- SerializeField
    [Header("�V�[���J�ڃ{�^��")]
    [SerializeField] private TextMeshProUGUI _reloadText;

    [Header("�A�N�e�B�u�؊����{�^��")]
    [SerializeField] private GameObject _setActiveObj;
    [SerializeField] private TextMeshProUGUI _activeButtonText;

    [Header("UI�ړ��{�^��")]
    [SerializeField] private RectTransform _moveUI;
    [SerializeField] private RectTransform[] _movePositions;
    [SerializeField] private float _duration;

    [Header("�C�x���g�I���{�^��")]
    [SerializeField] private UnityEvent[] _selectEvent;

    // ---------------------------- Field
    private static int _reloadCount = 0;

    private bool _isActiveObj = true;

    private bool _isOpenMenu = false;


    // ---------------------------- UnityMessage
    private void Start()
    {
        _reloadText.text = $"SceneReload:{_reloadCount}";   //  �e�L�X�g�ύX
    }



    // ---------------------------- PublicMethod
    /// <summary>
    /// �V�[���J�ڃ{�^��
    /// </summary>
    public void SceneReload()
    {
        _reloadCount++; //  �J�E���g
        _reloadText.text = $"SceneReload:{_reloadCount}";   //  �e�L�X�g�ύX

        var currentScene = SceneManager.GetActiveScene().buildIndex;    //  ���V�[���C���f�b�N�X
        SceneManager.LoadScene(currentScene);   //  �V�[���J��
    }

    /// <summary>
    /// �A�N�e�B�u�؊����{�^��
    /// </summary>
    public void ObjActive()
    {
        _isActiveObj = !_isActiveObj;   //  �؊���
        _activeButtonText.text = $"ObjActive:{_isActiveObj}";   //  �e�L�X�g�ύX
        _setActiveObj.SetActive(_isActiveObj);  //  �I���I�t
    }

    /// <summary>
    /// �A�N�e�B�u�؊����{�^��
    /// </summary>
    /// <param name="obj"></param>
    public void ObjActive(GameObject obj)
    {
        _isActiveObj = !_isActiveObj;   //  �؊���

        var text = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();   //  �q�I�u�W�F�N�g�擾
        text.text = $"ObjActive:{_isActiveObj}";   //  �e�L�X�g�ύX

        var activeObj = obj.transform.GetChild(1).gameObject;  //  �q�I�u�W�F�N�g�擾
        activeObj.SetActive(_isActiveObj);  //  �I���I�t
    }

    /// <summary>
    /// UI�ړ��{�^��
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
    /// �C�x���g���s
    /// </summary>
    public void UnityEventInvoke(int element)
    {
        _selectEvent[element]?.Invoke();
    }

    // ---------------------------- PrivateMethod


}
