using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    
    public GameObject openObject;
    public Rigidbody power;
    public GameObject dicePrefab;
    public GameObject canvas;
    public Text[] diceNumText;
    public GameObject rollButton;
    public GameObject resetButton;


    private GameObject instanDice;
    private bool a = false;
    private bool MoveCube;
    private bool[] diceDesList = new bool[5];
    private Vector3[] diceDesPosition = new Vector3[5];
    private Quaternion[] diceDesRotation = new Quaternion[5];
    // Start is called before the first frame update
    void Start()
    {
        instanDice = Instantiate(dicePrefab);
        //StartCoroutine(ForAdd());
#if UNITY_EDITOR
        rollButton.SetActive(false);
#else
rollButton.SetActive(true);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!MoveCube)
            {
                if (a)
                {
                    power.AddForce(-Vector3.forward * 40, ForceMode.Impulse);
                    this.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
                    //this.GetComponent<Rigidbody>().AddTorque(-Vector3.left * 30, ForceMode.VelocityChange);
                }
                else
                {
                    power.AddForce(Vector3.forward * 40, ForceMode.Impulse);
                    this.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
                    //this.GetComponent<Rigidbody>().AddTorque(Vector3.left * 30, ForceMode.VelocityChange);
                }
                a = !a;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            openObject.SetActive(false);
            JointLimits jlimits= this.GetComponent<HingeJoint>().limits;
            jlimits.min = -140f;
            this.GetComponent<HingeJoint>().limits = jlimits;
            this.GetComponent<HingeJoint>().useMotor = true;
            Invoke("PlayAniCameraMoveCube", 3f);
        }else if(Input.GetKeyDown(KeyCode.R))
        {
            if (MoveCube)
            {
                CancelInvoke("PlayAniCameraMoveCube");
                this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                StartCoroutine(InsDice());
                openObject.SetActive(true);
                JointLimits jlimits = this.GetComponent<HingeJoint>().limits;
                jlimits.min = -45f;
                this.GetComponent<HingeJoint>().limits = jlimits;
                this.GetComponent<HingeJoint>().useMotor = false;
                canvas.SetActive(false);
                if (MoveCube)
                {
                    Camera.main.GetComponent<Animation>().Play("MoveCupWatch");

                }
                MoveCube = false;
				

			}
        }
#else
		if ( Input.GetTouch( 0 ).phase == TouchPhase.Began )
		{
			
				if ( !MoveCube )
				{
					if ( a )
					{
						power.AddForce( -Vector3.forward * 40, ForceMode.Impulse );
						this.GetComponent<Rigidbody>().AddForce( Vector3.up * 10, ForceMode.Impulse );
						//this.GetComponent<Rigidbody>().AddTorque(-Vector3.left * 30, ForceMode.VelocityChange);
					}
					else
					{
						power.AddForce( Vector3.forward * 40, ForceMode.Impulse );
						this.GetComponent<Rigidbody>().AddForce( Vector3.up * 10, ForceMode.Impulse );
						//this.GetComponent<Rigidbody>().AddTorque(Vector3.left * 30, ForceMode.VelocityChange);
					}
					a = !a;
				}
			
		}
#endif
	}
	IEnumerator InsDice()
    {
        Destroy(instanDice);
        instanDice = Instantiate(dicePrefab);       
        yield return new WaitForSeconds(0.1f);
        instanDice.GetComponent<DiceSet>().ResetDice(diceDesList, diceDesPosition, diceDesRotation);
        yield return new WaitForSeconds(0.1f);
        for(int i=0;i<diceDesList.Length;i++)
        {
            diceDesList[i] = false;
            diceDesPosition[i] = Vector3.zero;
            diceDesRotation[i] = new Quaternion();
        }
    }
    IEnumerator ForAdd()
    {
        while (true)
        {
            this.GetComponent<Rigidbody>().AddForce(-Vector3.left * 30, ForceMode.VelocityChange);
            this.GetComponent<Rigidbody>().AddTorque(-Vector3.left * 30, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.2f);
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 30, ForceMode.VelocityChange);
            this.GetComponent<Rigidbody>().AddTorque(Vector3.left * 30, ForceMode.VelocityChange);
            yield return new WaitForSeconds(0.2f);
        }
    }
     public void PlayAniCameraMoveCube()
    {
        Camera.main.GetComponent<Animation>().Play("MoveCubeWatch");
        canvas.SetActive(true);
        instanDice.GetComponent<DiceSet>().DiceTextSet();
#if UNITY_EDITOR
#else
        rollButton.SetActive(false);
        resetButton.SetActive(true);
#endif

        MoveCube = true;
    }
    public void PlayAniCameraMoveCup()
    {
        Camera.main.GetComponent<Animation>().Play("MoveCupWatch");
    }
    public void DesDice(int diceNum, Vector3 diceTransform,Quaternion diceRo)
    {
        this.diceDesList[diceNum] = true;
        this.diceDesPosition[diceNum] = diceTransform;
        this.diceDesRotation[diceNum] = diceRo;
    }
    public void RollButton()
    {
        openObject.SetActive(false);
        JointLimits jlimits = this.GetComponent<HingeJoint>().limits;
        jlimits.min = -140f;
        this.GetComponent<HingeJoint>().limits = jlimits;
        this.GetComponent<HingeJoint>().useMotor = true;
        Invoke("PlayAniCameraMoveCube", 3f);
    }
    public void ReButton()
    {
        CancelInvoke("PlayAniCameraMoveCube");
        this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        StartCoroutine(InsDice());
        openObject.SetActive(true);
        JointLimits jlimits = this.GetComponent<HingeJoint>().limits;
        jlimits.min = -45f;
        this.GetComponent<HingeJoint>().limits = jlimits;
        this.GetComponent<HingeJoint>().useMotor = false;
        canvas.SetActive(false);
        if (MoveCube)
        {
            Camera.main.GetComponent<Animation>().Play("MoveCupWatch");
        }
        MoveCube = false;
        rollButton.SetActive(true);
        resetButton.SetActive(false);
    }
}
