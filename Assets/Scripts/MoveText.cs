using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    float Move_Time = 0.0f;

    public void Init()
    {
        Move_Time = 0.0f;
        gameObject.SetActive(true); // ���� ������Ʈ�� Ȱ��ȭ�մϴ�
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Move_Time < 1.0f)
        {
            Move_Time += Time.deltaTime;
            transform.position += Vector3.up * 0.5f;
            // ��ġ�� �� ���� �������� 0.5��ŭ �̵��ϰڽ��ϴ�
        }
        else
        {
            gameObject.SetActive(false); // ��Ȱ��ȭ
        }
    }
}
