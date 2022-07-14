using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace JustDnaceManeger.Maneger
{
    class SearchManager
    {
        //コンポーネント使用時に使用
        private MainWindow MAIN_COMPONENTS = null;
        //書き込み読み込み時に使用するクラス
        private ReadFileManeger readManegerCls = new ReadFileManeger();
        //画像関係に使用するクラス
        private PictureManager pictureManegerCls = null;
        //登録用タグ関係に使用するクラス
        private RegsterManager regsterManegerCls = null;

        //検索するタグ一覧
        public string[][] searchTags = new string[][] { };
        //検索するタグ一覧
        public string[][] noSearchTags = new string[][] { };

        public SearchManager(MainWindow mainComponents, PictureManager pictureManeger)
        {
            //コンポーネント保存
            MAIN_COMPONENTS = mainComponents;
            //画像用
            pictureManegerCls = pictureManeger;
            //詳細用パネル生成
            regsterManegerCls = new RegsterManager(mainComponents);

            searchTagPanelCreate();
            resultShow();
        }

        //検索用タグパネル生成
        private void searchTagPanelCreate()
        {
            fixSearchTagPanelCreate();
            freeSearchTagPanelCreate();
        }

        //検索用固定タグパネル生成
        private void fixSearchTagPanelCreate()
        {
            //ディレクトリのファイル読み込む
            var txtFiles = Directory.EnumerateFiles(readManegerCls.FIX_SEARCH_TAG_DIRECTORY, "*.txt", SearchOption.AllDirectories);
            foreach (string currentFile in txtFiles)
            {
                //ファイル名確保
                string fileName = currentFile.Substring(readManegerCls.FIX_SEARCH_TAG_DIRECTORY.Length + 1);

                //ファイル読み込み
                using (StreamReader reader = new StreamReader(readManegerCls.FIX_SEARCH_TAG_DIRECTORY + "\\" + fileName))
                {
                    addSearchTagDetail(fileName);
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string tagName = splitData(line, readManegerCls.FIX_SEARCH_TAG_TagName);
                        string tagThumbnail = splitData(line, readManegerCls.FIX_SEARCH_TAG_TagThumbnail);
                        addFreeSearchTag(tagName, tagThumbnail);
                    }
                }
            }
        }

        //検索用タグパネル生成
        private void freeSearchTagPanelCreate()
        {
            addSearchTagDetail(readManegerCls.OPTION_SEARCH_TAG_NAME);
            //ファイル存在したら作成
            if (File.Exists(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME))
            {
                //ファイル読み込み
                using (StreamReader reader = new StreamReader(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME))
                {
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string tagName = splitData(line, readManegerCls.FIX_SEARCH_TAG_TagName);
                        string tagThumbnail = splitData(line, readManegerCls.FIX_SEARCH_TAG_TagThumbnail);
                        addFreeSearchTag(tagName, tagThumbnail);
                    }
                }
            }
        }

        //共通事項、検索タグフォーカス機能
        private void commonSearchTagStackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel searchTagStackPanel = (StackPanel)sender;
            searchTagStackPanel.Background = System.Windows.Media.Brushes.PowderBlue;
        }

        //共通事項、検索タグアウトフォーカス機能
        private void commonSearchTagStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            //StackPanel下のTextBlock内容取得
            StackPanel searchTagStackPanel = (StackPanel)sender;
            TextBlock thumbnailTextBlock = (TextBlock)searchTagStackPanel.Children[1];

            WrapPanel searchTagWrapPanel = (WrapPanel)searchTagStackPanel.Parent;
            Grid grid = (Grid)searchTagWrapPanel.Parent;
            StackPanel SearchTagsMainStackPanel = (StackPanel)grid.Parent;
            int tagIndex = (SearchTagsMainStackPanel.Children.IndexOf(grid)-4) / 2 - 1;

            int searchIndex = Array.IndexOf(searchTags[tagIndex], thumbnailTextBlock.Text);
            int noSearchIndex = Array.IndexOf(noSearchTags[tagIndex], thumbnailTextBlock.Text);
            //検索タグ存在しなければ保存、存在したら検索タグから削除して非検索タグへ追加、非検索タグならば削除
            if ((searchIndex == -1) && (noSearchIndex == -1))
            {
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.White;
            }
            else if ((searchIndex != -1) && (noSearchIndex == -1))
            {
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.Green;
            }
            else
            {
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.Red;
            }
        }

        //共通事項、検索タグクリック時動作
        private void commonSearchTagStackPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //StackPanel下のTextBlock内容取得
            StackPanel searchTagStackPanel = (StackPanel)sender;
            TextBlock thumbnailTextBlock = (TextBlock)searchTagStackPanel.Children[1];

            WrapPanel searchTagWrapPanel = (WrapPanel)searchTagStackPanel.Parent;
            Grid grid = (Grid)searchTagWrapPanel.Parent;
            StackPanel SearchTagsMainStackPanel = (StackPanel)grid.Parent;
            int tagIndex = (SearchTagsMainStackPanel.Children.IndexOf(grid) - 4) / 2 - 1;

            int searchIndex = Array.IndexOf(searchTags[tagIndex], thumbnailTextBlock.Text);
            int noSearchIndex = Array.IndexOf(noSearchTags[tagIndex], thumbnailTextBlock.Text);
            //検索タグ存在しなければ保存、存在したら検索タグから削除して非検索タグへ追加、非検索タグならば削除
            if ((searchIndex == -1) && (noSearchIndex == -1))
            {
                //検索タグとして保存
                Array.Resize(ref searchTags[tagIndex], searchTags[tagIndex].Length + 1);
                searchTags[tagIndex][searchTags[tagIndex].Length - 1] = thumbnailTextBlock.Text;
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.Green;
            }
            else if ((searchIndex != -1) && (noSearchIndex == -1))
            {
                //検索タグから削除
                string[] data = new string[] { };
                Array.Resize(ref data, searchTags[tagIndex].Length - 1);
                int count = 0;
                int tagCount = 0;
                while (true)
                {
                    //タグ全部終わった
                    if (count >= searchTags[tagIndex].Length) break;
                    //指定タグのみ除外
                    if (count != searchIndex)
                    {
                        data[tagCount] = searchTags[tagIndex][count];
                        tagCount++;
                    }
                    count++;
                }
                searchTags[tagIndex] = data;
                //非検索タグ追加
                Array.Resize(ref noSearchTags[tagIndex], noSearchTags[tagIndex].Length + 1);
                noSearchTags[tagIndex][noSearchTags[tagIndex].Length - 1] = thumbnailTextBlock.Text;
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                //非検索タグから削除
                string[] data = new string[] { };
                Array.Resize(ref data, noSearchTags[tagIndex].Length - 1);
                if (data.Length > 0)
                {
                    int tagCount = 0;
                    for (int count = 0; count < noSearchTags[tagIndex].Length; count++)
                    {
                        //指定タグのみ除外
                        if (count != noSearchIndex)
                        {
                            data[tagCount] = noSearchTags[tagIndex][count];
                            tagCount++;
                        }
                    }
                }
                noSearchTags[tagIndex] = data;
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.White;
            }

            resultShow();
        }

        //タグ詳細追加
        private void addSearchTagDetail(string fileName)
        {
            //タグ一覧詳細としてのStackPanel
            StackPanel searchTagsDetailStackPanel = new StackPanel();
            searchTagsDetailStackPanel.Width = 228;
            searchTagsDetailStackPanel.Orientation = Orientation.Horizontal;
            searchTagsDetailStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            MAIN_COMPONENTS.SearchTagsMainStackPanel.Children.Add(searchTagsDetailStackPanel);
            //タグファイル名表記text追加
            TextBlock tagFileTextBlock = new TextBlock();
            tagFileTextBlock.Text = fileName;
            tagFileTextBlock.TextWrapping = TextWrapping.Wrap;
            tagFileTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            tagFileTextBlock.VerticalAlignment = VerticalAlignment.Top;
            tagFileTextBlock.Margin = new Thickness(0, 0, 10, 0);
            searchTagsDetailStackPanel.Children.Add(tagFileTextBlock);
            //タグ全選択ボタン
            Button tagAllChoiceButton = new Button();
            tagAllChoiceButton.Content = "全選択";
            tagAllChoiceButton.HorizontalAlignment = HorizontalAlignment.Left;
            tagAllChoiceButton.VerticalAlignment = VerticalAlignment.Top;
            tagAllChoiceButton.Margin = new Thickness(0, 0, 10, 0);
            tagAllChoiceButton.Click += allSearchTagChose_Click;
            searchTagsDetailStackPanel.Children.Add(tagAllChoiceButton);
            //タグ全解除ボタン
            Button tagAllReleaseButton = new Button();
            tagAllReleaseButton.Content = "全解除";
            tagAllReleaseButton.HorizontalAlignment = HorizontalAlignment.Left;
            tagAllReleaseButton.VerticalAlignment = VerticalAlignment.Top;
            tagAllReleaseButton.Click += allSearchTagRelease_Click;
            searchTagsDetailStackPanel.Children.Add(tagAllReleaseButton);

            //タグファイル纏めるgrid追加
            Grid grid = new Grid();
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            MAIN_COMPONENTS.SearchTagsMainStackPanel.Children.Add(grid);
            //タグ一覧のWrapPanel
            WrapPanel searchTagWrapPanel = new WrapPanel();
            searchTagWrapPanel.Background = System.Windows.Media.Brushes.WhiteSmoke;
            searchTagWrapPanel.Orientation = Orientation.Horizontal;
            searchTagWrapPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            searchTagWrapPanel.VerticalAlignment = VerticalAlignment.Stretch;
            searchTagWrapPanel.Margin = new Thickness(3, 3, 3, 3);
            grid.Children.Add(searchTagWrapPanel);

            Array.Resize(ref searchTags, searchTags.Length + 1);
            searchTags[searchTags.Length - 1] = new string[] { };
            Array.Resize(ref noSearchTags, noSearchTags.Length + 1);
            noSearchTags[noSearchTags.Length - 1] = new string[] { };
        }

        //タグ追加動作
        public void addFreeSearchTag(string tagName, string tagThumbnail)
        {
            UIElementCollection SearchTagsMainStackPanelChildren = MAIN_COMPONENTS.SearchTagsMainStackPanel.Children;
            int SearchTagsMainStackPanelChildrenCount = SearchTagsMainStackPanelChildren.Count;
            Grid grid = (Grid)SearchTagsMainStackPanelChildren[SearchTagsMainStackPanelChildrenCount - 1];
            WrapPanel searchTagWrapPanel = (WrapPanel)grid.Children[0];

            //1つのタグとしてのStackPanel
            StackPanel searchTagStackPanel = new StackPanel();
            searchTagStackPanel.Width = 70;
            searchTagStackPanel.Background = System.Windows.Media.Brushes.White;
            searchTagStackPanel.Orientation = Orientation.Vertical;
            searchTagStackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            searchTagStackPanel.VerticalAlignment = VerticalAlignment.Top;
            searchTagStackPanel.Margin = new Thickness(3, 3, 3, 3);
            searchTagStackPanel.MouseEnter += commonSearchTagStackPanel_MouseEnter;
            searchTagStackPanel.MouseLeave += commonSearchTagStackPanel_MouseLeave;
            searchTagStackPanel.MouseLeftButtonDown += commonSearchTagStackPanel_MouseClick;
            searchTagWrapPanel.Children.Add(searchTagStackPanel);
            //1つのタグのimage
            Image thumbnailImage = new Image();
            thumbnailImage.Margin = new Thickness(3, 3, 3, 3);
            //サムネイル登録済み
            if (tagThumbnail != readManegerCls.DEFAULT_IMAGE)
            {
                //サムネイルURL
                string thumbnaileURL = readManegerCls.FIX_SEARCH_TAG_IMAGE_DIRECTORY + "\\" + tagThumbnail;
                BitmapImage bi = new BitmapImage(new Uri(thumbnaileURL));
                thumbnailImage.Source = bi;
            }
            if (thumbnailImage.Source == null) thumbnailImage.Source = new BitmapImage(new Uri(readManegerCls.FIX_SEARCH_TAG_IMAGE_DIRECTORY + "\\" + tagThumbnail));
            searchTagStackPanel.Children.Add(thumbnailImage);
            //1つのタグ名称のtext
            TextBlock thumbnailTextBlock = new TextBlock();
            thumbnailTextBlock.TextWrapping = TextWrapping.Wrap;
            thumbnailTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            thumbnailTextBlock.VerticalAlignment = VerticalAlignment.Top;
            thumbnailTextBlock.Width = searchTagStackPanel.Width;
            thumbnailTextBlock.Text = tagName;
            searchTagStackPanel.Children.Add(thumbnailTextBlock);
        }

        //タグ全選択動作
        private void allSearchTagChose_Click(object sender, RoutedEventArgs e)
        {
            Button tagAllChoiceButton = (Button)sender;
            StackPanel searchTagsDetailStackPanel = (StackPanel)tagAllChoiceButton.Parent;
            StackPanel SearchTagsMainStackPanel = (StackPanel)searchTagsDetailStackPanel.Parent;
            int gridNumber = SearchTagsMainStackPanel.Children.IndexOf(searchTagsDetailStackPanel) + 1;
            Grid grid = (Grid)SearchTagsMainStackPanel.Children[gridNumber];
            WrapPanel searchTagWrapPanel = (WrapPanel)grid.Children[0];
            int searchTagStackPanelCount = searchTagWrapPanel.Children.Count;

            int tagIndex = (SearchTagsMainStackPanel.Children.IndexOf(grid) - 4) / 2 - 1;

            searchTags[tagIndex] = new string[] { };
            noSearchTags[tagIndex] = new string[] { };
            for (int tagCount = 0; tagCount < searchTagStackPanelCount; tagCount++)
            {
                StackPanel searchTagStackPanel = (StackPanel)searchTagWrapPanel.Children[tagCount];
                TextBlock thumbnailTextBlock = (TextBlock)searchTagStackPanel.Children[1];
                //検索タグとして保存
                Array.Resize(ref searchTags[tagIndex], searchTags[tagIndex].Length + 1);
                searchTags[tagIndex][searchTags[tagIndex].Length - 1] = thumbnailTextBlock.Text;
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.Green;
            }

            resultShow();
        }

        //タグ全解除動作
        private void allSearchTagRelease_Click(object sender, RoutedEventArgs e)
        {
            Button tagAllChoiceButton = (Button)sender;
            StackPanel searchTagsDetailStackPanel = (StackPanel)tagAllChoiceButton.Parent;
            StackPanel SearchTagsMainStackPanel = (StackPanel)searchTagsDetailStackPanel.Parent;
            int gridNumber = SearchTagsMainStackPanel.Children.IndexOf(searchTagsDetailStackPanel) + 1;
            Grid grid = (Grid)SearchTagsMainStackPanel.Children[gridNumber];
            WrapPanel searchTagWrapPanel = (WrapPanel)grid.Children[0];
            int searchTagStackPanelCount = searchTagWrapPanel.Children.Count;

            int tagIndex = (SearchTagsMainStackPanel.Children.IndexOf(grid) - 4) / 2 - 1;

            searchTags[tagIndex] = new string[] { };
            noSearchTags[tagIndex] = new string[] { };
            for (int tagCount = 0; tagCount < searchTagStackPanelCount; tagCount++)
            {
                StackPanel searchTagStackPanel = (StackPanel)searchTagWrapPanel.Children[tagCount];
                //色変更
                searchTagStackPanel.Background = System.Windows.Media.Brushes.White;
            }

            resultShow();
        }

        //登録データ分割
        private string splitData(string text, string beforeText)
        {
            int before = text.IndexOf(beforeText);
            int after = text.IndexOf(",", before);
            return text.Substring(before + beforeText.Length, after - before - beforeText.Length);
        }


        ObservableCollection<Hoge> Piyo = new ObservableCollection<Hoge>();
        private class Hoge
        {
            public string DanceNumber { get; set; }
            public string DanceTitle { get; set; }
            public string DanceMemo { get; set; }
        }

        //検索結果表示
        public void resultShow()
        {
            MAIN_COMPONENTS.SearchDanceListView.DataContext = null;
            MAIN_COMPONENTS.SearchDanceListView.Items.SortDescriptions.Clear();
            Piyo = new ObservableCollection<Hoge>();
            //ファイル存在したら作成
            if (File.Exists(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
            {
                //ファイル読み込み
                using (StreamReader reader = new StreamReader(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
                {
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string danceName = splitData(line, "danceName ");
                        string danceMemo = splitData(line, "memo ");
                        bool isSearch = true;
                        if (MAIN_COMPONENTS.SearchDanceTitleTextBox.Text != "")
                        {
                            isSearch = danceName.Contains(MAIN_COMPONENTS.SearchDanceTitleTextBox.Text);
                        }
                        if (isSearch && (MAIN_COMPONENTS.SearchMemoTextBox.Text != ""))
                        {
                            isSearch = danceMemo.Contains(MAIN_COMPONENTS.SearchMemoTextBox.Text);
                        }
                        if (isSearch)
                        {
                            string danceNumber = splitData(line, "danceNumber ");
                            if (danceMemo == "null") danceMemo = "";

                            bool isEnable = true;
                            bool isAddEnable = false;//falseにするとタグなしの検索しにくい

                            //ディレクトリのファイル読み込む
                            var txtFiles = Directory.EnumerateFiles(readManegerCls.FIX_SEARCH_TAG_DIRECTORY, "*.txt", SearchOption.AllDirectories);
                            int tagIndex = -1;
                            foreach (string currentFile in txtFiles)
                            {
                                //ファイル名確保
                                string fileName = currentFile.Substring(readManegerCls.FIX_SEARCH_TAG_DIRECTORY.Length + 1);
                                tagIndex = line.IndexOf("tag" + fileName + " ");
                                if (tagIndex != -1) break;
                            }

                            bool isNoTag = true;
                            //タグ一切ない場合通す
                            for (int detailCount = 0; detailCount < noSearchTags.Length; detailCount++)
                            {
                                if (noSearchTags[detailCount].Length != 0)
                                {
                                    isNoTag = false;
                                }
                            }
                            for (int detailCount = 0; detailCount < searchTags.Length; detailCount++)
                            {
                                if (searchTags[detailCount].Length != 0)
                                {
                                    isNoTag = false;
                                }
                            }

                            //タグがあり表記可能
                            if ((tagIndex != -1) && !isNoTag)
                            {
                                string tagDatas = line.Substring(tagIndex);
                                bool tagContains = false;
                                string tagKinds = "";
                                foreach (string currentFile in txtFiles)
                                {
                                    //ファイル名確保
                                    string fileName = currentFile.Substring(readManegerCls.FIX_SEARCH_TAG_DIRECTORY.Length + 1);
                                    tagKinds = "tag" + fileName + " ";
                                    tagContains = tagDatas.Contains(tagKinds);
                                    if (tagContains) break;
                                    else
                                    {
                                        tagKinds = "tag" + "オプションタグ" + " ";
                                        if (tagDatas.Contains(tagKinds))
                                        {
                                            tagContains = true;
                                            break;
                                        }
                                    }
                                }
                                while (tagContains)
                                {
                                    foreach (string currentFile in txtFiles)
                                    {
                                        //ファイル名確保
                                        string fileName = currentFile.Substring(readManegerCls.FIX_SEARCH_TAG_DIRECTORY.Length + 1);
                                        tagKinds = "tag" + fileName + " ";
                                        tagContains = tagDatas.Contains(tagKinds);
                                        if (tagContains) break;
                                        else {
                                            tagKinds = "tag" + "オプションタグ" + " ";
                                            if (tagDatas.Contains(tagKinds))
                                            {
                                                tagContains = true;
                                                break;
                                            }
                                        } 
                                    }
                                    if (tagDatas == "") break;
                                    if (!tagDatas.Contains(tagKinds)) break;
                                    int tagNameStartIndex = tagDatas.IndexOf(tagKinds);
                                    int tagNameEndIndex = tagDatas.IndexOf(",", tagNameStartIndex);
                                    string tagName = tagDatas.Substring(tagNameStartIndex+tagKinds.Length,tagNameEndIndex - tagNameStartIndex - tagKinds.Length);
                                    tagDatas = tagDatas.Remove(tagNameStartIndex, tagNameEndIndex - tagNameStartIndex+1);
                                    for (int detailCount = 0; detailCount < noSearchTags.Length; detailCount++)
                                    {
                                        for (int tagCount = 0; tagCount < noSearchTags[detailCount].Length; tagCount++)
                                        {
                                            //除外タグに引っかかれば
                                            if (noSearchTags[detailCount][tagCount] == tagName)
                                            {
                                                isAddEnable = false;
                                                isEnable = false;
                                                break;
                                            }
                                        }
                                        if (!isEnable) break;
                                    }
                                    if (!isEnable) break;

                                    for (int detailCount = 0; detailCount < searchTags.Length; detailCount++)
                                    {
                                        for (int tagCount = 0; tagCount < searchTags[detailCount].Length; tagCount++)
                                        {
                                            //検索タグに引っかかれば
                                            if (searchTags[detailCount][tagCount] == tagName)
                                            {
                                                isAddEnable = true;
                                            }
                                        }
                                    }
                                }
                            }
                            if (isAddEnable || isNoTag)
                            {
                                //アイテム
                                //string[] item = { danceNumber, danceName,  danceMemo };
                                //MAIN_COMPONENTS.SearchDanceListView.Items.Add(item);
                                Piyo.Add(new Hoge() { DanceNumber = danceNumber, DanceTitle = danceName, DanceMemo = danceMemo });
                                MAIN_COMPONENTS.SearchDanceListView.DataContext = Piyo;
                            }
                        }
                    }
                }
            }

            MAIN_COMPONENTS.SearchDanceListView.Items.SortDescriptions.Add(new SortDescription("DanceTitle", ListSortDirection.Ascending));
        }

        //listviewから詳細書き出し
        public void SearchDanceListView_MouseDoubleClick()
        {
            Hoge data = (Hoge)MAIN_COMPONENTS.SearchDanceListView.SelectedItem;
            string danceTitle = data.DanceTitle;
            //ファイル存在したら作成
            if (File.Exists(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
            {
                //ファイル読み込み
                using (StreamReader reader = new StreamReader(readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.DATA_FILE_NAME))
                {
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string danceName = splitData(line, "danceName ");
                        if (danceName == danceTitle)
                        {
                            //固定タグ+自由タグカウント
                            UIElementCollection detailMainStackPanelChildlen = MAIN_COMPONENTS.DetailMainStackPanel.Children;
                            int componentCount = detailMainStackPanelChildlen.Count;
                            //詳細用パネル生成
                            MAIN_COMPONENTS.DetailMainStackPanel.Children.RemoveRange(13, componentCount - 13);
                            regsterManegerCls = new RegsterManager(MAIN_COMPONENTS);
                            regsterManegerCls.regsterTagPanelCreate();

                            MAIN_COMPONENTS.DanceTitleTextBox.Text = danceName;
                            MAIN_COMPONENTS.DanceNumberTextBox.Text = splitData(line, "danceNumber ");
                            MAIN_COMPONENTS.youtubeURL.Text = splitData(line, "danceURL ");
                            if (MAIN_COMPONENTS.youtubeURL.Text == "null") MAIN_COMPONENTS.youtubeURL.Text = "";
                            MAIN_COMPONENTS.DanceMemoTextBox.Text = splitData(line, "memo ");
                            if (MAIN_COMPONENTS.DanceMemoTextBox.Text == "null") MAIN_COMPONENTS.DanceMemoTextBox.Text = "";
                            MAIN_COMPONENTS.DanceDataTextBox.Text = splitData(line, "date ");

                            //タグのみ残す
                            string tagText = line.Substring(line.IndexOf("date "));
                            tagText = tagText.Substring(tagText.IndexOf(",")+1);

                            for (int count = 13; count < componentCount; count++)
                            {
                                //タグカウント
                                StackPanel detailTagStackPanel = (StackPanel)detailMainStackPanelChildlen[count];
                                UIElementCollection detailTagStackPanelChildlen = detailTagStackPanel.Children;
                                StackPanel detailStackPanel = (StackPanel)detailTagStackPanelChildlen[0];
                                Button tagAddButton = (Button)detailStackPanel.Children[0];
                                TextBlock detailFileTextBlock = (TextBlock)detailStackPanel.Children[1];
                                //"tag"+これ+" "が認識名
                                string tagOption = "tag" + detailFileTextBlock.Text + " ";
                                while (true)
                                {
                                    if (tagText == "") break;
                                    if (!tagText.Contains(tagOption)) break;
                                    int tagStartIndex = tagText.IndexOf(tagOption);
                                    int tagEndIndex = tagText.IndexOf(",", tagStartIndex);
                                    string tag=tagText.Substring(tagStartIndex + tagOption.Length, tagEndIndex-(tagStartIndex + tagOption.Length));

                                    //ボタンによる追加行動動作
                                        regsterManegerCls.commonRegsterAddFixTag_Click(tagAddButton, null);
                                        int detailStackPanelCount = detailTagStackPanelChildlen.Count;
                                        StackPanel tagStackPanel = (StackPanel)detailTagStackPanelChildlen[detailStackPanelCount - 1];
                                        TextBox textBox = (TextBox)tagStackPanel.Children[0];
                                    textBox.Text = tag;

                                    tagText = tagText.Remove(tagStartIndex, tagEndIndex - tagStartIndex+1);
                                }
                            }
                            pictureManegerCls.oncePictureSet(readManegerCls.DANCE_IMAGE_DIRECTORY + "\\" + splitData(line, "dancePicture "));
                            return;
                        }
                    }
                }
            }
            pictureManegerCls. oncePictureSet(readManegerCls.DANCE_IMAGE_DIRECTORY + "\\" + readManegerCls.DEFAULT_IMAGE);
        }
    }
}