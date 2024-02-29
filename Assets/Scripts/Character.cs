using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/// <summary>
/// IDLE : 대기상태 , DEAD : 죽어있는 상태
/// </summary>
public enum Character_State
{
    IDLE,DEAD
}

//문제) 캐릭터의 함수에 적절하게 enum을 넣어보세요.
//ex) 체력 회복을 진행하는데 상태가 DEAD일 경우에는 회복할 필요가 없다.


public class Character : MonoBehaviour
{

    public BigInteger hp;
    public BigInteger max_hp;
    public BigInteger damage;
    public float speed; 
    public Character_State State;
    [Header("공격 시 사용할 오브젝트")]
    public GameObject AttackPrefab;
    [Header("효과음 사운드")]
    public AudioClip SFX_attack; // 오디오 클립 (오디오 소스에서 틀 음악)
    AudioSource audioSource; // 오디오 소스 (오디오를 틀기 위해 필요함.)


    /// <summary>
    /// 캐릭터의 정보를 초기화하는 기능
    /// </summary>
    public void Init(BigInteger hp_Value, BigInteger damage_Value)
    {
        max_hp = hp_Value;
        hp = max_hp;
        damage = damage_Value;
        speed = 1.0f;
        State = Character_State.IDLE; // 상태를 대기 상태로 설정

        audioSource = GetComponent<AudioSource>();
        // 오디오 소스 연결
    }

    /// <summary>
    /// point 수치만큼 체력을 증가시키는 기능, 체력이 최대치보다 커지면
    /// 최대 수치로 설정합니다
    /// </summary>
    /// <param name="point">증가시킬 수치</param>
    public void Heal(BigInteger point)
    {
        if (State == Character_State.DEAD) return;
        hp += point;
        if (hp > max_hp)
            hp = max_hp;
    }

    // 문제) 다음 설명에 맞는 함수를 설계하세요/
    // point 수치만큼 체력을 감수하는 함수 GetDamage
    // 체력이 0 이하가 되면 Dead 함수를 호출합니다.
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
        // 디버그를 통해 테스트
        // (수정) 게임 매니저를 통해 텍스트로 전달

        //name은 게임오브젝트의 변수로 해당 개체의 이름을 표현합니다.
        //Debug.Log($"{target.name}은 {damage}를 받았습니다");
        //Debug.Log($"{target.name} HP : {target.hp} / {target.max_hp} {target.State}");

        GameManager.Instance.Set_Text(damage.ToString(), target.transform.position);
        // 1. 데미지(숫자) -> 문장
        // 2. 캐릭터 타겟의 위치를 전달합니다.

        // 이펙트 생성
        GameObject effect = Instantiate(AttackPrefab, target.transform.position, UnityEngine.Quaternion.identity);      

        // 1초 뒤 이펙트 파괴
        Destroy(effect, 1.0f);

        // 소리 플레이
        audioSource.clip = SFX_attack;
        audioSource.Play();
    }

    public virtual void Dead()
    {
        State = Character_State.DEAD;
        Debug.Log("죽었습니다.");
    }


    /*// Biginteger는 int보다 큰 숫자를 처리할 수 있는 구조체(struct)입니다.
     private BigInteger hP;
     private BigInteger mAX_HP;
     private BigInteger damage;
     private float speed;

     public BigInteger HP { get => hP; set => hP = value; }
     public BigInteger MAX_HP { get => mAX_HP; set => mAX_HP = value; }
     public BigInteger Damage { get => damage; set => damage = value; }
     public float Speed { get => speed; set => speed = value; }*/


}
