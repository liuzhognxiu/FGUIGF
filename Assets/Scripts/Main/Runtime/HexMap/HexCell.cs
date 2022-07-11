using System;
using System.Globalization;
using UnityEngine;

namespace MetaArea
{
	public class HexCell : MonoBehaviour
	{
		public double CellIndex;

		public HexCoordinates coordinates;

		public Color color;

		[SerializeField] HexCell[] neighbors = new HexCell[6];

		public HexCell GetNeighbor(HexDirection direction)
		{
			return neighbors[(int) direction];
		}

		public void SetNeighbor(HexDirection direction, HexCell cell)
		{
			neighbors[(int) direction] = cell;
			cell.neighbors[(int) direction.Opposite()] = this;
		}

		public Transform BuildList;

		private void Awake()
		{
			BuildList = new GameObject(CellIndex.ToString(CultureInfo.InvariantCulture)).transform;
			BuildList.parent = transform;
		}
	}
}