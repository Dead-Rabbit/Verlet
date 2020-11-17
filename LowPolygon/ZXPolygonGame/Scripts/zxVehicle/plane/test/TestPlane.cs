using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace zxVehicle.plane
{
    public class TestPlane : Flight
    {
        // 飞机小于极速的1/5时，飞机下降
        private Boolean IsFBB = false;
        private Boolean IsLRB = false;
        private Single downSpeed;
        private Boolean IsRun = false;

        public override void MoveFB(float speed)
        {
            IsRun = true;
            CurrentSpeed += speed*aircaft.Acc*Time.deltaTime;
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, aircaft.MaxSpeed);
        }
        
        public override void MoveLR(float speed)
        {
            // 左右移动
            if ((IsSing) || IsOnGround) return;
            // IsLRB = false;
            Vector3 vector = body.right;
            vector.y = 0;

            Move(speed * vector * aircaft.MoveLRSpeed * Time.deltaTime * CurrentSpeed/aircaft.MoveFBSpeed);
            
            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -aircaft.AxisFB * speed), aircaft.RoteLRSpeed * Time.deltaTime*3);
        }
        
        public override void Operational()
        {
            // 操作
            Altigraph();
            
            if (CurrentSpeed < aircaft.OffSpeed)
            {
                // 落下
                if (!IsOnGround)
                {
                    Move(-Vector3.up * Time.deltaTime * 10 * (1 - CurrentSpeed / (aircaft.OffSpeed)));
                    downSpeed = Mathf.Lerp(downSpeed, 0.1f, Time.deltaTime);
                    // print("downSpeed" + downSpeed);
                    RoteUD(downSpeed);
                }
                if (!rigidbody) rigidbody = GetComponent<Rigidbody>();
                rigidbody.useGravity = IsOnGround;
            }
            else {
                downSpeed = 0;
            }
            Balance();
            if (!IsRun) {
                if (CurrentSpeed > aircaft.MoveFBSpeed) CurrentSpeed = Mathf.Lerp(CurrentSpeed, aircaft.MoveFBSpeed,Time.deltaTime);

                else if (CurrentSpeed > aircaft.OffSpeed && !IsOnGround) CurrentSpeed =Random.Range(aircaft.OffSpeed,aircaft.MoveFBSpeed);
                else if (IsOnGround && CurrentSpeed < aircaft.OffSpeed) {
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed,0,Time.deltaTime);

                }
            }
            Move(body.forward * CurrentSpeed * Time.deltaTime);
            IsRun = false;
        }
        public override void RoteLR(float speed)
        {
            // 左右旋转
            if ((IsSing) || IsOnGround) return;
            IsLRB = false;
            Rote(speed * Vector3.up * aircaft.RoteLRSpeed * Time.deltaTime * CurrentSpeed / aircaft.MoveFBSpeed);
            
            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y,-aircaft.AxisLR * speed), aircaft.RoteLRSpeed * Time.deltaTime);

        }

        
        public override void RoteUD(float speed)
        {
            // 上下旋转
            // 速度和角度
            if ((IsSing) || IsOnGround && CurrentSpeed < aircaft.MoveFBSpeed / 3.6f) return;
            if (CurrentSpeed < aircaft.MoveFBSpeed / 3.6f && speed<0) return;
            IsFBB = false;
            Balance(Quaternion.Euler(aircaft.AxisFB * speed, body.eulerAngles.y, body.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime * CurrentSpeed / aircaft.MoveFBSpeed);
            // print("RoteUD" + speed);
        }
        public override void Balance()
        {
            if (IsSing) return;
            if (IsLRB)
            {
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime/1.2f );
            }
            if (IsFBB)
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime /1.3f);
            }
            IsLRB = true;
            IsFBB = true;
        }
        private float lastSTime;
        
        public override void StuntLR(float axis) {
            if ((IsSing) || IsOnGround && CurrentSpeed < aircaft.MoveFBSpeed / 3.6f) return;

            if (!IsSing) {
                IsSing = true;
                StartCoroutine(SLR(axis));
            }
        }

        IEnumerator SLR(float speed) {
            // 这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            // 直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = body.right * (speed);
            aim.y = 0;
            while(Vector3.Dot(aim.normalized,body.forward.normalized)<0.99f){
                Rote(speed * Vector3.up * aircaft.RoteLRSpeed * Time.deltaTime);
                
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -85 * (speed )), aircaft.RoteLRSpeed * Time.deltaTime*3.8f);
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime *1.8f);
                yield return new WaitForFixedUpdate();
            }
            while ((body.eulerAngles.z > 15) && (body.eulerAngles.z < 180) || (body.eulerAngles.z < 345) && (body.eulerAngles.z > 270))
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime);
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime * 3);
                yield return new WaitForFixedUpdate();
            }
            IsSing = false;
        }
        
        public override void StuntUD(float axis)
        {
            if ((IsSing) || IsOnGround && CurrentSpeed < aircaft.MoveFBSpeed / 3.6f) return;

            if (!IsSing)
            {
                IsSing = true;
                StartCoroutine(SUD(axis));

            }
        }
        
        IEnumerator SUD(float speed)
        {
            // 这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            // 直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = -body.forward ;
            aim.y = 0;
            while (Vector3.Dot(aim.normalized, body.forward.normalized) < 0.8f)
            {
                Vector3 v = body.right;
                v.y= 0;
                Rote(body.right * Time.deltaTime * -90 * speed);
                Move(-Vector3.up * speed * Time.deltaTime * 10 * (CurrentSpeed / (aircaft.OffSpeed)));
                // body.Rotate(Vector3.right * Time.deltaTime * -90,Space.Self);
                // Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime*5);
                yield return new WaitForFixedUpdate();
            }
            while ((body.eulerAngles.z > 15) && (body.eulerAngles.z < 180) || (body.eulerAngles.z < 345) && (body.eulerAngles.z >270))
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime );
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime*3);
                yield return new WaitForFixedUpdate();
            }
            IsSing = false;
        }
    }
}