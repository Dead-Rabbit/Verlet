using UnityEngine;
public interface Machine  {
    void Operational();
    void Move(Vector3 vector);
    void Rote(Vector3 vector);
    void MoveFB(float speed);//前进后退
    void RoteLR(float speed);//左右旋转
}