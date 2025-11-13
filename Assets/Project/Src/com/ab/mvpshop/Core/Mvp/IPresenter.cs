using System;
using Zenject;

namespace com.ab.mvpshop.core.mvp
{
    public interface IPresenter : IDisposable
    {
        void Initialize();

        void Subscribe(SignalBus signals);

        void Unsubscribe(SignalBus signals);
    }
}