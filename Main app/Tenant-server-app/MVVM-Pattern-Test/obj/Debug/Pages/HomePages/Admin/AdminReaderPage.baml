   M S B A M L   `   `   `  ����L  HMVVM-Pattern-Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:MVVM_Pattern_Test.Pages.HomePages.Admin'MVVM_Pattern_Test.Pages.HomePages.Admin  5      =    7MVVM_Pattern_Test.Pages.HomePages.Admin.AdminReaderPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:MVVM_Pattern_Test.Pages.HomePages.Admin     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  
LetterPage��5       N� 
Background
 �����.��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $	��Top=�6%   .+�5      �� 
������ $��0q�5      $��Обновить��5      $��20(�5      ��5      �� *RecieveAllLetters+5      5      �� 5!   
   $��1��5"      $��0,0,0,20q�5#      $
��Auto=�5$      .��5%      �� -   5         ��Name$ leftLettersList��5&      $��1��5'      $��0,0,0,20q�5(       �� Orientation$ Vertical=�5)      .+�5+      �� $	��250��5,      
�������$��0q�5.      ��5/      �� *
AllLetters+ �� SelectedItem 50      �� *SelectedLetter+��51      �� 52      .R�53      �� .+�54   "   �� $��5q�55   %   $��14(�56   %   $��	Underline{�58   %   $
��Wrap=�59   %   ��57   %   ��  �� Path$ Title�59   %   �� 5:   "   $��5,0,5,0q�5;   %   $��12(�5<   %   $
��Wrap=�5>   %   ��5=   %   �� $ SenderLogin�5>   %   5?      5@      5B      5C      5D      �� 5F   
   $��2��5G      $��1��5H      $��1��5I      
�������5J      �� 5L   
   $��2��6   $��2��6(   .��5M       � -   5          �Name$ letterReaderFrame��5N      	  � NavigationUIVisibility$	 Hidden=�5P      ��5O      �� *ReadLetterPage+5P      5Q      5S      5T      