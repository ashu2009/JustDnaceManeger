   M S B A M L   `   `   `  ���� K  GJustDnaceManeger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:JustDnaceManegerJustDnaceManeger  !    JustDnaceManeger.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:JustDnaceManeger       � Title$  JustDunceManeger��$	��450��$	��800��$
��Left=�$	��Top=�$��Disabled=�$	��585��$	��320��.��� -    �Name$
 grid�� � ColumnDefinitions �� $	��250 �$	��250��$	��250���� $
��Auto ��� $
��366* �$	��100���� $
��Auto ��� $	��215 �$	��215��$	��215��.+��� -    ��Name$ TagScrollView��$��0��$
��Auto=�$
��Auto=�$��Stretch=�.���� -    ��Name$ SearchTagsMainStackPanel��$	��235�� �� Orientation$ Vertical=�$��Stretch=�.+��� $
��Left=�$��検索��$
��Wrap=�$	��Top=�$��0,0,0,10q��� $
��Left=�$ ��曲名検索(部分一致)��$
��Wrap=�$	��Top=��� -   $��SearchDanceTitleTextBox�� �� TextWrapping$
 Wrap=�$��0,0,0,5q��� $
��Left=�$ ��メモ検索(部分一致)��$
��Wrap=�$	��Top=��� -   $��SearchMemoTextBox��$
 Wrap=�$��0,0,0,5q��� $��1��$��Stretch=�$��2��$��Center=��� $��2��$
��Auto=�$
��Auto=�$��Stretch=�.���� $ Vertical=�$
��Auto=�$��Stretch=�$��Stretch=�$
|�True��.+��� $��検索結果��$
��Wrap=�$	��Top=� J� 	AllowDrop$ False��$|�False��$��0,0,0,20q�$��Stretch=��� -    ��Name$ SearchDanceListView��$
��Auto=�$
��Auto=�$��Stretch=�$|�False������ 	 �� View	 �� -   SearchResultColumGridView��.P��� 
 �� Width$
 45�� �� DisplayMemberBinding ��  �� Path$ DanceNumber�.���� $��	曲番号��
 � Tag$ DanceNumber���� $	
 150�� �� $ 
DanceTitle�.���� $��曲名��$ 
DanceTitle���� $	
 110�� �� $ 	DanceMemo�.���� $��メモ��$ 	DanceMemo���� $��3��$��Stretch=�$��2��$��Center=��� -   $ DetailScrollView��$��4��$
��Auto=�$
��Auto=�$��Stretch=�.���� -	   $ DetailMainStackPanel��$	��198��$ Vertical=�$
��Left=�.+��� $
��Left=�$��詳細��$
 Wrap=�$	��Top=�$��0,0,0,0q�$��0,0,0,10q��� -
   $��
DanceImage��$	��108��$
��Left=�$	��Top=�$	��198��$
��Fill=� �� StretchDirection$
 Both=�$
 True��$
|�True���� -   $��DancePictureChoseButton��$��画像選択��$��0,0,0,5q�$
��Left=�$	��Top=��� $
��Left=�$��	曲名：��$
��Wrap=�$	��Top=��� -   $��DanceTitleTextBox��$
 Wrap=�$��0,0,0,5q��� $
��Left=�$��曲番号：��$
��Wrap=�$	��Top=��� -   $��DanceNumberTextBox��$
 Wrap=�$��0,0,0,20q��� -   $��DanceDataTextBox��$
��Left=�$.��(最終更新日時：----/--/--/--:--:--��$
��Wrap=�$	��Top=��� -   $��RegsterButton��$��修正/登録��$��0,0,0,20q�$
��Left=�$	��Top=��� -   $ youtubeURLStackPanel��$	��198��$ 
Horizontal=�$
��Left=�.+��� $
��Left=�$��参照URL：��$
��Wrap=�$��Center=��� -   $��URL_GoButton��$��Go URL���� -   $��URL_CopyButton��$��
URL　Copy��$��30,0,0,0q��� -   $��
youtubeURL��$
 Wrap=�$��0,0,0,5q��� $
��Left=�$��メモ��$
��Wrap=�$	��Top=��� -   $��DanceMemoTextBox��$
 Wrap=�$��0,0,0,5q�