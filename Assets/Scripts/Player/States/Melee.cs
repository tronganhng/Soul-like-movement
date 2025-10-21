using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMethods;

public class Melee : State
{
    bool dodge = false;
    bool heavyAtk = false;
    public override void Enter(StateManager Player)
    {
        Player.ani.SetInteger("State", (int)AnimatorState.Melee);
        Player.state = AnimatorState.Melee.ToString();
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
        if (!animation.IsTag("Melee")) return;        

        if (animation.normalizedTime >= 1)
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
                {
                    Player.ChangeState(new Walk());
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Player.ani.SetTrigger("melee tran");
                
            }
        } 
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.ani.ResetTrigger("melee tran");
            dodge = true;
        }
        if(Input.GetMouseButtonDown(1))
        {
            Player.ani.ResetTrigger("melee tran");
            heavyAtk = true;
        }
        if(animation.normalizedTime >= 0.9f)
        {
            if(dodge)
                Player.ChangeState(new Dodge());
            else if(heavyAtk)
                Player.ChangeState(new HeavyAttack());
        }
    }
    public override void Exit(StateManager Player)
    {
        Player.ani.ResetTrigger("melee tran");
    }
}
