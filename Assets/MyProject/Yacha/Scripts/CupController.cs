using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupController : MonoBehaviour
{
	[Header( "스크립트" )]
	public ScoreController scoreController;

	[Header( " 캔버스 " )]
	public GameObject modilePanel;
	public Button RollButton;
	public Button ReButton;
	public Text touchText;

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

	private bool[] reRoleDiceSet = new bool[5] { true, true, true, true, true };
	private bool shakeToggle =false; // 흔들기용 토글
	private GameObject currentDice;  // 현재 주사위
	private int rerollChanceCounter =0; 
	public enum State
	{
		State01 = 0,        // 주사위 던지기 전
		State02 = 1,        // 주사위 던지기 후
		State03 = 2,        // 점수 패널
		State04 = 3         // 점수 계산
	}
	public State state = State.State01;

    // Start is called before the first frame update
    void Start()
    {
		currentDice = Instantiate( diceSet );
		scoreController.diceSet = currentDice.GetComponent<DiceSet>();
		scoreController.ScorePanelAble( false );
#if UNITY_EDITOR
		modilePanel.SetActive( false );
#elif UNITY_ANDROID
		modilePanel.SetActive( true );
		RollButton.gameObject.SetActive(true);
		ReButton.gameObject.SetActive(false);
		touchText.gameObject.SetActive(true);
#endif
	}

	// Update is called once per frame
	void Update()
    {
#if UNITY_EDITOR
		ShakeCupKeyA();
		PourCupKeyS();
		if ( !cupSet.activeSelf )
		{
			ReroleCupKeyR();
		}
#elif UNITY_ANDROID
		ShakeCupKeyTouch();
#endif
	}
	/// <summary>
	/// A키를 누르면 일어나는일
	/// </summary>
	public void ShakeCupKeyA()
	{
		if ( state == State.State01 && Input.GetKeyDown( KeyCode.A ) )
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
	public void ShakeCupKeyTouch()
	{
		if ( state == State.State01 && Input.touchCount == 1 )
		{			
			if ( Input.GetTouch( 0 ).phase == TouchPhase.Began )
			{
				touchText.gameObject.SetActive( false );
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
	}
	/// <summary>
	/// S키를 누르면 일어나는일
	/// </summary>
	public void PourCupKeyS()
	{
#if UNITY_EDITOR
		if ( state == State.State01 && Input.GetKeyDown(KeyCode.S))
		{
			JointLimits jlimits = cupHinge.limits;
			jlimits.min = -140f;
			cupHinge.limits = jlimits;
			cupHinge.useMotor = true;
			cupLid.SetActive( false );
			Invoke( "SelectPhase", 3f );
		}
#elif UNITY_ANDROID
		JointLimits jlimits = cupHinge.limits;
			jlimits.min = -140f;
			cupHinge.limits = jlimits;
			cupHinge.useMotor = true;
			cupLid.SetActive( false );
			Invoke( "SelectPhase", 3f );
#endif
	}
	/// <summary>
	/// R키누르면 주사위 재굴림
	/// </summary>
	public void ReroleCupKeyR()
	{		
#if UNITY_EDITOR
		if ( rerollChanceCounter <2 && state == State.State02 && Input.GetKeyDown( KeyCode.R ) )
		{
			cupSet.transform.eulerAngles = Vector3.zero;
			cupSet.SetActive( true );
			cupLid.SetActive( true );
			JointLimits jlimits = cupHinge.limits;
			jlimits.min = -45f;
			cupHinge.limits = jlimits;
			cupHinge.useMotor = false;
			Camera.main.orthographic = false;
			Camera.main.GetComponent<Animation>().Play( "MoveCupWatch" );
			scoreController.ScorePanelAble( false );
			scoreController.ScoreCheckPanelClose();
			state = State.State01;
			rerollChanceCounter++;			
			for ( int i = 0; i < 5; i++ )
			{
				if ( reRoleDiceSet[i] )
				{
					currentDice.GetComponent<DiceSet>().dice[i].transform.localPosition = new Vector3( 0.856851f, 1.64f, -1.265171f );
					currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().DiceEnalbe();
				}
				else
				{
					currentDice.GetComponent<DiceSet>().dice[i].transform.position = diceSetFalsePosition[i].position;
				}
			}
		}

#elif UNITY_ANDROID
		touchText.gameObject.SetActive( true );
		cupSet.transform.eulerAngles = Vector3.zero;
			cupSet.SetActive( true );
			cupLid.SetActive( true );
			JointLimits jlimits = cupHinge.limits;
			jlimits.min = -45f;
			cupHinge.limits = jlimits;
			cupHinge.useMotor = false;
			Camera.main.orthographic = false;
			Camera.main.GetComponent<Animation>().Play( "MoveCupWatch" );
			scoreController.ScorePanelAble( false );
			scoreController.ScoreCheckPanelClose();
		RollButton.gameObject.SetActive(true);
		ReButton.gameObject.SetActive(false);
		state = State.State01;
		rerollChanceCounter++;
			for ( int i = 0; i < 5; i++ )
			{
				if ( reRoleDiceSet[i] )
				{
					currentDice.GetComponent<DiceSet>().dice[i].transform.localPosition = new Vector3( 0.856851f, 1.64f, -1.265171f );
					currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().DiceEnalbe();
				}
				else
				{
					currentDice.GetComponent<DiceSet>().dice[i].transform.position = diceSetFalsePosition[i].position;
				}
			}
#endif
	}
	public void NextTurn()
	{
		touchText.gameObject.SetActive( true );
		cupSet.transform.eulerAngles = Vector3.zero;
		cupSet.SetActive( true );
		cupLid.SetActive( true );
		JointLimits jlimits = cupHinge.limits;
		jlimits.min = -45f;
		cupHinge.limits = jlimits;
		cupHinge.useMotor = false;
		Camera.main.orthographic = false;
		Camera.main.GetComponent<Animation>().Play( "MoveCupWatch" );
		scoreController.ScorePanelAble( false );
		scoreController.ScoreCheckPanelClose();
		RollButton.gameObject.SetActive( true );
		ReButton.gameObject.SetActive( false );
		state = State.State01;
		rerollChanceCounter=0;
		for (int i=0;i<reRoleDiceSet.Length;i++ )
		{
			reRoleDiceSet[i] = true;
		}		
		for ( int i = 0; i < 5; i++ )
		{
			currentDice.GetComponent<DiceSet>().dice[i].transform.localPosition = new Vector3( 0.856851f, 1.64f, -1.265171f );
			currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().DiceEnalbe();
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
		RollButton.gameObject.SetActive( false );
		ReButton.gameObject.SetActive( true );
		scoreController.ScorePanelAble( true );
		CheckDiceNum();
		DicePositioning();
		state = State.State02;
		if ( rerollChanceCounter >= 2 )
		{
			ReButton.gameObject.SetActive( false );
		}
	}
	// 숫자 체크
	public void CheckDiceNum()
	{
		for ( int i = 0; i < 5; i++ )
		{
			diceSetNum[i] = currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().myNum;
		}
	}
	// 주사위 위치 저장
	public void DicePositioning()
	{
		for( int i =0; i < 5; i++ )
		{
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
	//주사위 선택
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
