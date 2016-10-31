using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

   public static CameraShake instance;

   private Vector3 originPosition;
   private Quaternion originRotation;
   public float shake_decay;
   public float shake_intensity;

   void Awake() {

       MakeInstance();
   }

   void MakeInstance() {
       if (instance == null) {
           instance = this;
       }
   }

   void Update (){
      if (shake_intensity > 0){
         transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
         transform.rotation = new Quaternion(
         originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
         shake_intensity -= shake_decay;
      }
   }

   public void Shake(float shake_decay, float shake_intensity)
   {
      originPosition = transform.position;
      originRotation = transform.rotation;
      this.shake_intensity = shake_intensity;
      this.shake_decay = shake_decay;
   }

}
