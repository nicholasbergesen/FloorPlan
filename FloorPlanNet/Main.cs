using NeuralNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FloorPlanNet
{
    public partial class Main : Form
    {
        private const string FloorPlan = "FloorPlan";
        private const string NotFloorPlan = "NotFloorPlan";

        private double[] _curImageInput;
        private Network _curnetwork;
        private readonly HttpClient _client;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public Main()
        {
            InitializeComponent();
            _client = new HttpClient();
            _cancellationTokenSource = new CancellationTokenSource();
            _curnetwork = Network.LoadNetwork();
        }

        private static List<Training> Shuffle(string[] floorPlans, string[] notfloorPlans)
        {
            List<Training> shuffledList = new List<Training>();
            shuffledList.AddRange(floorPlans.Select(x => new Training(x, 1)));
            shuffledList.AddRange(notfloorPlans.Select(x => new Training(x, 0)));
            Random rng = new Random();

            int n = shuffledList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = shuffledList[k];
                shuffledList[k] = shuffledList[n];
                shuffledList[n] = value;
            }

            return shuffledList;
        }

        private struct Training
        {
            public string File;
            public double[] Expected;
            public Training(string file, double expectedOutput)
            {
                File = file;
                Expected = new double[1] { expectedOutput };
            }
        }

        private void BtnTrain_Click(object sender, EventArgs e)
        {
            double totalCost = 0;
            btnTrain.Enabled = false;
            btnStopTraining.Enabled = true;

            Status($"Loading Files...");

            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, FloorPlan), "*.*", SearchOption.AllDirectories);
            var notfloorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, NotFloorPlan));
            var totalTrainingFiles = floorPlans.Length + notfloorPlans.Length;
            var trainingdata = Shuffle(floorPlans, notfloorPlans);

            Status($"Loaded {totalTrainingFiles} for training.");
            progressBar1.Maximum = totalTrainingFiles;
            progressBar1.Step = 1;

            Status($"Training started...");
            var trainingThread = new Thread(() =>
            {
                CancellationToken token = _cancellationTokenSource.Token;
                int count = 0;
                foreach (var data in trainingdata)
                {
                    if (token.IsCancellationRequested)
                    {
                        btnTrain.Invoke((MethodInvoker)delegate
                        {
                            Status("Training stopped.");
                        });
                        break;
                    }

                    Bitmap originalImage;
                    originalImage = new Bitmap(data.File);
                    var trainingImage = ImageProcessor.Normalize(originalImage);
                    _curnetwork.BackPropagate(trainingImage, data.Expected);
                    progressBar1.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.PerformStep();
                        totalCost += _curnetwork.Cost;
                        Status($"Cost: {totalCost / ++count:0.####}");
                    });
                }

                btnTrain.Invoke((MethodInvoker)delegate
                {
                    Status($"Training finished.");
                    btnTrain.Enabled = true;
                    progressBar1.Value = 0;
                });
            })
            {
                IsBackground = true
            };
            trainingThread.Start();
        }        

        private void BtnFloorPlan_Click(object sender, EventArgs e)
        {
            if (!_curImageInput.Any())
                return;

            _curnetwork.BackPropagate(_curImageInput, new double[1] { 1 });
            lblCost.Text = _curnetwork.Cost.ToString();
        }

        private void BtnNotFloorPlan_Click(object sender, EventArgs e)
        {
            if (!_curImageInput.Any())
                return;

            _curnetwork.BackPropagate(_curImageInput, new double[1] { 0 });
            lblCost.Text = _curnetwork.Cost.ToString();
        }

        private void BtnCreateNetwork_Click(object sender, EventArgs e)
        {
            int[] layers = new int[4] { 784, 49, 16, 1 };
            Activation[] activation = new Activation[3] { Activation.LeakyRelu, Activation.LeakyRelu, Activation.LeakyRelu };
            _curnetwork = new Network(layers, activation);
            Status("Network  Created");
        }

        private void BtnLoadNetwork_Click(object sender, EventArgs e)
        {
            try
            {
                _curnetwork = Network.LoadNetwork();
                Status("Network  loaded");
            }
            catch (Exception ex)
            {
                Status(ex.Message);
            }
        }

        private void BtnSaveNetwork_Click(object sender, EventArgs e)
        {
            Network.SaveNetwork(_curnetwork);
            Status("Network saved");
        }

        private void Status(string status)
        {
            var now = DateTime.Now;
            txtStatus.AppendText($"[{now}] {status}{Environment.NewLine}");
        }

        private async void BtnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                var imageStream = await _client.GetStreamAsync(txtInput.Text);
                var image = new Bitmap(imageStream);
                pictureBox1.Image = image;
                var trainingImage = ImageProcessor.Normalize(image);
                _curImageInput = trainingImage;
                lblOutput.Text = _curnetwork.FeedForward(trainingImage)[0].ToString();
            }
            catch(NullReferenceException)
            {
                Status("Please create or load a network.");
            }
            catch
            {

            }
        }

        private void BtnGenerateClassifier_Click(object sender, EventArgs e)
        {
            Status("Generating file...");

            string[] imageIds = txtGenInputs.Text.Split(Environment.NewLine);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Html>");
            sb.AppendLine("<HEAD>");
            sb.AppendLine("<body>");

            for (int i = 0; i < imageIds.Length; i++)
                if(!string.IsNullOrWhiteSpace(imageIds[i]))
                    sb.AppendLine($"<img style=\"width:310px;height:310px\" loading=\"lazy\" src=\"{imageIds[i]}\" />");

            sb.AppendLine("<body>");
            sb.AppendLine("</HEAD>");
            sb.AppendLine("</Html>");

            File.WriteAllText("Gen.html", sb.ToString());

            Status("File Generated.");
        }

        private void BtnTestData_Click(object sender, EventArgs e)
        {
            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, FloorPlan), "*.*", SearchOption.TopDirectoryOnly);
            foreach (var data in floorPlans)
            {
                var img = Image.FromFile(data);
                //img.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                var res = ImageProcessor.ChangeWhite(img);

                res.Save(Path.Combine(txtTrainingFiles.Text, FloorPlan, "Mod", new FileInfo(data).Name), ImageFormat.Jpeg);
            }
        }

        private async void BtnShowNormalizeImage_Click(object sender, EventArgs e)
        {
            try
            {
                var imageStream = await _client.GetStreamAsync(txtInput.Text);
                var image = new Bitmap(imageStream);
                var greyImage = ImageProcessor.MakeGreyScale(image);
                var smallImage = ImageProcessor.ResizeImage(greyImage, 28, 28);
                pictureBox2.Image = smallImage;
            }
            catch
            {

            }
        }

        private void BtnStopTraining_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource.Cancel();
            btnStopTraining.Enabled = false;
        }
    }
}
