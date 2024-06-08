using Core.Mouse3D;
using GridSystem.Pathfinding;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    [RequireComponent(typeof(GridUnit))]
    public class GridUnit : MonoBehaviour, IAmGridUnit
    {
        [field: SerializeField] public HexGridManager GridManager { get; private set; }

        private PathfindingMover _pathfindingMover;

        private void Awake()
        {
            _pathfindingMover = GetComponent<PathfindingMover>();

            if (GridManager == null)
            {
                GridManager = FindObjectOfType<HexGridManager>();
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Move(transform.position, Mouse3D.GetMouseWorldPosition());
            }
        }

        public void MakeInteractable()
        {
            _pathfindingMover.CanMove(true);
        }

        public void MakeNonInteractable()
        {
            _pathfindingMover.CanMove(false);
        }

        public void Move(Vector3 from, Vector3 to)
        {
            List<Vector3> path = GridManager.Pathfinding.FindPath(from, to);

            if (path.Count > 1)
            {
                _pathfindingMover.Move(path);
                GridManager.DeselectHex();
                MakeNonInteractable();
            }
            else
            {
                Debug.Log("<color=red>Can't Move</color>");
            }
        }
    }
}
