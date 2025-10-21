using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class InAir : State
{
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.InAir);
        Player.state = AnimatorState.InAir.ToString();
    }

    public override void Update(StateManager Player)
    {
        bool isGrounded = ActionMethod.IsGrounded(Player.transform, Player.GroundLayer);
        // Gravity
        Player.y_velocity = ActionMethod.AddGravity(Player.controller, Player.gravity, Player.y_velocity);

        // Move
        Vector3 direct = ActionMethod.GetDirect(Player);
        float speed;
        if(direct.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                speed = 2 * Player.speed;
            else
                speed = Player.speed;
            ActionMethod.MovePlayer(direct, speed, Player);
        }

        // Change State
        if (isGrounded)
            Player.ChangeState(new Idle());
    }

    public override void Exit(StateManager Player)
    {

    }
}
