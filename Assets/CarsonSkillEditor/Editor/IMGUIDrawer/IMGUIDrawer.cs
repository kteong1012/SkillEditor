using System;
using UnityEngine;

namespace SkillEditor
{
    public abstract class IMGUIDrawer
    {
        private static int DYNAMIC_GENERATE_ID;
        protected int Id { get; }

        protected IMGUIDrawer()
        {
            Id = DYNAMIC_GENERATE_ID++;
        }

        public abstract void OnGUI(Rect rect);

        public bool HandleEvent(Rect rect)
        {
            var isHandled = false;
            var evt = Event.current;

            switch (evt.GetTypeForControl(Id))
            {
                case EventType.MouseDown:
                    if (evt.clickCount < 2)
                    {
                        isHandled = evt.button switch
                        {
                            0 => MouseDown(evt, rect),
                            1 => MouseRightClick(evt, rect),
                            _ => false
                        };
                    }
                    else
                    {
                        isHandled = MouseDoubleClick(evt, rect);
                    }

                    if (isHandled)
                    {
                        GUIUtility.hotControl = Id;
                    }

                    break;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl == Id)
                    {
                        isHandled = MouseUp(evt, rect);
                        GUIUtility.hotControl = 0;
                        evt.Use();
                    }

                    break;
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl == Id)
                    {
                        isHandled = MouseDrag(evt, rect);
                        evt.Use();
                    }

                    break;
                case EventType.ScrollWheel:
                    isHandled = ScrollWheel(evt, rect);
                    break;
                case EventType.KeyDown:
                    isHandled = KeyDown(evt, rect);
                    break;
                case EventType.KeyUp:
                    isHandled = KeyUp(evt, rect);
                    break;
            }

            return isHandled;
        }


        protected virtual bool MouseDown(Event evt, Rect rect) => false;
        protected virtual bool MouseRightClick(Event evt, Rect rect) => false;
        protected virtual bool MouseDoubleClick(Event evt, Rect rect) => false;
        protected virtual bool MouseUp(Event evt, Rect rect) => false;
        protected virtual bool MouseDrag(Event evt, Rect rect) => false;
        protected virtual bool ScrollWheel(Event evt, Rect rect) => false;
        protected virtual bool KeyDown(Event evt, Rect rect) => false;
        protected virtual bool KeyUp(Event evt, Rect rect) => false;
    }
}