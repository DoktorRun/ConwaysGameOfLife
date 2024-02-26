using ConwaysGameOfLife_UI.Implementations;
using ConwaysGameOfLife_UI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ConwaysGameOfLife;

namespace ConwaysGameOfLife_UI.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public GameModel GameModel { get; }
        private DispatcherTimer _timer;

        string milliSeconds = string.Empty;
        const double DEFAULT_INTERVAL = 500;


        private bool _isRunning;

        public ObservableCollection<ObservableCollection<bool>> Cells { get; set; } = new();
        public GameViewModel(GameModel gameModel)
        {
            GameModel = gameModel;
            GameModel.GameRule = new Snake();
            Cells = ConvertArrayToObservableCollection(GameModel.Cells);

            _timer = new();
            _timer.Interval = TimeSpan.FromMilliseconds(DEFAULT_INTERVAL);
            //Ist ein Eventhandler der aufgerufen werden soll, jedes mal wenn ein Tick von Interval ausgeführt wird.
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            GameModel.NextGeneration();
            UpdateCells();
        }

        public void SetTimerMilliSeconds(double ms)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(ms);
        }

        public async void UpdateCells()
        {
            //await Task.Delay(50);
            //Dispatcher.Invoke wird genutzt, um auf dem UI Thread die Methode auszuführen.
            Application.Current.Dispatcher.Invoke(() =>
            {
                Cells = ConvertArrayToObservableCollection(GameModel.Cells);
                OnPropertyChanged(nameof(Cells));
            });
        }

        private ObservableCollection<ObservableCollection<bool>> ConvertArrayToObservableCollection(bool[,] cells)
        {
            ObservableCollection<ObservableCollection<bool>> result = new();

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                var row = new ObservableCollection<bool>();

                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    row.Add(cells[i, j]);
                }

                result.Add(row);
            }

            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine("TESTTESTTESTTEST");
        }

        public void StartGame()
        {
            _timer.Start();
        }

        public void PauseGame()
        {
            _timer.Stop();
        }
    }
}
