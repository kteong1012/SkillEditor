using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class TimelineDraggableManipulator : PointerManipulator
    {
        private bool _isDragging;
        private float _startMousePosition;
        private int _pointerId;

        public TimelineDraggableManipulator()
        {
            _isDragging = false;
            _startMousePosition = 0;
            _pointerId = -1;
            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<PointerDownEvent>(OnMouseDown);
            target.RegisterCallback<PointerMoveEvent>(OnMouseMove);
            target.RegisterCallback<PointerUpEvent>(OnMouseUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<PointerDownEvent>(OnMouseDown);
            target.UnregisterCallback<PointerMoveEvent>(OnMouseMove);
            target.UnregisterCallback<PointerUpEvent>(OnMouseUp);
        }

        private void OnMouseDown(PointerDownEvent evt)
        {
            if (_isDragging)
            {
                evt.StopImmediatePropagation();
                return;
            }

            if (CanStartManipulation(evt))
            {
                _startMousePosition = evt.localPosition.x;
                _isDragging = true;
                _pointerId = evt.pointerId;

                target.CaptureMouse();
                evt.StopPropagation();
            }
        }

        private void OnMouseMove(PointerMoveEvent evt)
        {
            if (!_isDragging || !target.HasPointerCapture(_pointerId))
            {
                return;
            }

            var offsetX = evt.localPosition.x - _startMousePosition;
            target.style.left = target.layout.x + offsetX;
        }

        private void OnMouseUp(PointerUpEvent evt)
        {
            if (!_isDragging || !CanStopManipulation(evt))
            {
                return;
            }

            _isDragging = false;
            _pointerId = -1;
            target.ReleaseMouse();
            evt.StopPropagation();
        }
    }
}