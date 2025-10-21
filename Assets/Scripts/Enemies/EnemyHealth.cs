using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Animator ani;

    // Update is called once per frame
    void Update()
    {
           
    }

    public void TakeDame(Vector3 pushDir)
    {
        GetComponent<Rigidbody>().AddForce(pushDir*4, ForceMode.Impulse);
        int randomBehaviour = Random.Range(1, 11);
        if(randomBehaviour <= 5)
        {
            ani.SetTrigger("hurtA");
        }    
        else
        {
            ani.SetTrigger("hurtB");
        }    
    }
}
