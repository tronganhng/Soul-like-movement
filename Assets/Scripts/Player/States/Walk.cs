using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;
using Cinemachine;

public class Walk : State
{ 
    enum WalkType
    {
        forward = 0,
        back = 1,
    }

    public override void Enter(StateManager Player)
    {
        if(Player.lockOn)
        {
            Player.ani.SetBool("lockOn", true);
        }
        else
            Player.ani.SetBool("lockOn", false);
        Player.ani.SetInteger("State", (int)AnimatorState.Walk);
        Player.state = AnimatorState.Walk.ToString();
    }

    public override void Update(StateManager Player)
    {
        bool isGrounded = ActionMethod.IsGrounded(Player.transform, Player.GroundLayer);
        Vector3 direct = ActionMethod.GetDirect(Player);

        // Gravity
        Player.y_velocity = ActionMethod.AddGravity(Player.controller, Player.gravity, Player.y_velocity);

        // Change State
        CheckState(Player, direct, isGrounded);

        // Walk
        float speed = Player.speed;
        if (Player.lockOn)
        {
            ActionMethod.RotatePlayerToEnemy(Player);

            float TargetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + Player.Cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0, TargetAngle, 0) * Vector3.forward;
            Player.controller.Move(moveDir * speed * Time.deltaTime);

            Player.ani.SetFloat("velocityX", Input.GetAxis("Horizontal") * 2);
            Player.ani.SetFloat("velocityZ", Input.GetAxis("Vertical") * 2);
        }
        else
        {
            ActionMethod.MovePlayer(direct, speed, Player);
        }
    }

    public override void Exit(StateManager Player)
    {
        
    }

    void CheckState(StateManager Player, Vector3 direct, bool isGrounded)
    {
        if (!isGrounded)
        {
            Player.ChangeState(new InAir());
        }
        else
        {
            if (Player.y_velocity <= 0)
                Player.y_velocity = -2f;
        }

        if (direct.magnitude <= 0.1f)
            Player.ChangeState(new Idle());
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!Player.lockOn)
                Player.ChangeState(new Run());
        }
        if (Input.GetKey(KeyCode.C))
        {
            Player.ChangeState(new Shield());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.ChangeState(new Dodge());
        }
        switch (Player.rightHand)
        {
            case Weapons.Crossbow:
                if (Input.GetMouseButton(1) && !Player.isHurt)
                    Player.ChangeState(new Aim());
                break;
            default:
                if (Input.GetMouseButtonDown(0) && !Player.isHurt)
                    Player.ChangeState(new Melee());
                else if (Input.GetMouseButtonDown(1) && !Player.isHurt)
                    Player.ChangeState(new HeavyAttack());
                break;
        }
    }
}
