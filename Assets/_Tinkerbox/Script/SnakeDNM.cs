using System.Collections.Generic;
using UnityEngine;

public class SnakeDNM : MonoBehaviour
{
    public Transform leader;                    // Lider objeyi (baş) temsil eder
    public float distance = 1.0f;              // Objeler arasındaki sabit mesafe
    public float followSpeed = 5.0f;           // Objelerin takip hızını kontrol eder

    public List<Transform> followers = new List<Transform>(); // Takip eden objelerin listesi

    private void Start()
    {
        // Başlangıçta tüm objeleri takipçi listesine ekle
      
    }

    private void LateUpdate()
    {
        if (followers.Count == 0) return;

        // Liderin pozisyonunu güncelle
        Vector3 leaderPosition = leader.position;

        // Liderin pozisyonunu takip eden objelerin pozisyonlarını ayarla
        for (int i = 0; i < followers.Count; i++)
        {
            Transform follower = followers[i];

            // Eğer bu takipçi en öndeki değilse, bir önceki takipçinin pozisyonunu al
            Vector3 targetPosition = (i == 0) ? leaderPosition : followers[i - 1].position;
            Vector3 direction = (targetPosition - follower.position).normalized;

            // Mesafeyi koruyarak pozisyonu güncelle
            
            follower.position = Vector3.MoveTowards(follower.position, targetPosition - direction * distance, Time.deltaTime * followSpeed);
            Debug.Log(AngleFromDirection(direction) + " " + AngleFromDirection(targetPosition-follower.position));
            
           /* Vector3 targetDir = targetPosition - follower.position;
            var targetTurnRate = Vector3.Angle(targetDir, follower.forward);
            float turnRate = 0;
            turnRate = Mathf.MoveTowardsAngle(turnRate, targetTurnRate, 90 * Time.fixedDeltaTime);
              //turnRate = Mathf.Clamp(turnRate, yawMinMax.x, yawMinMax.y); 
            transform.localEulerAngles = turnRate * Vector3.up;*/
            /*var a = direction;
            a.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(a,Vector3.up);
            follower.rotation = Quaternion.Slerp(follower.rotation, targetRotation, Time.deltaTime * 9999);*/
        }
        
     
    }
    public static float AngleFromDirection(Vector3 dir)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(dir.z, dir.x);
    }
}