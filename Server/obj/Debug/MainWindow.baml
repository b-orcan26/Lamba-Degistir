   M S B A M L   `   `   `  ����A  =Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Server.ModelsServer.Models  5   	   clr-namespace:ServerServer  5   	       Server.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )mclr-namespace:Server.Models     5   	   &localclr-namespace:Server     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � 	Resources 5
      !   Server.BoolToColorConverter   BoolToColor��%   &	          5   
   6%   5      .��5      �  � RowDefinitions 5   
   �� 5      5      �� 5      6   .+�5   
   �� $��Center=�6   $��Center=�63   $��Lamba��6W   $
��Left=�6�   $��
138,75,0,0q�6�   $	��Top=�6�   $��60��6�   $	��118��6  ��6g   �� *AcikMi+ �� 	Converter# [  6  �� 5   
   -   5        $��Lamba Durumu Degistir��5      $
��Left=�61   $��435,109,0,0q�6L   $	��Top=�6a   $	��146��6y   6�   �� 5   
   $
��Left=�6\   $��26��6w   $��
138,99,0,0q�6�   $��1��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��118��6�   ��6   �� *Sayi+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5        $��Random Sayi Uret��5      $
��Left=�6,   $��
435,99,0,0q�6G   $��1��6[   $	��Top=�6h   $	��146��6�   6�   �� 5   
   $
��Left=�6*   $��138,162,0,0q�6E   $��1��6Z   $	��Top=�6g   $	��118��6   ��6   �� *Sayi+6   5      5      