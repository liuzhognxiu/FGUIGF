using UnityEngine;
using UnityEngine.EventSystems;

namespace MetaArea
{
	public class HexMapEditor : MonoBehaviour
	{

		public HexGrid hexGrid;

		void Update()
		{
			if (
				Input.GetMouseButtonUp(0) &&
				!EventSystem.current.IsPointerOverGameObject()
			)
			{
				HandleInput();
			}
		}

		void HandleInput()
		{
			Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(inputRay, out hit))
			{
				hexGrid.ClickCell(hit.point);
			}
		}
	}
}