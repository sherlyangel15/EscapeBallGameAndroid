using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    float timer = 0f;
    Slider healthSlider;
    [SerializeField] float _health;
    [SerializeField] GameObject Warningtext;

    GameManager gameManager;
    LevelManager levelManager;
    PlayerAttackMode playerAttackMode;

    float healthReductionRate = 1.67f;
    float healthReductionInterval = 5f;

    public float Gethealth() => _health;
    public void SetHealth(int Health) => _health = Health;
    
    void Start()
    {
        _health = 100;

        healthSlider = FindObjectOfType<Slider>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
        playerAttackMode = FindObjectOfType<PlayerAttackMode>();
        Warningtext = GameObject.FindWithTag("WarningText");
        Warningtext.SetActive(false);
    }

    void Update()
    {
        AutomaticHealthReducer();
        SliderUpdater();
        ZeroHealth();
        WarningMessage();
    }
    
    public void AddHealth(int health)
    {
        _health = Mathf.Min(_health +health, 100);
    }

    public void SubtractHealth(int health)
    {
        if(_health > 0)
        {
            _health -= health;
        }
    }

    void AutomaticHealthReducer()
    {
        if(playerAttackMode.GetAttackModeStatus()) {return;}

        timer += Time.deltaTime;
        if(timer >= healthReductionInterval)
        {
            _health -= healthReductionRate;
            timer = 0f;
            if(_health < 0) _health = 0;
        }
    }

    void SliderUpdater()
    {
        healthSlider.value = _health;
        bool value = (_health <= 1) ? false : true;
        healthSlider.fillRect.gameObject.SetActive(value);
    }

    void ZeroHealth()
    {
        if(_health > 0) return;
        if(_health <= 3 && !playerAttackMode.GetAttackModeStatus())
        {
            levelManager.LoadLevelsScene();
        }
    }

    void WarningMessage()
    {
        bool isWarning = _health >= 10 && _health <= 15;
        Warningtext.SetActive(isWarning);
    }
}
