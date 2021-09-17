using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public int[] diceNumList = new int[5];
    // Start is called before the first frame update
    void Start()
    {		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	/// <summary>
	/// ¼ÒÆÃ
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
		}else
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
		return index;
	}
	public int Yahtzee()
	{
		int index = 0;
		return index;
	}
}
