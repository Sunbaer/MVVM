   M S B A M L   `   `   `  ����M  IRoundBasedGameMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:RoundBasedGameMVVM.ViewsRoundBasedGameMVVM.Views  5      +    %RoundBasedGameMVVM.Views.BattleUiView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:RoundBasedGameMVVM.Views     5      .��5	      �   � RowDefinitions  5   
   �� 5      $��1* �6   �� 5      $��5* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��3* �6#   �� 5      $��1* �6#   .+�5   
   � $��0��6   $��Center=�6    $��Center=�6=   .+�5      �� .+�5      �� $
��Bold"�6!   ���#de951f��6H   ��  �� Path$ Player.Name� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�� 5      .+�5      �� $
��Bold"�6$   
���  ��$��0,0,5,0q�6G   Hp6]   �� 5      $
��Bold"�6$   
���  ��$��0,0,10,0q�6G   ��6Z   �� $ Player.HealthPoints�$ TwoWay=�$ PropertyChanged=��� 5      $
��Bold"�6$   
�������$��0,0,5,0q�6I   Armor6b   �� 5       $
��Bold"�6$   
�������$��0,0,10,0q�6I   ��6[   �� $ Player.Armor�$ TwoWay=�$ PropertyChanged=��� 5!      $
��Bold"�6$   ���#86ad4c$��0,0,5,0q�6L   ATK6c   �� 5"      $
��Bold"�6$   $��0,0,10,0q�67   ���#86ad4c��6^   �� $ Player.AttackDamage�$ TwoWay=�$ PropertyChanged=�5$      5&      5(      � 5*   
   $��1��6   $��Center=�6   $��Center=�6<   .+�5+      �� .+�5,      � .+�5-      �� $
��Bold"�6$   
����p����6P   �� $ Rounds.ExternRound�6|   �� 5.      $
��Bold"�6%   
����p��$��
/50 Rounds��6Q   5/      � 50      .+�51      �� $
��Bold"�6%   
����p����6Q   �� $ Rounds.BattleRound�6}   �� 52      $
��Bold"�6%   
����p��$�� Round/s��6Q   53      55      57      � 5:      $��2��6   $��Center=�6$   $��Center=�6A   .+�5;      �� .+�5<      �� $
��Bold"�6!   ���#de951f��6H   �� $ 
Enemy.Name�$ TwoWay=�$ PropertyChanged=�� 5=      .+�5>      �� $
��Bold"�6$   
���  ��$��0,0,5,0q�6G   Hp6]   �� 5?      $
��Bold"�6$   
���  ��$��0,0,10,0q�6G   ��6Z   �� $ Enemy.HealthPoints�$ TwoWay=�$ PropertyChanged=��� 5@      $
��Bold"�6$   
�������$��0,0,5,0q�6I   Armor6b   �� 5A      $
��Bold"�6$   
�������$��0,0,10,0q�6I   ��6[   �� $ Enemy.Armor�$ TwoWay=�$ PropertyChanged=��� 5B      $
��Bold"�6$   ���#86ad4c$��0,0,5,0q�6L   ATK6c   �� 5C      $
��Bold"�6$   $��0,0,10,0q�67   ���#86ad4c��6^   �� $ Enemy.AttackDamage�$ TwoWay=�$ PropertyChanged=�5E      5F      5G      5H      � 5M   
   $��1��6   ��5N      �� 5O       �� ImageSource$! /Images/FightBackground.png��6    5Q      �� 5R      $��1* �6#   �� 5S      $��3* �6#   �� 5T      $��1* �6#   .+�5W   
   � $��1��6   $��0��6   .+�5X      �� ��6   �� $ Player.ImageSource�5Z      5[      � 5\   
   $��1��6   $��1��6   .+�5]      �� Stats6   5^      � 5b   
   $��1��6   $��2��6   .+�5c      ��  J� RenderTransformOrigin$ 0.5,0.5&�6B   ��6   �� $ Enemy.ImageSource�z�5d      �� 5e       �� ScaleX$ -1Q�6)   5g      5h      5i      5l      5m      