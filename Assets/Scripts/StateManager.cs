using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    State currentState;

    public string state;
    public bool isHurt;
    public bool lockOn;
    public Weapons rightHand;
    public LayerMask GroundLayer, EnemyLayer;
    public WeaponOS weaponOS;
    public Animator ani;
    public Transform Cam, camFollowPoint, shootPoint, scanPoint, crossBow;
    public Transform enemyPos;
    public CharacterController controller;
    [HideInInspector] public float turnSmoothVelo;
    [HideInInspector] public float y_velocity = -2f;
    [HideInInspector] public float atk_velocity = 0;
    public float turnSmoothTime = 0.15f;
    public float scanSize = 5f;
    public float speed = 8f;
    public float dodgeSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpForce = 7f;
    public float aimSenvisity = 2.8f;

    public void Start()
    {
        ChangeState(new Idle());
        int index = weaponOS.weapons.FindIndex(x => x.name == rightHand);
        ani.runtimeAnimatorController = weaponOS.weapons[index].animatorController;
    }    
    
    void Update()
    {
        if (currentState != null)
            currentState.Update(this);
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.4f, 0.2f, 0.4f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(scanPoint.position, scanSize);
    }
}

public abstract class State
{
    public abstract void Enter(StateManager stateManager);
    public abstract void Update(StateManager stateManager);
    public abstract void Exit(StateManager stateManager);
}