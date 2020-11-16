using UnityEngine;
using zxVehicle.plane;

[RequireComponent(typeof(Rigidbody))]
public abstract class Flight : MonoBehaviour, Machine
{//飞机
    [HideInInspector]
    public Transform body;
    [HideInInspector]
    public ObjectAircraft aircaft;
    public float Height;
    public bool IsOnGround = true;
    // private FlightWheels[] wheels;
    [HideInInspector]
    public Rigidbody rigidbody;
    public float CurrentSpeed;
    public bool IsSing = false;
    public string FlightName="F22";
    public void Start()
    {
        // wheels = GetComponentsInChildren<FlightWheels>();
        //camera = Camera.main.GetComponent<GameCameraControl>();
        body = transform;
        
        rigidbody = GetComponent<Rigidbody>();
        aircaft = new ObjectAircraft(FlightName);
        //MyDataBase.Insert(aircaft);
        // aircaft = MyDataBase.Select<ObjectAircraft>(aircaft);
    }
    
    public abstract void StuntLR(float axis);
    public abstract void StuntUD(float axis);
    public void Move(Vector3 vector)
    {
        body.Translate(vector, Space.World);
    }
    public void Rote(Vector3 vector)
    {
        body.Rotate(vector, Space.World);
    }
    public abstract void Operational();
    public abstract void MoveLR(float speed);//左右移动
    public abstract void RoteUD(float speed);//上下旋转
    public abstract void MoveFB(float speed);//速度控制
    public abstract void RoteLR(float speed);//左右旋转
    private Ray ray = new Ray();
    private RaycastHit hit;

    public void Altigraph()
    {
        ray.origin = transform.position+Vector3.up;
        
        ray.direction = -Vector3.up;
        IsOnGround = Physics.Raycast(ray, out hit,1<<0);
        //print(hit.collider.name);
        if (!IsOnGround) Height = body.position.y;
        else
            Height = hit.distance;
        if (IsOnGround)
            IsOnGround = Height < 1.1f;

        // if (wheels == null) wheels = GetComponentsInChildren<FlightWheels>();
        //
        // for (int i = 0; i < wheels.Length; i++)
        // {
        //     wheels[i].WheelControl(Height < 10f || IsOnGround);
        // }
    }
    public void Balance(Quaternion r, float speed)
    {
        body.rotation = Quaternion.RotateTowards(body.rotation,
               r, speed);
    }
    public abstract void Balance(); 
    
}
