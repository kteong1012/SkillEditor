using UnityEditor;
using UnityEngine;

namespace SkillEditor
{
    public class IM_Axis : IMGUIDrawer
    {
        private readonly float _scrollSpeed;

        public IM_Axis(float scrollSpeed = 1f)
        {
            _scrollSpeed = scrollSpeed;
        }
        
        public override void OnGUI()
        {
            var frameCount = TimelineAxisManager.MaxFrameCount;

            Handles.BeginGUI();
            for (var i = 0; i < frameCount; i++)
            {
                var frame = i + 1;
                var position = TimelineAxisManager.FrameToPosition(frame);
                if (frame % TimelineAxisManager.LONGER_FRAME_COUNT == 0)
                {
                    Handles.color = Color.white;
                    Handles.DrawLine(new Vector3(position, 10), new Vector3(position, 30));
                    Handles.Label(new Vector3(position - 5f, 6), frame.ToString());
                }
                else
                {
                    Handles.color = Color.white;
                    Handles.DrawLine(new Vector3(position, 15), new Vector3(position, 30));
                }
            }

            Handles.EndGUI();
        }

        protected override bool ScrollWheel(Event evt, Rect rect)
        {   
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