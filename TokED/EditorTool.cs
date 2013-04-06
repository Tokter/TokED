using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokED.Editors;

namespace TokED
{
    [Flags]
    public enum Modifier
    {
        None = 0,
        Shift = 1,
        Control = 2,
        Alt = 4,
    }

    public enum ToolEventType { KeyDown, KeyUp, MouseDown, MouseUp, MouseMove, MouseWheel }

    public struct ToolEvent
    {
        public ToolEventType EventType;
        public Modifier Modifiers;
        public Key Key;
        public MouseButton Button;

        public static ToolEvent CreateDown(Key key)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.KeyDown;
            se.Modifiers = Modifier.None;
            se.Key = key;
            return se;
        }

        public static ToolEvent CreateDown(Key key, Modifier modifier)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.KeyDown;
            se.Modifiers = modifier;
            se.Key = key;
            return se;
        }

        public static ToolEvent CreateDown(MouseButton button)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseDown;
            se.Modifiers = Modifier.None;
            se.Button = button;
            return se;
        }

        public static ToolEvent CreateDown(MouseButton button, Modifier modifier)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseDown;
            se.Modifiers = modifier;
            se.Button = button;
            return se;
        }

        public static ToolEvent CreateUp(Key key)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.KeyUp;
            se.Modifiers = Modifier.None;
            se.Key = key;
            return se;
        }

        public static ToolEvent CreateUp(Key key, Modifier modifier)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.KeyUp;
            se.Modifiers = modifier;
            se.Key = key;
            return se;
        }

        public static ToolEvent CreateUp(MouseButton button)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseUp;
            se.Modifiers = Modifier.None;
            se.Button = button;
            return se;
        }

        public static ToolEvent CreateUp(MouseButton button, Modifier modifier)
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseUp;
            se.Modifiers = modifier;
            se.Button = button;
            return se;
        }

        public static ToolEvent CreateMouseMove()
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseMove;
            se.Modifiers = Modifier.None;
            return se;
        }

        public static ToolEvent CreateMouseWheel()
        {
            var se = new ToolEvent();
            se.EventType = ToolEventType.MouseWheel;
            se.Modifiers = Modifier.None;
            return se;
        }

        public static bool operator ==(ToolEvent x, ToolEvent y)
        {
            if (x.EventType != y.EventType) return false;

            switch (x.EventType)
            {
                case ToolEventType.KeyDown:
                case ToolEventType.KeyUp:
                    return x.Key == y.Key && x.Modifiers == y.Modifiers;

                case ToolEventType.MouseDown:
                case ToolEventType.MouseUp:
                    return x.Button == y.Button && x.Modifiers == y.Modifiers;

                default: return x.Modifiers == y.Modifiers;
            }
        }

        public static bool operator !=(ToolEvent x, ToolEvent y)
        {
            if (x.EventType != y.EventType) return true;

            switch (x.EventType)
            {
                case ToolEventType.KeyDown:
                case ToolEventType.KeyUp:
                    return x.Key != y.Key || x.Modifiers != y.Modifiers;

                case ToolEventType.MouseDown:
                case ToolEventType.MouseUp:
                    return x.Button != y.Button || x.Modifiers != y.Modifiers;

                default: return x.Modifiers != y.Modifiers;
            }
        }
    }

    public class EditorTool
    {
        private ToolEvent _trigger;
        private bool _stayActivated = false;

        public EditorTool(ToolEvent trigger, bool stayActivated)
        {
            _trigger = trigger;
            _stayActivated = stayActivated;
        }

        public ToolEvent Trigger 
        {
            get { return _trigger; }
        }

        public Editor Editor { get; set; }

        public Tools StateStack { get; set; }

        public virtual void Mouse_Move(MouseMoveEventArgs e)
        {
        }

        public virtual void Mouse_Wheel(MouseWheelEventArgs e)
        {
        }

        public virtual void Mouse_Button(MouseButtonEventArgs e)
        {
        }

        public virtual void Key_Down(KeyboardKeyEventArgs e)
        {
        }

        public virtual void Key_Up(KeyboardKeyEventArgs e)
        {
        }

        public bool StaysActivated
        {
            get { return _stayActivated; }
        }

        public virtual void Activated()
        {
        }

        public virtual bool Availbable()
        {
            return false;
        }

        /// <summary>
        /// if the state stays activated, then the state has to set this property to true once the state is done doing it's thing.
        /// </summary>
        public bool Done { get; set; }

        public string ExportName
        {
            get
            {
                var t = this.GetType();
                var atr = t.GetCustomAttributes(typeof(ExportAttribute), false);
                if (atr != null && atr.Length > 0 && atr[0] is ExportAttribute)
                {
                    return (atr[0] as ExportAttribute).ContractName;
                }
                return null;
            }
        }
    }
}
