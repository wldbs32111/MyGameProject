using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSet : MonoBehaviour
{
    public GameObject[] dice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void DiceDis()
	{
		foreach(GameObject dice in dice)
		{
			dice.GetComponent<DiceScript>().DiceDisable();
		}
	}
	public void DiceEn()
	{
		foreach ( GameObject dice in dice )
		{
			dice.GetComponent<DiceScript>().DiceEnalbe();
		}
	}
    public void DiceTextSet()
    {
        for(int i=0;i<dice.Length; i++)
        {
            dice[i].GetComponent<DiceScript>().DiceNum();
        }
    }
    public void ResetDice(bool[] bo,Vector3[] v3,Quaternion[] qu)
    {
        
        for(int i = 0; i < bo.Length; i++)
        {
            if (bo[i])
            {
                dice[i].transform.position = v3[i];
                dice[i].transform.rotation = qu[i];
                print("Des");
            }
        }
    }
}
