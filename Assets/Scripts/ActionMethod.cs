using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMethods
{
    public static class ActionMethod
    {
        public static void MovePlayer(Vector3 direct, float speed, StateManager Player)
        {
            float TargetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + Player.Cam.eulerAngles.y;
            // Smooth rotation
            float angle = Mathf.SmoothDampAngle(Player.transform.eulerAngles.y, TargetAngle, ref Player.turnSmoothVelo, Player.turnSmoothTime);
            Player.transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, TargetAngle, 0) * Vector3.forward;
            Player.controller.Move(moveDir * speed * Time.deltaTime);
        }

        public static void RotatePlayerToCam(StateManager Player)
        {
            float TargetAngle = Player.Cam.eulerAngles.y;
            // Smooth rotation
            float angle = Mathf.SmoothDampAngle(Player.transform.eulerAngles.y, TargetAngle, ref Player.turnSmoothVelo, Player.turnSmoothTime);
            Player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public static void RotatePlayerToEnemy(StateManager Player)
        {
            Vector3 direct = Player.enemyPos.position - Player.transform.position;
            float TargetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg;
            // Smooth rotation
            float angle = Mathf.SmoothDampAngle(Player.transform.eulerAngles.y, TargetAngle, ref Player.turnSmoothVelo, Player.turnSmoothTime);
            Player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        public static bool IsGrounded(Transform transform , LayerMask GroundLayer)
        {
            return Physics.CheckBox(transform.position, new Vector3(0.4f, 0.2f, 0.4f), Quaternion.identity, GroundLayer);
        }
    
        public static float AddGravity(CharacterController controller, float gravity, float Y_velocity)
        {
            Y_velocity += gravity * Time.deltaTime;
            controller.Move(new Vector3(0,Y_velocity,0) * Time.deltaTime);

            return Y_velocity;
        }
    
        public static Vector3 GetDirect(StateManager Player)
        {
            if (Player.isHurt) return new Vector3(0, 0, 0);
            float XAxis = Input.GetAxisRaw("Horizontal");
            float ZAxis = Input.GetAxisRaw("Vertical");
            return new Vector3(XAxis, 0, ZAxis).normalized;
        }
    }    
}