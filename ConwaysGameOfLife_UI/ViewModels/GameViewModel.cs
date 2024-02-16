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

namespace ConwaysGameOfLife_UI.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private GameModel _gameModel;
        private DispatcherTimer _timer;

        public ObservableCollection<ObservableCollection<bool>> Cells { get; set; } = new();
        public GameViewModel(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.GameRule = new ConwayClassicRuleset();
            Cells = ConvertArrayToObservableCollection(_gameModel.Cells);

            _timer = new();
            _timer.Interval = TimeSpan.FromMilliseconds(250);
            //Ist ein Eventhandler der aufgerufen werden soll, jedes mal wenn ein Tick von Interval ausgeführt wird.
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _gameModel.NextGeneration();
            UpdateCells();
        }

        private void UpdateCells()
        {
            //Dispatcher.Invoke wird genutzt, um auf dem UI Thread die Methode auszuführen.
            Application.Current.Dispatcher.Invoke(() =>
            {
                Cells = ConvertArrayToObservableCollection(_gameModel.Cells);
                OnPropertyChanged(nameof(Cells));
            });
        }

        private ObservableCollection<ObservableCollection<bool>> ConvertArrayToObservableCollection(bool[,] cells)
        {
            ObservableCollection<ObservableCollection<bool>> result = new ObservableCollection<ObservableCollection<bool>>();

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
        }

        public void StartGame()
        {
            _timer.Start();
        }
    }
}
