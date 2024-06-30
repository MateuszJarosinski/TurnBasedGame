using Core.Mouse3D;
using GridSystem.Pathfinding;
using Interfaces;
using System.Collections.Generic;
using TurnSystem;
using UnityEngine;

namespace GridSystem
{
    [RequireComponent(typeof(GridUnit))]
    [RequireComponent(typeof(TurnObject))]
    public class GridUnit : MonoBehaviour, IAmGridUnit
    {
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }

            private set
            {
                transform.position = value;
            }
        }

        [field: SerializeField] public HexGridManager GridManager { get; private set; }
        [field: SerializeField] public Transform UnitHud { get; private set; }

        private PathfindingMover _pathfindingMover;
        private TurnObject _turnObject;

        private bool _listenToInputs;

        private void Awake()
        {
            _pathfindingMover = GetComponent<PathfindingMover>();
            _turnObject = GetComponent<TurnObject>();

            if (GridManager == null)
            {
                GridManager = FindObjectOfType<HexGridManager>();
            }
        }

        private void Update()
        {
            if (!_listenToInputs) return;

            if (Input.GetMouseButtonDown(0))
            {
                Move(transform.position, Mouse3D.GetMouseWorldPosition());
            }
        }

        public void MakeInteractable()
        {
            _listenToInputs = true;
            _turnObject.MakeTurn();
            UnitHud.gameObject.SetActive(true);
        }

        public void MakeNonInteractable()
        {
            _listenToInputs = false;
            UnitHud.gameObject.SetActive(false);
        }

        public void Move(Vector3 from, Vector3 to)
        {
            List<Vector3> path = GridManager.Pathfinding.FindPath(from, to);
            if (path != null && path.Count > 1)
            {
                _pathfindingMover.Move(path);
                GridManager.SelectHex();
            }
            else
            {
                Debug.Log("<color=red>Can't Move</color>");
            }
        }
    }
}
