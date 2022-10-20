using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZeroUIFrame
{

    public class UIButton : Button, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {

        bool isPressing;
        public bool IsPressing => isPressing;

        Action<PointerEventData> m_pointerDown;
        public void OnPointerDown_Clear() => m_pointerDown = null;
        public void OnPointerDown(Action<PointerEventData> action)
        {
            m_pointerDown += action;
        }

        Action<PointerEventData> m_pointerUp;
        public void OnPointerUp_Clear() => m_pointerUp = null;
        public void OnPointerUp(Action<PointerEventData> action)
        {
            m_pointerUp += action;
        }

        Action<PointerEventData> m_pointerDrag;
        public void OnPointerDrag_Clear() => m_pointerDrag = null;
        public void OnPointerDrag(Action<PointerEventData> action)
        {
            m_pointerDrag += action;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            m_pointerDown?.Invoke(eventData);
            isPressing = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            m_pointerUp?.Invoke(eventData);
            isPressing = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_pointerDrag?.Invoke(eventData);
        }

    }

}