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

            // IProgress spojí vlákna aby bylo možné přistoupit ke GUI
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

        private async void btnParallellAsync_Click(object sender, RoutedEventArgs e)
        {
            pb_Progress.Value = 0;
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            txbInfo.Text = "";
            var files = Directory.EnumerateFiles(@"d:\Data\SkoleniICTpro\BigFiles", "*.txt");

            // IProgress spojí vlákna aby bylo možné přistoupit ke GUI
            IProgress<string> progress = new Progress<string>(message =>
            {
                txbInfo.Text += message;
                pb_Progress.Value++;
            });

            pb_Progress.Maximum = files.Count();

            await Parallel.ForEachAsync(files, async (file,cancellationToken) =>
            {
                var result = FreqAnalysis.FreqAnalysisFromFile(file);
                
                string message = "";
                progress.Report(message);

                message += result.Source + Environment.NewLine;

                foreach (var word in result.GetTopTen())
                {
                    message += $"{word.Key} : {word.Value} {Environment.NewLine}";
                }

                message += Environment.NewLine;

                progress.Report(message);
            });

            stopwatch.Stop();
            progress.Report($"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}");
            Mouse.OverrideCursor = null;
        }

        private void btnWaitFirst_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            string url1 = "https://seznam.cz";
            string url2 = "https://seznamzpravy.cz";
            string url3 = "https://www.ictpro.cz/";

            var t1 = Task.Run(() => WebLoad.LoadUrl(url1));
            var t2 = Task.Run(() => WebLoad.LoadUrl(url2));
            var t3 = Task.Run(() => WebLoad.LoadUrl(url3));

            Task.WaitAny(t1, t2, t3);

            stopwatch.Stop();
            txbInfo.Text += "Doběhl první task";
            txbInfo.Text += $"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}";
            Mouse.OverrideCursor = null;
        }

        private void btnWaitAll_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            string url1 = "https://seznam.cz";
            string url2 = "https://seznamzpravy.cz";
            string url3 = "https://www.ictpro.cz/";

            var t1 = Task.Run(() => WebLoad.LoadUrl(url1));
            var t2 = Task.Run(() => WebLoad.LoadUrl(url2));
            var t3 = Task.Run(() => WebLoad.LoadUrl(url3));

            Task.WaitAll(t1, t2, t3);

            stopwatch.Stop();
            txbInfo.Text += "Doběhl všechny tasky";
            txbInfo.Text += $"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}";
            Mouse.OverrideCursor = null;
        }

        private async void btnWhenAny_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            string url1 = "https://seznam.cz";
            string url2 = "https://seznamzpravy.cz";
            string url3 = "https://www.ictpro.cz/";

            var t1 = Task.Run(() => WebLoad.LoadUrl(url1));
            var t2 = Task.Run(() => WebLoad.LoadUrl(url2));
            var t3 = Task.Run(() => WebLoad.LoadUrl(url3));

            var firstDone = await Task.WhenAny(t1, t2, t3);

            stopwatch.Stop();
            txbInfo.Text += $"Doběhl první task {Environment.NewLine}";
            txbInfo.Text += $"Web length je {firstDone.Result}";
            txbInfo.Text += $"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}";
            Mouse.OverrideCursor = null;
        }

        private async void btnWhenAll_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = "";
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch stopwatch = Stopwatch.StartNew();

            string url1 = "https://sezhhhnam.cz";
            string url2 = "https://seznamzpravy.cz";
            string url3 = "https://www.ictpro.cz/";

            var t1 = Task.Run(() => WebLoad.LoadUrl(url1));
            var t2 = Task.Run(() => WebLoad.LoadUrl(url2));
            var t3 = Task.Run(() => WebLoad.LoadUrl(url3));

            (int?,string,bool)[] results = await Task.WhenAll(t1, t2, t3);

            stopwatch.Stop();
            txbInfo.Text += $"Weby jsou dlouhé: {string.Join(", ", results)}";
            txbInfo.Text += $"{Environment.NewLine}Elapsed miliseconds: {stopwatch.ElapsedMilliseconds}";
            Mouse.OverrideCursor = null;
        }
    }
}
