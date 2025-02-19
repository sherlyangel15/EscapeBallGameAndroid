using System.Collections;
using UnityEngine;

public class HealthReducer : MonoBehaviour
{
    [SerializeField] int decreaseHealth;
    [SerializeField] float waitingSeconds;

    PlayerHealth playerHealth;
    PlayerAttackMode playerAttackMode;

    private bool _isNear;
    private Coroutine healthCoroutine;

    void Start () {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttackMode = FindObjectOfType<PlayerAttackMode>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            _isNear = true;
            if(healthCoroutine == null)
            {
                healthCoroutine = StartCoroutine(DelayTime());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            _isNear = false;
            if(healthCoroutine != null)
            {
                StopCoroutine(healthCoroutine);
                healthCoroutine = null;
            }
        }        
    }

    private IEnumerator DelayTime()
    {
        while(_isNear && !playerAttackMode.GetAttackModeStatus())
        {
            yield return new WaitForSeconds(waitingSeconds);
            playerHealth.SubtractHealth(decreaseHealth);
        }
        healthCoroutine = null;
    }
}
