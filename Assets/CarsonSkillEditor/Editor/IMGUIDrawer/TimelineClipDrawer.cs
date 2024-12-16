using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public abstract class TimelineClipDrawer : IMGUIDrawer, IMovableDrawer, IResizableDrawer
    {
        public bool IsMoving { get; set; }
        public bool IsResizing { get; set; }
        public ScalableRect ItemRect { get; set; }
        private float _pointerOffsetX;

        private Rect ResizeHandleRect => new(ItemRect.x + ItemRect.width - 5, ItemRect.y, 10, ItemRect.height);

        public override void OnGUI(Rect rect)
        {
            EditorGUI.DrawRect(ResizeHandleRect, Color.white);
            EditorGUIUtility.AddCursorRect(ResizeHandleRect, MouseCursor.ResizeHorizontal);
            AdjustItemRect();
        }

        private void AdjustItemRect()
        {
            var startPosX = ItemRect.x;
            var endPosX = ItemRect.x + ItemRect.width;

            var nearestStartPos = TimelineAxisManager.ClampToNearestFramePosition(startPosX);
            var nearestEndPos = TimelineAxisManager.ClampToNearestFramePosition(endPosX);

            var newWidth = nearestEndPos - nearestStartPos;

            ItemRect = new Rect(nearestStartPos, ItemRect.y, newWidth, ItemRect.height);
        }


        protected override bool MouseDown(Event evt, Rect rect)
        {
            if (ResizeHandleRect.Contains(evt.mousePosition))
            {
                IsResizing = true;
                IsMoving = false;
                return true;
            }

            if (ItemRect.Rect.Contains(evt.mousePosition))
            {
                _pointerOffsetX = evt.mousePosition.x - ItemRect.x;
                IsMoving = true;
                IsResizing = false;
                return true;
            }

            return false;
        }

        protected override bool MouseDrag(Event evt, Rect rect)
        {
            if (GUIUtility.hotControl == Id)
            {
                if (IsResizing)
                {
                    var x = evt.mousePosition.x;
                    var newWidth = x - ItemRect.x;
                    var minWidth = TimelineAxisManager.FrameToPosition(1);
                    if (newWidth < minWidth)
                    {
                        newWidth = minWidth;
                    }

                    ItemRect = new Rect(ItemRect.x, ItemRect.y, newWidth, ItemRect.height);
                    return true;
                }

                if (IsMoving)
                {
                    var x = evt.mousePosition.x - _pointerOffsetX;
                    if (x < 0)
                    {
                        x = 0;
                    }

                    ItemRect = new Rect(x, ItemRect.y, ItemRect.width, ItemRect.height);
                    return true;
                }
            }

            return false;
        }

        protected override bool MouseUp(Event evt, Rect rect)
        {
            IsMoving = false;
            IsResizing = false;
            return true;
        }
    }
}