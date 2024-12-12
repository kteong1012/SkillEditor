using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class TimelineScrollWheelManipulator : Manipulator
    {
        private VisualElement _target;
        private readonly float _scrollSpeed;
        
        public TimelineScrollWheelManipulator(VisualElement target, float scrollSpeed = 1f)
        {
            _target = target;
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
            
            // 立马触发重绘
            _target.MarkDirtyRepaint();
        }
    }
}