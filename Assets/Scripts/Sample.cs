using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; // ���ӽ����̽� (Namespcae) : �̸� ��Ƶ� ����
//using�� ����ϸ� �ش� ��ũ��Ʈ�� �־��� ���� �����մϴ�.

// MonoBehaviour Ŭ���� ����Ƽ ���ο��� ���� ������Ʈ�� �����ϱ� ���� �ݵ��
// ������ �־���ϴ� ���� Ŭ����(base class) 

public class Sample : MonoBehaviour
{
    public Text datatext;
    public NonMonoBehaviour nonMonobehaviour;
    public int value;
    // SerializeField�� �ʵ� ���� ����ȭ�ϴ� �Ӽ��Դϴ�.
    [SerializeField]private float value2;

    [Tooltip("�׾��� �� ������ ����Ƽ �̺�Ʈ")]
    public UnityEvent onDead;

    [Header("��� ŉ�淮 ����")]
    [Range(0, 100)]
    public int gold_up;


    // ������Ƽ (Ŭ������ �Ӽ��� ǥ���ϴ� ����)
    // ������Ƽ = ��;�� ���·� �۾��ϸ� �� ���� �ٷ� �����ǰ�, ������Ƽ�� ������. ����ó�� ����� �� �ֽ��ϴ�
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
        Debug.Log("Sample ��ũ��Ʈ�� ���� �ϰڽ��ϴ�"); // ����Ƽ �����Ϳ� �ִ� �ܼ� â�� �޼�����  ����ݴϴ�.
        datatext.text = $"���̵� �ڵ� : {nonMonobehaviour.id}\n";
        datatext.text += nonMonobehaviour.description;

        // ���ڿ� ���� ���� StringBuilder
        // 1. stringBuilder�� �����մϴ�.
        // 2. ������ �ʵ带 �߰��մϴ�.
        // 3. ������ ������Ƽ�� �߰��մϴ�.
        // 4. ������ �ؽ�Ʈ�� ������ ���� ���ڿ��� ���·� �ٲ㼭 �־��ݴϴ�.

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"\nvalue : {value}\n"); // �ʵ�
        stringBuilder.Append($"value2 : {Value2}"); // ������Ƽ

        datatext.text += stringBuilder.ToString(); // ToString()�� ���ڿ��� �����ϴ� �Լ��Դϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("�����Ӹ��� ��� ȣ���մϴ�0");
        Debug.Log("�����Ӹ��� ��� ȣ���մϴ�1");

    }
}
