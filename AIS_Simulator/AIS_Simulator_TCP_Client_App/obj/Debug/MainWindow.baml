   M S B A M L   `   `   `  ����W  SAIS_Simulator_TCP_Client_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AIS_Simulator_TCP_Client_AppAIS_Simulator_TCP_Client_App  5   	   -    'AIS_Simulator_TCP_Client_App.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:AIS_Simulator_TCP_Client_App     5   	     � Title$  
MainWindow��5   	   $	��396��6   $��556.978��6)   .��5	      � -   5        .+�5
   
   �� -   5         ��Name$ lblHost��5
      $
��Host��6!   $
��Left=�60   $��	80,29,0,0q�6K   $	��Top=�6^   $��26��6v   $��35��6�   �� 5   
   -   5         ��Name$ 
btnConnect��5      $��Connect��6%   $
��Left=�67   $��
379,35,0,0q�6R   $	��Top=�6f   $��75��6~   $��20��6�   6�   �� 5   
   -   5         ��Name$ txtHost��5      $
��Left=�6#   $��23��6>   $��
121,32,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $��	127.0.0.1��6r   $	��Top=�6�   $	��120��6�    �� 	UndoLimit$	 106��6�   �� 5   
   -   5        $ lblPort��5      $
��Port��6!   $
��Left=�60   $��
256,29,0,0q�6K   $	��Top=�6_   $��26��6w   $��31��6�   �� 5   
   -   5        $ txtPort��5      $
��Left=�6#   $��23��6>   $��
296,32,0,0q�6J   $
 Wrap=�6^   $
��8910��6r   $	��Top=�6~   $��56��6�   $	 106��6�   �� 5   
   -   5        $ txtRecievedData��5      $
��Left=�6+   $	��144��6F   $��	80,87,0,0q�6S   $
 Wrap=�6f   $	��Top=�6z   $	��374��6�   ��6�   �� *	dataBox+5      5      