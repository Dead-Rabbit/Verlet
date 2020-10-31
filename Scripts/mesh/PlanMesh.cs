using System;
using System.Resources;
using UnityEditor;
using UnityEngine;

namespace mesh
{
    [RequireComponent(typeof(MeshFilter))]
    public class PlanMesh : MonoBehaviour
    {
        #region Mesh

        private MeshFilter _meshFilter;
        private Mesh _mesh;
        private Material _material;
        
        private readonly Vector3[] _vertices = new Vector3[4];
        private readonly int[] triangles = new int[6];
        
        #endregion

        #region 控制球

        public Boolean OpenBallControl = true;
        public GameObject controlBallPrefab;
        private GameObject[] _controlBalls;

        #endregion

        private void Awake()
        {
            #region 初始化

            OpenBallControl = controlBallPrefab != null;
            InitDebug();

            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _mesh.name = "自定义平面";

            #endregion
            
            // 创建平面
            CreateMinePlan();
        }

        private void Update()
        {
            CheckAndMoveControlBalls();
        }

        #region Mesh

        /// <summary>
        /// 创建自定义平面
        /// </summary>
        private void CreateMinePlan()
        {
            _vertices[0] = new Vector3(0, 0);
            _vertices[1] = new Vector3(1, 1);
            _vertices[2] = new Vector3(1, 0);
            _vertices[3] = new Vector3(2, 1);
            _mesh.vertices = _vertices;

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;
            triangles[4] = 1;
            triangles[5] = 3;
            _mesh.triangles = triangles;

            CreateControlBalls();
        }

        #endregion

        #region 控制球
        
        private void CreateControlBalls()
        {
            if (!controlBallPrefab)
                return;
            
            _controlBalls = new GameObject[_vertices.Length];
            for (Int32 i = 0; i < _controlBalls.Length; i++) {
                _controlBalls[i] = Instantiate(controlBallPrefab, transform);
                _controlBalls[i].transform.localPosition = _vertices[i];
            }
        }

        private void CheckAndMoveControlBalls()
        {
            Boolean ifChange = false;
            for (Int32 i = 0; i < _controlBalls.Length; i++) {
                if (_controlBalls[i].transform.localPosition != _vertices[i])
                {
                    ifChange = true;
                    _vertices[i] = _controlBalls[i].transform.localPosition;
                }
            }

            if (ifChange)
            {
                _mesh.vertices = _vertices;
            }
        }
        
        #endregion

        #region 测试

        private void InitDebug()
        {
            Selection.selectionChanged += OnSelectionChange;
        }
        
        void OnSelectionChange()
        {
            if (null == Selection.activeGameObject)
            {
                return;
            }
            
            Debug.Log(Selection.activeGameObject.transform.position);
        }

        #endregion
    }
}