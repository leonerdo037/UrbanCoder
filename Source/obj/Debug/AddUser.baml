   M S B A M L   `   `   `  ����E  AUrbanCoder, Version=0.2.0.9, Culture=neutral, PublicKeyToken=null'clr-namespace:UrbanCoder
UrbanCoder  5   	       UrbanCoder.AddUser   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:UrbanCoder     5   	   $	��500��5        � WindowStyle$
  None=�6&    � 
ResizeMode$ NoResize=�69    � WindowStartupLocation$ CenterScreen=�6O   $	��270��6t   .��5      �� $��4q�6   ���	#FFB9B9B9._�5	   
   �  � RowDefinitions 5
      �� 5      $��57 �6    �� 5      $��1* �6    �� 5      $��57 �6    .+�5      �� -   5         ��Name$ Header��5      $��Add User to UrbanCoder��6(   $��16(�6F   $
��Bold"�6T   $��5q�6f   �� 5      -   5         ��Name$ B_Close��5      $��X��6&   $��16(�62   $��25��6@   $��25��6L   $��0,1,1,0q�6W   $
��Bold"�6h   $��Right=�6z   $	��Top=�6�   ��6�   Z� ��6�   Z� 6�   �� 5      ���	#FFA6A6A6$��2��6&   $��1��6:   	 �� X1$	 480��6G   	 �� X2$ 10��6P   �� 5       �� Orientation$ 
Horizontal=�6   $��1��62   $��Center=�6?   $��Center=�6\   .+�5      �� .+�5      �� $��	Username:��6   $��16(�60   $��Right=�6>   $��0,0,0,10q�6Z   �� 5      $��Role:��6   $��16(�6,   $��Right=�6:   5      �� 5      .+�5      �� -   5        	 ��Name$	 
T_Username��5      $	��200��62   $��31��6>      RoundTextBox#��[  6J   $��0,0,0,10q�6p   �� 5      -   5        $	 T_Role��5      $	��200��6.   $��31��6:   #��[  6F   �� 5      -   5        $ TB_Error��5       $��WrapWithOverflow=�62   $	��200��6R   $y�	Collapsed=�6^   $��Error.��6u   
���  ��$��0,5,0,0q�6�   5      5      �� 5      ���	#FFA6A6A6$��2��6&   $��2��6:   $	 480��6G   $ 10��6P   �� 5      $ 
Horizontal=�6   $��2��62   $��Right=�6?   .+�5      �� -   5        $
��Save��5      $��91��6(   $��26��63   
�������  RoundButton#��[ 6R   6w   �� 5       -   5        $��Cancel��5       $
��10,0q�6*   $��91��68   $��26��6C   
���   �#��[ 6b   ��5!      �� 5"      
 �� EndPoint$
 0.5,1&�6.    �� 
StartPoint$ 0.5,0&�6?   .��5#      �  � Color$ 	#FFD8D8D8��6+   � 5$      $ 	#BF8F8F8F��6+    � Offset$ 1Q�6=   5%      5'      5(      5)      5*      5+      