using UnityEngine;

namespace SkillEditor
{
    public struct ScalableRect
    {
        public int startFrame { get; set; }
        public float x => TimelineAxisManager.FrameToPosition(startFrame);
        public float y;
        public int frameCount { get; set; }
        public float width => TimelineAxisManager.FrameToPosition(frameCount);
        public float height;

        public Rect Rect => (Rect)this;

        // implicit conversion from Rect to ScalableRect
        public static implicit operator ScalableRect(Rect rect)
        {
            return new ScalableRect
            {
                startFrame = TimelineAxisManager.GetNearestFrame(rect.x),
                y = rect.y,
                frameCount = TimelineAxisManager.GetNearestFrame(rect.width),
                height = rect.height
            };
        }

        // implicit conversion from ScalableRect to Rect
        public static implicit operator Rect(ScalableRect scalableRect)
        {
            return new Rect(scalableRect.x, scalableRect.y, scalableRect.width, scalableRect.height);
        }
    }
}