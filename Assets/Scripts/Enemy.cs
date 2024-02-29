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

        // Time.deltaTime�� ������ �� �ɸ��� �ð��� �ǹ��մϴ�.

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
        // ��忡 ���� ������ ���� ��ġ�� ������ Earn �Լ� ����
        Spawn();
    }
    #region ���� ���
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
        Debug.Log($"���Ͱ� ��ȯ�Ǿ����ϴ� HP : {hp} Damage : {damage}");
    }
    #endregion
}
