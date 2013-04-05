using Squid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TokED.UI
{
    public class Binding
    {
        private Control _source;
        private PropertyInfo _sourceProperty;
        private bool _applySourceChanges = true;
        private INotifyPropertyChanged _target;
        private PropertyInfo _targetProperty;
        private bool _applyTargetChanges = true;
        private Func<object, object> _targetValueConverter = null;

        public Binding(Control source, string sourcePropertyName, INotifyPropertyChanged target, string targetPropertyName, Func<object,object> targetValueConverter)
        {
            _targetValueConverter = targetValueConverter;

            _target = target;
            _targetProperty = target.GetType().GetProperty(targetPropertyName);
            _target.PropertyChanged += TargetPropertyChanged;

            _source = source;
            //If it's the default property we support two way binding
            if (sourcePropertyName == null) 
            {
                switch (_source.GetType().Name)
                {
                    case "TextBoxEx":
                        sourcePropertyName = "Text";
                        (_source as TextBoxEx).TextChanged += Binding_TextChanged;
                        (_source as TextBoxEx).CanTextCommit += Binding_CanTextCommit;
                        (_source as TextBoxEx).TextCommit += Binding_TextCommit;
                        break;

                    case "CheckBox":
                        sourcePropertyName = "Checked";
                        (_source as CheckBox).CheckedChanged += Binding_CheckedChanged;
                        break;
                }
            }
            if (sourcePropertyName == null) throw new NotImplementedException(string.Format("No default property defined for control {0}!", _source.GetType().Name));
            _sourceProperty = source.GetType().GetProperty(sourcePropertyName);
            TargetPropertyChanged(_source, null);
        }

        void Binding_CheckedChanged(Control sender)
        {
            SourcePropertyChanged(sender, null);
        }

        void Binding_CanTextCommit(object sender, CanCommitEventArgs e)
        {
            try
            {
                SourcePropertyChanged(sender, null);
            }
            catch (FormatException)
            {
                e.Cancel = true;
            }
        }

        void Binding_TextCommit(object sender, EventArgs e)
        {
            SourcePropertyChanged(sender, null);
        }

        void Binding_TextChanged(Control sender)
        {
            try
            {
                SourcePropertyChanged(sender, null);
                sender.Style = "textbox";
            }
            catch (FormatException)
            {
                sender.Style = "textboxError";
            }
        }

        void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_applySourceChanges)
            {
                var sourceValue = _sourceProperty.GetValue(_source, null);
                var targetValue = _targetProperty.GetValue(_target, null);
                if (!Object.Equals(sourceValue, targetValue))
                {
                    _applyTargetChanges = false;
                    _targetProperty.SetValue(_target, Transform(sourceValue, targetValue), null);
                    _applyTargetChanges = true;
                }
            }
        }

        void TargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_applyTargetChanges)
            {
                var sourceValue = _sourceProperty.GetValue(_source, null);
                var targetValue = _targetProperty.GetValue(_target, null);
                if (!Object.Equals(sourceValue, targetValue))
                {
                    _applySourceChanges = false;
                    _sourceProperty.SetValue(_source, Transform(_targetValueConverter == null ? targetValue : _targetValueConverter(targetValue), sourceValue), null);
                    _applySourceChanges = true;
                }
            }
        }

        private object Transform(object source, object target)
        {
            switch (source.GetType().Name)
            {
                case "Single":
                    switch (target.GetType().Name)
                    {
                        case "String": return source.ToString();
                    }
                    break;

                case "Boolean":
                    switch (target.GetType().Name)
                    {
                        case "String": return source.ToString();
                    }
                    break;

                case "String":
                    switch (target.GetType().Name)
                    {
                        case "Int32": return Convert.ToInt32(source);
                        case "Boolean": return Convert.ToBoolean(source);
                        case "Single": return Convert.ToSingle(source);
                    }
                    break;

                case "Int32":
                    switch (target.GetType().Name)
                    {
                        case "String": return source.ToString();
                    }
                    break;
            }
            return source;
        }

        public void Dispose()
        {
            _target.PropertyChanged -= TargetPropertyChanged;
            switch (_source.GetType().Name)
            {
                case "TextBoxEx":
                    (_source as TextBoxEx).TextChanged -= Binding_TextChanged;
                    (_source as TextBoxEx).TextCommit -= Binding_TextCommit;
                    (_source as TextBoxEx).CanTextCommit -= Binding_CanTextCommit;
                    break;

                case "CheckBox":
                    (_source as CheckBox).CheckedChanged -= Binding_CheckedChanged;
                    break;
            }
        }
    }

    public static class Bindings
    {
        private static Dictionary<Control, List<Binding>> bindings = new Dictionary<Control, List<Binding>>();

        public static Control Bind(this Control control, INotifyPropertyChanged o, string property)
        {
            return Bind(control, null, o, property, null);
        }

        public static Control Bind(this Control control, INotifyPropertyChanged o, string property, Func<object, object> targetValueConverter)
        {
            return Bind(control, null, o, property, targetValueConverter);
        }

        public static Control Bind(this Control control, string controlProperty, INotifyPropertyChanged o, string property)
        {
            return Bind(control, controlProperty, o, property, null);
        }

        public static Control Bind(this Control control, string controlProperty, INotifyPropertyChanged o, string property, Func<object, object> targetValueConverter)
        {
            if (!bindings.ContainsKey(control))
            {
                bindings.Add(control, new List<Binding>());
            }

            bindings[control].Add(new Binding(control, controlProperty, o, property, targetValueConverter));
            return control;
        }

        public static void UnBind(this Control control)
        {
            if (bindings.ContainsKey(control))
            {
                foreach (var binding in bindings[control])
                {
                    binding.Dispose();
                }
                bindings.Remove(control);
            }
            if (control is IControlContainer)
            {
                foreach (var childControl in (control as IControlContainer).Controls)
                {
                    UnBind(childControl);
                }
            }
        }
    }
}
