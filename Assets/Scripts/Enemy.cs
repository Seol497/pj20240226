using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : Character
{
    public Text text_enemy_hp;
    public Character target;
    float cooltime_atk = 0.0f;
    public BigInteger Gold = 20;
    // Start is called before the first frame update
    void Start()
    {
        Init(10, 2);
    }

    // Update is called once per frame
    void Update()
    {

        // Time.deltaTime은 프레임 당 걸리는 시간을 의미합니다.

        if (cooltime_atk < speed)
            cooltime_atk += Time.deltaTime; 
        else
        {
            Attack(target, damage);
            cooltime_atk = 0.0f;
        }
            text_enemy_hp.text = "HP : " + hp;
    }

    public override void Dead()
    {
        base.Dead();
        GameManager.Instance.player_data.Earn(Gold, transform.position);
        // 골드에 대한 정보와 적의 위치를 전달해 Earn 함수 실행
        Spawn();
    }
    #region 스폰 기능
    public int LV_HP = 100;
    public int LV_Damage = 100;
    public int LV_Gold = 100;

    public void Spawn()
    {
        max_hp += max_hp * LV_HP / 200;
        damage += damage * LV_Damage / 150;
        Gold += Gold * LV_Gold / 100 ;
        LV_HP += 100;
        LV_Damage += 50;
        LV_Gold += 150;
        hp = max_hp;
        State = Character_State.IDLE;
        Debug.Log($"몬스터가 소환되었습니다 HP : {hp} Damage : {damage}");
    }
    #endregion
}
