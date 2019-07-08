using Microsoft.Win32;
using ProbaWPF.mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProbaWPF
{
    class MainWindowViewModel : ViewModel
    {
        private string _FileName;
        public string FileName
        {
            get => _FileName;
            set => Set(ref _FileName, value);
        }

        private string _Title = "Test Application";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        private int? _Result;

        public int? Result
        {
            get => _Result;
            set => Set(ref _Result, value);
        }
        private double _ComputeProgress;

        public double ComputeProgress
        {
            get => _ComputeProgress;
            set => Set(ref _ComputeProgress, value);
        }

        public ICommand ComputeSummComand { get; }

        public ICommand CancelSummCalculationCommand { get; }

        public ICommand OpenFileCommand { get; }
        public ICommand FileToDataBaseCommand { get; }

        public MainWindowViewModel()
        {
            ComputeSummComand = new LamdaCommand(OnComputeSummCommandExecuted, CanComputeSummCommandExecute);
            CancelSummCalculationCommand = new LamdaCommand(OnCancelSummCalculationCommandExecuted, CanCancelSummCalculationCommandExecute);
            OpenFileCommand = new LamdaCommand(OnOpenFileCommandExecuted);
            FileToDataBaseCommand = new LamdaCommand(OnFileToDataBaseCommand, CanFileToDataBaseCommandExecute);
        }

        private bool CanFileToDataBaseCommandExecute(object p)
        {
            return !(string.IsNullOrEmpty(FileName) 
                || FileName == "[Файл не выбран]" 
                || !File.Exists(FileName) 
                || Path.GetExtension(FileName).ToLower() != ".csv");
        }

        private async void OnFileToDataBaseCommand(object p)
        {
            await Task.Run(() => Debug.WriteLine("Load to DataBfse is OK!"));
        }



        private void OnOpenFileCommandExecuted(object Obj)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выбор файла для загрузки",
                Filter = "Csv-файлы (*.csv)|*.csv|Все файлы (*.*)|*.*"
            };
            if (dialog.ShowDialog() is true) //return;
            {
                var file_name = dialog.FileName;
                var file_extension = Path.GetExtension(file_name).ToLower();
                switch (file_extension)
                {
                    case ".csv":
                        FileName = file_name;
                        break;
                    default:
                        break;
                }
            }
        }
        private bool CanCancelSummCalculationCommandExecute(object p) => _ComputeSummCancellation != null;

        private void OnCancelSummCalculationCommandExecuted(object p) => _ComputeSummCancellation?.Cancel();

        private bool CanComputeSummCommandExecute(object p) => _CanComputeExecute;

        private bool _CanComputeExecute = true;
        private CancellationTokenSource _ComputeSummCancellation;

        private async void OnComputeSummCommandExecuted(object p)
        {
            _CanComputeExecute = false;

            IProgress<double> progress = new Progress<double>(percent => ComputeProgress = percent);
            var cancellation_token_source = new CancellationTokenSource();
            _ComputeSummCancellation = cancellation_token_source;

            var cancel = cancellation_token_source.Token;
            try
            {
                var result = await ComputeSummAsync(100, Progress: progress, Cancel: cancel);
                Result = result;
            }
            catch (OperationCanceledException e)
            {
                Result = null;
                progress.Report(0);
            }
            _CanComputeExecute = true;
            _ComputeSummCancellation = null;
            CommandManager.InvalidateRequerySuggested();
        }

        private static Task<int> ComputeSummAsync(int N, int Timeout = 75,
            IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            return Task.Run(() => ComputeSumm(N, Timeout, Progress, Cancel));
        }

        private static int ComputeSumm(int N, int Timeout = 75,
            IProgress<double> Progress = null, CancellationToken Cancel = default)
        {
            var result = 0;
            for (var i = 0; i <= N; i++)
            {
                result += i;
                Thread.Sleep(Timeout);
                Progress?.Report(i / (double)N);

                //if (Cancel.IsCancellationRequested) return 0;
                Cancel.ThrowIfCancellationRequested();
                //throw new OperationCanceledException();
            }

            Progress?.Report(1);
            return result;
        }
    }
}
