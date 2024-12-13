using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class TimelineScrollWheelManipulator : Manipulator
    {
        private readonly float _scrollSpeed;

        public TimelineScrollWheelManipulator(float scrollSpeed = 1f)
        {
            _scrollSpeed = scrollSpeed;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<WheelEvent>(OnScroll);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<WheelEvent>(OnScroll);
        }

        private void OnScroll(WheelEvent evt)
        {
            var deltaY = evt.delta.y;
            var scrollDelta = deltaY * _scrollSpeed;

            TimelineAxisManager.Scale += scrollDelta * 0.01f;

            if (TimelineAxisManager.MaxFrameCount == 0 || deltaY < 0)
            {
                var viewWidth = target.layout.width;
                TimelineAxisManager.UpdateMaxFrameCount(viewWidth);
            }

            // 立马触发重绘
            target.MarkDirtyRepaint();
        }
    }
}