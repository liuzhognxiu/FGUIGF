using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnhancedUI.EnhancedScroller;
using PlasticGui.Configuration.CloudEdition.Welcome;
using TimingWheel.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MetaArea.Hotfix
{
	
	public class TeleportPointScrollCtl : IEnhancedScrollerDelegate
	{
		/// <summary>
		/// 展示类型
		/// </summary>
		public enum Mode
		{
			None,
			/// <summary>
			/// 自定义
			/// </summary>
			Custom,
			/// <summary>
			/// 预设
			/// </summary>
			Preset,
			/// <summary>
			/// 筛选
			/// </summary>
			Filter,
		}
		private List<TeleportPointData> m_CustomPoints;
		private List<TeleportPointData> m_PresetPoints;
		private List<TeleportPointData> m_FilterPoints;

		private Mode m_Mode = Mode.None;
		private Mode m_LastMode = Mode.None;
		public UITeleportPointItemLogic m_TeleportPointItemLogic;

		public void Filter(string filterStr)
		{
			m_FilterPoints.Clear();
			switch (m_LastMode)
			{
				case Mode.None:
				case Mode.Filter:
					break;
				case Mode.Custom:
				{
					foreach (TeleportPointData item in m_CustomPoints)
					{
						if (item.Name.Contains(filterStr))
						{
							m_FilterPoints.Add(item);
						}
					}
				}
					break;
				case Mode.Preset:
				{
					foreach (TeleportPointData item in m_PresetPoints)
					{
						if (item.Name.Contains(filterStr))
						{
							m_FilterPoints.Add(item);
						}
					}
				}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public TeleportPointScrollCtl()
		{
		}
		
		public void SetDataList(List<TeleportPointData> mCustomPoint, List<TeleportPointData> mPresetPoint)
		{
			m_CustomPoints = mCustomPoint;
			m_PresetPoints = mPresetPoint;
			m_FilterPoints = new List<TeleportPointData>();
		}

		public void SetMode(Mode mode)
		{
			if (m_Mode!= mode)
			{
				m_Mode = mode;
				m_LastMode = m_Mode;
			}
		}

		public int GetNumberOfCells(EnhancedScroller scroller)
		{
			switch (m_Mode)
			{
				case Mode.Custom:
					return m_CustomPoints.Count;
				case Mode.Preset:
					return m_PresetPoints.Count;
				case Mode.Filter:
					return m_FilterPoints.Count;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private TeleportPointData GetData(int index)
		{
			switch (m_Mode)
			{
				case Mode.Custom:
					return m_CustomPoints[index];
				case Mode.Preset:
					return m_PresetPoints[index];
				case Mode.Filter:
					return m_FilterPoints[index];
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
		{
			return 150;
		}

		public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
		{
			UITeleportPointItemLogic itemLogic =
				scroller.GetCellView(m_TeleportPointItemLogic) as UITeleportPointItemLogic;

			if (itemLogic == null)
			{
				throw new NullReferenceException("TeleportPointScroll item view is null.");
			}
			itemLogic.SetData(GetData(dataIndex));
			return itemLogic;
		}
	}

	public partial class UIUMXMainIslandFormLogic : UGuiFormLogic
	{
		private TeleportPointScrollCtl m_TeleportPointScrollCtl;

		private long m_LeftShowTime = 0;
		private ITimeTask m_LeftShowTimeTask;

		protected override void OnInit(object userdata)
		{
			base.OnInit(userdata);
			GetBindComponents(gameObject);
			m_TeleportPointScrollCtl = new TeleportPointScrollCtl();
			m_EnhancedScroller_ScrollView.Delegate = m_TeleportPointScrollCtl;

			m_RectTransform_LeftInfo.anchoredPosition = new Vector2(0, m_RectTransform_LeftInfo.anchoredPosition.y);
			m_Button_PutAway.onClick.AddListener(PutAway);
			
			m_TMP_InputField_Find.onValueChanged.AddListener(FindPoint);
			
			m_Toggle_Custom.onValueChanged.AddListener(CustomToggleValueChanged);
			m_Toggle_Preset.onValueChanged.AddListener(PresetToggleValueChanged);
			
			MenuEventTrigger();
			CommunityTrigger();
			MyIsLandTrigger();
		}

		private void PresetToggleValueChanged(bool isOn)
		{
			if (isOn)
			{
				SetTeleportPointScrollMode(TeleportPointScrollCtl.Mode.Preset);
			}
		}

		private void CustomToggleValueChanged(bool isOn)
		{
			if (isOn)
			{
				SetTeleportPointScrollMode(TeleportPointScrollCtl.Mode.Custom);
			}
		}

		private void FindPoint(string arg0)
		{
			m_TeleportPointScrollCtl.SetMode(TeleportPointScrollCtl.Mode.Filter);
			m_TeleportPointScrollCtl.Filter(m_TMP_InputField_Find.text);
			m_EnhancedScroller_ScrollView.ReloadData();
		}

		private void PutAway()
		{
			m_LeftShowTimeTask.Cancel();
			m_RectTransform_LeftInfo.DOMoveX(m_RectTransform_LeftInfo.sizeDelta.x, 1f);
		}

		protected override void OnOpen(object userData)
		{
			base.OnOpen(userData);

			m_LeftShowTimeTask = GameEntry.TimingWheel.AddTask(TimeSpan.FromMilliseconds(m_LeftShowTime), _ =>
			{
				if (_)
				{
					m_RectTransform_LeftInfo.DOMoveX(m_RectTransform_LeftInfo.sizeDelta.x, 1f);
				}
			});

			//todo 真实数据 这里只是模拟
			m_TMP_Text_Message.text = "默认消息";
			m_TeleportPointScrollCtl.SetDataList(new List<TeleportPointData>()
			{
				
			},new List<TeleportPointData>()
			{
				
			});
			SetTeleportPointScrollMode(TeleportPointScrollCtl.Mode.Preset);
	
		}

		private void MenuEventTrigger()
		{
			EventTrigger.Entry onPointEnter = new EventTrigger.Entry();
			onPointEnter.eventID = EventTriggerType.PointerEnter;
			onPointEnter.callback.AddListener(eventData =>
			{
				m_Button_Community.gameObject.SetActive(true);
				m_Button_MyIsland.gameObject.SetActive(true);
			});
			EventTrigger.Entry onPointExit = new EventTrigger.Entry();
			onPointExit.eventID = EventTriggerType.PointerExit;
			onPointExit.callback.AddListener(eventData =>
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
				{
					position = Input.mousePosition
				};
				List<RaycastResult> result = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointerEventData, result);
				if (!result.Exists(_=>_.gameObject == m_Button_Community.gameObject || _.gameObject == m_Button_MyIsland.gameObject))
				{
					m_Button_Community.gameObject.SetActive(false);
					m_Button_MyIsland.gameObject.SetActive(false);
				}
			});
			
			m_EventTrigger_Menu.triggers.Add(onPointEnter);
			m_EventTrigger_Menu.triggers.Add(onPointExit);
		}

		private void CommunityTrigger()
		{
			EventTrigger.Entry onPointExit = new EventTrigger.Entry();
			onPointExit.eventID = EventTriggerType.PointerExit;
			onPointExit.callback.AddListener(eventData =>
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
				{
					position = Input.mousePosition
				};
				List<RaycastResult> result = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointerEventData, result);
				if (!result.Exists(_=>_.gameObject == m_EventTrigger_Menu.gameObject))
				{
					m_Button_Community.gameObject.SetActive(false);
					m_Button_MyIsland.gameObject.SetActive(false);
				}
			});
			
			m_EventTrigger_Community.triggers.Add(onPointExit);
		}
		private void MyIsLandTrigger()
		{
			EventTrigger.Entry onPointExit = new EventTrigger.Entry();
			onPointExit.eventID = EventTriggerType.PointerExit;
			onPointExit.callback.AddListener(eventData =>
			{
				PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
				{
					position = Input.mousePosition
				};
				List<RaycastResult> result = new List<RaycastResult>();
				EventSystem.current.RaycastAll(pointerEventData, result);
				if (!result.Exists(_=>_.gameObject == m_EventTrigger_Menu.gameObject))
				{
					m_Button_Community.gameObject.SetActive(false);
					m_Button_MyIsland.gameObject.SetActive(false);
				}
			});
			
			m_EventTrigger_MyIsland.triggers.Add(onPointExit);
		}

		private void SetTeleportPointScrollMode(TeleportPointScrollCtl.Mode mode)
		{
			m_TeleportPointScrollCtl.SetMode(mode);
			m_EnhancedScroller_ScrollView.ReloadData();
		}

		protected override void OnClose(bool isShutdown, object userData)
		{
			base.OnClose(isShutdown, userData);
			m_LeftShowTimeTask?.Cancel();
			m_LeftShowTimeTask = null;
			m_EnhancedScroller_ScrollView.ClearAll();
		}
	}
}
