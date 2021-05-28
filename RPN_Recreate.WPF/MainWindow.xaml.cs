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
using RPN_Recrate.Consolee;
using System.Text.RegularExpressions;

namespace RPN_Recreate.WPF
{
    public partial class MainWindow : Window
    {
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            Function.Task = task.Text;
            string[] Funct = Regex.Split(Function.Task, @"-?([\W])");
            Function.Step = double.Parse(StepX.Text);
            Function.Start = double.Parse(startRange.Text);
            Function.End = double.Parse(endRange.Text);
            Result.Text = "";
            foreach (var i in RPN_Recreate.Postfix.GetExpression(Funct))
            {
                Result.Text += i;
            }
            List<ModelResult> Results = new List<ModelResult>();
            new Calculating(RPN_Recreate.Postfix.GetExpression(Funct));
            for (var i = 0; i < Calculating.ResultList.Count; i++)
            {
                Results.Add(new ModelResult(Calculating.XList[i].ToString(), Calculating.ResultList[i].ToString()));
            }
            dgRes.ItemsSource = Results;
            
            IDrawer drawer = new IDrawer(MyCanvas);
            Transform.ScaleX = 0.8;
            Transform.ScaleY = 0.8;
            drawer.DrawAll();
        }
        private void ZoomUp_Click(object sender, RoutedEventArgs e)
        {
            Transform.ScaleX += 0.1;
            Transform.ScaleY += 0.1;
        }
        private void ZoomDown_Click(object sender, RoutedEventArgs e)
        {
            Transform.ScaleX -= 0.1;
            Transform.ScaleY -= 0.1;
        }

        private bool IsDragAndDrop; // перемещено или нет
        private Point PointDrugAndDrop; // точка перемещения
        private Thickness MarginDragAndDrop;
        private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            IsDragAndDrop = false; // Не перемещается, т.к false
        }
        private void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            IsDragAndDrop = false; // Не перемещается, т.к false
        }
        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsDragAndDrop = true;
            PointDrugAndDrop = Mouse.GetPosition(this); // передаём текущее значение позиции курсора
            MarginDragAndDrop = MyCanvas.Margin;
        }
        private void CanvasMove(object sender, MouseEventArgs e)
        {
            //var current = Mouse.GetPosition(this);
            //var offset = current - pointDragAndDrop;
            if (IsDragAndDrop)
            {
                var current = Mouse.GetPosition(this);
                var offset = current - PointDrugAndDrop; // разница в координатах между текущей и новой
                MyCanvas.Margin = new Thickness(MarginDragAndDrop.Left + offset.X, MarginDragAndDrop.Top + offset.Y, 0, 0); // само перемещение
            }
        }

    }
    
    
}

    


