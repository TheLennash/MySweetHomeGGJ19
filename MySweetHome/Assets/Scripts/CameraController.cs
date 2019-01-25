using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool DoFollow = true;

    public Transform Gyro;

    public GameObject Target;


    public float smoothing = 4f;

    // Update is called once per frame
    void Update()
    {
        if (!DoFollow)
            return;

        if (Target == null)
            return;

        Gyro.LookAt(Target.transform);

        Quaternion Wanted = Quaternion.Euler(new Vector3(0,Gyro.eulerAngles.y, 0));

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Wanted, Time.deltaTime * smoothing);
    }



}
