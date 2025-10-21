using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class Dodge : State
{
    Vector3 direct;
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.Dodge);
        Player.state = AnimatorState.Dodge.ToString();
        if(Player.lockOn)
        {
            direct = ActionMethod.GetDirect(Player);
        }
    }
    public override void Update(StateManager Player)
    {
        //Gravity
        ActionMethod.AddGravity(Player.controller, Player.gravity, -2f);

        // Change State
        if(Player.ani.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            if (Player.ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                direct = ActionMethod.GetDirect(Player);
                if(direct.magnitude <= 0.1f)
                {
                    Player.ChangeState(new Idle());
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                        Player.ChangeState(new Run());
                    else
                        Player.ChangeState(new Walk());
                }
            }
        }

        // Logic
        if(direct.magnitude > 0.1f)
        {
            float TargetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + Player.Cam.eulerAngles.y;
            // Smooth rotation
            float angle = Mathf.SmoothDampAngle(Player.transform.eulerAngles.y, TargetAngle, ref Player.turnSmoothVelo, 0.07f);
            Player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
    public override void Exit(StateManager Player)
    {
        
    }
}
