using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            Function.Task = task.Text.Split(" ");
            Function.Step = double.Parse(StepX.Text);
            Function.Start = double.Parse(startRange.Text);
            Function.End = double.Parse(endRange.Text);
            Result.Text = "";
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
            IDrawer drawer = new IDrawer();
            drawer.DrawAll();
            if(Function.Task.ToString() != task.Text || Function.Step != double.Parse(StepX.Text) || Function.Start != double.Parse(startRange.Text) || Function.End != double.Parse(endRange.Text))
            {
                Field.Children.Clear();
                
                drawer.DrawAll();
            }
        }

        private void zoomUp_Click(object sender, RoutedEventArgs e)
        {

            Transform.ScaleX += 0.1;
            Transform.ScaleY += 0.1;
        }

        private void zoomDown_Click(object sender, RoutedEventArgs e)
        {
            Transform.ScaleX -= 0.1;
            Transform.ScaleY -= 0.1;
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
            var current = Mouse.GetPosition(this);
            var offset = current - pointDragAndDrop;
            if (isDragAndDrop)
            {
                current = Mouse.GetPosition(this);
                offset = current - pointDragAndDrop; // разница в координатах между текущей и новой
                Field.Margin = new Thickness(marginDragAndDrop.Left + offset.X, marginDragAndDrop.Top + offset.Y, 0, 0);

                // само перемещение
            }
        }


    }
    
    
}

    


