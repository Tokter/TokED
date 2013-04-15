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
        private object _target;
        private PropertyInfo _targetProperty;
        private FieldInfo _targetField;
        private bool _applyTargetChanges = true;
        private Func<object, object> _targetValueConverter = null;

        public Binding(Control source, string sourcePropertyName, object target, string targetPropertyName, Func<object,object> targetValueConverter)
        {
            _targetValueConverter = targetValueConverter;

            _target = target;
            _targetProperty = target.GetType().GetProperty(targetPropertyName);
            _targetField = target.GetType().GetField(targetPropertyName);
            if (_target is INotifyPropertyChanged)
            {
                (_target as INotifyPropertyChanged).PropertyChanged += TargetPropertyChanged;
            }

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

                    case "DropDownList":
                        sourcePropertyName = "SelectedItem";
                        (_source as DropDownList).SelectedItemChanged += Binding_SelectedItemChanged;
                        break;

                    case "ColorButton":
                        sourcePropertyName = "Color";
                        (_source as ColorButton).ColorChanged += Binding_ColorChanged;
                        break;
                }
            }
            if (sourcePropertyName == null) throw new NotImplementedException(string.Format("No default property defined for control {0}!", _source.GetType().Name));
            _sourceProperty = source.GetType().GetProperty(sourcePropertyName);
            TargetPropertyChanged(_source, null);
        }

        void Binding_ColorChanged(object sender, EventArgs e)
        {
            SourcePropertyChanged(sender, null);
        }

        void Binding_SelectedItemChanged(Control sender, ListBoxItem value)
        {
            SourcePropertyChanged(sender, null);
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
                object targetValue = null;
                if (_targetProperty!=null) targetValue = _targetProperty.GetValue(_target, null);
                if (_targetField != null) targetValue = _targetField.GetValue(_target);
                if (!Object.Equals(sourceValue, targetValue))
                {
                    _applyTargetChanges = false;
                    if (_targetProperty != null) _targetProperty.SetValue(_target, Transform(sourceValue, targetValue, _source, _target), null);
                    if (_targetField != null) _targetField.SetValue(_target, Transform(sourceValue, targetValue, _source, _target));
                    _applyTargetChanges = true;
                }
            }
        }

        void TargetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_applyTargetChanges)
            {
                var sourceValue = _sourceProperty.GetValue(_source, null);
                object targetValue = null;
                if (_targetProperty != null) targetValue = _targetProperty.GetValue(_target, null);
                if (_targetField != null) targetValue = _targetField.GetValue(_target);
                if (!Object.Equals(sourceValue, targetValue))
                {
                    _applySourceChanges = false;
                    _sourceProperty.SetValue(_source, Transform(_targetValueConverter == null ? targetValue : _targetValueConverter(targetValue), sourceValue, _target, _source), null);
                    _applySourceChanges = true;
                }
            }
        }

        private string GetTypeName(object value, object source)
        {
            if (source is Enum) return "Enum";
            if (source is DropDownList) return "DropDownList";
            if (value == null) return source.GetType().Name;
            if (value is Enum) return "Enum";
            return value.GetType().Name;
        }

        private object Transform(object sourceValue, object targetValue, object source, object target)
        {
            switch (GetTypeName(sourceValue, source))
            {
                case "DropDownList":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "Enum":
                            return Enum.Parse(targetValue.GetType(), (sourceValue as ListBoxItem).Text);

                        case "String":
                            return (sourceValue as ListBoxItem).Text;
                    }
                    break;

                case "Enum":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "DropDownList":
                            foreach (var item in (target as DropDownList).Items)
                            {
                                if (item.Text == sourceValue.ToString()) return item;
                            }
                            break;
                    }
                    break;

                case "Single":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "String": return sourceValue.ToString();
                    }
                    break;

                case "Boolean":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "String": return sourceValue.ToString();
                    }
                    break;

                case "String":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "Int32": return Convert.ToInt32(sourceValue);
                        case "Boolean": return Convert.ToBoolean(sourceValue);
                        case "Single": return Convert.ToSingle(sourceValue);
                        case "DropDownList":
                            foreach (var item in (target as DropDownList).Items)
                            {
                                if (item.Text == sourceValue.ToString()) return item;
                            }
                            break;
                    }
                    break;

                case "Int32":
                    switch (GetTypeName(targetValue, target))
                    {
                        case "String": return sourceValue.ToString();
                    }
                    break;
            }
            return sourceValue;
        }

        public void Dispose()
        {
            if (_target is INotifyPropertyChanged)
            {
                (_target as INotifyPropertyChanged).PropertyChanged -= TargetPropertyChanged;
            }
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

                case "DropDownList":
                    (_source as DropDownList).SelectedItemChanged -= Binding_SelectedItemChanged;
                    break;


                case "ColorButton":
                    (_source as ColorButton).ColorChanged -= Binding_ColorChanged;
                    break;
            }
        }
    }

    public static class Bindings
    {
        private static Dictionary<Control, List<Binding>> bindings = new Dictionary<Control, List<Binding>>();

        public static Control Bind(this Control control, object o, string property)
        {
            return Bind(control, null, o, property, null);
        }

        public static Control Bind(this Control control, object o, string property, Func<object, object> targetValueConverter)
        {
            return Bind(control, null, o, property, targetValueConverter);
        }

        public static Control Bind(this Control control, string controlProperty, object o, string property)
        {
            return Bind(control, controlProperty, o, property, null);
        }

        public static Control Bind(this Control control, string controlProperty, object o, string property, Func<object, object> targetValueConverter)
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
