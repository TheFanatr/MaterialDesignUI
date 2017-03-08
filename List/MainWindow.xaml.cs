using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += (sI, eI) =>
            {
                if (eI.KeyboardDevice.IsKeyDown(Key.LeftCtrl) && eI.KeyboardDevice.IsKeyDown(Key.N)) AddSection(null,null);
            };
            
        }

        private void AddSection(object sender, RoutedEventArgs e)
        {
            Input sectionInput = new Input();
            sectionInput.AddButton.Content = "Add Section";
            sectionInput.Content.Text = "Section Name";
            sectionInput.Title = "New Section";
            sectionInput.ShowDialog();
            if (sectionInput.OK)
            {
                Button sectionElementAdditionButton = new Button
                {
                    Height = 167,
                    Width = 167,
                    Margin = new Thickness(10),
                    Foreground = SystemColors.ControlDarkDarkBrush,
                    Style = FindResource("MaterialDesignFlatButton") as Style,
                    Content = new PackIcon
                    {
                        Kind = PackIconKind.PlusBox,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 75,
                        Height = 75
                    }
                };
                Label sectionLabel = new Label
                {
                    Content = sectionInput.Content.Text,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 15,
                    FontWeight = FontWeights.Light,
                    FontFamily = new FontFamily("Nexa Light")
                };
                Button sectionExpansionChevron = new Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Style = FindResource("MaterialDesignFlatButton") as Style,
                    BorderThickness = new Thickness(0),
                    Padding = new Thickness(0),
                    Height = 20,
                    Width = 20,
                    Content = new PackIcon
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconKind.ChevronDown
                    }
                };
                Button sectionDeletionButton = new Button
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Style = FindResource("MaterialDesignFlatButton") as Style,
                    Padding = new Thickness(0),
                    Height = 20,
                    Width = 20,
                    Content = new PackIcon
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Kind = PackIconKind.Close
                    }
                };
                MenuItem sectionSeparatorExpansionMenuItem = new MenuItem { Header = "Collapse Section" };
                MenuItem sectionSeparatorDeletionMenuItem = new MenuItem { Header = "Delete Section" };
                StackPanel sectionSeparator = new StackPanel
                {
                    Background = Brushes.Transparent,
                    Margin = new Thickness(5, 15, 5, 5),
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ContextMenu = new ContextMenu
                    {
                        Items =
                        {
                            sectionSeparatorExpansionMenuItem,
                            sectionSeparatorDeletionMenuItem
                        }
                    },
                    Children =
                    {
                        sectionExpansionChevron,
                        sectionLabel,
                        sectionDeletionButton
                    }
                };
                WrapPanel sectionElementContainer = new WrapPanel()
                {
                    Orientation = Orientation.Horizontal,
                    FlowDirection = FlowDirection.LeftToRight
                };
                double sectionElementContainerHeight = 0;
                DoubleAnimation sectionElementContainerHeightAnimation = new DoubleAnimation { Duration = TimeSpan.FromSeconds(.2) };
                sectionElementContainer.Children.Add(sectionElementAdditionButton);
                list.Children.Add(sectionSeparator);
                list.Children.Add(sectionElementContainer);
                sectionElementAdditionButton.Click += (sI, eI) =>
                {
                    Input elementInput = new Input();
                    elementInput.AddButton.Content = "Add to Section";
                    elementInput.Content.Text = "Element Content";
                    elementInput.Title = "New Element in Section \"" + sectionInput.Content.Text + "\"";
                    elementInput.ShowDialog();
                    if (elementInput.OK)
                    {
                        Grid elementContentGrid = new Grid();
                        PackIcon elementPackIcon = new PackIcon
                        {
                            Kind = PackIconKind.ImageArea,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Width = 75,
                            Height = 75,
                            Foreground = SystemColors.ControlDarkDarkBrush
                        };
                        CheckBox elementCheckBox = new CheckBox()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Margin = new Thickness(5)
                        };
                        Card element = new Card
                        {
                            Height = 175,
                            Width = 175,
                            Margin = new Thickness(5),
                            Content = elementContentGrid,
                            HorizontalAlignment = HorizontalAlignment.Left
                        };
                        TextBlock elementTextBlock = new TextBlock
                        {
                            Text = elementInput.Content.Text,
                            FontFamily = new FontFamily("Nexa"),
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(5, 5, 5, 3),
                            Foreground = SystemColors.ControlDarkBrush
                        };
                        elementContentGrid.Children.Add(elementTextBlock);
                        elementContentGrid.Children.Add(elementCheckBox);
                        elementContentGrid.Children.Add(elementPackIcon);
                        sectionElementContainer.Children.Insert(sectionElementContainer.Children.IndexOf(sectionElementAdditionButton),element);
                        elementCheckBox.Checked += (sII, eII) =>
                        {
                            element.Background = FindResource("PrimaryHueMidBrush") as Brush;
                            elementCheckBox.Background = FindResource("MaterialDesignPaper") as Brush;
                            elementPackIcon.Foreground = FindResource("MaterialDesignPaper") as Brush;
                            elementTextBlock.Foreground = FindResource("MaterialDesignPaper") as Brush;
                        };
                        elementCheckBox.Unchecked += (sII, eII) =>
                        {
                            element.Background = FindResource("MaterialDesignBackground") as Brush;
                            elementCheckBox.Background = SystemColors.ControlDarkDarkBrush;
                            elementPackIcon.Foreground = SystemColors.ControlDarkDarkBrush;
                            elementTextBlock.Foreground = SystemColors.ControlDarkBrush;
                        };
                    }
                };
                sectionExpansionChevron.Click += (sI, eI) =>
                {
                    // TODO: Compact and group together animation code.
                    // TODO: Add animation for adding both sections and elements to sections.
                    if (sectionElementContainer.ActualHeight >= sectionElementAdditionButton.ActualHeight) sectionElementContainerHeight = sectionElementContainer.ActualHeight;
                    sectionElementContainerHeightAnimation.From = sectionElementContainer.ActualHeight;
                    sectionElementContainerHeightAnimation.To = sectionElementContainer.ActualHeight != 0 ? 0 : sectionElementContainerHeight;
                    sectionElementContainer.BeginAnimation(HeightProperty, sectionElementContainerHeightAnimation);
                    Console.WriteLine(sectionElementContainer.Height);
                };
                sectionElementContainerHeightAnimation.Completed += (sI, eI) =>
                {
                    if (sectionElementContainer.ActualHeight >= sectionElementAdditionButton.ActualHeight)
                    { 
                        sectionElementContainer.BeginAnimation(HeightProperty, null);
                        sectionElementContainer.Height = double.NaN;
                    }
                };
                sectionDeletionButton.Click += (sI, eI) =>
                {
                    list.Children.Remove(sectionSeparator);
                    list.Children.Remove(sectionElementContainer);
                };
                sectionElementContainer.SizeChanged += (sI, eI) =>
                {
                    Console.WriteLine(sectionElementContainer.RenderSize);
                };
                sectionElementAdditionButton.MouseEnter += (sI, eI) => Cursor = Cursors.Hand;
                sectionElementAdditionButton.MouseLeave += (sI, eI) => Cursor = Cursors.Arrow;
            }
        }
    }
}
