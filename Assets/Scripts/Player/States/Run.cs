using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class Run : State
{
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.Run);
        Player.state = AnimatorState.Run.ToString();
    }

    public override void Update(StateManager Player)
    {
        bool isGrounded = ActionMethod.IsGrounded(Player.transform, Player.GroundLayer);
        Vector3 direct = ActionMethod.GetDirect(Player);

        // Gravity
        Player.y_velocity = ActionMethod.AddGravity(Player.controller, Player.gravity, Player.y_velocity);

        // Change State
        CheckState(Player, direct, isGrounded);

        // Run
        float speed = Player.speed * 1.7f;
        ActionMethod.MovePlayer(direct, speed, Player);
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
        if (!Input.GetKey(KeyCode.LeftShift) || Player.lockOn)
            Player.ChangeState(new Walk());
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
