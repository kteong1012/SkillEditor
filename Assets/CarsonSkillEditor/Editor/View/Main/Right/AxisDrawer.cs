using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class AxisDrawer : IMGUIDrawer
    {
        private readonly Vector2 _startPos;
        private readonly float _height;
        private readonly float _shortLineHeight;
        private readonly float _longLineHeight;
        private readonly float _scrollSpeed;

        private const float _labelPosY = 5f;
        private const float _labelOffsetX = 0f;

        public AxisDrawer(Vector2 startPos, float height = 40f, float shortLineHeight = 15f, float longLineHeight = 30f, float scrollSpeed = 1f)
        {
            _startPos = startPos;
            _height = height;
            _shortLineHeight = shortLineHeight;
            _longLineHeight = longLineHeight;
            _scrollSpeed = scrollSpeed;
        }

        public override void OnGUI(Rect rect)
        {
            if (TimelineAxisManager.MaxFrameCount <= 0)
            {
                var viewWidth = rect.width;
                TimelineAxisManager.UpdateMaxFrameCount(viewWidth);
                return;
            }

            var frameCount = TimelineAxisManager.MaxFrameCount;

            Handles.BeginGUI();
            Handles.color = Color.white;
            DrawFrameLine(0);
            for (var i = 0; i < frameCount; i++)
            {
                var frame = i + 1;
                DrawFrameLine(frame);
            }

            Handles.EndGUI();
        }

        private void DrawFrameLine(int frame)
        {
            var position = TimelineAxisManager.FrameToPosition(frame);
            var showFrame = frame % TimelineAxisManager.LONGER_FRAME_COUNT == 0;
            var lineHeight = showFrame ? _longLineHeight : _shortLineHeight;
            Handles.DrawLine(new Vector3(_startPos.x + position, _labelPosY + _height - lineHeight), new Vector3(_startPos.x + position, _height));
            if (showFrame)
            {
                Handles.Label(new Vector3(_startPos.x + position + _labelOffsetX, _labelPosY), frame.ToString());
            }
        }

        protected override bool ScrollWheel(Event evt, Rect rect)
        {
            var evtPos = evt.mousePosition;
            var selfRect = new Rect(_startPos, new Vector2(rect.width, _height));
            if (!selfRect.Contains(evtPos))
            {
                return false;
            }

            var deltaY = evt.delta.y;
            var scrollDelta = deltaY * _scrollSpeed;

            TimelineAxisManager.Scale += scrollDelta * 0.01f;

            if (TimelineAxisManager.MaxFrameCount == 0 || deltaY < 0)
            {
                var viewWidth = rect.width;
                TimelineAxisManager.UpdateMaxFrameCount(viewWidth);
            }

            evt.Use();

            return true;
        }
    }
}