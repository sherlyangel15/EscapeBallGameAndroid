using UnityEngine;

public class DefaultPlayerFace : MonoBehaviour
{
    [SerializeField] Sprite _defaultFaceSprite;
    public Sprite GetDefaultSprite()
    {
        return _defaultFaceSprite;
    }
    public void SetDefaultSprite(Sprite sprite)
    {
        _defaultFaceSprite = sprite;
    }
}
