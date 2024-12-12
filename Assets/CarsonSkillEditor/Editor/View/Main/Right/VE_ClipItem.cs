using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_ClipItem : VisualElement
    {
        private bool _isDragging;
        private bool _isResizing;
        private Vector2 _startMousePosition;
        private float _offsetX;
        private float _startWidth;

        public VE_ClipItem()
        {
            style.position = Position.Absolute;
            style.width = 100;
            style.height = 100;
            style.backgroundColor = new StyleColor(Color.gray);
            style.marginLeft = 5;
            style.marginRight = 5;
            
            var timelineItem = new TimelineDraggableManipulator();
            this.AddManipulator(timelineItem);
            
            var timelineResizable = new TimelineResizableManipulator(this);
            this.AddManipulator(timelineResizable);
        }
    }
}