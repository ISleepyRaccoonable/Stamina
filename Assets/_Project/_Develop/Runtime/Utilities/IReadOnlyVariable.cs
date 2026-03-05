using System;

namespace Assets.Project._Develop.Runtime.Utilities
{
    public interface IReadOnlyVariable<T>
    {
        T Value { get; }

        IDisposable Subscribe(Action<T,T> action);
    }
}