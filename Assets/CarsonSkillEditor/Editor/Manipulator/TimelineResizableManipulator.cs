using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class TimelineResizableManipulator : PointerManipulator
    {
        private VisualElement _target;
        private bool _isResizing;
        private float _startMousePosition;
        private float _startWidth;
        private int _pointerId;
        private readonly VisualElement _resizeHandle;
        
        // TODO 目前先写死成一个常量，后面会变成一个全局的静态变量
        private const float minWidth = 10f;

        public TimelineResizableManipulator(VisualElement target)
        {
            _target = target;
            _isResizing = false;
            _startMousePosition = 0;
            _startWidth = 0;
            _pointerId = -1;
            activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
            
            
            // 在target右侧创建一个能把Cursor变成ew-resize的VisualElement
            _resizeHandle = new VisualElement();
            _resizeHandle.style.position = Position.Absolute;
            _resizeHandle.style.right = 0;
            _resizeHandle.style.top = 0;
            _resizeHandle.style.bottom = 0;
            _resizeHandle.style.width = 5;
            _resizeHandle.style.backgroundColor = new StyleColor(Color.black);
            _resizeHandle.SetCursor(MouseCursor.ResizeHorizontal);
            target.Add(_resizeHandle);
        }

        protected override void RegisterCallbacksOnTarget()
        {
            _resizeHandle.RegisterCallback<PointerDownEvent>(OnMouseDown);
            _resizeHandle.RegisterCallback<PointerMoveEvent>(OnMouseMove);
            _resizeHandle.RegisterCallback<PointerUpEvent>(OnMouseUp);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            _resizeHandle.UnregisterCallback<PointerDownEvent>(OnMouseDown);
            _resizeHandle.UnregisterCallback<PointerMoveEvent>(OnMouseMove);
            _resizeHandle.UnregisterCallback<PointerUpEvent>(OnMouseUp);
        }

        private void OnMouseDown(PointerDownEvent evt)
        {
            if (_isResizing)
            {
                evt.StopImmediatePropagation();
                return;
            }

            if (CanStartManipulation(evt))
            {
                _startMousePosition = evt.position.x;
                _startWidth = _target.style.width.value.value;
                _isResizing = true;
                _pointerId = evt.pointerId;

                _resizeHandle.CaptureMouse();
                evt.StopPropagation();
            }
        }

        private void OnMouseMove(PointerMoveEvent evt)
        {
            if (!_isResizing || !_resizeHandle.HasPointerCapture(_pointerId))
            {
                return;
            }
            
            var offsetX = evt.position.x - _startMousePosition;
            var newWidth = _startWidth + offsetX;
            if (newWidth < minWidth)
            {
                newWidth = minWidth;
            }
            _target.style.width = newWidth;
        }

        private void OnMouseUp(PointerUpEvent evt)
        {
            if (!_isResizing || !_resizeHandle.HasPointerCapture(_pointerId) || !CanStopManipulation(evt))
            {
                return;
            }

            _isResizing = false;
            _pointerId = -1;
            _resizeHandle.ReleaseMouse();
            evt.StopPropagation();
        }
    }
}