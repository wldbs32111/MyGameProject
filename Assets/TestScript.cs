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
		//������Ʈ == ��ũ��Ʈ
		//b == a.GetComponent<TestScript>()

		// ���⼭ �ڽ� �θ�� ����Ƽ���� �ڽ��� ��ġ
		//GameObject.FindWithTag( "A" ); // �±׷� ã��
		//transform.Find( "A1" );  // �ڽ� ã�� (���ڿ�)
		//transform.GetChild( 1 ); // �ڽ� ã�� (int)
		//transform.parent;        // �θ� ã��
		//transform.root;          // �ֻ��� ã��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
