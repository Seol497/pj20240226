using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class Player : Character
{
    public Text text_player_hp;
    public Character target;
    float cooltime_atk = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Init(50, Random.Range(8, 20));
        // Random.Range(최소,최대) : 최소 ~ 최대 사이의 랜덤 값
    }

    // Update is called once per frame
    void Update()
    {
        if (State == Character_State.DEAD)
        {
            return;
        }
        // Time.deltaTime은 프레임 당 걸리는 시간을 의미합니다.

        if (cooltime_atk < speed)
            cooltime_atk += Time.deltaTime;
        else
        {
            Attack(target, damage);
            cooltime_atk = 0.0f;
        }
        text_player_hp.text = "HP : " + hp;
    }

    public override void Dead()
    {
        base.Dead();
        Spawn();
    }

    public void Spawn()
    {
        hp = max_hp;
        State = Character_State.IDLE;
        Debug.Log($"플레이어가 다시 살아났습니다 HP : {hp} Damage : {damage}");
    }

    public void OnHP_ButtonEnter()
    {
        if (Instance.player_data.gold >= Instance.player_data.hp_per_level * 20)
        {
            GameManager.Instance.player_data.SetData(max_hp);           
            hp = max_hp / 2;
            if (hp < max_hp)
                hp = max_hp;
            Debug.Log($"{Instance.player_data.hp_per_level}, {max_hp} ");
        }
        else
            Instance.text_level_hp.text = $"돈 부족 {Instance.player_data.hp_per_level * 20}필요";
    }
    public void OnDamage_ButtonEnter()
    {
        if (Instance.player_data.gold >= Instance.player_data.damage_per_level * 30)
        {
            damage = damage *= Instance.player_data.damage_per_level;

            Debug.Log($"{Instance.player_data.damage_per_level}, {damage}");
        }
    }
}
