using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

	public DiceSet diceSet;
	public CupController cupController;
	public GameObject ScoreCheckPanel;

	public Button ScoreCheckButton;
	public Button[] scoreSelectButton;
	public int[] diceNumList = new int[5];
	// 현재 숫자 //  0:num1, 1:num2, 2:num3, 3:num4, 4:num5, 5:num6, 6:Bonus ,7:choice, 8:fourOfAKind, 9:smallStraight, 10:largeStraight, 11:fullHouse, 12:yahtzee, 13:Score
	private int[] numList = new int[14];
	// 선택 숫자 //  0:num1, 1:num2, 2:num3, 3:num4, 4:num5, 5:num6, 6:Bonus ,7:choice, 8:fourOfAKind, 9:smallStraight, 10:largeStraight, 11:fullHouse, 12:yahtzee, 13:Score
	private int[] numSList = new int[14];
	//private bool bNum1 =true, bNum2 = true, bNum3 = true, bNum4 = true, bNum5 = true, bNum6 = true, bBonus = false,
	//	bChoice = true, bFourOfAKind = true, bSmallStraight = true, bLargeStraight = true, bFullHouse = true, bYahtzee = true, bAllScore = false;
	public Text[] textScore; //  0:num1, 1:num2, 2:num3, 3:num4, 4:num5, 5:num6, 6:Bonus ,7:choice, 8:fourOfAKind, 9:smallStraight, 10:largeStraight, 11:fullHouse, 12:yahtzee, 13:Score
	public Text[] selectedTextScore; //  0:num1, 1:num2, 2:num3, 3:num4, 4:num5, 5:num6, 6:Bonus ,7:choice, 8:fourOfAKind, 9:smallStraight, 10:largeStraight, 11:fullHouse, 12:yahtzee, 13:Score
	private float firstX;
    // Start is called before the first frame update
    void Start()
    {
		ScoreCheckButton.onClick.AddListener( ScoreCheckPanelOpen );
		firstX = ScoreCheckPanel.GetComponent<RectTransform>().anchoredPosition.x;
		ScoreReset();
	}

    // Update is called once per frame
    void Update()
    {
		SetNum();
		Sorting();
		AllRankSetting();
    }
	public void SetNum()
	{
		
		int index = 0;
		foreach ( GameObject ddd in diceSet.dice )
		{
			diceNumList[index] = ddd.GetComponent<DiceScript>().myNum;
			index++;
		}
	}
	public void AllRankSetting()
	{
		numList[0] = Num1();
		textScore[0].text = "" + numList[0];
		numList[1] = Num2();
		textScore[1].text = "" + numList[1];
		numList[2] = Num3();
		textScore[2].text = "" + numList[2];
		numList[3] = Num4();
		textScore[3].text = "" + numList[3];
		numList[4] = Num5();
		textScore[4].text = "" + numList[4];
		numList[5] = Num6();
		textScore[5].text = "" + numList[5];
		numList[6] = Bonus();
		textScore[6].text = "" + numList[6];
		numList[7] = Choice();
		textScore[7].text = "" + numList[7];
		numList[8] = FourOfAKind();
		textScore[8].text = "" + numList[8];
		numList[9] = SmallStraight();
		textScore[9].text = "" + numList[9];
		numList[10] = LargeStraight();
		textScore[10].text = "" + numList[10];
		numList[11] = FullHouse();
		textScore[11].text = "" + numList[11];
		numList[12] = Yahtzee();
		textScore[12].text = "" + numList[12];
		numList[13] = AllScore();
		textScore[13].text = "" + numList[13];
	}
	/// <summary>
	/// 소팅
	/// </summary>
	public void Sorting()
	{
		for(int i=0; i<diceNumList.Length;i++ )
		{
			for(int j =i; j<diceNumList.Length;j++ )
			{
				int index = diceNumList[i];
				if(diceNumList[i] > diceNumList[j])
				{
					diceNumList[i] = diceNumList[j];
					diceNumList[j] = index;
				}
				else
				{

				}
			}
		}
	}
	public int Num1()
	{
		int index = 0;
		for(int i=0;i< diceNumList.Length;i++ )
		{
			if(diceNumList[i] == 1)
			{
				index += 1;
			}
		}
		return index;
	}
	public int Num2()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( diceNumList[i] == 2 )
			{
				index += 2;
			}
		}
		return index;
	}
	public int Num3()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( diceNumList[i] == 3 )
			{
				index += 3;
			}
		}
		return index;
	}
	public int Num4()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( diceNumList[i] == 4 )
			{
				index += 4;
			}
		}
		return index;
	}
	public int Num5()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( diceNumList[i] == 5 )
			{
				index += 5;
			}
		}
		return index;
	}
	public int Num6()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( diceNumList[i] == 6 )
			{
				index += 6;
			}
		}
		return index;
	}
	public int Bonus()
	{
		int index =0;
		if( numSList[0] + numSList[1] + numSList[2] + numSList[3] + numSList[4] + numSList[5] >= 63)
		{
			index = 35;
		}else
		{
			index = 0;
		}
		return index;
	}
	public int Choice()
	{
		int index = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			index += diceNumList[i];
		}
		return index;
	}
	public int FourOfAKind()
	{
		int index = 0;
		int count = 0;
		int checkcount = 0; 
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if(count == 0)
			{
				checkcount = diceNumList[i];
				count++;
			}
			else
			{
				if(count == 4)
				{
					break;
				}
				if(checkcount == diceNumList[i])
				{
					count++;
				}
				else
				{
					checkcount = diceNumList[i];
					count = 1;
				}
			}
		}

		if(count == 4)
		{
			for ( int i = 0; i < diceNumList.Length; i++ )
			{
				index += diceNumList[i];
			}
		}
		else
		{
			index = 0;
		}
		return index;
	}
	public int SmallStraight()
	{
		int index = 0;
		int count = 0;
		for(int i=0;i<diceNumList.Length;i++ )
		{
			if(count ==0)
			{
				count++;
			}
			else
			{
				if(diceNumList[i-1]+1 == diceNumList[i])
				{
					count++;
				}
				else
				{
					count = 0;
				}
			}
		}
		if(count >=4)
		{
			index = 15;
		}
		else
		{
			index = 0;
		}
		return index;
	}
	public int LargeStraight()
	{
		int index = 0;
		int count = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( count == 0 )
			{
				count++;
			}
			else
			{
				if ( diceNumList[i - 1] + 1 == diceNumList[i] )
				{
					count++;
				}
				else
				{
					count = 0;
				}
			}
		}
		if ( count >= 5 )
		{
			index = 30;
		}
		else
		{
			index = 0;
		}
		return index;
	}
	public int FullHouse()
	{
		int index = 0;
		int count = 0;
		int checkcount = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( count == 0 )
			{
				checkcount = diceNumList[i];
				count++;
			}
			else
			{
				if(count ==3)
				{
					break;
				}
				if ( checkcount == diceNumList[i] )
				{
					count++;
				}
				else
				{
					checkcount = diceNumList[i];
					count = 1;
				}
			}
		}
		if(count ==3)
		{
			int count2 = 0;
			int checkcount2 = 0;
			for(int i =0;i<diceNumList.Length;i++ )
			{
				if ( checkcount == diceNumList[i] )
				{ }
				else
				{
					if ( count2 == 0 )
					{
						checkcount2 = diceNumList[i];
						count2++;
					}
					else
					{						
						if ( checkcount2 == diceNumList[i] )
						{
							count2++;
						}
						else
						{
							checkcount2 = diceNumList[i];
							count2 = 1;
						}
					}
				}
			}
			if(count2 ==2)
			{
				index = (checkcount * 3) + (checkcount2 * 2);
			}
			else
			{
				index = 0;
			}
		}
		else
		{
			index = 0;
		}
		return index;
	}
	public int Yahtzee()
	{

		int index = 0;
		int count = 0;
		int checkcount = 0;
		for ( int i = 0; i < diceNumList.Length; i++ )
		{
			if ( count == 0 )
			{
				checkcount = diceNumList[i];
				count++;
			}
			else
			{
				if ( checkcount == diceNumList[i] )
				{
					count++;
				}
				else
				{
					checkcount = diceNumList[i];
					count = 1;
				}
			}
		}

		if ( count == 5 )
		{
			index = 50;
		}
		else
		{
			index = 0;
		}
		return index;
	}
	public int AllScore()
	{
		int index = 0;
		for(int i =0;i< numSList.Length;i++ )
		{
			index += numSList[i];
		}
		return index;
	}

	public void ScoreCheckPanelOpen()
	{
		ScoreCheckPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		ScoreCheckButton.onClick.AddListener( ScoreCheckPanelClose );// 마지막에 버튼 기능 변경
	}

	public void ScoreCheckPanelClose()
	{
		ScoreCheckPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2( firstX, 0f);
		ScoreCheckButton.onClick.AddListener( ScoreCheckPanelOpen ); // 마지막에 버튼 기능 변경
	}
	
	public void ScoreSelectButton(int i)
	{
		scoreSelectButton[i].gameObject.SetActive( false );
		textScore[i].gameObject.SetActive( false );
		selectedTextScore[i].gameObject.SetActive( true );
		selectedTextScore[i].text = textScore[i].text;
		numSList[i] = numList[i];
		cupController.NextTurn();
	}

	public void ScorePanelAble( bool active = true)
	{
		ScoreCheckPanel.SetActive( active );
	}

	public void ScoreReset()
	{
		for(int i =0;i< selectedTextScore.Length;i++)
		{
			selectedTextScore[i].text = "";
		}
	}
}
