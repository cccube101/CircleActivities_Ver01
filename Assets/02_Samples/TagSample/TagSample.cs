using UnityEngine;

public class TagSample : MonoBehaviour
{
    // ---------------------------- SerializeField


    // ---------------------------- Field



    // ---------------------------- UnityMessage

    //  �I���R���W����(�ڐG����)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("coEnter");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagName.Wall))
        {
            Debug.Log("coStay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagName.Wall))
        {
            Debug.Log("coExit");
        }
    }

    //  �I���g���K�[(�ڐG�Ȃ�)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagName.Wall))
        {
            Debug.Log("trEnter");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagName.Wall))
        {
            Debug.Log("trStay");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagName.Wall))
        {
            Debug.Log("trExit");
        }
    }

    //  3D�̏ꍇ2D�ł͂Ȃ�

    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod



    #region ------ Base

    // ---------------------------- Enum

    // ---------------------------- Property

    #region ------ 0



    #endregion

    #endregion
}
