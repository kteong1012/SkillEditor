using System;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace SkillEditor
{
    internal static class TimelineAxisManager
    {
        public const float BASIC_AXIS_UNIT_INTERVAL = 10;
        public const float MIN_SCALE = 0.5f;
        public const float MAX_SCALE = 2f;
        public const int LONGER_FRAME_COUNT = 5;

        private static float _scale = 1f;
        public static int MaxFrameCount { get; private set; } = 100;

        public static float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                if (_scale < MIN_SCALE)
                {
                    _scale = MIN_SCALE;
                }
                else if (_scale > MAX_SCALE)
                {
                    _scale = MAX_SCALE;
                }

                OnScaleChanged?.Invoke(_scale);
            }
        }

        public static event Action<float> OnScaleChanged;

        public static float FrameToPosition(int frame)
        {
            return frame * BASIC_AXIS_UNIT_INTERVAL * _scale;
        }
        
        public static void UpdateMaxFrameCount(float viewWidth)
        {
            MaxFrameCount = (int)(viewWidth / FrameToPosition(1));
        }
    }
}