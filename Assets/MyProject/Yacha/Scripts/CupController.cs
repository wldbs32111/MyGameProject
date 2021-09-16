using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
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

	public bool[] reRoleDiceSet = new bool[5] { true, true, true, true, true };
	private bool shakeToggle =false; // ����� ���
	private GameObject currentDice;  // ���� �ֻ���

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
	/// AŰ�� ������ �Ͼ����
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
	/// SŰ�� ������ �Ͼ����
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
