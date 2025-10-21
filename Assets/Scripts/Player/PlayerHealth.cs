using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator ani;
    Coroutine hurting;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDame();
    }

    public void TakeDame()
    {
        ani.SetTrigger("hurt");
        if (hurting != null) StopCoroutine(hurting);
        hurting =  StartCoroutine(SetHurt());
    }

    IEnumerator SetHurt()
    {
        GetComponent<StateManager>().isHurt = true;
        yield return new WaitForSeconds(0.52f);
        GetComponent<StateManager>().isHurt = false;
    }
}
