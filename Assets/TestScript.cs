using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
	public GameObject a;
	public TestScript b;
	public Text c;

    // Start is called before the first frame update
    void Start()
    {
		//컴포넌트 == 스크립트
		//b == a.GetComponent<TestScript>()

		// 여기서 자식 부모는 유니티에서 자신의 위치
		//GameObject.FindWithTag( "A" ); // 태그로 찾기
		//transform.Find( "A1" );  // 자식 찾기 (문자열)
		//transform.GetChild( 1 ); // 자식 찾기 (int)
		//transform.parent;        // 부모 찾기
		//transform.root;          // 최상위 찾기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
