using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shotPoint;
    [SerializeField] float timeBetweenShot;
    float shotTime;

    void Update()
    {
        //hàm ScreenToWorldPoint sẽ trả về Vector2 (hoặc 3), tọa độ của tham số truyền vào (ở đây là mousePosition)
        //theo hệ tọa độ của Screen, hiểu như này, Input.mousePosition sẽ trả về tọa độ chuột theo pixel, thường sẽ
        //khó đọc, hàm ScreenToWorldPoint biến đổi nó thành dạng kiểu như (-1, 3.5, 0), thử thì biết

        //còn tham số truyền vào, tại sao lại trừ transform.position, trừ để tìm ra hướng từ vị trí chuột đến player
        //của mình, từ đó weapon sẽ hướng theo vị trí chuột (toán lớp 10, điểm A(xa, ya), điểm B(xb, yb), vector AB = (xb-xa, yb-ya)
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition - transform.position);  // (1)

        //Atan2 trả về góc giữa trục x và vector có tọa độ (x, y) => (truyền vào là y, x)
        //Rad2Deg (Radian to Degree) chuyển đổi radian sang độ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   //(2)

        //Quaternion này sẽ tạo một rotation, xoay một thể nào đó xung quanh theo một góc (angle - 90) xung quanh một
        //trục (Vector3.forward: là cách viết tắt của (0,0,1), nghĩa là xoay theo một góc nào đó quanh trục z)
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); //(3)

        //Từ (1), (2), (3), tìm được góc và xoay weapon theo vị trí chuột.
        //(1): tìm tọa độ vector tạo giữa weapon và chuột
        //(2): tìm góc của vector đó khi hợp với trục x
        //(3): xoay đầu của weapon theo góc đó
        transform.rotation = rotation;

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            //Time.time là thời gian bắt đầu mỗi frame, shotTime = Time.time + timeBetweenShot vì
            //VD: Time.time = 10, timeBetweenShot = 5, thì shotTime = 15, để bắn được thì phải chờ thêm 5 giây nữa, Time.time = 15, OK hiểu
            if (Time.time >= shotTime)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                shotTime = Time.time + timeBetweenShot;
            }
        }
    }
}
