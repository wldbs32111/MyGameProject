using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupController : MonoBehaviour
{
	[Header( "��ũ��Ʈ" )]
	public ScoreController scoreController;

	[Header( " ĵ���� " )]
	public GameObject modilePanel;
	public Button RollButton;
	public Button ReButton;
	public Text touchText;

	[Header( "��" )]
	public GameObject cupSet;  // ��
	public GameObject cupLid;  // �� �Ѳ�
	public Rigidbody cupRigid; //�� Rigidbody
	public HingeJoint cupHinge; // �� HingeJoint

	[Header( "�ֻ���" )]
	public GameObject diceSet;           // �ֻ��� ����
	public Transform[] diceSetTruePosition;    // �ֻ����� ��ġ ������
	public Transform[] diceSetFalsePosition;    // �ֻ����� ��ġ ������
	public int[] diceSetNum = new int[5];             // �ֻ����� �� ������

	private bool[] reRoleDiceSet = new bool[5] { true, true, true, true, true };
	private bool shakeToggle =false; // ����� ���
	private GameObject currentDice;  // ���� �ֻ���
	private int rerollChanceCounter =0; 
	public enum State
	{
		State01 = 0,        // �ֻ��� ������ ��
		State02 = 1,        // �ֻ��� ������ ��
		State03 = 2,        // ���� �г�
		State04 = 3         // ���� ���
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
	/// AŰ�� ������ �Ͼ����
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
	/// SŰ�� ������ �Ͼ����
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
	/// RŰ������ �ֻ��� �籼��
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
	/// ���� �ܰ�� �Ѿ
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
	// ���� üũ
	public void CheckDiceNum()
	{
		for ( int i = 0; i < 5; i++ )
		{
			diceSetNum[i] = currentDice.GetComponent<DiceSet>().dice[i].GetComponent<DiceScript>().myNum;
		}
	}
	// �ֻ��� ��ġ ����
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
	//�ֻ��� ����
	public void ClickDice(int i)
	{
		reRoleDiceSet[i] = !reRoleDiceSet[i];
		//���� �ֻ��� ���� �ܰ���
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
