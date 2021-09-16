using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
	[Header( "컵" )]
	public GameObject cupSet;  // 컵
	public GameObject cupLid;  // 컵 뚜껑
	public Rigidbody cupRigid; //컵 Rigidbody
	public HingeJoint cupHinge; // 컵 HingeJoint

	[Header( "주사위" )]
	public GameObject diceSet;           // 주사위 모음
	public Transform[] diceSetTruePosition;    // 주사위의 위치 데이터
	public Transform[] diceSetFalsePosition;    // 주사위의 위치 데이터
	public int[] diceSetNum = new int[5];             // 주사위의 눈 데이터

	public bool[] reRoleDiceSet = new bool[5] { true, true, true, true, true };
	private bool shakeToggle =false; // 흔들기용 토글
	private GameObject currentDice;  // 현재 주사위

    // Start is called before the first frame update
    void Start()
    {
		currentDice = Instantiate( diceSet );
	}

    // Update is called once per frame
    void Update()
    {
		ShakeCupKeyA();
		PourCupKeyS();
	}
	/// <summary>
	/// A키를 누르면 일어나는일
	/// </summary>
	public void ShakeCupKeyA()
	{
		if ( Input.GetKeyDown( KeyCode.A ) )
		{

			if ( shakeToggle )
			{
				cupRigid.AddForce( -Vector3.forward * 40, ForceMode.Impulse );
				cupSet.GetComponent<Rigidbody>().AddForce( Vector3.up * 10, ForceMode.Impulse );
				//this.GetComponent<Rigidbody>().AddTorque(-Vector3.left * 30, ForceMode.VelocityChange);
			}
			else
			{
				cupRigid.AddForce( Vector3.forward * 40, ForceMode.Impulse );
				cupSet.GetComponent<Rigidbody>().AddForce( Vector3.up * 10, ForceMode.Impulse );
				//this.GetComponent<Rigidbody>().AddTorque(Vector3.left * 30, ForceMode.VelocityChange);
			}
			shakeToggle = !shakeToggle;

		}
	}
	/// <summary>
	/// S키를 누르면 일어나는일
	/// </summary>
	public void PourCupKeyS()
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			JointLimits jlimits = cupHinge.limits;
			jlimits.min = -140f;
			cupHinge.limits = jlimits;
			cupHinge.useMotor = true;
			cupLid.SetActive( false );
			Invoke( "SelectPhase", 3f );
		}
	}
	/// <summary>
	/// 선택 단계로 넘어감
	/// </summary>
	public void SelectPhase()
	{
		Camera.main.GetComponent<Animation>().Play( "MoveCubeWatch" );
		cupSet.SetActive( false );
		currentDice.GetComponent<DiceSet>().DiceDis();
		Camera.main.orthographic = true;
		Camera.main.orthographicSize = 1.1f;
		//Camera.main.orthographic = false;
		CheckDiceNum();
		DicePositioning();
	}
	public void CheckDiceNum()
	{
		for ( int i = 0; i < 5; i++ )
		{
			diceSetNum[i] = currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().myNum;
		}
	}
	public void DicePositioning()
	{
		for( int i =0; i < 5; i++ )
		{
			currentDice.GetComponent<DiceSet>().dice[i].transform.position = diceSetTruePosition[i].position;
		}
	}
	public void ClickDice(int i)
	{
		reRoleDiceSet[i] = !reRoleDiceSet[i];
		//현재 주사위 선택 단계임
		if ( reRoleDiceSet[i] )
		{
			currentDice.GetComponent<DiceSet>().dice[i].transform.position = diceSetTruePosition[i].position;
		}
		else
		{
			currentDice.GetComponent<DiceSet>().dice[i].transform.position = diceSetFalsePosition[i].position;
		}
	}
}
