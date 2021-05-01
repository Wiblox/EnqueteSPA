using MahApps.Metro.Controls;
using System.Windows;

namespace EnquteSPA
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddDocument : MetroWindow
    {
        public AddDocument(string numeroEnqute)
        {
            InitializeComponent();
        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Move;
     
        }

        private void dragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Info.Text = System.IO.Path.GetFileName(files[0]);
           
        
        }
    }
        }



   