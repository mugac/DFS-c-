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

namespace DFS
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ellipse kruh;
        Line line;
        

        public bool add = true; //true node   false path

        public List<int> nodes = new List<int>();

        public List<int> pathFrom = new List<int>();
        public List<int> pathTo = new List<int>();

        public TextBlock tb_;

        public MainWindow()
        {
            InitializeComponent();
            tb_ = tb;
        }

        private void bt_node_Click(object sender, RoutedEventArgs e)
        {
            add = true;
        }

        private void bt_path_Click(object sender, RoutedEventArgs e)
        {
            add = false;
        }

        int nodeNum = 0;
        private void canv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (add)
            {
                
                kruh = new Ellipse();
                kruh.Stroke = Brushes.Black;
                kruh.Fill = Brushes.DarkBlue;
                kruh.HorizontalAlignment = HorizontalAlignment.Left;
                kruh.VerticalAlignment = VerticalAlignment.Center;
                kruh.Width = 40;
                kruh.Height = 40;

                nodes.Add(nodeNum);

                Canvas.SetLeft(kruh, e.GetPosition(canv).X - 20);
                Canvas.SetTop(kruh, e.GetPosition(canv).Y - 20);

                kruh.Name = Convert.ToString("node" + nodeNum + "e" + e.GetPosition(canv).X + "e" + e.GetPosition(canv).Y);
                
                kruh.MouseDown += Kruh_MouseDown;
                nodeNum++;
                canv.Children.Add(kruh);
            }
        }

        int node1 = -1;
        int node2 = -1;

        double pointX = 0;
        double pointY = 0;
        private void Kruh_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse ellipse)
            {
                int name = Convert.ToInt32(ellipse.Name.Split('e')[1]);
                Double X = Convert.ToDouble(ellipse.Name.Split('e')[2]);
                Double Y = Convert.ToDouble(ellipse.Name.Split('e')[3]);
                if (add == false)
                {
                    if (node1 == -1)
                    {
                        node1 = name;
                        pointX = X;
                        pointY = Y;
                        
                        //tb.Text += node1 + node2 + "\n";
                    }
                    else if (node2 == -1)
                    {
                        node2 = name;

                        line = new Line();
                        line.Stroke = Brushes.Red;
                        line.X1 = pointX;
                        line.Y1 = pointY;
                        line.X2 = X;
                        line.Y2 = Y;
                        line.StrokeThickness = 2;

                        canv.Children.Add(line);
                        
                        addDir(pointX, pointY, X, Y);

                        pathFrom.Add(node1);
                        pathTo.Add(node2);

                        node1 = -1;
                        node2 = -1;
                    }
                }
            }
        }
        private void bt_res_Copy_Click(object sender, RoutedEventArgs e)
        {
            canv.Children.Clear();
            node1 = 0;
            node2 = 0;
            pathFrom.Clear();
            pathTo.Clear();
            nodes.Clear();
            tb.Text = "";
        }

        private void addDir(double X1, double Y1, double X2, double Y2)
        {
            double pointX = 0;
            double pointY = 0;

            pointX = (X2 + X1) / 2;
            pointX = (pointX + X2) / 2;

            pointY = (Y2 + Y1) / 2;
            pointY = (pointY + Y2) / 2;

            kruh = new Ellipse();
            kruh.Fill = Brushes.Red;
            kruh.HorizontalAlignment = HorizontalAlignment.Left;
            kruh.VerticalAlignment = VerticalAlignment.Center;
            kruh.Width = 8;
            kruh.Height = 8;
            Canvas.SetLeft(kruh, pointX - 4);
            Canvas.SetTop(kruh, pointY - 4);
            canv.Children.Add(kruh);
        }

        public void bt_play_Click(object sender, RoutedEventArgs e)
        {
            Search();
            
        }
        public void Search()
        {
            Graph g = new Graph(nodes.Count(), tb);

            for (int i = 0; i < pathFrom.Count; i++)
            {
                g.AddEdge(pathFrom[i], pathTo[i]);
            }
            g.DFS(0);
        }
    }
    class Graph
    {
        public int V; //Počet vrcholů
        public TextBlock tb_output;

        // POle listů pro sousedící vrcholy
        public List<int>[] adj;

        // Constructor
        public Graph(int v, TextBlock tb)
        {
            V = v;
            adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
             tb_output = tb;
        }

        //Přidávání stran do listu
        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
        }

        public void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            tb_output.Text += v + "\n";
                
            List<int> vList = adj[v];
            foreach (var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        public void DFS(int v)
        {
            bool[] visited = new bool[V];

            DFSUtil(v, visited);
        }
    }
}




