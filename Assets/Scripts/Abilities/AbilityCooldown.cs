using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private Image fillImage;
    [SerializeField][ReadOnly] private bool canUse = true;

    private Coroutine cooldownCoroutine;

    public bool CanUse { get { return canUse; } }
    public float Cooldown { 
        set {
            cooldown = value;
            ResetCooldown();
        }
    }


    public void ResetCooldown(){
        if(cooldownCoroutine != null){
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }

        canUse = true;
        UpdateCooldownImage(1);
    }

    public void StartCooldown(){
        if(cooldown == 0) return;

        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine(){
        canUse = false;

        float t = 0;
        while(t < cooldown){
            t += Time.deltaTime;
            UpdateCooldownImage(t/cooldown);
            yield return null;
        }

        canUse = true;
    }

    private void UpdateCooldownImage(float amount){
        if(fillImage == null) return;

        fillImage.fillAmount = amount;
    }
}
