using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool isOpen;

    void Awake()
    {
        panel = GameObject.FindWithTag("SettingsPanel");
    }

    void Start()
    {
        panel.SetActive(false);
        isOpen = false;
    }

    public void OpenPanel()
    {
        if(!isOpen)
        {
            panel.SetActive(true);
            isOpen = true;
        }
        else if(isOpen)
        {
            panel.SetActive(false);
            isOpen = false;
        }
    }
    
    public void MuteAllAudio()
    {
        AudioListener.pause = true;
    }
    public void UnMuteAllAudio()
    {
        AudioListener.pause = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isClickedInside(panel))
        {
            panel.SetActive(false);
        }
    }

    bool isClickedInside(GameObject panel)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        Vector2 mousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        return rectTransform.rect.Contains(mousePosition);
    }
}
