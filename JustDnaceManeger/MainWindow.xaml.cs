using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace JustDnaceManeger
{
    public partial class MainWindow : Window
    {
        //データ管理クラス
        Maneger.DataManeger dataManegerCls = null;

        public MainWindow()
        {
            //コンポーネント初期化
            InitializeComponent();
            //データ管理クラス初期化
            dataManegerCls = new Maneger.DataManeger(this);
        }

        //指定(主につべ)に飛ぶボタン
        private void URL_GoButton_Click(object sender, RoutedEventArgs e)
        {
            dataManegerCls.goWebSite(youtubeURL.Text);
        }

        //入力済みURLコピーボタン
        private void URL_CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var text = youtubeURL.Text;
            if ((text != null) && (text != ""))
            {
                Clipboard.SetDataObject(text, true);
            }
        }

        //画像選択ボタン
        private void DancePictureChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            dataManegerCls.dancePictureChoice();
        }

        //画像許可
        private void DancePicture_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        //画像仮保持
        private void DancePicture_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var name in fileNames)
                {
                    if (name.EndsWith(".png") || name.EndsWith(".PNG") || name.EndsWith(".jpg") || name.EndsWith(".JPG")) { }
                    //仮画像として保存
                    dataManegerCls.oncePictureSet(name);
                }
            }
        }

        //データ登録/修正ボタン
        private void RegsterButton_Click(object sender, RoutedEventArgs e)
        {
            dataManegerCls.regsterData();
        }


        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader columnHeader = (GridViewColumnHeader)sender;
            string columnTag = columnHeader.Tag.ToString();

            if (SearchDanceListView.Items.Count > 1)
            {
                ListSortDirection sortDirection;

                if (SearchDanceListView.Items.SortDescriptions.Count == 0)
                {
                    sortDirection = ListSortDirection.Descending;
                }
                else
                {
                        //sortDirection = ListSortDirection.Descending;
                        sortDirection = ListSortDirection.Ascending;
                    SearchDanceListView.Items.SortDescriptions.Clear();
                }
                SearchDanceListView.Items.SortDescriptions.Add(new SortDescription(columnTag, sortDirection));
            }
        }

        //listviewから詳細書き出し
        private void SearchDanceListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dataManegerCls.SearchDanceListView_MouseDoubleClick();
        }

        //テキスト変更時も検索する
        private void SearchDanceTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataManegerCls.resultShow();
        }
    }
}