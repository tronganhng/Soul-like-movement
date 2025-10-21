using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    public GameObject rightHand;
    [SerializeField] Collider weapon;

    private void Start()
    {
        foreach(Transform weapons in rightHand.transform)
        {
            if(weapons.gameObject.activeSelf && weapons.gameObject.CompareTag("Weapon"))
            {
                weapon = weapons.GetComponent<Collider>();
            }    
        }
    }

    public void SetAttackMove()
    {
        GetComponentInParent<StateManager>().atk_velocity = 7;
    }

    public void TurnOnWeaponCollider()
    {
        weapon.enabled = true;
    }

    public void TurnOffWeaponCollider()
    {
        weapon.enabled = false;
    }
}
