using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Sword : MonoBehaviour
{
    public LayerMask targetLayer;
    [SerializeField] CinemachineImpulseSource impulseSource;
    public Transform playerPos;

    [SerializeField] AudioSource audioSource;
    public Sound hit1, hit2, hit3;
    private void OnTriggerEnter(Collider enemy)
    {
        if(((1 << enemy.gameObject.layer) & targetLayer.value) != 0)
        {
            if(enemy.TryGetComponent(out EnemyHealth enemyHealth))
            {
                Vector3 pushDir = (enemy.transform.position - playerPos.position).normalized;
                enemyHealth.TakeDame(pushDir);
                impulseSource.GenerateImpulse();
                PlayHitSFX();
            }
            else
            {
                Debug.Log("Target dont have health!");
            }
        }
    }

    void PlayHitSFX()
    {
        int random = Random.Range(1, 16);
        if(random <= 5)
        {
            audioSource.volume = hit1.volume;
            audioSource.pitch = hit1.pitch;
            audioSource.PlayOneShot(hit1.clip);
        }
        else if(random >= 11)
        {
            audioSource.volume = hit2.volume;
            audioSource.pitch = hit2.pitch;
            audioSource.PlayOneShot(hit2.clip);
        }
        else
        {
            audioSource.volume = hit3.volume;
            audioSource.pitch = hit3.pitch;
            audioSource.PlayOneShot(hit3.clip);
        }
    }
}
