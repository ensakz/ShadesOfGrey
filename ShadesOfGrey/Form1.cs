namespace ShadesOfGrey
{
    public partial class Form1 : Form
    {
        private ImageProcessor _imageProcessor;
        public Form1()
        {
            InitializeComponent();
            _imageProcessor = new ImageProcessor();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // dialog box to choose file
            OpenFileDialog ofd = new OpenFileDialog();
            // file format filter
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            // in case if ok was clicked on dialog box
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // picture upload
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch // MessageBox in case of erroo
                {
                    MessageBox.Show("Can't open selected file", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // check if image is present in pictureBox1
            if (pictureBox1.Image != null)
            {
                // create Bitmap from image in pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // create Bitmap for grey picture
                Bitmap output = new Bitmap(input.Width, input.Height);
                // loop over all possible pixels
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // get (i, j) pixel
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // get pixel color components
                        // red
                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        // green
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        // blue
                        float B = (float)(pixel & 0x000000FF);
                        // turn image color to grey by finding arithmetic mean of R+G+B and assigning it to R,B,G
                        R = G = B = (R + G + B) / 3.0f;
                        // create new pixel
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // set new pixel to output
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // output grey (color to black) Bitmap to pictureBox2
                pictureBox2.Image = output;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) // if image is present in PictureBox2
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save picture as...";
                // whether to show "Overwrite file" if the user specifies a file name that already exists
                sfd.OverwritePrompt = true;
                // whether the dialog displays a warning if the user specifies a path that does not exist
                sfd.CheckPathExists = true;
                // file format filter
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                // if ok was pressed in Dialog box
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // save image
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch // Message box in case of error
                    {
                        MessageBox.Show("Impossible to save selected file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}