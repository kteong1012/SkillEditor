using System.Collections.Generic;
using CarsonSkill.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SkillEditor
{
    public class VE_MainRight : VisualElement
    {
        private readonly ListView _trackDetailListView;

        // 单独渲染轴
        private List<IMGUIDrawer> _imguiDrawers;

        public VE_MainRight()
        {
            var imguiContainer = new IMGUIContainer(OnGUI);
            Add(imguiContainer);

            // 撑满
            imguiContainer.style.flexGrow = 1;

            InitializeIMGUIDrawers();
        }

        public void InitializeIMGUIDrawers()
        {
            _imguiDrawers = new List<IMGUIDrawer>
            {
                new AxisDrawer(new Vector2(0, 0)),
                new AnimationClipDrawer()
                {
                    ItemRect = new Rect(100, 100, 100, 20)
                },
                new AnimationClipDrawer()
                {
                    ItemRect = new Rect(100, 200, 100, 20)
                }
            };
        }

        // 滚动位置
        private Vector2 _scrollPosition;
        private readonly TimelineScrollWheelManipulator _scrollManipulator;

        private void OnGUI()
        {
            var evt = Event.current;
            if (evt.type == EventType.ScrollWheel)
            {
                if (_scrollManipulator != null)
                {
                    evt.Use();
                    return;
                }
            }

            // 横竖方向都可以滚动
            var scrollRectWidth = TimelineAxisManager.FrameToPosition(TimelineAxisManager.MaxFrameCount);
            var scrollRectHeight = 1000; // todo

            // 获取MainRight自身的宽高
            var viewRectWidth = this.layout.width;
            var viewRectHeight = this.layout.height;

            var viewRect = new Rect(0, 0, viewRectWidth, viewRectHeight);
            var scrollRect = new Rect(0, 0, scrollRectWidth, scrollRectHeight);


            _scrollPosition = GUI.BeginScrollView(viewRect, _scrollPosition, scrollRect, false, false);

            var handled = false;
            foreach (var imguiDrawer in _imguiDrawers)
            {
                if (!handled)
                {
                    handled = imguiDrawer.HandleEvent(viewRect);
                }
                imguiDrawer.OnGUI(viewRect);
            }

            GUI.EndScrollView();
        }
    }
}