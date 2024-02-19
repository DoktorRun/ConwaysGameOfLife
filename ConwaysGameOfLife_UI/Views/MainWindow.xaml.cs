using ConwaysGameOfLife_UI.Helpers;
using ConwaysGameOfLife_UI.Models;
using ConwaysGameOfLife_UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConwaysGameOfLife_UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool simulationIsRunning = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateField(object sender, RoutedEventArgs e)
        {
            DataContext = new GameViewModel(new GameModel(15, 15));
            GameViewModel viewModel = (GameViewModel)DataContext;

            gameCanvas.Children.Clear();

            for (int row = 0; row < viewModel.Cells.Count; row++)
            {
                for (int col = 0; col < viewModel.Cells[row].Count; col++)
                {
                    Rectangle rect = new();

                    rect.Width = gameCanvas.ActualWidth / viewModel.Cells[row].Count - 1;
                    rect.Height = gameCanvas.ActualHeight / viewModel.Cells.Count - 1;

                    Binding binding = new Binding($"Cells[{row}][{col}]");
                    binding.Source = viewModel;
                    binding.Converter = new BoolToColorConverter();

                    rect.SetBinding(Rectangle.FillProperty, binding);

                    gameCanvas.Children.Add(rect);
                    Canvas.SetLeft(rect, col * gameCanvas.ActualWidth / viewModel.Cells[row].Count);
                    Canvas.SetTop(rect, row * gameCanvas.ActualHeight / viewModel.Cells.Count);
                    rect.MouseDown += FlipState;
                }
            }
        }

        private void FlipState(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            GameViewModel viewModel = (GameViewModel)DataContext;

            int row = (int)(Math.Ceiling(Canvas.GetTop(rect) / gameCanvas.ActualHeight * viewModel.Cells.Count));
            int col = (int)(Math.Ceiling(Canvas.GetLeft(rect) / gameCanvas.ActualWidth * viewModel.Cells[row].Count));
            viewModel.GameModel.FlipCell(row, col);
            viewModel.UpdateCells();
        }

        private void StartSimulation(object sender, RoutedEventArgs e)
        {
            GameViewModel viewModel = (GameViewModel)DataContext;
            if (!simulationIsRunning)
            {
                SimulationStatusButton.Content = "Pause simulation!";
                viewModel.SetTimerMilliSeconds(double.Parse(TimerMSText.Text));
                viewModel.StartGame();
                simulationIsRunning = true;
            }
            else
            {
                SimulationStatusButton.Content = "Start simulation!";
                viewModel.PauseGame();
                simulationIsRunning = false;
            }
            
        }
    }
}
