using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class Shield : State
{
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.Shield);
        Player.state = AnimatorState.Shield.ToString();
    }
    public override void Update(StateManager Player)
    {
        //Gravity
        Player.y_velocity = ActionMethod.AddGravity(Player.controller, Player.gravity, Player.y_velocity);
        if (ActionMethod.IsGrounded(Player.transform, Player.GroundLayer) && Player.y_velocity <= 0)
            Player.y_velocity = -2f;

        // Change State
        if (Input.GetKeyUp(KeyCode.C))
        {
            Player.ChangeState(new Idle());
        }

        // Logic
        Vector3 direct = ActionMethod.GetDirect(Player);
        float speed = Player.speed * 0.7f;
        if (Player.lockOn)
        {
            ActionMethod.RotatePlayerToEnemy(Player);
            float TargetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + Player.Cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0, TargetAngle, 0) * Vector3.forward;
            if(direct.magnitude > 0.1f) Player.controller.Move(moveDir * speed * Time.deltaTime);
            Player.ani.SetFloat("velocityX", Input.GetAxis("Horizontal") * 2);
            Player.ani.SetFloat("velocityZ", Input.GetAxis("Vertical") * 2);
        }
        else
        {
            if (direct.magnitude > 0.1f) ActionMethod.MovePlayer(direct, speed, Player);
            Player.ani.SetFloat("velocityX", 0);
            float x = Mathf.Abs(Input.GetAxis("Horizontal"));
            float z = Mathf.Abs(Input.GetAxis("Vertical"));            
            Player.ani.SetFloat("velocityZ", Mathf.Max(x,z) * 1.7f);
        }
    }
    public override void Exit(StateManager Player)
    {

    }
}
