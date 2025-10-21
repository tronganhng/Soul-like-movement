using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LockOn : MonoBehaviour
{
    StateManager Player;
    GameObject standardCam;
    GameObject combatCam;
    Vector3 camDir;
    void Start()
    {
        Player = GetComponent<StateManager>();
        standardCam = GameObject.Find("Camera").transform.GetChild(0).gameObject;
        combatCam = GameObject.Find("Camera").transform.GetChild(2).gameObject;
    }

    
    void Update()
    {
        Vector3 scanPos = Player.scanSize * (Player.transform.position - Player.Cam.transform.position).normalized + Player.transform.position;
        scanPos.y = Player.transform.position.y;
        Player.scanPoint.position = scanPos;
        Collider[] enemies = Physics.OverlapSphere(Player.scanPoint.position, Player.scanSize, Player.EnemyLayer);
        if (enemies.Length > 0)
        {
            Player.enemyPos = enemies[0].transform;
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                Player.lockOn = !Player.lockOn;
            }
        }
        else
        {
            Player.lockOn = false;
            Player.enemyPos = null;
        }

        
        if (Player.lockOn)
        {
            Player.ani.SetBool("lockOn", true);
            combatCam.SetActive(true);            
            camDir = Player.enemyPos.position - Player.camFollowPoint.position;
            if (camDir.magnitude >= 0.1f) Player.camFollowPoint.rotation = Quaternion.LookRotation(camDir);
        }
        else
        {
            Player.ani.SetBool("lockOn", false);
            if (combatCam.activeSelf)
            {
                standardCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Player.camFollowPoint.rotation.eulerAngles.y;
                combatCam.SetActive(false);
                StartCoroutine(ResetcamFollowPoint());
            }
        }
    }

    IEnumerator ResetcamFollowPoint()
    {
        float transitionTime = 0;
        while(transitionTime < .6f)
        {
            Player.camFollowPoint.rotation = Quaternion.LookRotation(camDir);
            transitionTime += Time.deltaTime;
            yield return null;
        }    
        Player.camFollowPoint.rotation = Player.transform.rotation;
    }
}
