   M S B A M L   `   `   `  ����L  HMVVM-Pattern-Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WpfApp1.Server.PackagesWpfApp1.Server.Packages  5      )    #WpfApp1.Pages.HomePages.ProfilePage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:WpfApp1.Server.Packages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        N� Title$  ProfilePage��5       N� 
Background �#F4F6F7 � 	Resources 5         btnStyle �� Property �� Value  textBlockStyle��%.  &	        &	 �     �� 5   
   .�5      ��  � ��
 ���� 68   �� 5       � ��$ 	10 2 10 2q�65   �� 5       � ��$ 4 0 0 0q�6=   �� 5       � ��$	 0 2q�64   5      �� 5   
   .�5      ��  � ��$ 2q�64   �� 5       � ��$ 24(�66   5      5      .��5      � -   5         �Name$ mainProfilePanel��5   	   $��3��5   	   $	��510��5   	   $��40,40q�5   	    � RowDefinitions 5   
   �� 5       $
��Auto �6   �� 5!       � ColumnDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      �� 5&      .+�5)   
   � -   5         �Name$ profileImagePanel��5*      $��0��5+      $��Center=�5,      $��Center=�5-      .+�5.      �� $	��200��5/      $��5q�50      $��/refs/icons/userIcon2.png��51      52      �� 53   
   $��1��54      $��2��55      $
��Auto��56      	 �� Orientation$	 
Horizontal=�57      .+�58      �� -   5        
 ��Name$
 userInfo��58      $��10q�6+   .+�59      � #��[ 6   .+�5:      �� $��Логин: ��6    �� 5;      ��6    �� *AuthUser.Login+5<      � 5=      #��[ 6   .+�5>      �� $��ФИО: ��6    �� 5?      ��6    �� *
FullName+5@      � 5A      #��[ 6   .+�5B      �� $��Квартира: ��6    �� 5C      ��6    �� *AuthUser.Room+5D      �� 5E      $/��)Редактировать профиль��5F      $��24(�5G      
���   �#��[  5I      5J      5K      � 5L   
   $��1��6   $��0��6!   .+�5M      �� .+�5N      �� $+��%Отправленные письма��5O      $��24(�5P      #��[  5Q      �� 5R      $��Избранное��5S      $��24(�5T      #��[  5U      5V      5W      5X      5Y      