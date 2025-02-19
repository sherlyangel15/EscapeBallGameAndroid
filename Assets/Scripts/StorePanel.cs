using TMPro;
using UnityEngine;

public class StorePanel : MonoBehaviour
{
    GameObject storePanelUI;
    Coin coin;

    [SerializeField] GameObject CoinTop;
    [SerializeField] GameObject HeartTop;

    void Start ()
    {
        coin = FindObjectOfType<Coin>();
        storePanelUI = GameObject.FindWithTag("StorePanel");
        CoinTop = GameObject.FindWithTag("CoinStoreUI");
        HeartTop = GameObject.FindWithTag("HeartStoreUI");

        storePanelUI.SetActive(false);
    }

    void Update()
    {
        CoinTop.GetComponent<TextMeshProUGUI>().text = coin.GetCoinAmount().ToString();
        HeartTop.GetComponent<TextMeshProUGUI>().text = coin.GetHeartAmount().ToString();
    }

    public void ClosePanel()
    {
        storePanelUI.SetActive(false);
    }
    
    public void OpenPanel()
    {
        storePanelUI.SetActive(true);
    }
    
}
