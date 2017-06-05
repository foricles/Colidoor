using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

		private Image oJoystic = null;
		private Image oStick   = null;
		private GameObject oRange   = null;

		public static string horizontalAxisName = "JoysticHorizontal"; 
		public static string verticalAxisName = "JoysticVertical";
		CrossPlatformInputManager.VirtualAxis oHorizontalVirtualAxis; 
		CrossPlatformInputManager.VirtualAxis oVerticalVirtualAxis; 

		[SerializeField]private float oStickRange = 70.71068f;

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

		void Awake()
		{
			oRange = gameObject;
			Image[] array = oRange.GetComponentsInChildren<Image> (true);
			if (array != null || array.Length > 0)
			{
				foreach (Image img in array)
					if (img.tag == "gJoystic")
						setJoystic (img);
					else if (img.tag == "gStick")
						setStick (img);
			}
				
			oJoystic.raycastTarget = false;
			oStick.raycastTarget = false;
			setActive (false);
		}
			
	//------------events handlres--------------------------------------------------------------------
		public void OnPointerDown(PointerEventData data)
		{
			setJoysticPosition (data.position);
			setActive (true);
		}
		public void OnPointerUp(PointerEventData data)
		{
			setActive (false);
			oHorizontalVirtualAxis.Update (0);
			oVerticalVirtualAxis.Update (0);
		}
		public void OnDrag(PointerEventData data)
		{
			oStick.rectTransform.position = data.position;
			oStick.rectTransform.localPosition = -Vector2.ClampMagnitude(-oStick.rectTransform.localPosition, oStickRange);
			oHorizontalVirtualAxis.Update (getSticPosition().x / oStickRange);
			oVerticalVirtualAxis.Update (getSticPosition().y / oStickRange);
		}
	//---------getters and setters-------------------------------------------------------------------------------------------------------
		public Vector2 getAxis()
		{
			Vector2 ret = Vector2.zero;
			ret.x = getSticPosition().x / oStickRange;
			ret.y = getSticPosition().y / oStickRange;
			return ret;
		}

		public bool isActive(){
			return oJoystic.gameObject.activeSelf;
		}
		public void setActive(bool active){
			oJoystic.gameObject.SetActive(active);
			oStick.gameObject.SetActive (active);
		}
			
		public Vector2 getJoysticPosition(){
			return oJoystic.rectTransform.position;
		}
		public void setJoysticPosition(Vector2 pos){
			oJoystic.rectTransform.position = pos;
			setStickPosition (Vector2.zero);
		}

		public Vector2 getSticPosition(){
			return oStick.rectTransform.localPosition;
		}
		public void setStickPosition(Vector2 pos){
			oStick.rectTransform.localPosition = pos;
		}
			
		public void setStick(Image stick){
			oStick = stick;
		}
		public void setJoystic(Image joystic)
		{
			oJoystic = joystic;
		}
	}
}
