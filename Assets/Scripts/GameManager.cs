using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ���� Ŭ����(���� �Ŵ��� Ŭ���� ���ο����� �����ϴ� �뵵�� ����մϴ�.)
    // �����Ϸ��� ���� �Ŵ����� ��ó�� �մϴ�.

    public class Player_Data
    {
        public BigInteger gold = 0;
        public BigInteger hp_per_level = 1;
        public BigInteger damage_per_level = 1;

        public void Earn(BigInteger point, UnityEngine.Vector3 pos)
        {
            gold += point;
            Instance.text_gold.text = "Gold :" + gold;

            // 1. ����(����) -> ����
            // 2. ��ġ �����ؼ� Set_Text�� �ѱ�� ���� Earn �Լ��� Vector3�� �߰��մϴ�.
            Instance.Set_Text(point.ToString(), pos);
        }

        /*public void Get_hp_per_level()
        {
            // ��尡 ���� �� ü�� ��ġ = 20���� ũ�� ������ ���
            if(gold >= hp_per_level * 20)
            {
                // �ش� ��ġ��ŭ�� ����� �����մϴ�
                gold -= hp_per_level * 20;
                // ���� 1 ���
                hp_per_level++;
                // ��ġ ���
                Instance.text_level_hp.text = "Level HP : " + hp_per_level;
            }
            else
            {
                Instance.text_level_hp.text = $"�� ���� {hp_per_level * 20}�ʿ�";
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
                Instance.text_level_damage.text = $"�� ���� {damage_per_level * 30}�ʿ�";

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
    #region �̱���
    public static GameManager Instance;
    // Enemy�� Player ��� �Ŵ����� �������� �ʰ�
    // �ٷ� ����� �� �ְ� �ϱ� ���� static�� ó���մϴ�.

    // �̱���(Singleton)
    // �ش� ��ü�� ���α׷� ��ü���� ������ �� �ְ�, �ϳ��� �����ؾ� �ϴ� ��쿡 ���� �����Դϴ�.
    // 1. �ڱ� �ڽ��� ���·� static ������ ������ݴϴ�.
    // 2. ���α׷� ���� ���� ������ �ڱ� �ڽ����� ǥ���մϴ�.
    #endregion
    public Player_Data player_data;

    public Text text_gold;
    public Text text_level_hp;
    public Text text_level_damage;

    public Text text_damage;
    public List<Text> Text_List; // �ؽ�Ʈ�� ��Ƽ� �����ϴ� �ؽ�Ʈ ����Ʈ
    // List<T>�� �����͸� ������� ������ �� ������, ����Ʈ�� �����͸� �߰�, ����, �˻�
    // �� �� �ִ� ����� ���� �ڷᱸ���Դϴ�.
    // <T>�� ��ġ���� �ش� ����Ʈ�� � �����͸� ������ �������� �ۼ��մϴ�.


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player_data = new Player_Data(); // Ŭ���� ����
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
    /// HP ��ư�� ������ �� ȣ���� ���
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
    /// �ؽ�Ʈ�� �����ϴ� �Լ� 
    /// ���� �ڵ忡�� using System.Numerics;�� ����ϰ� �ֱ⿡
    /// �ش� �ڵ忡 �ִ� Vector3�� UnityEngine������ Vector3�� ȥ���� ����ų �� ����.
    /// ���� �̷� ��쿡�� UnityEngine�� ������ ����������.(BigInteger ���)
    /// </summary>
    /// <param name="text">�ؽ�Ʈ ����</param>
    /// <param name="pos">��ġ</param>
    public void Set_Text(string text, UnityEngine.Vector3 pos)
    {
        bool set = false;

        // �ؽ�Ʈ ����Ʈ ���ο� �ִ� �ؽ�Ʈ �ϳ��ϳ��� ���� �۾��� �����մϴ�
        foreach(Text t in Text_List)
        {
            // ���࿡ �ؽ�Ʈ�� Ȱ��ȭ�� ���°� �ƴ϶��
            // ���޹��� �ؽ�Ʈ�� �����ϰ� 
            // �ؽ�Ʈ�� ��ġ�� ī�޶��� ���޹��� ��ġ�� �����մϴ�.
            // �ؽ�Ʈ�� Ȱ��ȭ�մϴ�.
            if (!t.gameObject.activeSelf)
            {
                t.text = "-"+text;
                t.transform.position = Camera.main.WorldToScreenPoint(pos);
                t.transform.position += UnityEngine.Vector3.up * 10f;
                // WorldToScreenPoint(Vector3 position);�� ���� ������ ������
                // ��ũ�� ������ �������� �������ִ� �ڵ��Դϴ�.
                // 3D ���ӿ��� ī�޶� �̿��� ��ũ���� ��ü�� ǥ���� �� ����ϴ� �ڵ�
                //t.gameObject.SetActive(true);
                //(����) �ؽ�Ʈ�� ������ �ִ� MoveText ������Ʈ�κ��� Init �Լ� ȣ��
                t.GetComponent<MoveText>().Init();
                // set�� true�� ��ȯ(���� ����)
                set = true;
                break;
            } 
        }

        if (!set)
        {
            // �������� ��ġ�� �ؽ�Ʈ UI�� �����ϰ�
            // �θ� ������ ������ �� (���� Ŭ���� ����)
            // ���� �ؽ�Ʈ�� ���޹��� �ؽ�Ʈ�� ���� �����ϰ�
            // ���� �Ŵ��� ������ �ؽ�Ʈ ����Ʈ�� �ش� �ؽ�Ʈ�� �߰��մϴ�.

            Text t = Instantiate(text_damage, 
                Camera.main.WorldToScreenPoint(pos),
                UnityEngine.Quaternion.identity).GetComponent<Text>();

            t.transform.position += UnityEngine.Vector3.up * 10f;
            // ȸ���� 0 == Quaternion.identity
            // GetComponent<T>�� ������Ʈ�κ��� T ������ ������Ʈ�� ������ ����Դϴ�

            t.transform.SetParent(text_damage.transform.parent);
            // SetParent�� ������Ʈ�� �θ�� �����ϴ� ����Դϴ�.

            t.text = text;

            Text_List.Add(t);
            //����Ʈ�� Add �Լ��� ���� ����Ʈ�� �����͸� �߰��մϴ�.
        }
    }
}