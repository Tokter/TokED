using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TokED.Editors
{
    class POCOBinding : IDisposable
    {
        private INotifyPropertyChanged _source;
        private PropertyInfo _sourceProperty;
        private bool _applySourceChanges = true;
        private INotifyPropertyChanged _target;
        private PropertyInfo _targetProperty;
        private bool _applyTargetChanges = true;

        public POCOBinding(INotifyPropertyChanged source, string sourcePropertyName, INotifyPropertyChanged target, string targetPropertyName)
        {
            _source = source;
            _sourceProperty = source.GetType().GetProperty(sourcePropertyName);
            _target = target;
            _targetProperty = target.GetType().GetProperty(targetPropertyName);

            _source.PropertyChanged += SourcePropertyChanged;
            _target.PropertyChanged += TargetPropertyChanged;
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
                    _sourceProperty.SetValue(_source, Transform(targetValue, sourceValue), null);
                    _applySourceChanges = true;
                }
            }
        }

        private object Transform(object source, object target)
        {
            switch (source.GetType().Name)
            {
                case "Vector2":
                    switch (target.GetType().Name)
                    {
                        case "Vector3": return new Vector3((Vector2)source);
                    }
                    break;

                case "Vector3":
                    switch (target.GetType().Name)
                    {
                        case "Vector2": return new Vector2(((Vector3)source).X, ((Vector3)source).Y);
                    }
                    break;
            }
            return source;
        }

        public void Dispose()
        {
            _source.PropertyChanged -= SourcePropertyChanged;
            _target.PropertyChanged -= TargetPropertyChanged;
        }
    }

    public class BindingManager : IDisposable
    {
        private List<POCOBinding> _bindings = new List<POCOBinding>();

        public void Bind(INotifyPropertyChanged source, string sourcePropertyName, INotifyPropertyChanged target, string targetPropertyName)
        {
            _bindings.Add(new POCOBinding(source, sourcePropertyName, target, targetPropertyName));
        }

        public void Clear()
        {
            foreach (var binding in _bindings)
            {
                binding.Dispose();
            }
            _bindings.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
