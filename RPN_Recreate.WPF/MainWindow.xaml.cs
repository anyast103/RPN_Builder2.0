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
using RPN_Recreate;
using RPN_Recreate.WPF.Models;

namespace RPN_Recreate.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            Function.Task = task.Text.Split(" ");
            Function.Step = double.Parse(StepX.Text);
            Function.Start = double.Parse(startRange.Text);
            Function.End = double.Parse(endRange.Text);
            foreach (var i in RPN_Recreate.Postfix.GetExpression(Function.Task))
            {
                Result.Text += i;
            }

            List<ModelResult> resultts = new List<ModelResult>();
            new Calculating(RPN_Recreate.Postfix.GetExpression(Function.Task));
            for (var i = 0; i < Calculating.ResultList.Count; i++)
            {
                resultts.Add(new ModelResult(Calculating.XList[i].ToString(), Calculating.ResultList[i].ToString()));
            }
            dgRes.ItemsSource = resultts;
            Draw();
        }

        private void Draw()
        {
            DrawOXOY();
            XMarks();
            YMarks();
            DrawNumsX();
            DrawNumsY();
            DrawFunction();
        }

        private void DrawOXOY()
        {
            Field.Children.Clear();
            var oX = new Line();

            //var height = Field.ActualHeight;
            //var width = Field.ActualWidth;

            oX.X1 = 0;
            oX.Y1 = Field.Height / 2;
            oX.X2 = Field.Width;
            oX.Y2 = Field.Height / 2;
            oX.Stroke = Brushes.Black;
            oX.StrokeThickness = 2;

            var oY = new Line();
            oY.X1 = Field.Width / 2;
            oY.Y1 = 0;
            oY.X2 = Field.Width / 2;
            oY.Y2 = Field.Height;
            oY.Stroke = Brushes.Black;
            oY.StrokeThickness = 2;

            Field.Children.Add(oX);

            Field.Children.Add(oY);
        }

        private void XMarks()
        {
            var height = Field.ActualHeight;
            var width = Field.ActualWidth;

            for (int i = 0; i <= width / 40; i++)
            {
                var markX = new Ellipse()
                {
                    Width = 5,
                    Height = 5,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(markX, i * 50 - 10);
                Canvas.SetTop(markX, height / 2 - 3);

                Field.Children.Add(markX);
            }
        }
        private void YMarks()
        {
            var height = Field.ActualHeight;
            var width = Field.ActualWidth;

            for (int i = 0; i <= height / 50; i++)
            {
                var markY = new Ellipse()
                {
                    Width = 5,
                    Height = 5,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(markY, width / 2 - 3);
                Canvas.SetTop(markY, i * 50 + 3);

                Field.Children.Add(markY);
            }
        }
        private void DrawNumsY()
        {
            var height = Field.ActualHeight;
            var width = Field.ActualWidth;
            for (double i = Math.Round(height / 80, 1); i >= height / -80; i--)
            {
                Label numY = new Label();
                numY.Content = $"{Math.Round(i, 0)}";
                Canvas.SetLeft(numY, width / 2 + 6);
                Canvas.SetTop(numY, (height / 80 - i) * 40 - 15);
                Field.Children.Add(numY);
            }
        }
        private void DrawNumsX()
        {
            var height = Field.ActualHeight;
            var width = Field.ActualWidth;
            for (double i = Math.Round(width / -80, 1); i <= width / 80; i++)
            {
                Label numX = new Label();
                numX.Content = $"{Math.Round(i, 0)}";
                Canvas.SetLeft(numX, (i + width / 80) * 40 - 8);
                Canvas.SetTop(numX, height / 2 + 6);
                Field.Children.Add(numX);
            }
        }
        private void DrawFunction()
        {
            var height = Field.ActualHeight;
            var width = Field.ActualWidth;
            var func = new Polyline();
            func.Points = new PointCollection();

            for (var i = 0; i < Calculating.ResultList.Count; i++)
            {
                if (double.IsNaN(Calculating.ResultList[i]))
                    continue;
                func.Points.Add(new Point(width / 2 + 40 * Calculating.XList[i], Math.Round(height / 2 - 40 * Calculating.ResultList[i], 12)));
            }
            func.Stroke = Brushes.Black;
            Field.Children.Add(func);
        }

        private bool isDragAndDrop; // перемещено или нет
        private Point pointDragAndDrop; // точка перемещения
        private Thickness marginDragAndDrop;
        private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragAndDrop = false; // Не перемещается, т.к false
        }
        private void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            isDragAndDrop = false; // Не перемещается, т.к false
        }
        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragAndDrop = true;
            pointDragAndDrop = Mouse.GetPosition(this); // передаём текущее значение позиции курсора
            marginDragAndDrop = Field.Margin;
        }
        private void CanvasMove(object sender, MouseEventArgs e)
        {
            if (isDragAndDrop)
            {
                var current = Mouse.GetPosition(this);
                var offset = current - pointDragAndDrop; // разница в координатах между текущей и новой
                Field.Margin = new Thickness(marginDragAndDrop.Left + offset.X, marginDragAndDrop.Top + offset.Y, 0, 0); // само перемещение
            }
        }
    }

}
