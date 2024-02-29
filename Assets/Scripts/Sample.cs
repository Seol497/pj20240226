using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // 네임스페이스 (Namespcae) : 이름 모아둔 공간
//using을 사용하면 해당 스크립트에 넣어준 값을 연결합니다.

// MonoBehaviour 클래스 유니티 내부에서 게임 오브젝트에 연결하기 위해 반드시
// 가지고 있어야하는 기초 클래스(base class) 

public class Sample : MonoBehaviour
{
    public Text datatext;
    public NonMonoBehaviour nonMonobehaviour;
    public int value;
    // SerializeField는 필드 값을 직렬화하는 속성입니다.
    [SerializeField]private float value2;

    [Tooltip("죽었을 때 실행할 유니티 이벤트")]
    public UnityEvent onDead;

    [Header("골드 흭득량 증가")]
    [Range(0, 100)]
    public int gold_up;


    // 프로퍼티 (클래스의 속성을 표현하는 도구)
    // 프로퍼티 = 값;의 형태로 작업하면 그 값이 바로 수정되고, 프로퍼티를 적으로. 변수처럼 사용할 수 있습니다
    public float Value2 { 
        get 
        { 
            return value2; 
        } 

        set
        {
            value2 = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sample 스크립트를 시작 하겠습니다"); // 유니티 에디터에 있는 콘솔 창에 메세지를  띄어줍니다.
        datatext.text = $"아이디 코드 : {nonMonobehaviour.id}\n";
        datatext.text += nonMonobehaviour.description;

        // 문자열 수정 도구 StringBuilder
        // 1. stringBuilder를 생성합니다.
        // 2. 빌더에 필드를 추가합니다.
        // 3. 빌더에 프로퍼티를 추가합니다.
        // 4. 데이터 텍스트에 빌더의 값을 문자열의 형태로 바꿔서 넣어줍니다.

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"\nvalue : {value}\n"); // 필드
        stringBuilder.Append($"value2 : {Value2}"); // 프로퍼티

        datatext.text += stringBuilder.ToString(); // ToString()은 문자열로 변경하는 함수입니다.
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("프레임마다 계속 호출합니다0");
        Debug.Log("프레임마다 계속 호출합니다1");

    }
}
