using System.Collections;
using System;
using UnityEngine;
using MyMethods;
using Cinemachine;

public class Aim : State
{
    GameObject standardCam = GameObject.Find("Camera").transform.GetChild(0).gameObject;
    GameObject aimCam = GameObject.Find("Camera").transform.GetChild(1).gameObject;
    GameObject mainCam = Camera.main.gameObject;
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.Aim);
        Player.state = AnimatorState.Aim.ToString();
        aimCam.SetActive(true);
        standardCam.gameObject.SetActive(false);        
    }
    public override void Update(StateManager Player)
    {
        //Gravity
        ActionMethod.AddGravity(Player.controller, Player.gravity, -2f);

        // Change State
        if(Input.GetMouseButtonUp(1))
        {
            Player.ChangeState(new Idle());
        }

        // Logic
        Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * 30f, Color.red);
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hitinfo, 30f))
        {
            if(Player.ani.GetCurrentAnimatorStateInfo(0).IsName("1H_Ranged_Aiming"))
            {
                Vector3 shootDir = hitinfo.point - Player.shootPoint.position;
                Debug.DrawRay(Player.shootPoint.position, shootDir.normalized * shootDir.magnitude, Color.green);
                Player.crossBow.rotation = Quaternion.LookRotation(shootDir);
            }
        }
        else Player.crossBow.localRotation = Quaternion.Euler(0, -90, 0);

        ActionMethod.RotatePlayerToCam(Player);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0)
        {
            Player.transform.Rotate(Vector3.up, mouseX * Player.aimSenvisity);
        }
        if(mouseY != 0)
        {
            Player.camFollowPoint.Rotate(Vector3.right, -mouseY * Player.aimSenvisity * 0.5f);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (Player.ani.GetCurrentAnimatorStateInfo(0).IsName("1H_Ranged_Aiming"))
            {
                Player.ani.SetTrigger("shoot");
                Player.ani.SetTrigger("reload");
                PlayerEvents.Shoot?.Invoke();
            }
        }            
    }
    public override void Exit(StateManager Player)
    {
        aimCam.gameObject.SetActive(false);
        standardCam.gameObject.SetActive(true);
        standardCam.GetComponent<CinemachineFreeLook>().m_XAxis.Value = Camera.main.transform.rotation.eulerAngles.y;
    }
}
