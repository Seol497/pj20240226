using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/// <summary>
/// IDLE : ������ , DEAD : �׾��ִ� ����
/// </summary>
public enum Character_State
{
    IDLE,DEAD
}

//����) ĳ������ �Լ��� �����ϰ� enum�� �־����.
//ex) ü�� ȸ���� �����ϴµ� ���°� DEAD�� ��쿡�� ȸ���� �ʿ䰡 ����.


public class Character : MonoBehaviour
{

    public BigInteger hp;
    public BigInteger max_hp;
    public BigInteger damage;
    public float speed; 
    public Character_State State;
    [Header("���� �� ����� ������Ʈ")]
    public GameObject AttackPrefab;
    [Header("ȿ���� ����")]
    public AudioClip SFX_attack; // ����� Ŭ�� (����� �ҽ����� Ʋ ����)
    AudioSource audioSource; // ����� �ҽ� (������� Ʋ�� ���� �ʿ���.)


    /// <summary>
    /// ĳ������ ������ �ʱ�ȭ�ϴ� ���
    /// </summary>
    public void Init(BigInteger hp_Value, BigInteger damage_Value)
    {
        max_hp = hp_Value;
        hp = max_hp;
        damage = damage_Value;
        speed = 1.0f;
        State = Character_State.IDLE; // ���¸� ��� ���·� ����

        audioSource = GetComponent<AudioSource>();
        // ����� �ҽ� ����
    }

    /// <summary>
    /// point ��ġ��ŭ ü���� ������Ű�� ���, ü���� �ִ�ġ���� Ŀ����
    /// �ִ� ��ġ�� �����մϴ�
    /// </summary>
    /// <param name="point">������ų ��ġ</param>
    public void Heal(BigInteger point)
    {
        if (State == Character_State.DEAD) return;
        hp += point;
        if (hp > max_hp)
            hp = max_hp;
    }

    // ����) ���� ���� �´� �Լ��� �����ϼ���/
    // point ��ġ��ŭ ü���� �����ϴ� �Լ� GetDamage
    // ü���� 0 ���ϰ� �Ǹ� Dead �Լ��� ȣ���մϴ�.
    public void GetDamage(BigInteger point)
    {

        if (State == Character_State.DEAD) return;

        hp -= point;
        if (hp <= 0)
            Dead();
    }

    public void Attack(Character target, BigInteger damage)
    {
        if (State == Character_State.DEAD) return;


        target.GetDamage(damage);
        // ����׸� ���� �׽�Ʈ
        // (����) ���� �Ŵ����� ���� �ؽ�Ʈ�� ����

        //name�� ���ӿ�����Ʈ�� ������ �ش� ��ü�� �̸��� ǥ���մϴ�.
        //Debug.Log($"{target.name}�� {damage}�� �޾ҽ��ϴ�");
        //Debug.Log($"{target.name} HP : {target.hp} / {target.max_hp} {target.State}");

        GameManager.Instance.Set_Text(damage.ToString(), target.transform.position);
        // 1. ������(����) -> ����
        // 2. ĳ���� Ÿ���� ��ġ�� �����մϴ�.

        // ����Ʈ ����
        GameObject effect = Instantiate(AttackPrefab, target.transform.position, UnityEngine.Quaternion.identity);      

        // 1�� �� ����Ʈ �ı�
        Destroy(effect, 1.0f);

        // �Ҹ� �÷���
        audioSource.clip = SFX_attack;
        audioSource.Play();
    }

    public virtual void Dead()
    {
        State = Character_State.DEAD;
        Debug.Log("�׾����ϴ�.");
    }


    /*// Biginteger�� int���� ū ���ڸ� ó���� �� �ִ� ����ü(struct)�Դϴ�.
     private BigInteger hP;
     private BigInteger mAX_HP;
     private BigInteger damage;
     private float speed;

     public BigInteger HP { get => hP; set => hP = value; }
     public BigInteger MAX_HP { get => mAX_HP; set => mAX_HP = value; }
     public BigInteger Damage { get => damage; set => damage = value; }
     public float Speed { get => speed; set => speed = value; }*/


}
