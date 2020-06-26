using NeuralNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FloorPlanNet
{
    public partial class Main : Form
    {
        private const string NetworkFiles = "NetworkFiles";
        private const string FloorPlan = "FloorPlan";
        private const string NotFloorPlan = "NotFloorPlan";

        private Network _curnetwork;

        public Main()
        {
            InitializeComponent();
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
            public float[] Expected;
            public Training(string file, float expectedOutput)
            {
                File = file;
                Expected = new float[1] { expectedOutput };
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;

            Status($"Loading Files...");

            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, FloorPlan));
            var notfloorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, NotFloorPlan));
            var totalTrainingFiles = floorPlans.Length + notfloorPlans.Length;
            var trainingdata = Shuffle(floorPlans, notfloorPlans);

            Status($"Loaded {totalTrainingFiles} for training.");
            progressBar1.Maximum = totalTrainingFiles;
            progressBar1.Step = 1;

            Status($"Training started...");

            var trainingThread = new Thread(() =>
            {
                foreach (var data in trainingdata)
                {
                    var originalImage = new Bitmap(data.File);
                    var trainingImage = ImageProcessor.Normalize(originalImage);
                    _curnetwork.BackPropagate(trainingImage, data.Expected);
                    progressBar1.Invoke((MethodInvoker) delegate { 
                        progressBar1.PerformStep();
                        Status($"Cost: {_curnetwork.cost}");
                    });
                }

                btnTrain.Invoke((MethodInvoker)delegate {
                    Status($"Training finished.");
                    btnTrain.Enabled = true;
                });
            })
            {
                IsBackground = true
            };
            trainingThread.Start();
        }

        private void btnFloorPlan_Click(object sender, EventArgs e)
        {
            _curnetwork.BackPropagate(curImageInput, new float[1] { 1 });
            lblCost.Text = _curnetwork.cost.ToString();
        }

        private void btnNotFloorPlan_Click(object sender, EventArgs e)
        {
            _curnetwork.BackPropagate(curImageInput, new float[1] { 0 });
            lblCost.Text = _curnetwork.cost.ToString();
        }

        private void btnCreateNetwork_Click(object sender, EventArgs e)
        {
            int[] layers = new int[4] { 784, 28, 28, 1 };
            string[] activation = new string[3] { "leakyrelu", "leakyrelu", "leakyrelu" };
            _curnetwork = new Network(layers, activation);
            Status("Netork Created");
        }

        private void btnLoadNetwork_Click(object sender, EventArgs e)
        {
            try
            {
                _curnetwork = Network.LoadNetwork();
                Status("Netork loaded");
            }
            catch (Exception ex)
            {
                Status(ex.Message);
            }
        }

        private void btnSaveNetwork_Click(object sender, EventArgs e)
        {
            _curnetwork.Save(NetworkFiles);
            Status("Netork saved");
        }

        private void Status(string status)
        {
            var now = DateTime.Now;
            txtStatus.AppendText($"[{now.ToShortDateString()} {now.ToShortTimeString()}] {status}{Environment.NewLine}");
        }

        HttpClient client = new HttpClient();
        float[] curImageInput;
        private async void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                var imageStream = await client.GetStreamAsync(txtInput.Text);
                var image = new Bitmap(imageStream);
                pictureBox1.Image = image;
                var trainingImage = ImageProcessor.Normalize(image);
                curImageInput = trainingImage;
                lblOutput.Text = _curnetwork.FeedForward(trainingImage)[0].ToString();
            }
            catch
            {

            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            var segments = txtInput.Text.Split('/');
            var id = int.Parse(segments.Last());
            segments[^1] = (++id).ToString();
            txtInput.Text = string.Join('/', segments);

            btnLoadImage_Click(sender, e);
        }
    }
}
