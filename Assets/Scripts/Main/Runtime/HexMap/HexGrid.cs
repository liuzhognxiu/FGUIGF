using System;
using System.Linq.Expressions;
using GameFramework;
using MetaArea;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MetaArea
{
    public class HexGrid : MonoBehaviour
    {
        public int width;
        public int height;
        public Color defaultColor = Color.white;
        public HexCell cellPrefab;
        HexCell[] cells;
        public HexMesh hexMesh;
        public RotateAround CameraGO;

        public bool IsOnClick = true;

        void Awake()
        {

            cells = new HexCell[height * width];

            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        void Start()
        {
            hexMesh.Triangulate(cells);
            CameraGO.target = cells[5].transform;
            CameraGO.MoveToTarget();
        }

        public async void ClickCell(Vector3 position)
        {
            if (!IsOnClick)
            {
                return;
            }

            position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
            HexCell cell = cells[index];
            var result = await GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("Welcome"),
                Message = GameEntry.Localization.GetString("GotoCell"),
            });
            switch (result)
            {
                case PopResult.Confirm:
                    GameEntry.Event.Fire(this, GoToLandEventArgs.Create(cell));
                    break;
                case PopResult.Cancel:
                    break;
                case PopResult.Other:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public HexCell GetCellByPosition(Vector3 position)
        {
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);
            int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
            HexCell cell = cells[index];
            return cell;
        }

        void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
            position.y = 0f;
            position.z = z * (HexMetrics.outerRadius * 1.5f);

            HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
            Transform cellTransform = cell.transform;
            cellTransform.SetParent(transform, false);
            cellTransform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
            cell.color = Color.blue;

            if (x > 0)
            {
                cell.SetNeighbor(HexDirection.W, cells[i - 1]);
            }

            if (z > 0)
            {
                if ((z & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                    if (x > 0)
                    {
                        cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                    if (x < width - 1)
                    {
                        cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                    }
                }
            }
        }
    }
}