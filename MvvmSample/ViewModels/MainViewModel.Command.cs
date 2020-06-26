using MvvmSample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MvvmSample.ViewModels
{
    public partial class MainViewModel
    {

        private bool _canExecuteSimpleCommand = true;

        private bool _canAsyncExecuteSimpleCommand = true;

        private DispatcherTimer _dispatcherTimer;

        //Func<bool> _canExecuteSimpleCommand;

        private RelayCommand _simpleCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand SimpleCommand
        {
            get
            {
                return _simpleCommand ?? (_simpleCommand = new RelayCommand(ExecuteSimpleCommand));
            }
        }

        private RelayCommand _timeCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand TimeCommand
        {
            get
            {
                return _timeCommand ?? (_timeCommand = new RelayCommand(ExecuteTimeCommand, () => { return _canExecuteSimpleCommand; }));
            }
        }

        private RelayCommand _timeAsyncCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand TimeAsyncCommand
        {
            get
            {
                return _timeAsyncCommand ?? (_timeAsyncCommand = new RelayCommand(ExecuteTimeAsyncCommand, () => { return _canAsyncExecuteSimpleCommand; }));
            }
        }



        private RelayCommand _changeCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ChangeCommand
        {
            get
            {
                return _changeCommand ?? (_changeCommand = new RelayCommand(ExecuteChangeCommand));
            }
        }

        private void ExecuteChangeCommand()
        {
            if (string.IsNullOrEmpty(InputTxt))
            {
                SampleText = TITLE_CONST;
            }

            SampleText = string.IsNullOrEmpty(InputTxt) ? TITLE_CONST : InputTxt;
        }

        private void ExecuteSimpleCommand()
        {
            MessageBox.Show("接收到点击");

        }

        private void ExecuteTimeCommand()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(Seconds);
            _dispatcherTimer.Tick += (s, e) =>
            {
                _canExecuteSimpleCommand = true;
                CommandManager.InvalidateRequerySuggested();
                _dispatcherTimer.Stop();
            };
            _dispatcherTimer.Start();
            _canExecuteSimpleCommand = false;
        }

        private void ExecuteTimeAsyncCommand()
        {
            Task.Run(async () =>    //如果采用异步更改CanExecute，UI更新会出问题。
            {
                await Task.Delay(TimeSpan.FromSeconds(Seconds));
                _canAsyncExecuteSimpleCommand = true;
                App.Current.Dispatcher.Invoke(() => //拉回UI线程
                {
                    CommandManager.InvalidateRequerySuggested();//强制CommandManager重新检查ICommand,必须在UI线程上调用否则将没有效果
                });
            });
            _canAsyncExecuteSimpleCommand = false;
        }
    }
}
