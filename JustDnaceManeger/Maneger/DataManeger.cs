using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using System;

namespace JustDnaceManeger.Maneger
{
    class DataManeger
    {
        //System.Diagnostics.Trace.WriteLine(tagName);
        //コンポーネント使用時に使用
        private MainWindow MAIN_COMPONENTS = null;
        //書き込み読み込み時に使用するクラス
        private ReadFileManeger readManegerCls = new ReadFileManeger();
        //検索用タグ関係に使用するクラス
        private SearchManager searchManegerCls = null;
        //登録用タグ関係に使用するクラス
        private RegsterManager regsterManegerCls = null;
        //画像関係に使用するクラス
        private PictureManager pictureManegerCls = null;

        public DataManeger(MainWindow mainComponents)
        {
            //コンポーネント保存
            MAIN_COMPONENTS = mainComponents;
            //画像用
            pictureManegerCls = new PictureManager(mainComponents);
            //検索用タグパネル生成
            searchManegerCls = new SearchManager(mainComponents, pictureManegerCls);
            //詳細用パネル生成
            regsterManegerCls = new RegsterManager(mainComponents);
            regsterManegerCls.regsterTagPanelCreate(); 


            pictureManegerCls._pictureFileName = readManegerCls.DEFAULT_IMAGE;
            pictureManegerCls._pictureData = null;
            //仮画像セット
            pictureManegerCls.oncePictureSet(readManegerCls.DANCE_IMAGE_DIRECTORY + "\\" + readManegerCls.DEFAULT_IMAGE);
        }

        //登録データ分割
        private string splitData(string text, string beforeText)
        {
            int before = text.IndexOf(beforeText);
            int after = text.IndexOf(",", before);
            return text.Substring(before + beforeText.Length, after - before - beforeText.Length);
        }

        //指定のURLでサイトに飛ぶ
        public void goWebSite(string url)
        {
            try
            {
                ProcessStartInfo pi = new ProcessStartInfo()
                {
                    FileName = url,
                    UseShellExecute = true,
                };
                Process.Start(pi);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
            }
            catch (System.InvalidOperationException e)
            {
            }
        }

        //画像選択する
        public void dancePictureChoice()
        {
            //ダイアログ新規作成
            var ofDialog = new Microsoft.Win32.OpenFileDialog() { Filter = "選択画像形式 (*.png,*.PNG,*.jpg,*.JPG)|*.png;*.PNG;*.jpg;*.JPG", CheckFileExists = false };
            //ダイアログのタイトルを指定する
            ofDialog.Title = "JustDance画像選択";
            //ダイアログを表示し、okで決定と設定
            if ((bool)ofDialog.ShowDialog())
            {
                //仮画像として保存
                pictureManegerCls.oncePictureSet(ofDialog.FileName);
            }
        }

        //データ登録/修正
        public void regsterData()
        {
            //ディレクトリ無ければ作成
            if (!Directory.Exists(readManegerCls.CONFIG_DIRECTORY)) Directory.CreateDirectory(readManegerCls.CONFIG_DIRECTORY);
            //ファイルしなければ作成
            if (!File.Exists(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
            {
                //ファイル書き込み
                using (StreamWriter write = new StreamWriter(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME, false))
                {
                }
            }
            //記録予定のデータ
            string danceTitle = nameReplace(MAIN_COMPONENTS.DanceTitleTextBox.Text);
            MAIN_COMPONENTS.DanceTitleTextBox.Text = "";
            string danceNumber = nameReplace(MAIN_COMPONENTS.DanceNumberTextBox.Text);
            MAIN_COMPONENTS.DanceNumberTextBox.Text = "";
            if ((danceTitle == "") || (danceNumber == "")) return;
            string youtubeURL = nameReplace(MAIN_COMPONENTS.youtubeURL.Text);
            MAIN_COMPONENTS.youtubeURL.Text = "";
            if (youtubeURL == "") youtubeURL = "null";
            string memo = nameReplace(MAIN_COMPONENTS.DanceMemoTextBox.Text);
            MAIN_COMPONENTS.DanceMemoTextBox.Text = "";
            if (memo == "") memo = "null";
            string pictureSource = nameReplace(pictureManegerCls._pictureFileName);
            if (pictureSource != readManegerCls.DEFAULT_IMAGE)
            {
                pictureSource = danceTitle + ".png";
                pictureManegerCls.pictureRegster(danceTitle);
            }
            pictureManegerCls.oncePictureSet(readManegerCls.DANCE_IMAGE_DIRECTORY + "\\" + readManegerCls.DEFAULT_IMAGE);
            pictureManegerCls._pictureFileName = readManegerCls.DEFAULT_IMAGE; 
            //日付
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy/MM/dd/HH:mm:ss");
            MAIN_COMPONENTS.DanceDataTextBox.Text = "最終更新日時：----/--/--/--:--:--";

            //自由タグ一覧
            string[] tagList = new string[] { };

            //ファイルあれば
            if (File.Exists(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME))
            {
                //ファイル読み込み
                using (StreamReader reader = new StreamReader(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME))
                {
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Array.Resize(ref tagList, tagList.Length + 1);
                        tagList[tagList.Length - 1] = splitData(line, readManegerCls.FIX_SEARCH_TAG_TagName);
                    }
                }
            }

            //書き込み内容
            string writeText = "";

            //固定タグ+自由タグカウント
            UIElementCollection detailMainStackPanelChildlen = MAIN_COMPONENTS.DetailMainStackPanel.Children;
            int componentCount = detailMainStackPanelChildlen.Count;
            //ファイル読み込み
            using (StreamReader reader = new StreamReader(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
            {
                //1行ずつ確認更新
                string line = null;
                int dataCount = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    //曲名
                    string danceName = splitData(line, "danceName ");
                    if (danceName != danceTitle)
                    {
                        if (dataCount != 0) writeText += "\n";
                        writeText += line;
                        dataCount++;
                    }
                }
                if (dataCount != 0) writeText += "\n";
                writeText += "danceName " + danceTitle + ",";
                writeText += "danceNumber " + danceNumber + ",";
                writeText += "danceURL " + youtubeURL + ",";
                writeText += "memo " + memo + ",";
                writeText += "dancePicture " + pictureSource + ",";
                writeText += "date " + "最終更新日時：" + date + ",";

                for (int count = 13; count < componentCount; count++)
                {
                    //タグカウント
                    StackPanel detailTagStackPanel = (StackPanel)detailMainStackPanelChildlen[count];
                    UIElementCollection detailTagStackPanelChildlen = detailTagStackPanel.Children;
                    int tagCountMax = detailTagStackPanelChildlen.Count;
                    for (int tagCount = 1; tagCount < tagCountMax; tagCount++)
                    {
                        StackPanel tagStackPanel = (StackPanel)detailTagStackPanelChildlen[tagCount];
                        TextBox tagText = (TextBox)tagStackPanel.Children[0];
                        string text = nameReplace(tagText.Text);
                        if (text != "")
                        {
                            //自由タグの時のみここ
                            if (count == (componentCount - 1))
                            {
                                //タグ比較
                                bool isNoContains = true;
                                for (int tagListCount = 0; tagListCount < tagList.Length; tagListCount++)
                                {
                                    if (tagList[tagListCount] == text)
                                    {
                                        isNoContains = false;
                                        break;
                                    }
                                }
                                if (isNoContains)
                                {
                                    //ファイル書き込み
                                    using (StreamWriter write = new StreamWriter(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME, true))
                                    {
                                        write.WriteLine(readManegerCls.FIX_SEARCH_TAG_TagName + tagText.Text + "," + readManegerCls.FIX_SEARCH_TAG_TagThumbnail + readManegerCls.DEFAULT_IMAGE + ",");

                                        string tagName = tagText.Text;
                                        string tagThumbnail = readManegerCls.DEFAULT_IMAGE;
                                        searchManegerCls.addFreeSearchTag(tagName, tagThumbnail);
                                    }
                                }
                            }
                            StackPanel detailStackPanel = (StackPanel)detailTagStackPanelChildlen[0];
                            TextBlock detailFileTextBlock = (TextBlock)detailStackPanel.Children[1];
                            writeText += "tag" + detailFileTextBlock.Text + " " + tagText.Text + ",";
                        }
                    }
                }
            }

            //ファイル書き込み
            using (StreamWriter write = new StreamWriter(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME, false))
            {
                write.Write(writeText);
            }
            searchManegerCls.resultShow();

            //詳細用パネル生成
            MAIN_COMPONENTS.DetailMainStackPanel.Children.RemoveRange(13, componentCount - 13);
            regsterManegerCls = new RegsterManager(MAIN_COMPONENTS);
            regsterManegerCls.regsterTagPanelCreate();
        }

        //仮画像セット
        public void oncePictureSet(string pictureName)
        {
            pictureManegerCls.oncePictureSet(pictureName);
        }

        //検索結果表示
        public void resultShow()
        {
            searchManegerCls.resultShow();
        }

        //listviewから詳細書き出し
        public void SearchDanceListView_MouseDoubleClick() {
            searchManegerCls.SearchDanceListView_MouseDoubleClick();
        }

        //データ置き換え
        private string nameReplace(string name)
        {
            if (name != null)
            {
                name = name.Replace(",", "，");
                name = name.Replace(" ", "　");
            }
            return name;
        }
    }
}
