using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public int clampRange = 500;
		public static string horizontalAxisName = "Horizontal"; 
		public static string verticalAxisName = "Vertical";
		CrossPlatformInputManager.VirtualAxis oHorizontalVirtualAxis; 
		CrossPlatformInputManager.VirtualAxis oVerticalVirtualAxis; 

		private int oTouchId = -1;

		void OnEnable() 
		{
			oHorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(oHorizontalVirtualAxis);
			
			oVerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
			CrossPlatformInputManager.RegisterVirtualAxis(oVerticalVirtualAxis);
		}

		void OnDisable()
		{
			if (CrossPlatformInputManager.AxisExists(horizontalAxisName))
				CrossPlatformInputManager.UnRegisterVirtualAxis(horizontalAxisName);

			if (CrossPlatformInputManager.AxisExists(verticalAxisName))
				CrossPlatformInputManager.UnRegisterVirtualAxis(verticalAxisName);
		}
		

		void Update () 
		{
			if ((Input.touchCount >= oTouchId + 1) && (oTouchId > -1)) {
				Vector2 dest = Vector2.zero;
				float dt = 1f;
				dt = Input.touches [oTouchId].deltaTime <= 0.00001f ? 0.00001f : Input.touches [oTouchId].deltaTime;
				dest.x = Mathf.Clamp (Input.touches [oTouchId].deltaPosition.x / dt, -clampRange, clampRange);
				dest.y = Mathf.Clamp (Input.touches [oTouchId].deltaPosition.y / dt, -clampRange, clampRange);
				oHorizontalVirtualAxis.Update (dest.x);
				oVerticalVirtualAxis.Update (dest.y);
			} 
			else
			{
				oHorizontalVirtualAxis.Update (0);
				oVerticalVirtualAxis.Update (0);
			}
		}

		public void OnPointerDown(PointerEventData data)
		{
			oTouchId = data.pointerId;
		}

		public void OnPointerUp(PointerEventData data)
		{
			oTouchId = -1;
			oHorizontalVirtualAxis.Update (0);
			oVerticalVirtualAxis.Update (0);
		}
	}
}