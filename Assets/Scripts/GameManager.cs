using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 내부 클래스(게임 매니저 클래스 내부에서만 접근하는 용도로 사용합니다.)
    // 접근하려면 게임 매니저를 거처야 합니다.

    public class Player_Data
    {
        public BigInteger gold = 0;
        public BigInteger hp_per_level = 1;
        public BigInteger damage_per_level = 1;

        public void Earn(BigInteger point, UnityEngine.Vector3 pos)
        {
            gold += point;
            Instance.text_gold.text = "Gold :" + gold;

            // 1. 점수(숫자) -> 문장
            // 2. 위치 전달해서 Set_Text에 넘기기 위해 Earn 함수에 Vector3를 추가합니다.
            Instance.Set_Text(point.ToString(), pos);
        }

        /*public void Get_hp_per_level()
        {
            // 골드가 레벨 당 체력 수치 = 20보다 크게 존재할 경우
            if(gold >= hp_per_level * 20)
            {
                // 해당 수치만큼의 비용을 지불합니다
                gold -= hp_per_level * 20;
                // 레벨 1 상승
                hp_per_level++;
                // 수치 출력
                Instance.text_level_hp.text = "Level HP : " + hp_per_level;
            }
            else
            {
                Instance.text_level_hp.text = $"돈 부족 {hp_per_level * 20}필요";
            }

        }*/
        public void Get_damage_per_level()
        {
            if(gold >= damage_per_level * 30)
            {
                gold -= damage_per_level * 30;
                damage_per_level++;
                Instance.text_level_damage.text = "Level Damage : " + damage_per_level;
            }
            else
            {
                Instance.text_level_damage.text = $"돈 부족 {damage_per_level * 30}필요";

            }

        }
        public BigInteger SetData(BigInteger point)
        {
            gold -= hp_per_level * 20;
            hp_per_level++;
            point *= hp_per_level;
            Instance.text_level_hp.text = "Level HP : " + hp_per_level;
            Debug.Log(point);
            return point;
            
        }
    }
    #region 싱글톤
    public static GameManager Instance;
    // Enemy나 Player 등에서 매니저를 연결하지 않고
    // 바로 사용할 수 있게 하기 위해 static을 처리합니다.

    // 싱글톤(Singleton)
    // 해당 객체가 프로그램 전체에서 돌려쓸 수 있고, 하나만 존재해야 하는 경우에 쓰는 패턴입니다.
    // 1. 자기 자신의 형태로 static 변수를 만들어줍니다.
    // 2. 프로그램 시작 전에 변수가 자기 자신임을 표현합니다.
    #endregion
    public Player_Data player_data;

    public Text text_gold;
    public Text text_level_hp;
    public Text text_level_damage;

    public Text text_damage;
    public List<Text> Text_List; // 텍스트를 모아서 관리하는 텍스트 리스트
    // List<T>는 데이터를 순서대로 저장할 수 있으며, 리스트에 데이터를 추가, 삭제, 검색
    // 할 수 있는 기능을 가진 자료구조입니다.
    // <T>의 위치에는 해당 리스트가 어떤 데이터를 관리할 것인지를 작성합니다.


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player_data = new Player_Data(); // 클래스 생성
        Instance.text_gold.text = "Gold :" + player_data.gold;
        Instance.text_level_hp.text = "Level :" + player_data.hp_per_level;
        Instance.text_level_damage.text = "Damage : " + player_data.damage_per_level;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }


    /// <summary>
    /// HP 버튼을 눌렀을 때 호출할 기능
    /// </summary>
    /*public void OnHP_ButtonEnter()
    {
        player_data.Get_hp_per_level ();
        Instance.text_gold.text = "Gold :" + player_data.gold;

    }*/

    public void OnDamage_ButtonEnter()
    {
        player_data.Get_damage_per_level();
        Instance.text_gold.text = "Gold :" + player_data.gold;

    }

    /// <summary>
    /// 텍스트를 설정하는 함수 
    /// 현재 코드에서 using System.Numerics;를 사용하고 있기에
    /// 해당 코드에 있는 Vector3와 UnityEngine에서의 Vector3가 혼동을 일으킬 수 있음.
    /// 따라서 이런 경우에는 UnityEngine의 것임을 명시해줘야함.(BigInteger 사용)
    /// </summary>
    /// <param name="text">텍스트 내용</param>
    /// <param name="pos">위치</param>
    public void Set_Text(string text, UnityEngine.Vector3 pos)
    {
        bool set = false;

        // 텍스트 리스트 내부에 있는 텍스트 하나하나에 대해 작업을 진행합니다
        foreach(Text t in Text_List)
        {
            // 만약에 텍스트가 활성화된 상태가 아니라면
            // 전달받은 텍스트를 적용하고 
            // 텍스트의 위치는 카메라의 전달받은 위치로 설정합니다.
            // 텍스트를 활성화합니다.
            if (!t.gameObject.activeSelf)
            {
                t.text = "-"+text;
                t.transform.position = Camera.main.WorldToScreenPoint(pos);
                t.transform.position += UnityEngine.Vector3.up * 10f;
                // WorldToScreenPoint(Vector3 position);는 월드 공간의 지점을
                // 스크린 공강의 지점으로 변경해주는 코드입니다.
                // 3D 게임에서 카메라를 이용해 스크린에 객체를 표현할 때 사용하는 코드
                //t.gameObject.SetActive(true);
                //(수정) 텍스트가 가지고 있는 MoveText 컴포넌트로부터 Init 함수 호출
                t.GetComponent<MoveText>().Init();
                // set를 true로 전환(조건 변경)
                set = true;
                break;
            } 
        }

        if (!set)
        {
            // 지정받은 위치에 텍스트 UI를 생성하고
            // 부모 설정을 진행한 뒤 (상위 클래스 설정)
            // 만든 텍스트에 전달받은 텍스트의 값을 적용하고
            // 게임 매니저 내부의 텍스트 리스트에 해당 텍스트를 추가합니다.

            Text t = Instantiate(text_damage, 
                Camera.main.WorldToScreenPoint(pos),
                UnityEngine.Quaternion.identity).GetComponent<Text>();

            t.transform.position += UnityEngine.Vector3.up * 10f;
            // 회전값 0 == Quaternion.identity
            // GetComponent<T>는 오브젝트로부터 T 형태의 컴포넌트를 얻어오는 기능입니다

            t.transform.SetParent(text_damage.transform.parent);
            // SetParent는 오브젝트를 부모로 설정하는 기능입니다.

            t.text = text;

            Text_List.Add(t);
            //리스트의 Add 함수를 통해 리스트에 데이터를 추가합니다.
        }
    }
}