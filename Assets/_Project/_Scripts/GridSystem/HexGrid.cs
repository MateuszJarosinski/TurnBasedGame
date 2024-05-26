using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class HexGrid<T>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPos;
        private T[,] _gridArray;

        public HexGrid(int width, int height, float cellSize, Vector3 originPos, Func<T> createGridObject)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPos = originPos;

            _gridArray = new T[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < _gridArray.GetLength(1); z++)
                {
                    _gridArray[x, z] = createGridObject();
                }
            }
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }

        public float GetCellSize()
        {
            return _cellSize;
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return
            new Vector3(x, 0, 0) * _cellSize +
                new Vector3(0, 0, z) * _cellSize * 0.75f +
                ((Mathf.Abs(z) % 2) == 1 ? .5f * _cellSize * new Vector3(1, 0, 0) : Vector3.zero) +
                _originPos;
        }

        public T GetGridObject(int x, int z)
        {
            if (x >= 0 && z >= 0 && x < _width && z < _height)
            {
                return _gridArray[x, z];
            }
            else
            {
                return default;
            }
        }

        public T GetGridObject(Vector3 worldPosition)
        {
            int x, z;
            GetXZ(worldPosition, out x, out z);
            return GetGridObject(x, z);
        }

        public void GetXZ(Vector3 worldPosition, out int x, out int z)
        {
            int roughX = Mathf.RoundToInt((worldPosition - _originPos).x / _cellSize);
            int roughZ = Mathf.RoundToInt((worldPosition - _originPos).z / _cellSize / 0.75f);

            Vector3Int roughXZ = new Vector3Int(roughX, 0, roughZ);

            bool oddRow = roughZ % 2 == 1;

            List<Vector3Int> neighbourXZList = new List<Vector3Int>
            {
                roughXZ + new Vector3Int(-1, 0, 0),
                roughXZ + new Vector3Int(+1, 0, 0),

                roughXZ + new Vector3Int(oddRow ? +1 : -1, 0, +1),
                roughXZ + new Vector3Int(+0, 0, +1),

                roughXZ + new Vector3Int(oddRow ? +1 : -1, 0, -1),
                roughXZ + new Vector3Int(+0, 0, -1),
            };

            Vector3Int closestXZ = roughXZ;

            foreach (Vector3Int neighbourXZ in neighbourXZList)
            {
                if (Vector3.Distance(worldPosition, GetWorldPosition(neighbourXZ.x, neighbourXZ.z)) <
                    Vector3.Distance(worldPosition, GetWorldPosition(closestXZ.x, closestXZ.z)))
                {
                    // Closer than closest
                    closestXZ = neighbourXZ;
                }

            }

            x = closestXZ.x;
            z = closestXZ.z;
        }
    }
}
