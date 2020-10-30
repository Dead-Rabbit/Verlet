using System;
using UnityEngine;

namespace mesh
{
    [RequireComponent(typeof(MeshFilter))]
    public class PlanMesh : MonoBehaviour
    {
        private readonly Vector3[] _vertices = new Vector3[4];
        private readonly int[] _triangles = new int[6];

        #region Mesh

        private MeshFilter _meshFilter;
        private Mesh _mesh;
        private Material _material;

        #endregion
        
        private void Awake()
        {
            gameObject.AddComponent<MeshRenderer>();
            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshFilter.mesh = _mesh = new Mesh();
            _mesh.name = "自定义平面";
            
            CreateMinePlan();
        }

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

            _triangles[0] = 0;
            _triangles[1] = 1;
            _triangles[2] = 2;
            _triangles[3] = 2;
            _triangles[4] = 1;
            _triangles[5] = 3;
            _mesh.triangles = _triangles;
        }
        
        private void OnDrawGizmos () {
            Gizmos.color = Color.black;
            for (int i = 0; i < _vertices.Length; i++) {
                Gizmos.DrawSphere(_vertices[i], 0.1f);
            }
        }
    }
}