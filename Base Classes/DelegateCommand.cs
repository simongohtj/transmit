﻿using System;
using System.Windows.Input;

namespace Transmit
{
    class DelegateCommand : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
