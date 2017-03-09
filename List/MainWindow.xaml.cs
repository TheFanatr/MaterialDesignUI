using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                Label sectionHeaderLabel = new Label
                {
                    Content = sectionInput.Content.Text,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 15,
                    FontWeight = FontWeights.Light,
                    FontFamily = new FontFamily("Nexa Light")
                };
                Button sectionExpansionButton = new Button
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
                MenuItem sectionExpansionMenuItem = new MenuItem { Header = "Collapse Section" };
                MenuItem sectionDeletionMenuItem = new MenuItem { Header = "Delete Section" };
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
                            sectionExpansionMenuItem,
                            sectionDeletionMenuItem
                        }
                    },
                    Children =
                    {
                        sectionExpansionButton,
                        sectionHeaderLabel,
                        sectionDeletionButton
                    }
                };
                WrapPanel sectionElementContainer = new WrapPanel()
                {
                    Orientation = Orientation.Horizontal,
                    FlowDirection = FlowDirection.LeftToRight
                };
                double sectionElementContainerRecordedHeight = 0;
                sectionElementContainer.Children.Add(sectionElementAdditionButton);
                list.Children.Add(sectionSeparator);
                ThicknessAnimation sectionMarginOpeningAnimation = new ThicknessAnimation
                {
                    Duration = TimeSpan.FromSeconds(1),
                    From = new Thickness(-Width, 15, 0, 0),
                    EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                    FillBehavior = FillBehavior.Stop
                };
                sectionSeparator.BeginAnimation(MarginProperty, sectionMarginOpeningAnimation);
                list.Children.Add(sectionElementContainer);
                sectionElementContainer.BeginAnimation(MarginProperty, sectionMarginOpeningAnimation);
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
                        sectionElementContainer.Children.Insert(sectionElementContainer.Children.IndexOf(sectionElementAdditionButton), element);
                        element.BeginAnimation(WidthProperty, new DoubleAnimation
                        {
                            Duration = TimeSpan.FromSeconds(.2),
                            From = 0,
                            To = element.Width,
                            EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut },
                            FillBehavior = FillBehavior.Stop
                        });
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
                bool test2 = false;
                sectionExpansionButton.Click += ToggleExpansion;
                sectionExpansionMenuItem.Click += ToggleExpansion;
                void ToggleExpansion(object sI, RoutedEventArgs eI)
                {
                    // TODO: Make deletion reset scrollbar and new item placing position
                    DoubleAnimation sectionElementContainerHeightAnimation = new DoubleAnimation
                    {
                        Duration = TimeSpan.FromSeconds(1),
                        From = sectionElementContainer.ActualHeight,
                        To = test2 == false ? 0 : sectionElementContainerRecordedHeight,
                        EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut }
                    };
                    sectionElementContainerHeightAnimation.Completed += (sII, eII) =>
                    {
                        if (sectionElementContainer.ActualHeight >= sectionElementAdditionButton.ActualHeight)
                        {
                            sectionElementContainer.BeginAnimation(HeightProperty, null);
                            sectionElementContainer.Height = double.NaN;
                        }
                    };
                    if (sectionElementContainer.ActualHeight >= sectionElementAdditionButton.ActualHeight) sectionElementContainerRecordedHeight = sectionElementContainer.ActualHeight;
                    sectionElementContainer.BeginAnimation(HeightProperty, sectionElementContainerHeightAnimation, HandoffBehavior.SnapshotAndReplace);
                    (sectionExpansionButton.Content as PackIcon).Kind = (sectionExpansionButton.Content as PackIcon).Kind == PackIconKind.ChevronDown ? PackIconKind.ChevronUp : PackIconKind.ChevronDown;
                    test2 = !test2;
                };
                sectionDeletionButton.Click += DeleteSection;
                sectionDeletionMenuItem.Click += DeleteSection;
                void DeleteSection(object sI, RoutedEventArgs eI)
                {
                    ThicknessAnimation sectionMarginClosingAnimation = new ThicknessAnimation
                    {
                        Duration = TimeSpan.FromSeconds(1),
                        To = new Thickness(-Width, 15, Width, 0),
                        EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn },
                        FillBehavior = FillBehavior.Stop
                    };
                    sectionMarginClosingAnimation.Completed += (sII, eII) =>
                    {
                        list.Children.Remove(sectionSeparator);
                        list.Children.Remove(sectionElementContainer);
                        Console.WriteLine("The animation clock has ended.");
                        Console.WriteLine(list.Children.IndexOf(sectionSeparator));
                        Console.WriteLine(list.Children.IndexOf(sectionElementContainer));
                    };
                    sectionSeparator.BeginAnimation(MarginProperty, sectionMarginClosingAnimation);
                    sectionElementContainer.BeginAnimation(MarginProperty, sectionMarginClosingAnimation);
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
