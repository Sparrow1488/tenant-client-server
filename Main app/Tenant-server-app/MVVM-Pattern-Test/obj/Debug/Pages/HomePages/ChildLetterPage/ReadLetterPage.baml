   M S B A M L   `   `   `  ����L  HMVVM-Pattern-Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullu?clr-namespace:MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage1MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage  5      F    @MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage.ReadLetterPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Qlocal?clr-namespace:MVVM_Pattern_Test.Pages.HomePages.ChildLetterPage     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  ReadLettersPage��5       N� 
Background
 ����� � 	Resources 5         letterButtons �� Property �� Value  defaultTextStyle  titleTextStyle��%�  &	        &	 �     &	     �� 5   
   .�5      ��  � ��
 ���� 69   �� 5       � ��
 ���� 68   �� 5       � ��$ Calibri)�68   5      �� 5   
   .�5      ��  � ��$ 0q�6=   �� 5       � ��$ 16(�66   �� 5       � ��$ Calibri)�68   5      �� 5   
   .�5      ��  � ��$ 24(�66   �� 5       � ��$ 0q�6=   �� 5       � ��$ DemiBold"�68   �� 5       � ��$ Calibri)�68   5      5      .��5       � $��	20,0,20,0q�5!   	   
�������&   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5#   	    � RowDefinitions 5$   
   �� 5%      $	��110��6   �� 5&      .+�5)   
   �� $��0��6   $��Center=�6"   .+�5*      �� -   5        $��topTitle��5+      $��0q�5,       �� 
IsReadOnly . #��[ 5.       �� TextWrapping$
 Wrap=�50      ��5/      �� *ReadLetter.Title+50      �� 51      -   5        $��senderLetter��52      $��0q�53       . #��[ 55      ��56      �� *ReadLetter.SenderLogin+�� 57      -   5        $��dateSend��58      $��0q�59       . #��[ 5;      ��5<      �� *ReadLetter.ShortDateCreate+�� 5=      $��1��6   
���   �6$   5>      �� 5@   
   $��1��5A      $��0,0,0,25q�5B      
�������.+�5E      �� $��0,0,5,0q�6   	 �� Orientation$	 Vertical=�6*   .+�5G      �� 
�������$��1q�5I      $��12(�5J      $��Письмо��5K      
 J� Effect
 5L      3  -System.Windows.Media.Effects.DropShadowEffect  5M         
BlurRadius$ 10Q�5N         Opacity$	 0.2Q�5O         RenderingBias$ Quality=�5P         Color$
 Gray��5Q      .��5S      �� .+�5T      �� -   5        $��	mainTitle��5U      $��0q�5V       . #��[ 5X      $
 Wrap=�5Z      ��5Y      �� *ReadLetter.Title+5Z      �� 5\      -   5        $��
descLetter��5]      $��0q�5^      $��20(�5_       . $
 Wrap=�5b      ��5a      �� *ReadLetter.Description+ �� Mode$ OneWay=�5b      �� 5c      $��1��5d      $
��50,0q�5e      
���   �5f      � 5g      $
��0,10q�5h      $��Center=�5i       � Orientation$ 
Horizontal=�5j      .+�5k      �� -   5        $��replyBtn��5l   !   $
��10,0q�5m   !   $
��Left=�5n   !   $��Ответить��5p   !   $��18(�5q   !   #��[  5r   !   ��5o   !   �� *ShowReplyPage+5r   !   �� 5s      $
��10,0q�5t   !   $
��Left=�5u   !   $��Удалить��5v   !   $��18(�5w   !   #��[  5x   !   �� 5y      $
��10,0q�5z   !   $
��Left=�5{   !   $��Важное��5|   !   $��18(�5}   !   #��[  5~   !   5      5�      5�      �� 5�      $	 Vertical=�6   .+�5�       � -   5          �Name$ responseFrame��5�      $��0,40,0,0q�5�      $��Center=�5�        � NavigationUIVisibility$ Hidden=�5�      ��5�      �� *	ReplyPage+5�       � 5�      -   5        $ attachmentsFrame��5�      $��0,40,0,0q�5�      $��Center=�5�      $ Hidden=�5�      ��5�      �� *
AttachPage+5�      5�      5�      5�      5�      5�      