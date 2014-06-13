namespace Samples.Wpf.Utils
{
    using System;
    using System.ComponentModel;

    public class CancellableCommand : StyleMVVM.ViewModel.DelegateCommand
    {
        public CancellableCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
        }

        public CancellableCommand(Action<object> executeMethod)
            : base(executeMethod)
        {
        }

        public override void Execute(object parameter)
        {
            var eventHandler = Executing;
            var cancelEventArgs = new CancelEventArgs(true);
            if (eventHandler != null)
            {
                eventHandler(parameter, cancelEventArgs);
            }

            if (!cancelEventArgs.Cancel)
            {
                base.Execute(parameter);
            }
        }

        public event CancelEventHandler Executing;

    }

}