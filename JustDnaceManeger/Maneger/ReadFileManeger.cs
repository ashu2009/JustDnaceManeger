using System.IO;

namespace JustDnaceManeger.Maneger
{
    class ReadFileManeger
    {
        //固定タグ置き先
        public readonly string FIX_SEARCH_TAG_DIRECTORY = Directory.GetCurrentDirectory() + "\\DefaultSearchTags";
        //固定タグ中身判断用
        public readonly string FIX_SEARCH_TAG_TagName = "TagName ";
        public readonly string FIX_SEARCH_TAG_TagThumbnail = "TagThumbnail ";
        //固定タグ用画像置き先
        public readonly string FIX_SEARCH_TAG_IMAGE_DIRECTORY = Directory.GetCurrentDirectory() + "\\Thumbnail";
        //曲用画像置き先
        public readonly string DANCE_IMAGE_DIRECTORY = Directory.GetCurrentDirectory() + "\\Picture";


        //タグファイル名
        public readonly string SEARCH_TAG_FILE_NAME = "Tag.txt";
        //オプションタグ名
        public readonly string OPTION_SEARCH_TAG_NAME = "オプションタグ";


        //保存データファイル名
        public readonly string DATA_FILE_NAME = "Data.txt";


        //設定関係ファイル置き先
        public readonly string CONFIG_DIRECTORY = Directory.GetCurrentDirectory() + "\\Config";


        //デフォルト画像
        public readonly string DEFAULT_IMAGE = "DefaultThumbnail.png";
    }
}
