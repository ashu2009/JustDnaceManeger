   M S B A M L   `   `   `  ����K  GJustDnaceManeger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:JustDnaceManegerJustDnaceManeger  5   	   !    JustDnaceManeger.MainWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:JustDnaceManeger     5   	     � Title$  JustDunceManeger��5   	   $	��450��6"   $	��800��6/   $
��Left=�6;   $	��Top=�6V   $��Disabled=�6n   $	��585��6�   $	��320��6�   .��5	      � -   5         �Name$
 grid��5	       � ColumnDefinitions 5
   
   �� 5      $	��250 �6   $	��250��6+   $	��250��6:   �� 5      $
��Auto �6   �� 5      $
��366* �6   $	��100��6,   �� 5      $
��Auto �6   �� 5      $	��215 �6   $	��215��6+   $	��215��6:   .+�5   
   �� -   5         ��Name$ TagScrollView��5      $��0��6.   $
��Auto=�6>   $
��Auto=�6c   $��Stretch=�6�   .��5      �� -   5         ��Name$ SearchTagsMainStackPanel��5   %   $	��235��6    �� Orientation$ Vertical=�6G   $��Stretch=�6^   .+�5      �� $
��Left=�6   $��検索��67   $
��Wrap=�6A   $	��Top=�6U   $��0,0,0,10q�6n   �� 5      $
��Left=�6   $ ��曲名検索(部分一致)��67   $
��Wrap=�6I   $	��Top=�6]   �� 5      -   5        $��SearchDanceTitleTextBox��5       �� TextWrapping$
 Wrap=�69   $��0,0,0,5q�6M   6^   �� 5      $
��Left=�6   $ ��メモ検索(部分一致)��67   $
��Wrap=�6I   $	��Top=�6]   �� 5      -   5        $��SearchMemoTextBox��5      $
 Wrap=�63   $��0,0,0,5q�6G   6X   5      5      �� 5   
   $��1��6   $��Stretch=�6'   $��2��6C   $��Center=�6M   �� 5   
   $��2��6   $
��Auto=�6'   $
��Auto=�6L   $��Stretch=�6o   .��5      �� $ Vertical=�6   $
��Auto=�60   $��Stretch=�6b   $��Stretch=�6�   $
|�True��6�   .+�5      �� $��検索結果��6   $
��Wrap=�6(   $	��Top=�6<    J� 	AllowDrop$ False��6T   $|�False��6f   $��0,0,0,20q�6x   $��Stretch=�6�   �� 5      -   5         ��Name$ SearchDanceListView��5      $
��Auto=�6P   $
��Auto=�6�   $��Stretch=�6�   $|�False��6  ��68   �� 	 �� View	 5      �� 5       -   5        SearchResultColumGridView��.P�5!      �� 
 �� Width$
 45��6`    �� DisplayMemberBinding 6-   ��  �� Path$ DanceNumber�.��5"   "   �� $��	曲番号��67   
 � Tag$ DanceNumber��6E   5#      �� 5$      $	
 150��6_    6-   �� $ 
DanceTitle�.��5%   "   �� $��曲名��67   $ 
DanceTitle��6E   5&      �� 5'      $	
 110��6^    6-   �� $ 	DanceMemo�.��5(   "   �� $��メモ��67   $ 	DanceMemo��6D   5)      5*      5,      5-      5.      �� 5/   
   $��3��6   $��Stretch=�6'   $��2��6C   $��Center=�6M   �� 50   
   -   5        $ DetailScrollView��50      $��4��61   $
��Auto=�6A   $
��Auto=�6f   $��Stretch=�6�   .��51      �� -	   5        $ DetailMainStackPanel��51   %   $	��198��6   $ Vertical=�6C   $
��Left=�6Z   .+�52      �� $
��Left=�6   $��詳細��65   $
 Wrap=�6?   $	��Top=�6S   $��0,0,0,0q�6l   $��0,0,0,10q�6�   �� 53      -
   5        $��
DanceImage��53      $	��108��6*   $
��Left=�67   $	��Top=�6R   $	��198��6j   $
��Fill=�6v    �� StretchDirection$
 Both=�6�   $
 True��6�   $
|�True��6�   6�   �� 54      -   5        $��DancePictureChoseButton��54      $��画像選択��68   $��0,0,0,5q�6G   $
��Left=�6X   $	��Top=�6s   6�   �� 55      $
��Left=�6   $��	曲名：��67   $
��Wrap=�6B   $	��Top=�6V   �� 56      -   5        $��DanceTitleTextBox��56      $
 Wrap=�63   $��0,0,0,5q�6G   �� 57      $
��Left=�6   $��曲番号：��67   $
��Wrap=�6C   $	��Top=�6W   �� 58      -   5        $��DanceNumberTextBox��58      $
 Wrap=�64   $��0,0,0,20q�6H   �� 59      -   5        $��DanceDataTextBox��59      $
��Left=�64   $.��(最終更新日時：----/--/--/--:--:--��6O   $
��Wrap=�6q   $	��Top=�6�   �� 5:      -   5        $��RegsterButton��5:      $��修正/登録��6.   $��0,0,0,20q�6>   $
��Left=�6P   $	��Top=�6k   6�   �� 5;      -   5        $ youtubeURLStackPanel��5;   )   $	��198��6   $ 
Horizontal=�6G   $
��Left=�6`   .+�5<      �� $
��Left=�6    $��参照URL：��6;   $
��Wrap=�6I   $��Center=�6]   �� 5=      -   5        $��URL_GoButton��5=      $��Go URL��61   6B   �� 5>      -   5        $��URL_CopyButton��5>      $��
URL　Copy��63   $��30,0,0,0q�6c   5?      �� 5@      -   5        $��
youtubeURL��5@      $
 Wrap=�6,   $��0,0,0,5q�6@   �� 5A      $
��Left=�6   $��メモ��67   $
��Wrap=�6A   $	��Top=�6U   �� 5B      -   5        $��DanceMemoTextBox��5B      $
 Wrap=�62   $��0,0,0,5q�6F   5C      5D      5E      5F      