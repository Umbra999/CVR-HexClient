using ABI_RC.Core;
using Hexed.Wrappers;
using System.Collections;
using UnityEngine;

namespace Hexed.Modules
{
    internal class Thirdperson : MonoBehaviour
    {
        private static GameObject BackCamera;
        private static GameObject FrontCamera;
        private static int CameraSetup = 0;

        public void Start()
        {
            GameObject referenceCamera = Camera.main.gameObject;

            BackCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BackCamera.name = "BackCamera";
            Destroy(BackCamera.GetComponent<BoxCollider>());
            Destroy(BackCamera.GetComponent<MeshRenderer>());
            Destroy(BackCamera.GetComponent<MeshFilter>());
            BackCamera.transform.localScale = referenceCamera.transform.localScale;
            Rigidbody BackRigidbody = BackCamera.AddComponent<Rigidbody>();
            BackRigidbody.isKinematic = true;
            BackRigidbody.useGravity = false;
            BackCamera.GetComponent<Renderer>().enabled = false;
            BackCamera.AddComponent<Camera>();
            BackCamera.transform.parent = referenceCamera.transform;
            BackCamera.transform.rotation = referenceCamera.transform.rotation;
            BackCamera.transform.position = referenceCamera.transform.position;
            BackCamera.transform.position -= BackCamera.transform.forward * 2f;
            BackCamera.GetComponent<Camera>().fieldOfView = 75f;

            FrontCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
            FrontCamera.name = "FrontCamera";
            Destroy(FrontCamera.GetComponent<BoxCollider>());
            Destroy(FrontCamera.GetComponent<MeshRenderer>());
            Destroy(FrontCamera.GetComponent<MeshFilter>());
            FrontCamera.transform.localScale = referenceCamera.transform.localScale;
            Rigidbody FrontRigidbody = FrontCamera.AddComponent<Rigidbody>();
            FrontRigidbody.isKinematic = true;
            FrontRigidbody.useGravity = false;
            FrontCamera.GetComponent<Renderer>().enabled = false;
            FrontCamera.AddComponent<Camera>();
            BackCamera.GetComponent<BoxCollider>().enabled = false;
            FrontCamera.transform.parent = referenceCamera.transform;
            FrontCamera.transform.rotation = referenceCamera.transform.rotation;
            FrontCamera.transform.Rotate(0f, 180f, 0f);
            FrontCamera.transform.position = referenceCamera.transform.position;
            FrontCamera.transform.position += -FrontCamera.transform.forward * 2f;

            FrontCamera.GetComponent<Camera>().fieldOfView = 75f;

            FrontCamera.GetComponent<Camera>().enabled = false;
            BackCamera.GetComponent<Camera>().enabled = false;
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
            {
                switch (CameraSetup)
                {
                    case 0:
                        CameraSetup = 1;
                        break;

                    case 1:
                        CameraSetup = 2;
                        break;

                    case 2:
                        CameraSetup = 0;
                        break;
                }
            }

            else if (BackCamera != null && FrontCamera != null)
            {
                switch (CameraSetup)
                {
                    case 0:
                        RootLogic.Instance.activeCamera.enabled = true;
                        BackCamera.GetComponent<Camera>().enabled = false;
                        FrontCamera.GetComponent<Camera>().enabled = false;
                        break;

                    case 1:
                        RootLogic.Instance.activeCamera.enabled = false;
                        BackCamera.GetComponent<Camera>().enabled = true;
                        FrontCamera.GetComponent<Camera>().enabled = false;
                        break;

                    case 2:
                        RootLogic.Instance.activeCamera.enabled = false;
                        BackCamera.GetComponent<Camera>().enabled = false;
                        FrontCamera.GetComponent<Camera>().enabled = true;
                        break;
                }

                if (CameraSetup != 0)
                {
                    float axis = Input.GetAxis("Mouse ScrollWheel");
                    if (axis > 0)
                    {
                        BackCamera.transform.position += BackCamera.transform.forward * 0.1f;
                        FrontCamera.transform.position -= BackCamera.transform.forward * 0.1f;
                    }
                    else if (axis < 0)
                    {
                        BackCamera.transform.position -= BackCamera.transform.forward * 0.1f;
                        FrontCamera.transform.position += BackCamera.transform.forward * 0.1f;
                    }
                }
            }
        }
    }
}
