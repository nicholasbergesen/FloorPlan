using NeuralNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace FloorPlanNet
{
    public partial class Main : Form
    {
        private const string NetworkFiles = "NetworkFiles";
        private const string FloorPlan = "FloorPlan";
        private const string NotFloorPlan = "NotFloorPlan";

        private Network _curnetwork;
        private long _curListingNumber;
        private int _curImageId;
        private byte[] _curImageBytes;
        private HashSet<int> _previouslyAddedImageIds;

        public Main()
        {
            InitializeComponent();
        }


        private void btnTrain_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;

            Status($"Loading Files...");
            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, FloorPlan));
            var notfloorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, NotFloorPlan));
            int totalTrainingFiles = floorPlans.Length + notfloorPlans.Length;
            HashSet<string> trained = new HashSet<string>();
            Status($"Loaded {totalTrainingFiles} for training.");

            _curnetwork.learningRate

            foreach (var listingNumber in _listingNumberss)
            {
                Status($"Loading images for {listingNumber}");
                var listingImages = GetListingImages();
                for (int i = 0; i < listingImages.Length; i++)
                {
                    pictureBox1.Image = //use image service
                }
            }
            var originalImage = new Bitmap("");
            pictureBox1.Image = originalImage;
            var trainingImage = ImageProcessor.Normalize(originalImage);

            var output = _curnetwork.FeedForward(trainingImage);

            label1.Text = output[0].ToString();
        }

        private void btnFloorPlan_Click(object sender, EventArgs e)
        {
            var originalImage = new Bitmap("");
            File.WriteAllBytes(Path.Combine(txtTrainingFiles.Text, FloorPlan, $"{_curImageId}.jpg"), _curImageBytes);
            pictureBox1.Image = originalImage;
            var trainingImage = ImageProcessor.Normalize(originalImage);
            _curnetwork.BackPropagate(trainingImage, new float[] { 1 });

            btnTrain_Click(sender, e);
        }

        private void btnNotFloorPlan_Click(object sender, EventArgs e)
        {
            var originalImage = new Bitmap("");
            pictureBox1.Image = originalImage;
            var trainingImage = ImageProcessor.Normalize(originalImage);
            _curnetwork.BackPropagate(trainingImage, new float[] { 0 });

            btnTrain_Click(sender, e);
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
                _curnetwork.Load(NetworkFiles);
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
            txtStatus.Text = $"[{now.ToShortDateString()} {now.ToShortTimeString()}] {status}{Environment.NewLine}";
        }

        private int[] GetListingImages(long listingNumber)
        {
            return new int[0];
        }
    }
}
