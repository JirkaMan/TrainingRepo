using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFiles_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            txbInfo.Text = "";
            var files = Directory.EnumerateFiles(@"d:\Data\SkoleniICTpro\BigFiles","*.txt");

            foreach (var file in files)
            {
                var result = FreqAnalysis.FreqAnalysisFromFile(file);

                txbInfo.Text += result.Source + Environment.NewLine;

                foreach (var word in result.GetTopTen())
                {
                    txbInfo.Text += $"{word.Key} : {word.Value} {Environment.NewLine}";
                }
                txbInfo.Text += Environment.NewLine;
            }

            stopwatch.Stop();
            txbInfo.Text += $"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}";
            Mouse.OverrideCursor = null;

        }

        private void btnParallell_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            txbInfo.Text = "";
            var files = Directory.EnumerateFiles(@"d:\Data\SkoleniICTpro\BigFiles", "*.txt");

            // Progress spojí vlákna aby bylo možné přistoupit ke GUI
            IProgress<string> progress = new Progress<string>(message =>
            {
                txbInfo.Text += message;
            });

            Parallel.ForEach(files, file =>
            {
                var result = FreqAnalysis.FreqAnalysisFromFile(file);

                string message = "";
                progress.Report(message);

                message += result.Source + Environment.NewLine;

                foreach (var word in result.GetTopTen())
                {
                    message += $"{word.Key} : {word.Value} {Environment.NewLine}";
                }

                message+=Environment.NewLine;
                
                progress.Report(message);
            });

            stopwatch.Stop();
            progress.Report($"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}");
            Mouse.OverrideCursor = null;
        }
    }
}
