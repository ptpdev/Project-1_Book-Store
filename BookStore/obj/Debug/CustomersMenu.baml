   M S B A M L   `   `   `  ����D  @BookStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BookStore	BookStore  5   	       BookStore.CustomersMenu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:BookStore     5   	   -   5          � Title$*  $จัดการลูกค้า��5   	   $	��400��6   $	��900��6+    � 
ResizeMode$ CanMinimize=�6U    � WindowStartupLocation$ CenterScreen=�6n   .��5	      � .+�5
   
   �� -   5         ��Name$ searchTextBox��5
      $
��Left=�6)   $��26��6D   $��
210,62,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��157��6�   �� 5   
   -   5         ��Name$ searchLabel��5      $��ค้นหา��6%   $
��Left=�65   $��27��6P   $��	79,63,0,0q�6\   $	��Top=�6o   $��38��6�    J� RenderTransformOrigin$ 0.716,0.437&�6�   �� 5   
   -   5         ��Name$ 
searchList��5      $
��Left=�6'   $��26��6B   $��
122,63,0,0q�6N   $	��Top=�6b   $��83��6z    �� 
IsReadOnly . $	 �� IsSynchronizedWithCurrentItem$	 FalseY�6�   $ 0.386,0.407&�6�   �� 5   
   -   5        
 ��Name$
 	searchBtn��5      $��ค้นหา��6$   $
��Left=�64   $��26��6O   $��
372,63,0,0q�6[   $	��Top=�6o   $��72��6�   6�   �� 5   
   -   5         ��Name$ customersListView��5   +   $��79,105,0,102.4q�6   $
��Left=�6|   $	��365��6�    �� View 5      �� 5      .P�5      �� $$��รหัสสมาชิก��6%    �� Width$ 80��69    �� DisplayMemberBinding 6D   �� *
CustomerId+�� 5      $$��ชื่อลูกค้า��6%   $	 140��69    6E   �� *CustomerName+�� 5      $��ที่อยู่��6%   $	 140��66    6B   �� *	Address+�� 5      $��E-mail��6%   $	 100��65    6A   �� *Email+! �� ColumnHeaderContainerStyle 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � {� �� Value .  6:   5      5      5      �� 5   
   -   5        $
 addBtn��5      $'��!เพิ่มข้อมูล��6!   $
��Left=�67   $��34��6R   $��503,234,0,0q�6^   $	��Top=�6s   $��85��6�   $��5��6�   �� 5   
   -   5        $ customerIdTextBox��5      $
��Left=�6-   $��27��6H   $��
614,61,0,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $	��194��6�   $��1��6�    �� 	MaxLength$ 6��6�    � ToolTip$Q Kรหัสสมาชิกยาวไม่เกิน 6 หลัก��6  �� 5   
   -	   5        $ customerNameTextBox��5      $
��Left=�6/   $��27��6J   $��
614,95,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $	��194��6�   $��2��6�   �� 5    
   -
   5        $ emailTextBox��5       $
��Left=�6(   $��27��6C   $��614,186,0,0q�6O   $
 Wrap=�6d   $	��Top=�6x   $	��194��6�   $��4��6�   �� 5!   
   -   5        $ customerIdLabel��5!      $$��รหัสสมาชิก��6)   $
��Left=�6>   $��27��6Y   $��
503,61,0,0q�6e   $	��Top=�6y   $��96��6�   $ 0.511,0.341&�6�   �� 5"   
   -   5        $ customerNameLabel��5"      $��ชื่อ��6+   $
��Left=�6:   $��27��6U   $��
503,95,0,0q�6a   $	��Top=�6u   $��96��6�   �� 5#   
   -   5        $ emailLabel1��5#      $��E-mail��6%   $
��Left=�66   $��27��6Q   $��503,186,0,0q�6]   $	��Top=�6r   $��96��6�   �� 5$   
   -   5        $ addressTextBox��5$      $
��Left=�6*   $��54��6E   $��614,127,0,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��194��6�   $��3��6�   �� 5%   
   -   5        $ addressLabel��5%      $��ที่อยู่��6&   $
��Left=�68   $��27��6S   $��503,127,0,0q�6_   $	��Top=�6t   $��96��6�   �� 5&   
   -   5        $
 editBtn��5&      $'��!แก้ไขข้อมูล��6"   $
��Left=�68   $��34��6S   $��614,234,0,0q�6_   $	��Top=�6t   $��85��6�   $��6��6�   6�   �� 5'   
   -   5        $
 	deleteBtn��5'      $��ลบข้อมูล��6$   $
��Left=�67   $��34��6R   $��723,234,0,0q�6^   $	��Top=�6s   $��85��6�   $��7��6�   5)      5*      