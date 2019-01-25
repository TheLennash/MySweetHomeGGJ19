using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool DoFollow = true;

    public Transform Gyro;

    public GameObject Target;

    private GameObject Cam;


    private Vector3 DeltaPos;


    public float smoothing = 4f;

    private void Start()
    {
        Cam = Camera.main.gameObject;
        DeltaPos = Cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DoFollow)
            return;

        if (Target == null)
            return;


        var dist = Vector3.Distance(Gyro.position, Target.transform.position);
        Cam.transform.LookAt(Target.transform);
        Cam.transform.localPosition = Vector3.Lerp(DeltaPos, new Vector3(0, 8, dist + 10), Time.deltaTime * smoothing);
        DeltaPos = Cam.transform.localPosition;

        Gyro.LookAt(Target.transform);
        Quaternion Wanted = Quaternion.Euler(new Vector3(0, Gyro.eulerAngles.y, 0));
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Wanted, Time.deltaTime * smoothing);


    }



}
