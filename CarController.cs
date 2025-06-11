using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider front_wheel_L1;
    public WheelCollider front_wheel_L2;
    public WheelCollider front_wheel_R1;
    public WheelCollider front_wheel_R2;
    public WheelCollider back_wheel_L1;
    public WheelCollider back_wheel_L2;
    public WheelCollider back_wheel_R1;
    public WheelCollider back_wheel_R2;

    public Transform front_wheel_L1_trans;
    public Transform front_wheel_L2_trans;
    public Transform front_wheel_R1_trans;
    public Transform front_wheel_R2_trans;
    public Transform back_wheel_L1_trans;
    public Transform back_wheel_L2_trans;
    public Transform back_wheel_R1_trans;
    public Transform back_wheel_R2_trans;

    public Vector3 eulertest;
    float maxFwdSpeed = -3000f;
    float maxBwdSpeed = 1000f;
    float gravity = 9.8f;
    private bool braked = false;
    private float maxBrakeTorque = 500;
    private Rigidbody rb;
    public Transform centreofmass;
    private float maxTorque = 1000;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centreofmass.localPosition;
    }

    void FixedUpdate()
    {
        if (!braked)
        {
            front_wheel_L1.brakeTorque = 0;
            front_wheel_L2.brakeTorque = 0;
            front_wheel_R1.brakeTorque = 0;
            front_wheel_R2.brakeTorque = 0;
            back_wheel_L1.brakeTorque = 0;
            back_wheel_L2.brakeTorque = 0;
            back_wheel_R1.brakeTorque = 0;
            back_wheel_R2.brakeTorque = 0;
        }

        float motor = maxTorque * Input.GetAxis("Vertical");

        back_wheel_L1.motorTorque = motor;
        back_wheel_L2.motorTorque = motor;
        back_wheel_R1.motorTorque = motor;
        back_wheel_R2.motorTorque = motor;

        float steer = 30 * Input.GetAxis("Horizontal");
        front_wheel_L1.steerAngle = steer;
        front_wheel_L2.steerAngle = steer;
        front_wheel_R1.steerAngle = steer;
        front_wheel_R2.steerAngle = steer;
    }

    void Update()
    {
        HandBrake();

        RotateWheel(front_wheel_L1, front_wheel_L1_trans);
        RotateWheel(front_wheel_L2, front_wheel_L2_trans);
        RotateWheel(front_wheel_R1, front_wheel_R1_trans);
        RotateWheel(front_wheel_R2, front_wheel_R2_trans);
        RotateWheel(back_wheel_L1, back_wheel_L1_trans);
        RotateWheel(back_wheel_L2, back_wheel_L2_trans);
        RotateWheel(back_wheel_R1, back_wheel_R1_trans);
        RotateWheel(back_wheel_R2, back_wheel_R2_trans);

        AdjustSteeringVisual(front_wheel_L1, front_wheel_L1_trans);
        AdjustSteeringVisual(front_wheel_R1, front_wheel_R1_trans);

        eulertest = front_wheel_L1_trans.localEulerAngles;
    }

    void RotateWheel(WheelCollider col, Transform trans)
    {
        trans.Rotate(col.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }

    void AdjustSteeringVisual(WheelCollider col, Transform trans)
    {
        Vector3 localEuler = trans.localEulerAngles;
        localEuler.y = col.steerAngle - trans.localEulerAngles.z;
        trans.localEulerAngles = localEuler;
    }

    void HandBrake()
    {
        if (Input.GetButton("Jump"))
            braked = true;
        else
            braked = false;

        if (braked)
        {
            back_wheel_L1.brakeTorque = maxBrakeTorque * 20;
            back_wheel_L2.brakeTorque = maxBrakeTorque * 20;
            back_wheel_R1.brakeTorque = maxBrakeTorque * 20;
            back_wheel_R2.brakeTorque = maxBrakeTorque * 20;

            back_wheel_L1.motorTorque = 0;
            back_wheel_L2.motorTorque = 0;
            back_wheel_R1.motorTorque = 0;
            back_wheel_R2.motorTorque = 0;
        }
    }
}
