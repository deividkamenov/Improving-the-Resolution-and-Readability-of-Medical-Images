using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ImprovingTheResolutionOfMedicalImagesWFApp
{
    public partial class Form1 : Form
    {
        //Main variables
        private byte[,] input;
        private byte[,] matrixOutput;
        private Bitmap bitmapImage;
        private Image originalInput;
        private int matrixCols;
        private int matrixRows;

        //Variables for extracting image and saivig it as a matrix
        private bool inputImageIsSet = false;
        private bool inputImageIsConverted = false;

        //Variables for GettingVariables function
        //Matrix 3 x 3 named as follows:
        //  F   U   C
        //  L   I   R   
        //  N   D   M
        private float U = 0, R = 0, D = 0, L = 0, I = 0;
        private float C = 0 /*Right Up Corner */, M = 0 /*Ringt Down Corner*/, F = 0 /*Left Up Corner*/, N = 0 /*Left Down Corner*/;


        //Variables for Improving Image Readability algorithm
        private float lambda = 1;
        private float[,] hPrim1 = new float[,] { { 0,-0.25f, 0 },
                                                      { -0.25f, 2, -0.25f },
                                                      { 0,-0.25f, 0 }};

        private float[,] hPrim2 = new float[,] { { 0.077847f,  0.123317f, 0.077847f },
                                                      { 0.123317f, 0.195346f, 0.123317f },
                                                      { 0.077847f,  0.123317f, 0.077847f }};

        private float[,] hPrim3 = new float[,] { {0.024879f,  0.107973f, 0.024879f },
                                                      {0.107973f, 0.468592f, 0.107973f },
                                                      {0.024879f,  0.107973f, 0.024879f }};

        private int n = 1;// The formula for invreasing the resolution: (2^2*n); The coeficient "n" is the coeficient of increasing the resolution 
        private int numIterations = 200;

        //Edge preservation variables
        private float epsilon = 1;
        private static float hx = 0.1f;// hx/2 = deltaX
        private float deltaX = hx;
        private static float hy = 0.1f;//hy/2 = deltaY
        private float deltaY = hy;// hx = deltaX

        private float deltaT = 0.005f;//Variable used for bluring and debluring the image in ImproveReadability
        private int maxImageValue = 128;
        private int minImageValue = 0;

        private string pictureName = "";
        private string pictureLocation = "";

        private int startImageWidth;
        private int startImageHeight;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)//Load Image Button
        {
            deltaT = 0.003f;//Set deltaT value

            LoadImageToGreyscale();//Load the image and convert it to greyscale
            matrixOutput = input;
            EnableButtons();
        }

        private void LoadImageToGreyscale()
        {
            //Open file dialog to set the input image
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Jpg-|*.jpg|Png-|*.png|Jpeg-|*.jpeg|Bitmaps|*.bmp|Tif|*.tif"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)//If image is set
            {
                lblInputHeight.Text = "";
                lblInputWidth.Text = "";
                lblOutputHeight.Text = "";
                lblOutputWidth.Text = "";

                originalInput = Bitmap.FromFile(openFileDialog.FileName);


                //picInputImage.Image = originalInput;


                pictureLocation = openFileDialog.FileName.ToString();
                pictureName = pictureLocation.Substring(pictureLocation.LastIndexOf('\\') + 1);
                label3.Text = pictureName;

                inputImageIsSet = true;//Used to chek if the input image is set
                Bitmap bitmapImage = new Bitmap(originalInput);

                matrixCols = bitmapImage.Width;//Set the height and width of the image
                matrixRows = bitmapImage.Height;

                startImageWidth = matrixCols;
                startImageHeight = matrixRows;

                for (int y = 0; y < matrixRows; y++)//Convert colour image to greyscale
                {
                    for (int x = 0; x < matrixCols; x++)
                    {
                        Color c = bitmapImage.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        int avg = (r + g + b) / 3;
                        bitmapImage.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                    }
                }

                // int newImageSizeWidth = 600;
                // float factor = 255f / rows;
                //int newImageSizeHeight = (int)Math.Round((rows * factor));
                
               // int newImageSizeWidth = (int)Math.Round((cols * factor));

                picInputImage.Image = bitmapImage;//Set the image to Input Image Picture 
                float[,] bitmapImageArray = BitmapToArray2D(bitmapImage);
                byte[,] byteImage = new byte[bitmapImageArray.GetLength(0), bitmapImageArray.GetLength(1)];

                for (int i = 0; i < bitmapImageArray.GetLength(0); i++)//Convert colour image to greyscale
                {
                    for (int j = 0; j < bitmapImageArray.GetLength(1); j++)
                    {
                        byteImage[i, j] = (byte)bitmapImageArray[i, j];
                    }
                }
                input = byteImage;
                lblInputWidth.Text = "Input Image Width: " + Convert.ToString(matrixCols);
                lblInputHeight.Text = "Input Image Height: " + Convert.ToString(matrixRows);
                lblDeltaT.Text = "DeltaT value: " + Convert.ToString(deltaT);
            }
        }

        private float[,] BitmapToArray2D(Bitmap image)
        {
            float[,] array = new float[image.Height, image.Width];
            Color tmp = new Color();
            // Loop through the images pixels to reset colour.

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    tmp = image.GetPixel(x, y);
                    array[y, x] = (float)tmp.R;
                }
            }
            return array;
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet && (pictureName == "flower.jpg" || pictureName == "maths.jpg"))
            {
                btnConvert.Enabled = false;
                for (int i = 0; i < 6; i++)
                {
                    btnUpsampleEdgePreserving.PerformClick();
                }
            }

            else if (inputImageIsSet && pictureName == "number.jpg")//not working
            {
                btnConvert.Enabled = false;
                deltaT = 0.0055f;
                for (int i = 0; i < 3; i++)
                {
                    btnUpsampleEdgePreserving.PerformClick();
                }
                btnImproveReadabilityEdgePreserving.PerformClick();

                maxImageValue = 160;
                minImageValue = 0;
                btnSegment.PerformClick();
                btnUpsampleEdgePreserving.PerformClick();
            }

            else if (inputImageIsSet && pictureName == "scull.jpg")
            {
                btnConvert.Enabled = false;
                maxImageValue = 255;
                minImageValue = 128;
                btnUpsampleEdgePreserving.PerformClick();
                btnSegment.PerformClick();
            }

            else if (inputImageIsSet && pictureName == "bone.jpg")
            {
                btnConvert.Enabled = false;
                for (int i = 0; i < 3; i++)
                {
                    btnUpsampleEdgePreserving.PerformClick();
                }
                deltaT = 0.005f;
                btnImproveReadabilityEdgePreserving.PerformClick();

                maxImageValue = 255;
                minImageValue = 122;
                btnSegment.PerformClick();
            }
        }

        private void GettingVariables(int i, int j, float[,] matrixInput)
        {
            //Matrix 3 x 3 named as follows:
            //  F   U   C
            //  L   I   R   
            //  N   D   M

            I = matrixInput[i, j];

            if (i == 0 || j == 0 || i == matrixRows - 1 || j == matrixCols - 1)
            {
                //Corners
                if (i == 0 && j == matrixCols - 1)//Right up orner
                {
                    F = matrixInput[i, j - 1];
                    U = I;
                    C = I;
                    R = I;
                    M = matrixInput[i + 1, j];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }

                else if (i == matrixRows - 1 && j == 0)//Left down corner
                {
                    F = matrixInput[i - 1, j];
                    U = I;
                    M = matrixInput[i, j + 1];
                    D = I;
                    N = I;
                    U = matrixInput[i - 1, j];
                    L = matrixInput[i, j + 1];
                    R = matrixInput[i, j + 1];
                }

                else if (i == 0 && j == 0)//left up corner
                {
                    F = I;
                    U = I;
                    C = R;
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j];
                    L = I;
                }

                else if (i == matrixRows - 1 && j == matrixCols - 1)//Right down corner 
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j];
                    R = I;
                    M = I;
                    D = I;
                    N = matrixInput[i, j - 1];
                    L = matrixInput[i, j - 1];
                }

                //Sides
                else if (i == 0)//Up side
                {
                    F = matrixInput[i, j - 1];
                    U = I;
                    R = matrixInput[i, j + 1];
                    C = R;
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }
                else if (i == matrixRows - 1)//Down side
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j + 1];
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i, j + 1];//R
                    D = I;
                    N = matrixInput[i, j - 1];
                    L = matrixInput[i, j - 1];
                }

                else if (j == 0)//Left side
                {
                    F = matrixInput[i - 1, j];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j + 1];
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j];
                    L = I;
                }

                else if (j == matrixCols - 1)// Right side
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j];
                    R = I;
                    M = matrixInput[i + 1, j];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }
            }
            else
            {
                //Matrix 3 x 3:
                //  F   U   C
                //  L   I   R   
                //  N   D   M

                F = matrixInput[i - 1, j - 1];//Up left corner
                U = matrixInput[i - 1, j];//Up central
                C = matrixInput[i - 1, j + 1];//Up right corner
                R = matrixInput[i, j + 1];//Reft central
                M = matrixInput[i + 1, j + 1];//Right down
                D = matrixInput[i + 1, j];//Down central
                N = matrixInput[i + 1, j - 1];//Left down
                L = matrixInput[i, j - 1];//Left central
            }
        }

        private void GettingVariables2(int i, int j, byte[,] matrixInput)
        {
            //Matrix 3 x 3 named as follows:
            //  F   U   C
            //  L   I   R   
            //  N   D   M

            I = matrixInput[i, j];

            if (i == 0 || j == 0 || i == matrixRows - 1 || j == matrixCols - 1)
            {
                //Corners
                if (i == 0 && j == matrixCols - 1)//Right up orner
                {
                    F = matrixInput[i, j - 1];
                    U = I;
                    C = I;
                    R = I;
                    M = matrixInput[i + 1, j];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }

                else if (i == matrixRows - 1 && j == 0)//Left down corner
                {
                    F = matrixInput[i - 1, j];
                    U = I;
                    M = matrixInput[i, j + 1];
                    D = I;
                    N = I;
                    U = matrixInput[i - 1, j];
                    L = matrixInput[i, j + 1];
                    R = matrixInput[i, j + 1];
                }

                else if (i == 0 && j == 0)//left up corner
                {
                    F = I;
                    U = I;
                    C = R;
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j];
                    L = I;
                }

                else if (i == matrixRows - 1 && j == matrixCols - 1)//Right down corner 
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j];
                    R = I;
                    M = I;
                    D = I;
                    N = matrixInput[i, j - 1];
                    L = matrixInput[i, j - 1];
                }

                //Sides
                else if (i == 0)//Up side
                {
                    F = matrixInput[i, j - 1];
                    U = I;
                    R = matrixInput[i, j + 1];
                    C = R;
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }
                else if (i == matrixRows - 1)//Down side
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j + 1];
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i, j + 1];//R
                    D = I;
                    N = matrixInput[i, j - 1];
                    L = matrixInput[i, j - 1];
                }

                else if (j == 0)//Left side
                {
                    F = matrixInput[i - 1, j];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j + 1];
                    R = matrixInput[i, j + 1];
                    M = matrixInput[i + 1, j + 1];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j];
                    L = I;
                }

                else if (j == matrixCols - 1)// Right side
                {
                    F = matrixInput[i - 1, j - 1];
                    U = matrixInput[i - 1, j];
                    C = matrixInput[i - 1, j];
                    R = I;
                    M = matrixInput[i + 1, j];
                    D = matrixInput[i + 1, j];
                    N = matrixInput[i + 1, j - 1];
                    L = matrixInput[i, j - 1];
                }
            }
            else
            {
                //Matrix 3 x 3:
                //  F   U   C
                //  L   I   R   
                //  N   D   M

                F = matrixInput[i - 1, j - 1];//Up left corner
                U = matrixInput[i - 1, j];//Up central
                C = matrixInput[i - 1, j + 1];//Up right corner
                R = matrixInput[i, j + 1];//Reft central
                M = matrixInput[i + 1, j + 1];//Right down
                D = matrixInput[i + 1, j];//Down central
                N = matrixInput[i + 1, j - 1];//Left down
                L = matrixInput[i, j - 1];//Left central
            }
        }

        //Edge Preservation Method
        private float MinMod(float d, float e)
        {
            return (((Sgn(d) + Sgn(e)) / 2.0f) * Math.Min(Math.Abs(d), Math.Abs(e)));
        }

        //Edge Preservation Method 2
        private float Sgn(float var)
        {
            if (var >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        //Increasing the Resolution Method
        private byte[,] UpsamplingAlgorithm(byte[,] matrixInput)
        {
            byte[,] matrixUpsampling = new byte[2 * matrixRows, 2 * matrixCols];
            for (int i = 0; i < matrixRows; i++) // i = x horizontal, j = y vertical
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    GettingVariables2(i, j, matrixInput);

                    matrixUpsampling[2 * i, 2 * j] = (byte)((6 * I + U + L) / 8);// up left
                    matrixUpsampling[2 * i, 2 * j + 1] = (byte)((6 * I + U + R) / 8);//up right
                    matrixUpsampling[2 * i + 1, 2 * j] = (byte)((6 * I + D + L) / 8);//down left
                    matrixUpsampling[2 * i + 1, 2 * j + 1] = (byte)((6 * I + D + R) / 8); //down right
                }
            }
            matrixRows *= 2;
            matrixCols *= 2;

            return matrixUpsampling;
        }

        //Increasing the Resolution Method with Edge Preservation
        private byte[,] EdgePreservationUpsamplingAlgorithm(byte[,] matrixInput)
        {
            byte[,] matrixUpsampling = new byte[2 * matrixRows, 2 * matrixCols];
            for (int i = 0; i < matrixRows; i++) // i = x horizontal, j = y vertical
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    GettingVariables2(i, j, matrixInput);

                    float a = MinMod(((I - L) / deltaX), ((R - I) / deltaX));
                    float b = MinMod(((I - D) / deltaY), ((U - I) / deltaY));

                    matrixUpsampling[2 * i, 2 * j] = (byte)(I + ((a * deltaX) / 4) - ((b * deltaY) / 4));// up left
                    matrixUpsampling[2 * i, 2 * j + 1] = (byte)(I + ((a * deltaX) / 4) + ((b * deltaY) / 4));//up right
                    matrixUpsampling[2 * i + 1, 2 * j] = (byte)(I - ((a * deltaX) / 4) - ((b * deltaY) / 4));//down left
                    matrixUpsampling[2 * i + 1, 2 * j + 1] = (byte)(I - ((a * deltaX) / 4) + ((b * deltaY) / 4)); //down right
                }
            }
            matrixRows *= 2;
            matrixCols *= 2;

            return matrixUpsampling;
        }

        //Algorithm for decreasing the resolution
        private byte[,] DownsamplingAlgorithm(byte[,] matrixInput)
        {
            //Downsampling algorithm: (17t -3u - 3m + b) /12 
            //M = height, N = width
            int M = matrixRows / 2;
            int N = matrixCols / 2;

            byte[,] matrixDownsampling = new byte[M, N];

            for (int i = 0; i < M - 1; i++)//Most of the image
            {
                for (int j = 0; j < N - 1; j++)
                {
                    matrixDownsampling[i, j] = (byte)((17 * matrixInput[2 * i + 1, 2 * j + 1] - 3 * matrixInput[2 * i + 2, 2 * j + 1]
                        - 3 * matrixInput[2 * i + 1, 2 * j + 2] + matrixInput[2 * i + 2, 2 * j + 2]) / 12);
                }
            }

            for (int j = 0; j < N - 1; j++)//Bottom line 
            {
                matrixDownsampling[M - 1, j] = (byte)((17 * matrixInput[2 * M - 2, 2 * j + 1] - 3 * matrixInput[2 * M - 2, 2 * j + 2]
                        - 3 * matrixInput[2 * M - 3, 2 * j + 1] + matrixInput[2 * M - 3, 2 * j + 2]) / 12);
            }

            for (int i = 0; i < M - 1; i++)//End right colum
            {
                matrixDownsampling[i, N - 1] = (byte)((17 * matrixInput[2 * i + 1, 2 * N - 2] - 3 * matrixInput[2 * i + 1, 2 * N - 3]
                        - 3 * matrixInput[2 * i + 2, 2 * N - 2] + matrixInput[2 * i + 2, 2 * N - 3]) / 12);
            }

            //Bottom line end righrt colun
            matrixDownsampling[M - 1, N - 1] = (byte)((17 * matrixInput[2 * M - 2, 2 * N - 2] - 3 * matrixInput[2 * M - 3, 2 * N - 2]
                        - 3 * matrixInput[2 * M - 2, 2 * N - 3] + matrixInput[2 * M - 3, 2 * N - 3]) / 12);


            matrixCols /= 2;
            matrixRows /= 2;

            return matrixDownsampling;
        }

        private byte[,] EdgePreservationDownsamplingAlgorithm(byte[,] matrixInput)
        {
            //Downsampling algorithm formula: (17t -3u - 3m + b) /12 
            //M = height, N = width
            int M = matrixRows / 2;
            int N = matrixCols / 2;

            byte[,] matrixDownsampling = new byte[M, N];

            for (int i = 0; i < M; i++)//most of the image
            {
                for (int j = 0; j < N; j++)
                {
                    matrixDownsampling[i, j] = (byte)((matrixInput[2 * i, 2 * j] + matrixInput[2 * i + 1, 2 * j + 1] + matrixInput[2 * i + 1, 2 * j]
                         + matrixInput[2 * i, 2 * j + 1]) / 4);
                }
            }

            matrixCols /= 2;
            matrixRows /= 2;

            return matrixDownsampling;
        }

        private byte[,] ImprovingReadabilityEdgePreservation(byte[,] matrix)//Using Edge preservation Up and Downsampling
        {
            byte[,] improved = matrix; // The matrix that is to be improved

            byte[,] matrixTmp = new byte[matrixRows, matrixCols];
            for (int s = 0; s < numIterations; s++)
            {
                byte[,] matrixConvolution = ConvolutionAlgorithm(improved, 2);
                byte[,] matrixDownsampling = EdgePreservationDownsamplingAlgorithm(matrixConvolution);

                matrixDownsampling = EdgePreservationUpsamplingAlgorithm(matrixDownsampling);

                for (int i = 0; i < matrixRows; i++)
                {
                    for (int j = 0; j < matrixCols; j++)
                    {
                        matrixTmp[i, j] = (byte)Math.Min((byte)Math.Abs(improved[i, j] - matrixDownsampling[i, j]), (byte)maxImageValue);
                    }
                }
                matrixTmp = ConvolutionAlgorithm(matrixTmp, 3);

                for (int i = 0; i < matrixRows; i++)//Width
                {
                    for (int j = 0; j < matrixCols; j++)//Length
                    {
                        GettingVariables2(i, j, improved);//Getting the variables

                        //The formula of the algorithm
                        float diff = deltaT * ((lambda * matrixTmp[i, j]) + (epsilon + (U - D) * (U - D) / (4 * hy * hy)) *
                        (R - 2 * I + L) / (hx * hx) + (epsilon + (R - L) * (R - L) / (4 * hx * hx) * (U - 2 * I + D)) / (hy * hy) - 2 * (R - L) * (U - D) * (C - F - M + N) / (16 * hx * hx * hy * hy))
                        / (epsilon + (U - D) * (U - D) / (4 * hy * hy) + (R - L) * (R - L) / (4 * hx * hx));

                        improved[i, j] = Math.Max(Math.Min((byte)255, (byte)(improved[i, j] + (byte)diff)), (byte)0);
                    }
                }
            }
            return improved;
        }

        private byte[,] ImprovingReadabilityStandard(byte[,] matrix) //Using Standard Up and Downsampling
        {
            byte[,] improved = matrix; //The matrix that is to be improved

            byte[,] matrixTmp = new byte[matrixRows, matrixCols];
            for (int s = 0; s < numIterations; s++)
            {
                byte[,] matrixConvolution = ConvolutionAlgorithm(improved, 2);
                byte[,] matrixDownsampling = DownsamplingAlgorithm(matrixConvolution);

                matrixDownsampling = UpsamplingAlgorithm(matrixDownsampling);

                for (int i = 0; i < matrixRows; i++)
                {
                    for (int j = 0; j < matrixCols; j++)
                    {
                        matrixTmp[i, j] = (byte)Math.Min(Math.Abs(improved[i, j] - matrixDownsampling[i, j]), maxImageValue);
                    }
                }
                matrixTmp = ConvolutionAlgorithm(matrixTmp, 3);

                for (int i = 0; i < matrixRows; i++)//width
                {
                    for (int j = 0; j < matrixCols; j++)//length
                    {
                        //The algorithm formula
                        GettingVariables2(i, j, improved);//we get the variables
                        float diff = deltaT * ((lambda * matrixTmp[i, j]) + (epsilon + (U - D) * (U - D) / (4 * hy * hy)) *
                            (R - 2 * I + L) / (hx * hx) + (epsilon + (R - L) * (R - L) / (4 * hx * hx) * (U - 2 * I + D)) / (hy * hy) - 2 * (R - L) * (U - D) * (C - F - M + N) / (16 * hx * hx * hy * hy))
                            / (epsilon + (U - D) * (U - D) / (4 * hy * hy) + (R - L) * (R - L) / (4 * hx * hx));

                        improved[i, j] = Math.Max(Math.Min((byte)255, (byte)(improved[i, j] + (byte)diff)), (byte)0);
                    }
                }
            }
            return improved;
        }

        private byte[,] ConvolutionAlgorithm(byte[,] matrix, int type)//Used for Improving the Readability
        {
            byte[,] matrixConvolution = new byte[matrixRows, matrixCols];
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    if (type == 1)
                    {
                        GettingVariables2(i, j, matrix);
                        matrixConvolution[i, j] = (byte)Math.Max(Math.Min(255, hPrim1[0, 0] * F + hPrim1[0, 1] * U + hPrim1[0, 2] * C + hPrim1[1, 0] * L + hPrim1[1, 1] * I +
                                 hPrim1[1, 2] * R + hPrim1[2, 0] * N + hPrim1[2, 1] * D + hPrim1[2, 2] * M), 0);//minImageValue
                    }
                    else if (type == 2)
                    {
                        GettingVariables2(i, j, matrix);
                        matrixConvolution[i, j] = (byte)Math.Max(Math.Min(255, hPrim2[0, 0] * F + hPrim2[0, 1] * U + hPrim2[0, 2] * C + hPrim2[1, 0] * L + hPrim2[1, 1] * I +
                                 hPrim2[1, 2] * R + hPrim2[2, 0] * N + hPrim2[2, 1] * D + hPrim2[2, 2] * M), 0);
                    }
                    else if (type == 3)
                    {
                        GettingVariables2(i, j, matrix);
                        matrixConvolution[i, j] = (byte)Math.Max(Math.Min(255, hPrim3[0, 0] * F + hPrim3[0, 1] * U + hPrim3[0, 2] * C + hPrim3[1, 0] * L + hPrim3[1, 1] * I +
                                 hPrim3[1, 2] * R + hPrim3[2, 0] * N + hPrim3[2, 1] * D + hPrim3[2, 2] * M), 0);
                    }
                }
            }
            return matrixConvolution;
        }

        private byte[,] SegmentationAlgorithm(byte[,] output)//Segmenting algorithm (example: extracting bone from skin in medical images)
        {
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    output[i, j] = (byte)(Math.Max(Math.Min(maxImageValue, output[i, j]), minImageValue));//0

                    if (output[i, j] == minImageValue)
                    {
                        output[i, j] = 0;
                    }
                    if (output[i, j] == maxImageValue)
                    {
                        output[i, j] = 255;
                    }
                }
            }

            return output;
        }

        private void PrintMatrixConsole(float[,] matrixOutput)
        {
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    Console.Write(matrixOutput[i, j] + "  ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private void OutputImage()
        {
            int outputWidth = 600;
            int outputHeight = 632;
            int rows = matrixOutput.GetLength(0);
            int cols = matrixOutput.GetLength(1);

            picOutputImage.Image = MatrixToImage(matrixOutput).GetThumbnailImage(outputWidth, outputHeight, null, System.IntPtr.Zero);
        }

        private Bitmap MatrixToImage(byte[,] output)//Conert from matrix to image
        {
            Bitmap bmp = new Bitmap(matrixCols, matrixRows);
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCols; j++)
                {
                    // True because of byte type
                    output[i, j] = (byte)Math.Max(Math.Min((byte)255, output[i, j]), (byte)0);//0

                    bmp.SetPixel(j, i, Color.FromArgb(255, Convert.ToInt16(output[i, j]),
                    Convert.ToInt16(output[i, j]), Convert.ToInt16(output[i, j])) /*Color.FromArgb(a, avg, avg, avg)*/);
                }
            }
            return bmp;
        }

        private void BtnSaveAs_Click(object sender, EventArgs e)//Save as
        {
            if (inputImageIsSet && inputImageIsConverted)
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "Jpg-|*.jpg|Jpeg-|*.jpeg|Png-|*.png|Bitmaps|*.bmp|Tif|*.tif"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmp = MatrixToImage(matrixOutput);

                    bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                }
            }
        }

        private void ImageIsConvertedMethod()
        {
            OutputImage();
            input = matrixOutput;

            lblOutputWidth.Text = "Output Image Width: " + Convert.ToString(matrixCols);
            lblOutputHeight.Text = "Output Image Height: " + Convert.ToString(matrixRows);
            lblDeltaT.Text = "DeltaT value: " + Convert.ToString(deltaT);

            inputImageIsConverted = true;
        }

        //*****************************************************************new

        private void btnUpsampleStandard_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet)
            {
                DisableButtons();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate (object s, DoWorkEventArgs args)
                {
                    args.Result = UpsamplingAlgorithm(matrixOutput);
                };
                worker.RunWorkerCompleted += delegate (object s, RunWorkerCompletedEventArgs args)
                {
                    matrixOutput = (byte[,])args.Result;
                    ImageIsConvertedMethod();
                    EnableButtons();
                };
                worker.RunWorkerAsync();
            }
        }
        private void DisableButtons()
        {
            btnImproveReadabilityStandard.Enabled = false;
            btnDownsampleEdgePreserving.Enabled = false;
            btnConvert.Enabled = false;
            btnDownsampleStandard.Enabled = false;
            btnImproveReadabilityEdgePreserving.Enabled = false;
            btnLoadImage.Enabled = false;
            btnSaveAs.Enabled = false;
            btnSegment.Enabled = false;
            btnUpsampleStandard.Enabled = false;
            btnUpsampleEdgePreserving.Enabled = false;
            btnImproveReadabilityStandard.Enabled = false;
        }

        /**
         * Resize the image to appropriate max size
         */
        private Tuple<int, int> scale(int rows, int cols, int maxWidth)
        {
            int newImageSizeHeight = maxWidth;
            float factor = (float) maxWidth / rows;
            int newImageSizeWidth = (int) Math.Round((cols * factor));
            if (rows <= maxWidth)
            {
                newImageSizeHeight = rows;
                newImageSizeWidth = cols;
            }
            return Tuple.Create(newImageSizeHeight, newImageSizeWidth);
        }

        private async Task<string> SendImage(Image img, string url)
        {
            int rows = img.Height;
            int cols = img.Width;

            byte[] imageWebR = new byte[rows * cols];
            byte[] imageWebG = new byte[rows * cols];
            byte[] imageWebB = new byte[rows * cols];


            // Scale the image

            (int newImageSizeHeight, int newImageSizeWidth) = this.scale(rows, cols, 100);

            Bitmap scaledImage = new Bitmap(img.GetThumbnailImage(newImageSizeHeight, newImageSizeWidth, null, System.IntPtr.Zero));

            for (int i = 0; i < newImageSizeHeight; i++)
            {
                for (int j = 0; j < newImageSizeWidth; j++)
                {
                    imageWebR[i * newImageSizeWidth + j] = scaledImage.GetPixel(i, j).R;
                    imageWebG[i * newImageSizeWidth + j] = scaledImage.GetPixel(i, j).G;
                    imageWebB[i * newImageSizeWidth + j] = scaledImage.GetPixel(i, j).B;
                }
            }
            HttpClient client = new HttpClient();

            var options = new
            {
                imageR = Convert.ToBase64String(imageWebR),
                imageG = Convert.ToBase64String(imageWebG),
                imageB = Convert.ToBase64String(imageWebB),
                Width = newImageSizeWidth,
                Height = newImageSizeHeight
            };

            var stringPayload = JsonConvert.SerializeObject(options);
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private async void btnRecogniseCovid_Click(object sender, EventArgs e)
        {
            btnRecogniseCovid.Text = "Loading...";
            btnRecogniseCovid.Enabled = false;
            try
            {
                string response = await this.SendImage(originalInput, "http://142.93.239.19:3456/covid");
                MessageBox.Show(response, "COVID-19 Recognition Results");
            } catch(Exception ex)
            {
                MessageBox.Show("Couldn't recognize object", "");
            }
            finally
            {
                btnRecogniseCovid.Text = "AI Recognise COVID-19";
                btnRecogniseCovid.Enabled = true;
            }
        }

        private async void btnRecogniseObject_Click(object sender, EventArgs e)
        {
            btnRecogniseObject.Text = "Loading...";
            btnRecogniseObject.Enabled = false;
            
            try
            {
                string response = await this.SendImage(originalInput, "http://142.93.239.19:3456/general");
                MessageBox.Show(response, "Object Recognition Results");
                
            } catch (Exception ex)
            {
                MessageBox.Show("Couldn't recognize object", "");
            }
            finally
            {
                btnRecogniseObject.Text = "Recognise Object";
                btnRecogniseObject.Enabled = true;
            }
            

           
        }

        private void EnableButtons()
        {
            btnImproveReadabilityStandard.Enabled = true;
            btnDownsampleEdgePreserving.Enabled = true;
            btnConvert.Enabled = true;
            btnDownsampleStandard.Enabled = true;
            btnImproveReadabilityEdgePreserving.Enabled = true;
            btnLoadImage.Enabled = true;
            btnSaveAs.Enabled = true;
            btnSegment.Enabled = true;
            btnUpsampleStandard.Enabled = true;
            btnUpsampleEdgePreserving.Enabled = true;
            btnImproveReadabilityStandard.Enabled = true;
        }

        private void btnUpsampleEdgePreserving_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet)
            {
                matrixOutput = EdgePreservationUpsamplingAlgorithm(matrixOutput);
                ImageIsConvertedMethod();
            }
        }

        private void btnDownsampleStandard_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet && matrixCols > (startImageWidth / 4) && matrixRows > (startImageHeight / 4))//Max two times downsampling 
            {
                matrixOutput = DownsamplingAlgorithm(matrixOutput);
                ImageIsConvertedMethod();
            }
        }

        private void btnDownsampleEdgePreserving_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet && matrixCols > (startImageWidth / 4) && matrixRows > (startImageHeight / 4))//Max two times downsampling 
            {
                matrixOutput = EdgePreservationDownsamplingAlgorithm(matrixOutput);
                ImageIsConvertedMethod();
            }
        }

        private void btnImproveReadabilityStandard_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet)
            {
                matrixOutput = ImprovingReadabilityStandard(matrixOutput);
                ImageIsConvertedMethod();
                deltaT += 0.001f;
            }
        }

        private void btnImproveReadabilityEdgePreserving_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet)
            {
                matrixOutput = ImprovingReadabilityEdgePreservation(matrixOutput);
                ImageIsConvertedMethod();
                deltaT += 0.001f;
            }
        }

        private void btnSegment_Click(object sender, EventArgs e)
        {
            if (inputImageIsSet)
            {
                matrixOutput = SegmentationAlgorithm(matrixOutput);
                ImageIsConvertedMethod();
            }
        }
    }
}