using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
	public CupController cupController;

	private LayerMask layerDice;
    // Start is called before the first frame update
    void Start()
    {
		layerDice = LayerMask.GetMask( "Dice" );
    }

	// Update is called once per frame
	void Update()
	{
		if ( cupController.state == CupController.State.State02 && Input.GetMouseButtonDown( 0 ) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			if ( Physics.Raycast( ray, out hit, Mathf.Infinity, layerDice ) )
			{
				switch ( hit.transform.gameObject.name )
				{
					case "d6_1":
						cupController.ClickDice( 0 );
						break;
					case "d6_2":
						cupController.ClickDice( 1 );
						break;
					case "d6_3":
						cupController.ClickDice( 2 );
						break;
					case "d6_4":
						cupController.ClickDice( 3 );
						break;
					case "d6_5":
						cupController.ClickDice( 4 );
						break;
					default:
						break;
				}
			}
		}
	}
}
