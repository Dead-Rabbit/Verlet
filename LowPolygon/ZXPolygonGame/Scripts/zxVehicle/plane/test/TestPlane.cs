using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace zxVehicle.plane
{
    public class TestPlane : TestPlaneInterface
    {

        public Single CurrentSpeed;
        public Single downSpeed;

        public Boolean IsRun;
        public Boolean IsSing;
        public Boolean IsOnGround;

        public Boolean IsLRB;
        public Boolean IsFBB;

        private Rigidbody rigidbody;

        private void Awake()
        {
            if (!rigidbody) rigidbody = GetComponent<Rigidbody>();
        }

        public override void MoveFB(float speed)//速度控制
        {
            IsRun = true;//主动控制打开
            CurrentSpeed += speed * aircaft.Acc * Time.deltaTime;//加/减速
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, aircaft.MaxSpeed);//控制速度在最大值范围内
            
        }
        
        /// <summary>
        /// 水平移动飞机，飞机的侧飞
        /// </summary>
        /// <param name="speed"></param>
        public override void MoveLR(float speed)
        {
            // 左右移动
            //如果在地面或者飞机处于特技状态
            if ((IsSing) || IsOnGround) return;
            //IsLRB = false;
            Vector3 vector = transform.right;
            vector.y = 0;

            // 侧飞
            Move(speed * vector * aircaft.MoveLRSpeed * Time.deltaTime * CurrentSpeed / aircaft.MoveFBSpeed);
            
            // 旋转机身，实现侧飞的效果
            Balance(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -aircaft.AxisLR * speed), aircaft.RoteLRSpeed * Time.deltaTime);
            // print("MoveLR" + speed);
            
        }

        #region 待编写

        private void Move(Single speed)
        {
            
        }

        private void Altigraph()
        {
            
        }

        #endregion

        public override void Balance(Quaternion r, float speed)
        {
        }

        /// <summary>
        /// 飞机的状态控制
        /// </summary>
        public override void Operational()
        {
            //测量高度
            Altigraph();
            
            //小于起飞速度
            if (CurrentSpeed < aircaft.OffSpeed)
            {
                //落下
                if (!IsOnGround)//在空中
                {
                    Move(-Vector3.up * Time.deltaTime * 10 * (1 - CurrentSpeed / (aircaft.OffSpeed)));//失重下落
                    downSpeed = Mathf.Lerp(downSpeed, 0.1f, Time.deltaTime);
                    //print("downSpeed" + downSpeed);
                    RoteUD(downSpeed);//机身前倾实现下落效果
                }
                rigidbody.useGravity = IsOnGround;//如果飞机在地面，启用重力，否则不使用重力
            }
            else {
                downSpeed = 0;
            }
            
            //保持飞机的平衡
            Balance();
            
            //保持飞机以正常速度飞行
            if (!IsRun) {
                if (CurrentSpeed > aircaft.MoveFBSpeed) CurrentSpeed = Mathf.Lerp(CurrentSpeed, aircaft.MoveFBSpeed,Time.deltaTime);

                else if (CurrentSpeed > aircaft.OffSpeed && !IsOnGround) CurrentSpeed =Random.Range(aircaft.OffSpeed,aircaft.MoveFBSpeed);
                else if (IsOnGround && CurrentSpeed < aircaft.OffSpeed) {
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed,0,Time.deltaTime);

                }
            }
            
            Move(transform.forward * CurrentSpeed * Time.deltaTime);//调用飞行方法
        }
        
        /// <summary>
        /// 飞机的转向
        /// </summary>
        /// <param name="speed"></param>
        public override void RoteLR(float speed)
        {
            //左右旋转
            if ((IsSing) || IsOnGround) return;
            IsLRB = false;
            Rote(speed * Vector3.up * aircaft.RoteLRSpeed * Time.deltaTime * CurrentSpeed / aircaft.MoveFBSpeed);
            
            Balance(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,-aircaft.AxisLR * speed), aircaft.RoteLRSpeed * Time.deltaTime);
            //print("RoteLR" + speed);
        }

        /// <summary>
        /// 飞机的转向
        /// </summary>
        /// <param name="speed"></param>
        public override void RoteUD(float speed)
        {
            //上下旋转
            //速度和角度
            if ((IsSing) || IsOnGround && CurrentSpeed < aircaft.MoveFBSpeed / 3.6f) return;
            if (CurrentSpeed < aircaft.MoveFBSpeed / 3.6f && speed<0) return;
            IsFBB = false;
            Balance(Quaternion.Euler(aircaft.AxisFB * speed, transform.eulerAngles.y, transform.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime * CurrentSpeed / aircaft.MoveFBSpeed);
            //print("RoteUD" + speed);
        }
        
        /// <summary>
        /// 飞机的平衡方法，当无输入事件时，飞机自动平衡
        /// </summary>
        public override void Balance()
        {
            if (IsSing) return;
            
            //z轴平衡（左右）
            if (IsLRB)
            {
                Balance(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime/1.2f );
            }
            
            //x轴平衡（上下）
            if (IsFBB)
            {
                Balance(Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z), aircaft.RoteFBSpeed * Time.deltaTime /1.3f);
            }
            IsLRB = true;//自动平衡打开
            IsFBB = true;//自动平衡打开
        }
    }
}