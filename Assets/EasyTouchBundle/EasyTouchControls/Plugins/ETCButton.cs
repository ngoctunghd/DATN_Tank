/***********************************************
				EasyTouch Controls
	Copyright © 2014-2015 The Hedgehog Team
  http://www.blitz3dfr.com/teamtalk/index.php
		
	  The.Hedgehog.Team@gmail.com
		
**********************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

[System.Serializable]
public class ETCButton : ETCBase, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler { 

	#region Unity Events
	[System.Serializable] public class OnDownHandler : UnityEvent{}
	[System.Serializable] public class OnPressedHandler : UnityEvent{}
	[System.Serializable] public class OnPressedValueandler : UnityEvent<float>{}
	[System.Serializable] public class OnUPHandler : UnityEvent{}

	[SerializeField] public OnDownHandler onDown;
	[SerializeField] public OnPressedHandler onPressed;
	[SerializeField] public OnPressedValueandler onPressedValue;
	[SerializeField] public OnUPHandler onUp;
	#endregion

	#region Members

	#region Public members
	public ETCAxis axis;

	public Sprite normalSprite;
	public Color normalColor;

	public Sprite pressedSprite;
	public Color pressedColor;
    #endregion

    //delete
    //#region Enumeration
    //public enum JoystickArea { UserDefined, FullScreen, Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight };
    //public enum JoystickType { Dynamic, Static };

    //#endregion
    //public JoystickType joystickType;
    //public bool allowJoystickOverTouchPad;
    //public JoystickArea joystickArea;
    //public RectTransform userArea;
    //private bool isDynamicActif;
    //delete

    #region Private members
    private Image cachedImage; 
	private bool isOnPress;
	private GameObject previousDargObject;
	private bool isOnTouch;
	#endregion

	#endregion

	#region Constructor
	public ETCButton(){

        //delete
        //joystickType = JoystickType.Static;
        //allowJoystickOverTouchPad = false;
        //joystickArea = JoystickArea.FullScreen;

        //isDynamicActif = false;

        axis = new ETCAxis( "Button");
		_visible = true;
		_activated = true;
		isOnTouch = false;

		enableKeySimulation = true;
		#if !UNITY_EDITOR
			enableKeySimulation = false;
		#endif

		axis.positivekey = KeyCode.Space;

		showPSInspector = true; 
		showSpriteInspector = false;
		showBehaviourInspector = false;
		showEventInspector = false;
	}
	#endregion

	#region Monobehaviour Callback
	protected override void Awake (){
		base.Awake ();

		cachedImage = GetComponent<UnityEngine.UI.Image>();
        //if (joystickType == JoystickType.Dynamic)
        //{
        //    this.rectTransform().anchorMin = new Vector2(0.5f, 0.5f);
        //    this.rectTransform().anchorMax = new Vector2(0.5f, 0.5f);
        //    //this.rectTransform().anchorMin = new Vector2(0f, 0f);
        //    //this.rectTransform().anchorMax = new Vector2(0f, 0f);
        //    this.rectTransform().SetAsLastSibling();

        //    visible = true;

        //}

    }

	void Start(){
		isOnPress = false;
        //GetComponent<RectTransform>().localPosition = new Vector3(-326f, 308f, 0f);
    }


	protected override void UpdateControlState ()
	{
		UpdateButton();
	}
	#endregion

	#region UI Callback
	public void OnPointerEnter(PointerEventData eventData){

		if (isSwipeIn && !isOnTouch){

			if (eventData.pointerDrag != null){
				if (eventData.pointerDrag.GetComponent<ETCBase>() && eventData.pointerDrag!= gameObject){
					previousDargObject=eventData.pointerDrag;
					//ExecuteEvents.Execute<IPointerUpHandler> (previousDargObject, eventData, ExecuteEvents.pointerUpHandler);
				}
			}

			eventData.pointerDrag = gameObject;
			eventData.pointerPress = gameObject;
			OnPointerDown( eventData);
		}

        //delete

        //if (joystickType == JoystickType.Dynamic && !isDynamicActif && _activated)
        //{
        //    eventData.pointerDrag = gameObject;
        //    eventData.pointerPress = gameObject;

        //    isDynamicActif = true;
        //}

        //if (joystickType == JoystickType.Dynamic && !eventData.eligibleForClick)
        //{
        //    OnPointerUp(eventData);
        //}
    }

	public void OnPointerDown(PointerEventData eventData){

		if (_activated && !isOnTouch){
			pointId = eventData.pointerId;

			axis.ResetAxis();
			axis.axisState = ETCAxis.AxisState.Down;

			isOnPress = false;
			isOnTouch = true;

			onDown.Invoke();
			ApllyState();
		}
	}

	public void OnPointerUp(PointerEventData eventData){
		if (pointId == eventData.pointerId){
			isOnPress = false;
			isOnTouch = false;

            //delete
            //if (joystickType == JoystickType.Dynamic)
            //{
            //    visible = false;
            //    isDynamicActif = false;
            //}

            axis.axisState = ETCAxis.AxisState.Up;
			axis.axisValue = 0;
			onUp.Invoke();
			ApllyState();

			if (previousDargObject){
				ExecuteEvents.Execute<IPointerUpHandler> (previousDargObject, eventData, ExecuteEvents.pointerUpHandler);
				previousDargObject = null;
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		if (pointId == eventData.pointerId){
			if (axis.axisState == ETCAxis.AxisState.Press && !isSwipeOut){
				OnPointerUp(eventData);
			}
		}
	}
	#endregion

	#region Button Update
	private void UpdateButton(){

        ///delete
        /// #region dynamic joystick
        //if (joystickType == JoystickType.Dynamic && !_visible && _activated)
        //{
        //    Vector2 localPosition = Vector2.zero;
        //    Vector2 screenPosition = Vector2.zero;

        //    if (isTouchOverJoystickArea(ref localPosition, ref screenPosition))
        //    {

        //        GameObject overGO = GetFirstUIElement(screenPosition);

        //        if (overGO == null || (allowJoystickOverTouchPad && overGO.GetComponent<ETCTouchPad>()) || (overGO != null && overGO.GetComponent<ETCArea>()))
        //        {
        //            cachedRectTransform.anchoredPosition = localPosition;
        //            visible = true;
                    
        //        }
        //    }
        //}

        if (axis.axisState == ETCAxis.AxisState.Down)
        {
            isOnPress = true;
            axis.axisState = ETCAxis.AxisState.Press;

        }

        if (isOnPress)
        {
            axis.UpdateButton();
            onPressed.Invoke();
            onPressedValue.Invoke(axis.axisValue);
        }

        if (axis.axisState == ETCAxis.AxisState.Up){
			isOnPress = false;
			axis.axisState = ETCAxis.AxisState.None;
           
		}


		if (enableKeySimulation && _activated && _visible && !isOnTouch){
			
			
			if (Input.GetKey( axis.positivekey) && axis.axisState ==ETCAxis.AxisState.None  ){
				axis.axisState = ETCAxis.AxisState.Down;
			}
			
			if (!Input.GetKey(axis.positivekey) && axis.axisState == ETCAxis.AxisState.Press){
				axis.axisState = ETCAxis.AxisState.Up;
				onUp.Invoke();
			}
		}

	}	
	#endregion

	#region Private Method
	protected override void SetVisible (){
		GetComponent<Image>().enabled = _visible;
	}

	private void ApllyState(){

		switch (axis.axisState){
		case ETCAxis.AxisState.Down:
			case ETCAxis.AxisState.Press:
				cachedImage.sprite = pressedSprite;
				cachedImage.color = pressedColor;
				break;
			default:
				cachedImage.sprite = normalSprite;
				cachedImage.color = normalColor;
				break;
		}


	
	}
    #endregion


    //delete
//    private bool isTouchOverJoystickArea(ref Vector2 localPosition, ref Vector2 screenPosition)
//    {

//        bool touchOverArea = false;
//        bool doTest = false;
//        screenPosition = Vector2.zero;

//        int count = GetTouchCount();

//        int i = 0;
//        while (i < count && !touchOverArea)
//        {
//#if ((UNITY_ANDROID || UNITY_IPHONE || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
//			if (Input.GetTouch(i).phase == TouchPhase.Began){
//				screenPosition = Input.GetTouch(i).position;
//				doTest = true;
//			}
//#else
//            if (Input.GetMouseButton(0))
//            {
//                screenPosition = Input.mousePosition;
//                doTest = true;
//            }
//#endif

//            if (doTest && isScreenPointOverArea(screenPosition, ref localPosition))
//            {
//                touchOverArea = true;
//            }

//            i++;
//        }

//        return touchOverArea;
//    }

    //private bool isScreenPointOverArea(Vector2 screenPosition, ref Vector2 localPosition)
    //{

    //    bool returnValue = false;

    //    if (joystickArea != JoystickArea.UserDefined)
    //    {
    //        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedRootCanvas.rectTransform(), screenPosition, null, out localPosition))
    //        {

    //            switch (joystickArea)
    //            {
    //                case JoystickArea.Left:
    //                    if (localPosition.x < 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.Right:
    //                    if (localPosition.x > 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.FullScreen:
    //                    returnValue = true;
    //                    break;

    //                case JoystickArea.TopLeft:
    //                    if (localPosition.y > 0 && localPosition.x < 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;
    //                case JoystickArea.Top:
    //                    if (localPosition.y > 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.TopRight:
    //                    if (localPosition.y > 0 && localPosition.x > 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.BottomLeft:
    //                    if (localPosition.y < 0 && localPosition.x < 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.Bottom:
    //                    if (localPosition.y < 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;

    //                case JoystickArea.BottomRight:
    //                    if (localPosition.y < 0 && localPosition.x > 0)
    //                    {
    //                        returnValue = true;
    //                    }
    //                    break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (RectTransformUtility.RectangleContainsScreenPoint(userArea, screenPosition, cachedRootCanvas.worldCamera))
    //        {
    //            RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedRootCanvas.rectTransform(), screenPosition, cachedRootCanvas.worldCamera, out localPosition);
    //            returnValue = true;
    //        }
    //    }

    //    return returnValue;

    //}

//    private int GetTouchCount()
//    {
//#if ((UNITY_ANDROID || UNITY_IPHONE || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
//		return Input.touchCount;
//#else
//        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
//        {
//            return 1;
//        }
//        else
//        {
//            return 0;
//        }
//#endif
//    }

}
