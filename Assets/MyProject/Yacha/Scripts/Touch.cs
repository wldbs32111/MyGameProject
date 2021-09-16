using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//#if UNITY_EDITOR
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit cubeHit, planeHit;
//            int cubeLayer = 1 << LayerMask.NameToLayer("Dice");
//            int planeLayer = 1 << LayerMask.NameToLayer("Plane");
//            if (Physics.Raycast(ray, out cubeHit, Mathf.Infinity, cubeLayer))
//            {
//                firstCubePoint = cubeHit.transform.position;
//                firstCubeObj = cubeHit.collider.gameObject;
//                //cubeHit.collider.gameObject.GetComponent<DiceScript>().CubeSelect();
//            }
//            if (Physics.Raycast(ray, out planeHit, Mathf.Infinity, planeLayer))
//            {
//                firstPlanePoint = planeHit.point;
//            }
//        }
//        else if (Input.GetMouseButton(0))
//        {
//            if (firstCubeObj != null)
//            {
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                RaycastHit planeHit;
//                int planeLayer = 1 << LayerMask.NameToLayer("Plane");
//                Vector3 planePoint = Vector3.zero;
//                if (Physics.Raycast(ray, out planeHit, Mathf.Infinity, planeLayer))
//                {
//                    planePoint = planeHit.point;
//                    firstCubeObj.transform.position = firstCubePoint + planePoint - firstPlanePoint;
//                    CubeRo();
//                }
//            }
//        }
//        else if(Input.GetMouseButtonUp(0))
//        {
//            firstCubeObj = null;
//            //CancelInvoke("CubeRo");
//        }
//#else
//if(Input.touchCount == 1)
//        {
//            if (Input.GetTouch(0).phase == TouchPhase.Began)
//            {
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                RaycastHit cubeHit, planeHit;
//                int cubeLayer = 1 << LayerMask.NameToLayer("Dice");
//                int planeLayer = 1 << LayerMask.NameToLayer("Plane");
//                if (Physics.Raycast(ray, out cubeHit, Mathf.Infinity, cubeLayer))
//                {
//                    firstCubePoint = cubeHit.transform.position;
//                    firstCubeObj = cubeHit.collider.gameObject;
//                    cubeHit.collider.gameObject.GetComponent<DiceScript>().CubeSelect();
//                }
//                if (Physics.Raycast(ray, out planeHit, Mathf.Infinity, planeLayer))
//                {
//                    firstPlanePoint = planeHit.point;
//                }
//            }
//            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
//            {
//                if (firstCubeObj != null)
//                {
//                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                    RaycastHit planeHit;
//                    int planeLayer = 1 << LayerMask.NameToLayer("Plane");
//                    Vector3 planePoint = Vector3.zero;
//                    if (Physics.Raycast(ray, out planeHit, Mathf.Infinity, planeLayer))
//                    {
//                        planePoint = planeHit.point;
//                        firstCubeObj.transform.position = firstCubePoint + planePoint - firstPlanePoint;
//                        CubeRo();
//                    }
//                }
//            }
//            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
//            {
//                firstCubeObj = null;
//            }
//        }
//#endif
    }
    private void CubeRo()
    {
        //firstCubeObj.GetComponent<DiceScript>().RotationCube();
    }
}
