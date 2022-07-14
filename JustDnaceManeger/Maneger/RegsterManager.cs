using System.IO;
using System.Windows;
using System.Windows.Controls;
using System;

namespace JustDnaceManeger.Maneger
{
    class RegsterManager
    {
        //コンポーネント使用時に使用
        private MainWindow MAIN_COMPONENTS = null;
        //書き込み読み込み時に使用するクラス
        private ReadFileManeger readManegerCls = new ReadFileManeger();

        public RegsterManager(MainWindow mainComponents)
        {
            //コンポーネント保存
            MAIN_COMPONENTS = mainComponents;
        }

        //タグの登録用パネル生成
        public void regsterTagPanelCreate()
        {
            fixRegsterTagPanelCreate();
            freeRegsterTagPanelCreate();
        }

        //登録用固定タグパネル生成
        private void fixRegsterTagPanelCreate()
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
                    commonRegsterAddFixTagComponent(fileName);
                }
            }
        }

        //登録用タグパネル生成
        private void freeRegsterTagPanelCreate()
        {
            commonRegsterAddFixTagComponent(readManegerCls.OPTION_SEARCH_TAG_NAME);
        }

        //パネルデータ作成
        private void commonRegsterAddFixTagComponent(string textInf)
        {
            //タグ一覧としてのStackPanel
            StackPanel detailTagStackPanel = new StackPanel();
            detailTagStackPanel.Width = 198;
            detailTagStackPanel.Orientation = Orientation.Vertical;
            detailTagStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            detailTagStackPanel.Margin = new Thickness(0, 0, 0, 5);
            MAIN_COMPONENTS.DetailMainStackPanel.Children.Add(detailTagStackPanel);
            //タグ一覧詳細としてのStackPanel
            StackPanel detailStackPanel = new StackPanel();
            detailStackPanel.Width = 198;
            detailStackPanel.Orientation = Orientation.Horizontal;
            detailStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            detailTagStackPanel.Children.Add(detailStackPanel);
            //タグ追加ボタン
            Button tagAddButton = new Button();
            tagAddButton.Content = "タグ追加";
            tagAddButton.HorizontalAlignment = HorizontalAlignment.Left;
            tagAddButton.VerticalAlignment = VerticalAlignment.Top;
            tagAddButton.Click += commonRegsterAddFixTag_Click;
            detailStackPanel.Children.Add(tagAddButton);
            //タグファイル名
            TextBlock detailFileTextBlock = new TextBlock();
            detailFileTextBlock.Text = textInf;
            detailFileTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            detailFileTextBlock.VerticalAlignment = VerticalAlignment.Center;
            detailFileTextBlock.Width = 151;
            detailFileTextBlock.TextWrapping = TextWrapping.Wrap;
            detailStackPanel.Children.Add(detailFileTextBlock);
        }

        //登録用タグのパネルデータにて追加降下時動作
        public void commonRegsterAddFixTag_Click(object sender, RoutedEventArgs e)
        {
            Button tagAddButton = (Button)sender;
            StackPanel detailStackPanel = (StackPanel)tagAddButton.Parent;
            TextBlock detailFileTextBlock = (TextBlock)detailStackPanel.Children[1];

            StackPanel detailTagStackPanel = (StackPanel)detailStackPanel.Parent;

            //1つのタグとしてのStackPanel
            StackPanel tagStackPanel = new StackPanel();
            tagStackPanel.Width = 198;
            tagStackPanel.Orientation = Orientation.Horizontal;
            tagStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            tagStackPanel.Margin = new Thickness(0, 0, 0, 5);
            detailTagStackPanel.Children.Add(tagStackPanel);
            //タグ内容
            TextBox tagText = new TextBox();
            tagText.HorizontalAlignment = HorizontalAlignment.Left;
            tagText.VerticalAlignment = VerticalAlignment.Center;
            tagText.Width = 140;
            if (detailFileTextBlock.Text != readManegerCls.OPTION_SEARCH_TAG_NAME) tagText.Focusable = false;
            tagStackPanel.Children.Add(tagText);
            //ComboBox内容
            ComboBox tagComboBox = new ComboBox();
            tagComboBox.DropDownOpened += commonRegsterTagComboBox_DropDownOpened;
            tagComboBox.DropDownClosed += commonRegsterTagComboBox_DropDownCloseed;
            tagStackPanel.Children.Add(tagComboBox);
            //タグ削除ボタン
            Button DeleteButton = new Button();
            DeleteButton.Content = "削除";
            DeleteButton.HorizontalAlignment = HorizontalAlignment.Left;
            DeleteButton.VerticalAlignment = VerticalAlignment.Center;
            DeleteButton.Click += commonRegsterDeleteFixTag_Click;
            tagStackPanel.Children.Add(DeleteButton);
        }

        //登録用タグのパネルデータにて削除降下時動作
        private void commonRegsterDeleteFixTag_Click(object sender, RoutedEventArgs e)
        {
            Button DeleteButton = (Button)sender;
            StackPanel tagStackPanel = (StackPanel)DeleteButton.Parent;
            StackPanel detailTagStackPanel = (StackPanel)tagStackPanel.Parent;
            int index = detailTagStackPanel.Children.IndexOf(tagStackPanel);
            detailTagStackPanel.Children.RemoveAt(index);
        }

        //登録用タグのパネルデータにてComboBox降下時動作
        private void commonRegsterTagComboBox_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox tagComboBox = (ComboBox)sender;
            StackPanel tagStackPanel = (StackPanel)tagComboBox.Parent;
            StackPanel detailTagStackPanel = (StackPanel)tagStackPanel.Parent;
            StackPanel detailStackPanel = (StackPanel)detailTagStackPanel.Children[0];
            TextBlock detailFileTextBlock = (TextBlock)detailStackPanel.Children[1];

            string fileName = readManegerCls.FIX_SEARCH_TAG_DIRECTORY + "\\" + detailFileTextBlock.Text;
            if (detailFileTextBlock.Text == readManegerCls.OPTION_SEARCH_TAG_NAME) fileName = readManegerCls.CONFIG_DIRECTORY + "\\" + readManegerCls.SEARCH_TAG_FILE_NAME;
            //ファイル存在したら作成
            if (File.Exists(fileName))
            {
                //ファイル読み込み
                using (StreamReader reader = new StreamReader(fileName))
                {
                    //1行ずつ確認更新
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //タグ名登録済み
                        if (line.Contains(readManegerCls.FIX_SEARCH_TAG_TagName))
                        {
                            tagComboBox.Items.Insert(tagComboBox.Items.Count, splitData(line, readManegerCls.FIX_SEARCH_TAG_TagName));
                        }
                    }
                }
            }
        }

        //登録用タグのパネルデータにてComboBox降下時動作
        private void commonRegsterTagComboBox_DropDownCloseed(object sender, EventArgs e)
        {
            ComboBox tagComboBox = (ComboBox)sender;
            StackPanel tagStackPanel = (StackPanel)tagComboBox.Parent;
            TextBox tagText = (TextBox)tagStackPanel.Children[0];
            if (tagComboBox.SelectedIndex != -1)
            {
                string text = (string)tagComboBox.SelectedItem;
                tagComboBox.SelectedIndex = -1;
                tagText.Text = text;
            }
            tagComboBox.Items.Clear();
        }

        //登録データ分割
        private string splitData(string text, string beforeText)
        {
            int before = text.IndexOf(beforeText);
            int after = text.IndexOf(",", before);
            return text.Substring(before + beforeText.Length, after - before - beforeText.Length);
        }
    }
}