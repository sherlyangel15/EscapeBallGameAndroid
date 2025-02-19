using System.Collections;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    [SerializeField] Sprite faceSprite;
    [SerializeField] float duration;

    Sprite _initialDefaultSprite;
    PlayerMove playerMove;
    DefaultPlayerFace defaultPlayerFace;
    GameObject _face;
    Coroutine faceCoroutine;
    bool stop;
    

    AndroidInputHandler androidInputHandlerScript;

    void Start()
    {
        _face = GameObject.FindWithTag("PlayerFace");
        playerMove = FindObjectOfType<PlayerMove>();
        defaultPlayerFace = FindObjectOfType<DefaultPlayerFace>();
        androidInputHandlerScript = FindObjectOfType<AndroidInputHandler>();

        if(defaultPlayerFace != null)
        {
            _initialDefaultSprite = defaultPlayerFace.GetDefaultSprite();
        }

        stop = false;
    }

    void Update()
    {
        if (stop)
        {
            if(faceCoroutine != null)
            {
                StopCoroutine(DelayTime());
            }
            stop = false;
        }
    }
    
    public Sprite GetDefaultSprite()
    {
        return _initialDefaultSprite;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            defaultPlayerFace.SetDefaultSprite(faceSprite);
            if(androidInputHandlerScript.GetIsMovingStatus())
            {
                faceCoroutine = StartCoroutine(DelayTime());
            }
        }
    }
    
    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(duration);
        if(defaultPlayerFace != null)
        {
            defaultPlayerFace.SetDefaultSprite(_initialDefaultSprite);
        }
        stop = true;
    }
}
