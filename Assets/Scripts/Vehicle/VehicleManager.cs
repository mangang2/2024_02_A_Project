using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;          //Ż�� ��ü �迭 ���� �Ѵ�.


    public Car car;                     //�ڵ��� ����
    public Bicycle bicycle;             //������ ����

    float Timer;                        //�갣ȯ �ð� float ���� ����

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            vehicles[i].Move();
        }       

        Timer -= Time.deltaTime;

        if(Timer < 0)                  //1�ʸ��� ȣ�� �ǰ� �Ѵ�.
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                vehicles[i].Horn();
            }
            Timer = 1;
        }
    }
}
