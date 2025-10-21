using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class HeavyAttack : State
{
    bool dodge = false;
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.HeavyAttack);
        Player.state = AnimatorState.HeavyAttack.ToString();
    }
    public override void Update(StateManager Player)
    {
        //Gravity
        ActionMethod.AddGravity(Player.controller, Player.gravity, -2);

        //Logic
        if (Player.enemyPos != null)
        {
            ActionMethod.RotatePlayerToEnemy(Player);
        }

        // Change State
        AnimatorStateInfo animation = Player.ani.GetCurrentAnimatorStateInfo(0);
        if (!animation.IsTag("Heavy attack")) return;
        if (animation.normalizedTime >= 1 || Player.isHurt)
        {
            Vector3 direct = ActionMethod.GetDirect(Player);
            if (direct.magnitude <= 0.1f)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dodge = true;
        }
        if (dodge && animation.normalizedTime >= 0.9)
        {
            Player.ChangeState(new Dodge());
        }
    }
    public override void Exit(StateManager Player)
    {
        
    }
}
