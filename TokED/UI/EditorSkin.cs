using Squid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public enum DropDownButtonType
    {
        Plus, Minus
    }
    
    public static class EditorSkin
    {
        public static Skin CreateSkin()
        {
            ControlStyle baseStyle = new ControlStyle();
            baseStyle.Tiling = TextureMode.Grid;
            baseStyle.Grid = new Margin(7);
            baseStyle.Texture = "button_default";
            baseStyle.Hot.Texture = "button_hot";
            baseStyle.Default.Texture = "button_default";
            baseStyle.Pressed.Texture = "button_down";
            baseStyle.Focused.Texture = "button_hot";
            baseStyle.SelectedPressed.Texture = "button_down";
            baseStyle.SelectedFocused.Texture = "button_hot";
            baseStyle.Selected.Texture = "button_hot";
            baseStyle.SelectedHot.Texture = "button_hot";
            baseStyle.CheckedPressed.Texture = "button_down";
            baseStyle.CheckedFocused.Texture = "button_down";
            baseStyle.Checked.Texture = "button_down";
            baseStyle.CheckedHot.Texture = "button_down";
            baseStyle.Font = "Black";
            baseStyle.TextColor = ColorInt.RGBA(0, 0, 0, 1);

            ControlStyle itemStyle = new ControlStyle(baseStyle);
            itemStyle.TextPadding = new Margin(2, 0, 0, 0);
            itemStyle.Grid = new Margin(3);
            itemStyle.Texture = "listbox_item";
            itemStyle.Hot.Texture = "listbox_item_hot";
            itemStyle.Focused.Texture = "listbox_item_hot";
            itemStyle.Selected.Texture = "listbox_item_selected";
            itemStyle.SelectedPressed.Texture = "listbox_item_selected_hot";
            itemStyle.SelectedFocused.Texture = "listbox_item_selected_hot";
            itemStyle.SelectedHot.Texture = "listbox_item_selected_hot";

            itemStyle.TextAlign = Alignment.MiddleLeft;

            ControlStyle buttonStyle = new ControlStyle(baseStyle);
            buttonStyle.TextPadding = new Margin(0);
            buttonStyle.TextAlign = Alignment.MiddleCenter;

            ControlStyle treeButtonStyle = new ControlStyle(baseStyle);
            treeButtonStyle.TextPadding = new Margin(0);
            treeButtonStyle.Grid = new Margin(0);
            treeButtonStyle.Texture = "treeview_button_plus";
            treeButtonStyle.Hot.Texture = "treeview_button_plus";
            treeButtonStyle.Default.Texture = "treeview_button_plus";
            treeButtonStyle.Pressed.Texture = "treeview_button_plus";
            treeButtonStyle.Focused.Texture = "treeview_button_plus";
            treeButtonStyle.SelectedPressed.Texture = "treeview_button_plus";
            treeButtonStyle.SelectedFocused.Texture = "treeview_button_plus";
            treeButtonStyle.Selected.Texture = "treeview_button_plus";
            treeButtonStyle.SelectedHot.Texture = "treeview_button_plus";
            treeButtonStyle.CheckedPressed.Texture = "treeview_button_minus";
            treeButtonStyle.CheckedFocused.Texture = "treeview_button_minus";
            treeButtonStyle.Checked.Texture = "treeview_button_minus";
            treeButtonStyle.CheckedHot.Texture = "treeview_button_minus";
            treeButtonStyle.TextAlign = Alignment.MiddleCenter;

            ControlStyle eyeButtonStyle = new ControlStyle(baseStyle);
            eyeButtonStyle.TextPadding = new Margin(0);
            eyeButtonStyle.Grid = new Margin(0);
            eyeButtonStyle.Texture = "eye_inactive";
            eyeButtonStyle.Hot.Texture = "eye_inactive";
            eyeButtonStyle.Default.Texture = "eye_inactive";
            eyeButtonStyle.Pressed.Texture = "eye_inactive";
            eyeButtonStyle.Focused.Texture = "eye_inactive";
            eyeButtonStyle.SelectedPressed.Texture = "eye_inactive";
            eyeButtonStyle.SelectedFocused.Texture = "eye_inactive";
            eyeButtonStyle.Selected.Texture = "eye_inactive";
            eyeButtonStyle.SelectedHot.Texture = "eye_inactive";

            eyeButtonStyle.CheckedPressed.Texture = "eye_active";
            eyeButtonStyle.CheckedFocused.Texture = "eye_active";
            eyeButtonStyle.Checked.Texture = "eye_active";
            eyeButtonStyle.CheckedHot.Texture = "eye_active";
            eyeButtonStyle.TextAlign = Alignment.MiddleCenter;

            ControlStyle saveButtonStyle = new ControlStyle();
            saveButtonStyle.Tiling = TextureMode.Grid;
            saveButtonStyle.Grid = new Margin(1);
            saveButtonStyle.Default.Texture = "save";
            saveButtonStyle.Hot.Texture = "save_hot";
            saveButtonStyle.Pressed.Texture = "save_down";
            saveButtonStyle.Focused.Texture = "save_hot";

            ControlStyle loadButtonStyle = new ControlStyle();
            loadButtonStyle.Tiling = TextureMode.Grid;
            loadButtonStyle.Grid = new Margin(1);
            loadButtonStyle.Default.Texture = "load";
            loadButtonStyle.Hot.Texture = "load_hot";
            loadButtonStyle.Pressed.Texture = "load_down";
            loadButtonStyle.Focused.Texture = "load_hot";


            ControlStyle tooltipStyle = new ControlStyle(buttonStyle);
            tooltipStyle.TextPadding = new Margin(8, 5, 5, 8);
            tooltipStyle.TextAlign = Alignment.TopLeft;
            tooltipStyle.Texture = "tooltip";
            tooltipStyle.Tiling = TextureMode.Grid;
            tooltipStyle.Grid = new Margin(8, 5, 5, 8);
            tooltipStyle.BackColor = ColorInt.RGBA(0, 0, 0, .9f);
            tooltipStyle.TextColor = ColorInt.RGBA(1, 1, 1, 1);
            tooltipStyle.Font = "White";

            ControlStyle inputStyle = new ControlStyle();
            inputStyle.Texture = "input";
            inputStyle.Hot.Texture = "input_hot";
            inputStyle.Focused.Texture = "input_hot";
            inputStyle.TextPadding = new Margin(8);
            inputStyle.Tiling = TextureMode.Grid;
            inputStyle.Focused.Tint = ColorInt.RGBA(0.5f, 1.0f, 0.5f, 1);
            inputStyle.Grid = new Margin(4,2,4,2);
            inputStyle.Font = "Black";
            inputStyle.TextColor = ColorInt.RGBA(0, 0, 0, 1);

            ControlStyle inputErrorStyle = new ControlStyle(inputStyle);
            inputErrorStyle.Focused.Tint = ColorInt.RGBA(1.0f, 0.5f, 0.5f, 1);

            ControlStyle windowStyle = new ControlStyle();
            windowStyle.Tiling = TextureMode.Grid;
            windowStyle.Grid = new Margin(8);
            windowStyle.Texture = "window.dds";
            windowStyle.BackColor = ColorInt.RGBA(0, 0, 0, .9f);

            ControlStyle frameStyle = new ControlStyle();
            frameStyle.Tiling = TextureMode.Grid;
            frameStyle.Grid = new Margin(2);
            frameStyle.Texture = "frame";
            frameStyle.TextPadding = new Margin(8);

            ControlStyle listBoxFrameStyle = new ControlStyle();
            listBoxFrameStyle.Tiling = TextureMode.Grid;
            listBoxFrameStyle.Grid = new Margin(4);
            listBoxFrameStyle.Texture = "listbox_frame";
            listBoxFrameStyle.TextPadding = new Margin(4);

            ControlStyle vscrollTrackStyle = new ControlStyle();
            vscrollTrackStyle.Tiling = TextureMode.Grid;
            vscrollTrackStyle.Grid = new Margin(6, 7, 6, 7);
            vscrollTrackStyle.Texture = "vscroll_track";

            ControlStyle vscrollButtonStyle = new ControlStyle();
            vscrollButtonStyle.Tiling = TextureMode.Grid;
            vscrollButtonStyle.Grid = new Margin(6);
            vscrollButtonStyle.Texture = "vscroll_button";
            vscrollButtonStyle.Hot.Texture = "vscroll_button_hot";
            vscrollButtonStyle.Pressed.Texture = "vscroll_button_down";

            ControlStyle vscrollUp = new ControlStyle();
            vscrollUp.Default.Texture = "vscrollUp_default.dds";
            vscrollUp.Hot.Texture = "vscrollUp_hot.dds";
            vscrollUp.Pressed.Texture = "vscrollUp_down.dds";
            vscrollUp.Focused.Texture = "vscrollUp_hot.dds";

            ControlStyle hscrollTrackStyle = new ControlStyle();
            hscrollTrackStyle.Tiling = TextureMode.Grid;
            hscrollTrackStyle.Grid = new Margin(3);
            hscrollTrackStyle.Texture = "hscroll_track.dds";

            ControlStyle hscrollButtonStyle = new ControlStyle();
            hscrollButtonStyle.Tiling = TextureMode.Grid;
            hscrollButtonStyle.Grid = new Margin(3);
            hscrollButtonStyle.Texture = "hscroll_button.dds";
            hscrollButtonStyle.Hot.Texture = "hscroll_button_hot.dds";
            hscrollButtonStyle.Pressed.Texture = "hscroll_button_down.dds";

            ControlStyle hscrollUp = new ControlStyle();
            hscrollUp.Default.Texture = "hscrollUp_default.dds";
            hscrollUp.Hot.Texture = "hscrollUp_hot.dds";
            hscrollUp.Pressed.Texture = "hscrollUp_down.dds";
            hscrollUp.Focused.Texture = "hscrollUp_hot.dds";

            ControlStyle checkButtonStyle = new ControlStyle();
            checkButtonStyle.Tiling = TextureMode.Grid;
            checkButtonStyle.Grid = new Margin(3);
            checkButtonStyle.Default.Texture = "checkbox";
            checkButtonStyle.Hot.Texture = "checkbox_hot";
            checkButtonStyle.Pressed.Texture = "checkbox";
            checkButtonStyle.Checked.Texture = "checkbox_checked";
            checkButtonStyle.CheckedFocused.Texture = "checkbox_checked_hot";
            checkButtonStyle.CheckedHot.Texture = "checkbox_checked_hot";
            checkButtonStyle.CheckedPressed.Texture = "checkbox_checked";

            #region Splitter

            ControlStyle vSplitterStyle = new ControlStyle(baseStyle);
            vSplitterStyle.TextPadding = new Margin(0);
            vSplitterStyle.Grid = new Margin(1, 0, 1, 0);
            vSplitterStyle.Texture = "vsplitter";
            vSplitterStyle.Hot.Texture = "vsplitter_hot";
            vSplitterStyle.Default.Texture = "vsplitter";
            vSplitterStyle.Pressed.Texture = "vsplitter_down";
            vSplitterStyle.Focused.Texture = "vsplitter_hot";
            vSplitterStyle.SelectedPressed.Texture = "vsplitter_down";
            vSplitterStyle.SelectedFocused.Texture = "vsplitter_hot";
            vSplitterStyle.Selected.Texture = "vsplitter_hot";
            vSplitterStyle.SelectedHot.Texture = "vsplitter_hot";
            vSplitterStyle.CheckedPressed.Texture = "vsplitter_down";
            vSplitterStyle.CheckedFocused.Texture = "vsplitter_down";
            vSplitterStyle.Checked.Texture = "vsplitter_down";
            vSplitterStyle.CheckedHot.Texture = "vsplitter_down";

            ControlStyle hSplitterStyle = new ControlStyle(baseStyle);
            hSplitterStyle.TextPadding = new Margin(0);
            hSplitterStyle.Grid = new Margin(1, 0, 1, 0);
            hSplitterStyle.Texture = "hsplitter";
            hSplitterStyle.Hot.Texture = "hsplitter_hot";
            hSplitterStyle.Default.Texture = "hsplitter";
            hSplitterStyle.Pressed.Texture = "hsplitter_down";
            hSplitterStyle.Focused.Texture = "hsplitter_hot";
            hSplitterStyle.SelectedPressed.Texture = "hsplitter_down";
            hSplitterStyle.SelectedFocused.Texture = "hsplitter_hot";
            hSplitterStyle.Selected.Texture = "hsplitter_hot";
            hSplitterStyle.SelectedHot.Texture = "hsplitter_hot";
            hSplitterStyle.CheckedPressed.Texture = "hsplitter_down";
            hSplitterStyle.CheckedFocused.Texture = "hsplitter_down";
            hSplitterStyle.Checked.Texture = "hsplitter_down";
            hSplitterStyle.CheckedHot.Texture = "hsplitter_down";

            #endregion

            #region ComboBox

            ControlStyle combopListBoxFrameStyle = new ControlStyle();
            combopListBoxFrameStyle.Tiling = TextureMode.Grid;
            combopListBoxFrameStyle.Grid = new Margin(4);
            combopListBoxFrameStyle.Texture = "combo_lisbox_frame";
            combopListBoxFrameStyle.TextPadding = new Margin(4);

            ControlStyle comboItemStyle = new ControlStyle();
            comboItemStyle.Tiling = TextureMode.Grid;
            comboItemStyle.Grid = new Margin(7);
            comboItemStyle.TextPadding = new Margin(18, 0, 0, 0);
            comboItemStyle.Texture = "listbox_item";
            comboItemStyle.Hot.Texture = "listbox_item_hot";
            comboItemStyle.Focused.Texture = "listbox_item_hot";
            comboItemStyle.Selected.Texture = "listbox_item_selected";
            comboItemStyle.SelectedPressed.Texture = "listbox_item_selected_hot";
            comboItemStyle.SelectedFocused.Texture = "listbox_item_selected_hot";
            comboItemStyle.SelectedHot.Texture = "listbox_item_selected_hot";
            comboItemStyle.Font = "White";
            comboItemStyle.TextColor = ColorInt.RGBA(1, 1, 1, 1);

            ControlStyle comboLabelStyle = new ControlStyle();
            comboLabelStyle.TextPadding = new Margin(5, 0, 0, 0);
            comboLabelStyle.Default.Texture = "combo";
            comboLabelStyle.Hot.Texture = "combo_hot";
            comboLabelStyle.Pressed.Texture = "combo";
            comboLabelStyle.Focused.Texture = "combo";
            comboLabelStyle.Tiling = TextureMode.Grid;
            comboLabelStyle.Grid = new Margin(4, 0, 0, 0);
            comboLabelStyle.Font = "White";
            comboLabelStyle.TextColor = ColorInt.RGBA(1, 1, 1, 1);

            ControlStyle comboButtonStyle = new ControlStyle();
            comboButtonStyle.Tiling = TextureMode.Grid;
            comboButtonStyle.Grid = new Margin(1);
            comboButtonStyle.Default.Texture = "combo_button";
            comboButtonStyle.Hot.Texture = "combo_button_hot";
            comboButtonStyle.Pressed.Texture = "combo_button_down";
            comboButtonStyle.Focused.Texture = "combo_button_hot";

            ControlStyle comboButtonPlusStyle = new ControlStyle();
            comboButtonPlusStyle.Tiling = TextureMode.Grid;
            comboButtonPlusStyle.Grid = new Margin(1);
            comboButtonPlusStyle.Default.Texture = "combo_plus";
            comboButtonPlusStyle.Hot.Texture = "combo_plus_hot";
            comboButtonPlusStyle.Pressed.Texture = "combo_plus_down";
            comboButtonPlusStyle.Focused.Texture = "combo_plus_hot";
            comboButtonPlusStyle.Disabled.Texture = "combo_plus";
            comboButtonPlusStyle.Disabled.Opacity = 0.5f;

            ControlStyle comboButtonMinusStyle = new ControlStyle();
            comboButtonMinusStyle.Tiling = TextureMode.Grid;
            comboButtonMinusStyle.Grid = new Margin(1);
            comboButtonMinusStyle.Default.Texture = "combo_minus";
            comboButtonMinusStyle.Hot.Texture = "combo_minus_hot";
            comboButtonMinusStyle.Pressed.Texture = "combo_minus_down";
            comboButtonMinusStyle.Focused.Texture = "combo_minus_hot";
            comboButtonMinusStyle.Disabled.Texture = "combo_minus";
            comboButtonMinusStyle.Disabled.Opacity = 0.5f;

            ControlStyle flatComboButtonStyle = new ControlStyle();
            flatComboButtonStyle.Tiling = TextureMode.Grid;
            flatComboButtonStyle.Grid = new Margin(1);
            flatComboButtonStyle.Default.Texture = "flat_combo_button";
            flatComboButtonStyle.Hot.Texture = "flat_combo_button_hot";
            flatComboButtonStyle.Pressed.Texture = "flat_combo_button_down";
            flatComboButtonStyle.Focused.Texture = "flat_combo_button_hot";

            #endregion


            ControlStyle borderStyle = new ControlStyle();
            borderStyle.Hot.Texture = "border.dds";
            borderStyle.Pressed.Texture = "border.dds";
            borderStyle.Tiling = TextureMode.Grid;
            borderStyle.Grid = new Margin(4);

            ControlStyle labelStyle = new ControlStyle();
            labelStyle.TextAlign = Alignment.TopLeft;
            labelStyle.TextPadding = new Margin(2);
            labelStyle.Font = "White";
            labelStyle.TextColor = ColorInt.RGBA(1, 1, 1, 1);

            ControlStyle handleNW = new ControlStyle();
            handleNW.Texture = "handleNW.dds";

            ControlStyle handleNE = new ControlStyle();
            handleNE.Texture = "handleNE.dds";

            ControlStyle categoryStyle = new ControlStyle();
            categoryStyle.TextColor = ColorInt.RGBA(243.0f / 255.0f, 161.0f / 255.0f, 56.0f / 255.0f, 1);
            categoryStyle.Font = "White";


            Skin skin = new Squid.Skin();

            skin.Styles.Add("item", itemStyle);
            skin.Styles.Add("textbox", inputStyle);
            skin.Styles.Add("textboxError", inputErrorStyle);            
            skin.Styles.Add("button", buttonStyle);
            skin.Styles.Add("treeButton", treeButtonStyle);
            skin.Styles.Add("eyeButton", eyeButtonStyle);
            skin.Styles.Add("save", saveButtonStyle);
            skin.Styles.Add("load", loadButtonStyle);
            skin.Styles.Add("window", windowStyle);
            skin.Styles.Add("frame", frameStyle);
            skin.Styles.Add("listboxFrame", listBoxFrameStyle);
            skin.Styles.Add("checkBox", checkButtonStyle);
            skin.Styles.Add("hsplitter", hSplitterStyle);
            skin.Styles.Add("vsplitter", vSplitterStyle);
            skin.Styles.Add("comboItem", comboItemStyle);
            skin.Styles.Add("comboLabel", comboLabelStyle);
            skin.Styles.Add("comboButton", comboButtonStyle);
            skin.Styles.Add("comboPlusButton", comboButtonPlusStyle);
            skin.Styles.Add("comboMinusButton", comboButtonMinusStyle);
            skin.Styles.Add("commboListboxFrame", combopListBoxFrameStyle);
            skin.Styles.Add("flatComboButton", flatComboButtonStyle);
            skin.Styles.Add("vscrollTrack", vscrollTrackStyle);
            skin.Styles.Add("vscrollButton", vscrollButtonStyle);
            skin.Styles.Add("vscrollUp", vscrollUp);
            skin.Styles.Add("hscrollTrack", hscrollTrackStyle);
            skin.Styles.Add("hscrollButton", hscrollButtonStyle);
            skin.Styles.Add("hscrollUp", hscrollUp);
            skin.Styles.Add("multiline", labelStyle);
            skin.Styles.Add("tooltip", tooltipStyle);
            skin.Styles.Add("border", borderStyle);
            skin.Styles.Add("handleNE", handleNE);
            skin.Styles.Add("handleNW", handleNW);
            skin.Styles.Add("category", categoryStyle);
            
            return skin;
        }

        public static Frame AddFrame(this Control parent, string style, int width, int height, DockStyle dockStyle)
        {
            var frame = new Frame();
            frame.Parent = parent;
            if (style != null) frame.Style = style;
            frame.Size = new Point(width, height);
            frame.Dock = dockStyle;
            return frame;
        }

        public static Frame AddFrame(this Control parent, int width, int height, DockStyle dockStyle)
        {
            return AddFrame(parent, null, width, height, dockStyle);
        }

        public static Panel AddPanel(this Control parent, string style)
        {
            var panel = new Panel();
            panel.Parent = parent;
            if (style != null) panel.Style = style;
            panel.Dock = DockStyle.Fill;
            panel.VScroll.Size = new Squid.Point(13, 13);
            panel.VScroll.Slider.Style = "vscrollTrack";
            panel.VScroll.Slider.Button.Style = "vscrollButton";
            panel.VScroll.ButtonUp.Visible = false;
            panel.VScroll.ButtonDown.Visible = false;
            panel.VScroll.Slider.Margin = new Margin(0, 2, 0, 2);

            //panel.HScroll.Size = new Squid.Point(13, 13);
            //panel.HScroll.Slider.Style = "vscrollTrack";
            //panel.HScroll.Slider.Button.Style = "vscrollButton";
            //panel.HScroll.ButtonUp.Visible = false;
            //panel.HScroll.ButtonDown.Visible = false;
            //panel.HScroll.Slider.Margin = new Margin(0, 2, 0, 2);

            panel.Margin = new Margin(0);
            return panel;
        }

        public static ImageControl AddImage(this Control parent, string textureName ,int width, int height, DockStyle dockStyle)
        {
            var image = new ImageControl();
            image.Parent = parent;
            image.Dock = dockStyle;
            image.Size = new Point(width, height);
            image.Texture = textureName;
            return image;
        }

        public static ImageControl AddImage(this Control parent, string textureName, int x, int y, int width, int height)
        {
            var image = new ImageControl();
            image.Parent = parent;
            image.Position = new Point(x, y);
            image.Size = new Point(width, height);
            image.Texture = textureName;
            return image;
        }

        public static Button AddButton(this Control parent, string style,int width, int height, DockStyle dockStyle)
        {
            var button = new Button();
            button.Parent = parent;
            button.Size = new Point(width, height);
            button.Dock = dockStyle;
            button.Style = style;
            return button;
        }

        public static Button AddButton(this Control parent, string style, int x, int y, int width, int height)
        {
            var button = new Button();
            button.Parent = parent;
            button.Size = new Point(width, height);
            button.Position = new Point(x, y);
            button.Style = style;
            return button;
        }

        public static DropDownList AddDropDownList(this Control parent, int x, int y, int width)
        {
            var combo = new DropDownList();
            combo.Parent = parent;
            combo.Size = new Squid.Point(width, 20);
            combo.Position = new Point(x, y);
            combo.Label.Style = "comboLabel";
            combo.Button.Style = "flatComboButton";
            combo.Button.Size = new Point(10, 20);
            combo.Listbox.ClipFrame.Margin = new Margin(8, 8, 8, 8);
            combo.Listbox.Style = "commboListboxFrame";
            combo.Listbox.Scrollbar.Size = new Squid.Point(13, 13);
            combo.Listbox.Scrollbar.Slider.Style = "vscrollTrack";
            combo.Listbox.Scrollbar.Slider.Button.Style = "vscrollButton";
            combo.Listbox.Scrollbar.ButtonUp.Visible = false;
            combo.Listbox.Scrollbar.ButtonDown.Visible = false;
            combo.Listbox.Scrollbar.Slider.Margin = new Margin(0, 2, 0, 2);
            return combo;
        }

        public static Button AddDropDownButton(this Control parent, int x, int y, DropDownButtonType buttonType)
        {
            var dropDownButton = new Button();
            dropDownButton.Size = new Squid.Point(20, 20);
            dropDownButton.Position = new Point(x, y);
            switch (buttonType)
            {
                case DropDownButtonType.Plus: dropDownButton.Style = "comboPlusButton"; break;
                case DropDownButtonType.Minus: dropDownButton.Style = "comboMinusButton"; break;
            }            
            if (parent is IControlContainer) (parent as IControlContainer).Controls.Add(dropDownButton);
            return dropDownButton;
        }

        public static TreeView AddTreeView(this Control parent, int x, int y, int width, int height)
        {
            var treeView = new TreeView();
            treeView.Parent = parent;
            treeView.Size = new Point(width, height);
            treeView.Position = new Point(x, y);
            treeView.Style = "listboxFrame";
            treeView.ClipFrame.Margin = new Margin(4);
            treeView.Scrollbar.Margin = new Margin(1, 1, 1, 1);
            treeView.Scrollbar.Size = new Squid.Point(13, 13);
            treeView.Scrollbar.Slider.Style = "vscrollTrack";
            treeView.Scrollbar.Slider.Button.Style = "vscrollButton";
            treeView.Scrollbar.ButtonUp.Visible = false;
            treeView.Scrollbar.ButtonDown.Visible = false;
            treeView.Scrollbar.Slider.Margin = new Margin(0, 2, 0, 2);
            treeView.Indent = 15;
            return treeView;
        }

        public static TestSplitContainer AddSplitContainer(this Control parent, Orientation orientation)
        {
            var splitter = new TestSplitContainer();
            splitter.Parent = parent;
            if (orientation == Orientation.Horizontal) splitter.SplitButton.Style = "hsplitter"; else splitter.SplitButton.Style = "vsplitter";
            splitter.Orientation = orientation;
            splitter.SplitButton.Size = new Point(4, 4);
            splitter.Dock = DockStyle.Fill;
            splitter.SplitFrame1.Size = new Point(250, 100);
            splitter.SplitFrame2.Size = new Point(250, 200);
            return splitter;
        }

        public static CheckBox AddCheckBox(this Control parent, string syle = "checkBox", int width = 14, int height = 14)
        {
            var checkBox = new CheckBox();
            checkBox.Parent = parent.AddFrame(0, 0, DockStyle.Fill);
            checkBox.Size = new Point(width, height);
            checkBox.Dock = DockStyle.Center;
            checkBox.Style = syle;
            return checkBox;
        }

        public static TextBoxEx AddTextBox(this Control parent, int x, int y, int width)
        {
            var textBox = new TextBoxEx();
            textBox.Parent = parent;
            textBox.Size = new Point(width, 20);
            textBox.Style = "textbox";
            textBox.Position = new Point(x, y);
            return textBox;
        }

        public static TextBoxEx AddTextBox(this Control parent)
        {
            var textBox = new TextBoxEx();
            textBox.Parent = parent;
            textBox.Dock = DockStyle.Fill;
            textBox.Style = "textbox";
            return textBox;
        }

        public static Label AddLabel(this Control parent, int x, int y, string text)
        {
            var label = new Label();
            label.Parent = parent;
            label.Position = new Point(x, y);
            label.Style = "multiline";
            label.Text = text;
            return label;
        }

        public static Label AddLabel(this Control parent, int width, string text)
        {
            var label = new Label();
            label.Parent = parent;
            label.Size = new Point(width, 20);
            label.Dock = DockStyle.Left;
            label.Style = "multiline";
            label.Text = text;
            return label;
        }

        private static int tabStopIndex = 1;

        public static void ResetTabIndex(this Control parent)
        {
            tabStopIndex = 1;
        }

        public static StackPanel AddStackPanel(this Control parent)
        {
            var panel = new StackPanel();
            panel.Parent = parent;
            panel.Dock = DockStyle.Fill;
            return panel;
        }

        public static TextBox AddLabeledTextBox(this Control parent, int width, string text, float scale = 1.0f)
        {
            var frame = parent.AddFrame(0, 20, DockStyle.Top);
            frame.AddLabel(width, text);
            frame.Margin = new Margin(10, 0, 0, 0);            
            var textBox = frame.AddTextBox();
            textBox.TabIndex = tabStopIndex++;
            textBox.Scale = scale;
            return textBox;
        }

        public static TextBox AddLabeledFilename(this Control parent, int width, string text, string defaultExt, string filter, string title)
        {
            var frame = parent.AddFrame(0, 20, DockStyle.Top);
            frame.AddLabel(width, text);
            frame.Margin = new Margin(10, 0, 0, 0);

            var button = frame.AddButton("button", 20, 20, DockStyle.Right);
            button.Text = "...";

            var textBox = frame.AddTextBox();
            textBox.TabIndex = tabStopIndex++;

            button.MouseClick += (s, e) =>
            {
                var dialog = new System.Windows.Forms.OpenFileDialog();
                if (textBox.Text.Length > 0)
                {
                    dialog.FileName = textBox.Text;
                }
                dialog.DefaultExt = defaultExt;
                dialog.Filter = filter;
                dialog.Title = title;
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBox.Text = dialog.FileName;
                }
            };

            return textBox;
        }

        public static CheckBox AddLabeledCheckBox(this Control parent, int width, string text)
        {
            var frame = parent.AddFrame(0, 20, DockStyle.Top);
            frame.AddLabel(width, text);
            frame.Margin = new Margin(10, 0, 0, 0);
            var checkBox = frame.AddCheckBox();
            checkBox.TabIndex = tabStopIndex++;
            return checkBox;
        }
    }
}
