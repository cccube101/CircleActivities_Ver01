using UnityEngine;

public class TagSample : MonoBehaviour
{
    // ---------------------------- SerializeField


    // ---------------------------- Field



    // ---------------------------- UnityMessage

    //  オンコリジョン(接触あり)
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

    //  オントリガー(接触なし)
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

    //  3Dの場合2Dではない

    // ---------------------------- PublicMethod





    // ---------------------------- PrivateMethod



    #region ------ Base

    // ---------------------------- Enum

    // ---------------------------- Property

    #region ------ 0



    #endregion

    #endregion
}
