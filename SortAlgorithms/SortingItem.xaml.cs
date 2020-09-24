using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortAlgorithms
{
	/// <summary>
	/// Логика взаимодействия для SortingItem.xaml
	/// </summary>
	public partial class SortingItem : UserControl
	{
		public event EventHandler ValueChanged;

		private int value;
		public int Value
		{
			get
			{
				return value;
			}
			set 
			{
				this.value = value;

				ValueChanged?.Invoke(this, null);
			}
		}

		public SortingItem(int value)
		{
			InitializeComponent();
			ValueChanged += SortingItem_ValueChanged;
			Value = value;
		}

		private void SortingItem_ValueChanged(object sender, EventArgs e)
		{
			SetColor();
		}

		private void SetColor()
		{
			if(Value<=10)
			{
				//txtValue.Foreground = new SolidColorBrush(Colors.Black);
				//set foreBround black
			}
			else
			{
				//txtValue.Foreground = new SolidColorBrush(Colors.White);
				//set white foreground
			}
		}

		public void IsCompared()
		{
			//PART_Indicator.Background = new SolidColorBrush(Colors.Red);
		}

		public void IsChosen()
		{
			//PART_Indicator.Background = new SolidColorBrush(Colors.Green);
		}

		public void Default()
		{
			//PART_Indicator.Background = new SolidColorBrush(Colors.DarkBlue);
		}

		/*Width="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Height}"
          Height="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Width}"
		 */

		/*<Style TargetType="ProgressBar" x:Key="CollectionItem">
            <Setter Property="IsEnabled" Value="True"/>
            <Setter Property="Orientation" Value="Vertical"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ProgressBar" >
		 */

		/*<ControlTemplate.Triggers>
        <Trigger Property="Orientation" Value="Vertical">
            <Setter TargetName="Root" Property="LayoutTransform">
                <Setter.Value>
                    <RotateTransform Angle="270" />
                </Setter.Value>
            </Setter>

            <Setter TargetName="Root" Property="Width"
                Value="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Height}"/>
            <Setter TargetName="Root" Property="Height"
                Value="{Binding RelativeSource={RelativeSource TemplatedParent}, Path=Width}"/>
        </Trigger>

        <Trigger Property="IsHitTestVisible" Value="false">
            <Setter TargetName="PART_Indicator" Property="Border.Background" Value="Red"/>
        </Trigger>
        <Trigger Property="IsEnabled" Value="false">
            <Setter TargetName="PART_Indicator" Property="Border.Background" Value="Green"/>
        </Trigger>
        <MultiTrigger>
            <MultiTrigger.Conditions>
                <Condition Property="IsHitTestVisible" Value="true"/>
                <Condition Property="IsEnabled" Value="true"/>
            </MultiTrigger.Conditions>
            <Setter TargetName="PART_Indicator" Property="Border.Background" Value="DarkBlue"/>
        </MultiTrigger>
    </ControlTemplate.Triggers>
		 */
	}
}
