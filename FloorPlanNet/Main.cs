using NeuralNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FloorPlanNet
{
    public partial class Main : Form
    {
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
            //shuffledList.AddRange(floorPlans.Select(x => new Training(x, 0)));
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

        private void btnTrain_Click(object sender, EventArgs e)
        {
            double totalCost = 0;
            btnTrain.Enabled = false;

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
                int count = 0;
                foreach (var data in trainingdata)
                {
                    Bitmap originalImage;
                    //if (data.Expected[0] == 0)
                    //{
                    //    var streamInput = client.GetStreamAsync("https://picsum.photos/200/200").Result;
                    //    originalImage = new Bitmap(streamInput);
                    //}
                    //else
                    //{
                    //    originalImage = new Bitmap(data.File);
                    //}
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

        private void btnFloorPlan_Click(object sender, EventArgs e)
        {
            _curnetwork.BackPropagate(curImageInput, new double[1] { 1 });
            lblCost.Text = _curnetwork.Cost.ToString();
        }

        private void btnNotFloorPlan_Click(object sender, EventArgs e)
        {
            _curnetwork.BackPropagate(curImageInput, new double[1] { 0 });
            lblCost.Text = _curnetwork.Cost.ToString();
        }

        private void btnCreateNetwork_Click(object sender, EventArgs e)
        {
            int[] layers = new int[4] { 784, 49, 16, 1 };
            Activation[] activation = new Activation[3] { Activation.LeakyRelu, Activation.LeakyRelu, Activation.LeakyRelu };
            _curnetwork = new Network(layers, activation);
            Status("Network  Created");
        }

        private void btnLoadNetwork_Click(object sender, EventArgs e)
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

        private void btnSaveNetwork_Click(object sender, EventArgs e)
        {
            Network.SaveNetwork(_curnetwork);
            Status("Network  saved");
        }

        private void Status(string status)
        {
            var now = DateTime.Now;
            txtStatus.AppendText($"[{now}] {status}{Environment.NewLine}");
        }

        HttpClient client = new HttpClient();
        double[] curImageInput;
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

        private const string DevelopmentIds = @"select top 1000 d.ImageId from Development a 
                                    inner join PropertySearch b on b.DevelopmentId = a.Id
                                    inner join MandateImage c on c.MandateId = b.MandateId
                                    inner join ImageReferenceIdToImageId d on d.ReferenceId = c.ImageReferenceId
                                    where d.Confidence is null";

        private const string HasDevelopmentIds = @"select top 1 d.ImageId from Development a 
                                    inner join PropertySearch b on b.DevelopmentId = a.Id
                                    inner join MandateImage c on c.MandateId = b.MandateId
                                    inner join ImageReferenceIdToImageId d on d.ReferenceId = c.ImageReferenceId
                                    where d.Confidence is null";

        private const string UnprocessedImages = @"select top 1000 ImageId from ImageReferenceIdToImageId where Confidence is null";
        private const string HasUnprocessedImages = @"select top 1 ImageId from ImageReferenceIdToImageId where Confidence is null";

        private const string MultiThreadImages = @"select top 100000 ImageId from ImageReferenceIdToImageId where Confidence is null";
        private const string GetRemainingTotal = @"select count(*) ImageId from ImageReferenceIdToImageId where Confidence is null";

        private async void btnIdentifyFloorPlans_Click(object sender, EventArgs e)
        {
            //await SingleThreadProcess();
            await MultiThreadProcess();
        }

        BlockingCollection<int> imageIdBatch = new BlockingCollection<int>(100000);

        private async Task MultiThreadProcess()
        {
            Status("Started Mltithread processing.");

            _curnetwork = Network.LoadNetwork();

            using SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FloorPlan;Integrated Security=true");
            await conn.OpenAsync();
            using var comCount = conn.CreateCommand();
            comCount.CommandText = GetRemainingTotal;
            int totalImages = (int)await comCount.ExecuteScalarAsync();

            new Thread(async () =>
            {
                using SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FloorPlan;Integrated Security=true");
                await conn.OpenAsync();
                using var comSelect = conn.CreateCommand();
                comSelect.CommandText = MultiThreadImages;
                var imageIdReader = await comSelect.ExecuteReaderAsync();
                while (imageIdReader.Read())
                    imageIdBatch.Add(imageIdReader.GetInt32(0));
                imageIdBatch.CompleteAdding();

                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
                    var newThread = new Thread(async () => { await ProcessMultiThread(); })
                    {
                        IsBackground = true
                    };
                    newThread.Start();
                    threads.Add(newThread);
                }
            })
            .Start();
        }

        private async Task ProcessMultiThread()
        {
            var token = _source.Token;
            var curnetwork = Network.LoadNetwork();

            while (!imageIdBatch.IsCompleted && imageIdBatch.TryTake(out int imageId) && !token.IsCancellationRequested)
            {
                var imageUrl = "https://img.trk.static-ptl.p24/" + imageId;
                try
                {
                    var imageStream = await client.GetStreamAsync(imageUrl);
                    var image = new Bitmap(imageStream);
                    var trainingImage = ImageProcessor.Normalize(image);
                    var confidence = curnetwork.FeedForward(trainingImage)[0];
                    await SaveConfidence(imageId, confidence);
                }
                catch
                {
                    await SaveConfidence(imageId, 100f);
                }
            }
        }

        private async Task SingleThreadProcess()
        {
            _curnetwork = Network.LoadNetwork();

            using SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FloorPlan;Integrated Security=true");
            await conn.OpenAsync();
            using var comSelect = conn.CreateCommand();
            comSelect.CommandText = HasUnprocessedImages;
            var imageId = await comSelect.ExecuteScalarAsync();

            while (imageId != null)
            {
                await Process();

                using var comSelect2 = conn.CreateCommand();
                comSelect2.CommandText = HasUnprocessedImages;
                imageId = await comSelect.ExecuteScalarAsync();
            }

            Status("Identify floor plans complete.");
        }

        private async Task Process()
        {
            using SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FloorPlan;Integrated Security=true");
            await conn.OpenAsync();
            using var comSelect = conn.CreateCommand();
            comSelect.CommandText = UnprocessedImages;
            using var selectReader = await comSelect.ExecuteReaderAsync();

            Status("Processing batch");
            while (selectReader.Read())
            {
                var imageId = selectReader.GetInt32(0);
                var imageUrl = "https://img.trk.static-ptl.p24/" + imageId;
                try
                {
                    var imageStream = await client.GetStreamAsync(imageUrl);
                    var image = new Bitmap(imageStream);
                    var trainingImage = ImageProcessor.Normalize(image);
                    var confidence = _curnetwork.FeedForward(trainingImage)[0];
                    await SaveConfidence(imageId, confidence);
                }
                catch
                {
                    await SaveConfidence(imageId, 100f);
                }
            }
            await selectReader.CloseAsync();
        }

        private async Task SaveConfidence(int imageId, double confidence)
        {
            using SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FloorPlan;Integrated Security=true");
            await conn.OpenAsync();
            using var comUpdate = conn.CreateCommand();
            comUpdate.Parameters.AddWithValue("@conf", confidence);
            comUpdate.Parameters.AddWithValue("@imageId", imageId);
            comUpdate.CommandText = "update ImageReferenceIdToImageId set Confidence = @conf where ImageId = @imageId";
            await comUpdate.ExecuteNonQueryAsync();

            if (confidence > 0.75 && confidence != 100)
            {
                txtGenInputs.Invoke((MethodInvoker)delegate
                {
                    txtGenInputs.AppendText("https://img.trk.static-ptl.p24/" + imageId + Environment.NewLine);
                });
            }
        }

        private void btnGenerateClassifier_Click(object sender, EventArgs e)
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

        CancellationTokenSource _source = new CancellationTokenSource();

        private void btnStop_Click(object sender, EventArgs e)
        {
            _source.Cancel();
            imageIdBatch = new BlockingCollection<int>(100000);
            Status("Multithreading stopped.");
            _source = new CancellationTokenSource();
        }

        private void btnTestData_Click(object sender, EventArgs e)
        {
            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, FloorPlan), "*.*", SearchOption.TopDirectoryOnly);
            foreach (var data in floorPlans)
            {
                //var originalImage = new Bitmap(floorPlans[0]);
                //txtInput.Text = floorPlans[0];
                //pictureBox1.Image = ImageProcessor.ChangeWhite(originalImage);
                
                var img = Image.FromFile(data);
                var res = ImageProcessor.ChangeWhite(img);

                res.Save(Path.Combine(txtTrainingFiles.Text, FloorPlan, "Mod", new FileInfo(data).Name), ImageFormat.Jpeg);
            }
        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            var floorPlans = Directory.GetFiles(Path.Combine(txtTrainingFiles.Text, NotFloorPlan), "*.*", SearchOption.TopDirectoryOnly);
            foreach (var data in floorPlans)
            {
                //var originalImage = new Bitmap(floorPlans[0]);
                //txtInput.Text = floorPlans[0];
                //pictureBox1.Image = ImageProcessor.ChangeWhite(originalImage);
                
                var img = Image.FromFile(data);
                img.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                var imgPath = Path.Combine(txtTrainingFiles.Text, NotFloorPlan, "Not_FlipXY", new FileInfo(data).Name);
                img.Save(imgPath, ImageFormat.Jpeg);
            }
        }

        private async void btnTestImage_Click(object sender, EventArgs e)
        {
            try
            {
                var imageStream = await client.GetStreamAsync(txtInput.Text);
                var image = new Bitmap(imageStream);
                var greyImage = ImageProcessor.MakeGreyScale(image);
                var smallImage = ImageProcessor.ResizeImage(greyImage, 28, 28);
                pictureBox2.Image = smallImage;
            }
            catch
            {

            }
        }
    }
}
