using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiceScript : MonoBehaviour
{

    public int myNum = 0;
	public bool diceRoll = false;
    // Start is called before the first frame update
    void Start()
    {     		
	}

    // Update is called once per frame
    void Update()
    {
		DiceNum();

	}
    public void DiceNum()
    {
		
        myNum = 0;
        Ray ray = new Ray();
        RaycastHit hit;
		float hitDis =0f;
        int layer = 1 << LayerMask.NameToLayer("Plane");
        if(Physics.Raycast(this.transform.position, transform.up , out hit,Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 2;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}else
			{
				if(hitDis < Vector3.Distance( this.transform.position, hit.point ))
					myNum = 2;
			}
        }
        if (Physics.Raycast(this.transform.position, -transform.up, out hit, Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 5;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}
			else
			{
				if ( hitDis < Vector3.Distance( this.transform.position, hit.point ) )
					myNum = 5;
			}
		}
        if (Physics.Raycast(this.transform.position, transform.right, out hit, Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 3;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}
			else
			{
				if ( hitDis < Vector3.Distance( this.transform.position, hit.point ) )
					myNum = 3;
			}
		}
        if (Physics.Raycast(this.transform.position, -transform.right, out hit, Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 4;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}
			else
			{
				if ( hitDis < Vector3.Distance( this.transform.position, hit.point ) )
					myNum = 4;
			}
		}
        if (Physics.Raycast(this.transform.position, transform.forward, out hit, Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 6;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}
			else
			{
				if ( hitDis < Vector3.Distance( this.transform.position, hit.point ) )
					myNum = 6;
			}
		}
        if (Physics.Raycast(this.transform.position, -transform.forward, out hit, Mathf.Infinity, layer))
        {
			if ( hitDis == 0f )
			{
				myNum = 1;
				hitDis = Vector3.Distance( this.transform.position, hit.point );  //
			}
			else
			{
				if ( hitDis < Vector3.Distance( this.transform.position, hit.point ) )
					myNum = 1;
			}
		}
		//text.text = myNum.ToString();
		//text.transform.parent.gameObject.GetComponent<Outline>().enabled = true;
	}
	public void DiceDisable()
	{
		this.GetComponent<Rigidbody>().isKinematic = true;
		Debug.Log( "Dis" );
		switch ( myNum )
		{
			case 1:
				this.transform.localRotation = Quaternion.Euler( new Vector3( -90f, -90f, 0f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(-90f, -90f, 0f)), 0.1f);
				break;
			case 2:
				this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, 180f, 180f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, 180f, 180f)), 0.1f);
				break;
			case 3:
				this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, -90f, -90f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, -90f, -90f)), 0.1f);
				break;
			case 4:
				this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, -90f, 90f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, -90f, 90f)), 0.1f);
				break;
			case 5:
				this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, 90f, 0f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, 90f, 0f)), 0.1f);
				break;
			case 6:
				this.transform.localRotation = Quaternion.Euler( new Vector3( 90f, 180f, 0f ) );
				//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(90f, 180f, 0f)), 0.1f);
				break;
			default:
				break;
		}
	}
		public void DiceEnalbe()
	{
		this.GetComponent<Rigidbody>().isKinematic = false;
	}

  //  public void RotationCube()
  //  {
		//switch ( this.gameObject.name )
		//{
		//	case "d6_1":
		//		transform.position = new Vector3( 0.5f, -2.365f, 0.14f );
		//		break;
		//	case "d6_2":
		//		transform.position = new Vector3( 0.22f, -2.365f, 0.14f );
		//		break;
		//	case "d6_3":
		//		transform.position = new Vector3( -0.02f, -2.365f, 0.14f );
		//		break;
		//	case "d6_4":
		//		transform.position = new Vector3( -0.27f, -2.365f, 0.14f );
		//		break;
		//	case "d6_5":
		//		transform.position = new Vector3( -0.53f, -2.365f, 0.14f );
		//		break;
		//	default:
		//		break;
		//}
		//switch (myNum)
  //      {
  //          case 1:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( -90f, -90f, 0f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(-90f, -90f, 0f)), 0.1f);
  //              break;
  //          case 2:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, 180f, 180f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, 180f, 180f)), 0.1f);
		//		break;
  //          case 3:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, -90f, -90f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, -90f, -90f)), 0.1f);
		//		break;
  //          case 4:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, -90f, 90f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, -90f, 90f)), 0.1f);
		//		break;
  //          case 5:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( 0f, 90f, 0f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(0f, 90f, 0f)), 0.1f);
		//		break;
  //          case 6:
		//		this.transform.localRotation = Quaternion.Euler( new Vector3( 90f, 180f, 0f ) );
		//		//this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(new Vector3(90f, 180f, 0f)), 0.1f);
		//		break;
  //      }
  //  }

  //  public void CubeSelect()
  //  {
  //      if (boSelectCube)
  //      {
		//	//text.text = "R";
		//	text.transform.parent.gameObject.GetComponent<Outline>().enabled = true;
  //          boSelectCube = !boSelectCube;
  //      }
  //      else
  //      {
		//	//text.text = myNum.ToString();
		//	text.transform.parent.gameObject.GetComponent<Outline>().enabled = false;
		//	boSelectCube = !boSelectCube;
  //      }
  //  }
  //  public void OnDestroy()
  //  {
  //      if (boSelectCube)
  //      {
  //          switch (this.gameObject.name)
  //          {
  //              case "d6_1":
  //                  GameObject.FindWithTag("Cup")?.GetComponent<Force>().DesDice(0, this.transform.position,this.transform.rotation);
  //                  break;
  //              case "d6_2":
  //                  GameObject.FindWithTag("Cup")?.GetComponent<Force>().DesDice(1, this.transform.position, this.transform.rotation);
  //                  break;
  //              case "d6_3":
  //                  GameObject.FindWithTag("Cup")?.GetComponent<Force>().DesDice(2, this.transform.position, this.transform.rotation);
  //                  break;
  //              case "d6_4":
  //                  GameObject.FindWithTag("Cup")?.GetComponent<Force>().DesDice(3, this.transform.position, this.transform.rotation);
  //                  break;
  //              case "d6_5":
  //                  GameObject.FindWithTag("Cup")?.GetComponent<Force>().DesDice(4, this.transform.position, this.transform.rotation);
  //                  break;
  //              default:
  //                  break;
  //          }
  //      }
        
  //  }
}
