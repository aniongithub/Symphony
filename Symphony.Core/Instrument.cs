using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;

namespace Symphony.Core
{
    public interface IInstrument
    {
        IObservable<PropertyInfo> Inputs { get; }
        IObservable<PropertyInfo> Outputs { get; }
    }
}

// Our mixin
public partial struct Instrument
{
    private static IObservable<PropertyInfo> _inputs;
    private static IObservable<PropertyInfo> _outputs;

    public IObservable<PropertyInfo> Inputs
    {
        get
        {
            if (_inputs == null)
                _inputs = (from property in GetType().GetProperties(BindingFlags.Default)
                           where property.GetCustomAttributes<Symphony.Core.InputAttribute>().Any()
                           select property).ToObservable();
            return _inputs;
        }
    }

    public IObservable<PropertyInfo> Outputs
    {
        get
        {
            if (_outputs == null)
                _outputs = (from property in GetType().GetProperties(BindingFlags.Default)
                            where property.GetCustomAttributes<Symphony.Core.OutputAttribute>().Any()
                            select property).ToObservable();
            return _outputs;
        }
    }
}
