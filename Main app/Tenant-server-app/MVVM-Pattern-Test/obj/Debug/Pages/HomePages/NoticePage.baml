   M S B A M L   `   `   `  ����L  HMVVM-Pattern-Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WpfApp1.Pages.HomePagesWpfApp1.Pages.HomePages  5      (    "WpfApp1.Pages.HomePages.NoticePage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:WpfApp1.Pages.HomePages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  
NoticePage��5       N� 
Background
 �����.��5      �� $��1��5   	   $	��800��5   	   $��0,0,0,20q�5   	   $��Center=�5   	   $	��Top=�5   	   ��. &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5   	   .��5   
   � .+�5      �� .+�5      �� $��Center=�5      
������ 
�������$��20(�5      ��5      �� *RecieveNews+.��5      � .+�5      �� $��30��5       $	��5,0q�5!      $��/refs/icons/reload.png��5"      �� 5#      $��Обновить��6$   5$      5%      �� 5&      -   5         ��Name$ tStack��5'      $��0��5(      $��0,20,0,0q�5)      ��5*      �� *RecievedNewsStruct+��5+      �� 5,      .R�5-      ��  �� Orientation$ Vertical=�6)   5.      ��50      �� 51      .R�52      ��  ��Name$ contentPanel��53   !   $	��650��54   !   $��0,0,0,10q�55   !    �� Padding$ 5q�56   !   $��Center=�57   !   
�������
�������$��1q�5:   !    �� CornerRadius$ 1��5;   !    J� Effect 5<   "   3  -System.Windows.Media.Effects.DropShadowEffect  5=   &   	   
BlurRadius$	 10Q�5>   )   
   Opacity$	
 0.2Q�5?   )      RenderingBias$ Quality=�5@   )      Color$
 Gray��5A   )   ._�5C   "   �� .+�5D   &   � $	��5,0q�60   .+�5E   *   �� $��35��60   $!��/refs/icons/profileIcon.png��6;   �� 5F   *   $��10,0,5,0q�5G   -   $��Center=�5H   -   $��18(�5I   -   $��Author:��5J   -   �� 5K   *   $��0,0,10,0q�5L   -   $��Center=�5M   -   $��18(�5N   -   ��5O   -   �� *ReadNews.Sender+�� 5P   *   $��0q�5Q   -   $��Center=�5R   -   $��18(�5S   -   $��-��5T   -   �� 5U   *   $
��10,0q�5V   -   $��Center=�5W   -   $��18(�5X   -   ��5Y   -   �� *ReadNews.DateTime+5Z   '   �� 5[   &   $	��5,0q�5\   )   $��20(�5]   )   $��DemiBold"�5^   )   ��5_   )   �� *ReadNews.Title+�� 5`   &   $��1��5a   )   $	��5,5q�5b   )   
�������5c   )   �� 5d   &   
������ $��0q�5f   )   $��20(�5g   )    �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�5j   )   ��5i   )   �� *ReadNews.Description+5j   )    � 5k   &   $��0,20,0,0q�5l   )     � NavigationUIVisibility$ Hidden=�5n   )   ��5m   )   �� *AttachmentsPage+5n   )   5o   #   5p      5q      5s      5t      5u      5v      5w      