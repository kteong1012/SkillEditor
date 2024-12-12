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
        private List<TrackData> _trackDataList = new();

        public VE_MainRight()
        {
            // IMGUIContainer, 绘制一条贯穿试图上下的线，表示时间轴上的游标
            var cursorLine = new IMGUIContainer(OnDrawCursorLine);
            cursorLine.style.height = 20f;
            cursorLine.style.width = 20f;
            cursorLine.style.backgroundColor =  new StyleColor(Color.red);
            var draggable = new TimelineDraggableManipulator();
            cursorLine.AddManipulator(draggable);
            Add(cursorLine);
            // 让cursorLine在渲染时，不会被其他元素遮挡
            cursorLine.BringToFront();
            
            // IMGUIContainer, 高度为30
            var imguiContainer = new IMGUIContainer(OnDrawFrameAxis);
            imguiContainer.style.height = 30;
            Add(imguiContainer);

            _trackDetailListView = new ListView(_trackDataList, 100, MakeTrackDetailListCell, BindTrackDetailListCell);
            Add(_trackDetailListView);

            // 纵向充满父物体
            style.flexGrow = 1;
            style.flexDirection = FlexDirection.Column;
            
            // 添加滚轮事件
            this.AddManipulator(new TimelineScrollWheelManipulator(imguiContainer));
        }

        private VisualElement MakeTrackDetailListCell()
        {
            return new VE_TrackDetailListCell();
        }

        private void BindTrackDetailListCell(VisualElement element, int index)
        {
            var cell = element as VE_TrackDetailListCell;
            cell.BindItem(_trackDataList[index]);
        }

        public void UpdateTrackDataList(List<TrackData> trackDataList)
        {
            _trackDataList = trackDataList;
            _trackDetailListView.itemsSource = trackDataList;
            _trackDetailListView.Rebuild();
        }

        private void OnDrawFrameAxis()
        {
            var viewWidth = this.layout.width;
            var frameCount = (int)(viewWidth / TimelineAxisManager.FrameToPosition(1));

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
        
        private void OnDrawCursorLine()
        {
            var viewHeight = this.layout.height;
            var cursorPosition = 10f;
            Handles.BeginGUI();
            Handles.color = Color.red;
            Handles.DrawLine(new Vector3(cursorPosition, 0), new Vector3(cursorPosition, viewHeight));
            Handles.EndGUI();
        }
    }
}