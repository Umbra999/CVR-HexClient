using Hexed.Wrappers;
using System;
using UnityEngine;

namespace Hexed.Modules
{
    internal class Movement : MonoBehaviour
    {
        private static bool IsFlying = false;
        public static float FlySpeed = 4.2f;
        private static bool IsFlyBoost = false;
        private static new Transform transform;

        private static void FlyEnable()
        {
            PlayerWrappers.GetLocalPlayerSetup()._movementSystem.canMove = false;
            if (transform == null) transform = Camera.main.transform;
            IsFlying = true;
        }

        private static void FlyDisable()
        {
            IsFlying = false;
            PlayerWrappers.GetLocalPlayerSetup()._movementSystem.canMove = true;
        }

        public static void ToggleFly()
        {
            if (IsFlying) FlyDisable();
            else FlyEnable();
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) & Input.GetKey(KeyCode.LeftControl))
            {
                ToggleFly();
            }

            if (IsFlying)
            {
                if (GeneralWrappers.IsInVr())
                {
                    if (Math.Abs(Input.GetAxis("Vertical")) != 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * FlySpeed * Time.deltaTime * Input.GetAxis("Vertical");
                    if (Math.Abs(Input.GetAxis("Horizontal")) != 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * FlySpeed * Time.deltaTime * Input.GetAxis("Horizontal");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                }

                else
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        if (!IsFlyBoost)
                        {
                            FlySpeed *= 2f;
                            IsFlyBoost = true;
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        if (IsFlyBoost)
                        {
                            FlySpeed /= 2f;
                            IsFlyBoost = false;
                        }
                    }
                    if (Input.GetKey(KeyCode.E)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.Space)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.Q)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * -1f * FlySpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.W)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.S)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * -1f * FlySpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.A)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * -1f * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.D)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * FlySpeed * Time.deltaTime;
                }
            }
        }
    }
}
