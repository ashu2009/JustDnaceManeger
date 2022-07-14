using System;
using System.IO;
using System.Windows.Media.Imaging;


namespace JustDnaceManeger.Maneger
{
    class PictureManager
    {
        //コンポーネント使用時に使用
        private MainWindow MAIN_COMPONENTS = null;
        //書き込み読み込み時に使用するクラス
        private ReadFileManeger readManegerCls = new ReadFileManeger();

        public string _pictureFileName = "";
        public BitmapImage _pictureData = null;

        public PictureManager(MainWindow mainComponents)
        {
            //コンポーネント保存
            MAIN_COMPONENTS = mainComponents;
        }

        //仮画像セット
        public void oncePictureSet(string pictureName)
        {
            //一旦生画像表示
            BitmapImage bi = new BitmapImage(new Uri(pictureName));
            _pictureData = bi;
            MAIN_COMPONENTS.DanceImage.Source = bi;
            _pictureFileName = pictureName.Substring(pictureName.LastIndexOf("\\") + 1);
        }

        //本画像保存
        public void pictureRegster(string pictureName)
        {
            try
            {
                // BitmapSourceを保存する
                using (Stream stream = new FileStream(readManegerCls.DANCE_IMAGE_DIRECTORY + "\\" + pictureName + ".png", FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(_pictureData));
                    encoder.Save(stream);
                }
            }
            catch (IOException e)
            {
            }
        }
    }
}
