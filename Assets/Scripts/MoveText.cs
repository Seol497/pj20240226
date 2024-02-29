using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{
    float Move_Time = 0.0f;

    public void Init()
    {
        Move_Time = 0.0f;
        gameObject.SetActive(true); // 게임 오브젝트를 활성화합니다
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
            // 위치를 윗 방향 기준으로 0.5만큼 이동하겠습니다
        }
        else
        {
            gameObject.SetActive(false); // 비활성화
        }
    }
}
