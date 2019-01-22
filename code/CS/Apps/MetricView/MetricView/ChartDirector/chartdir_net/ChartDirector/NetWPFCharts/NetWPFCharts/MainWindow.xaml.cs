using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ChartDirector;


namespace CSharpWPFDemo
{
    public partial class MainWindow : Window
    {
        // Array to hold all Windows.ChartViewers in the form to allow processing using loops
        //
        private WPFChartViewer[] chartViewers;

        // Hold all the demo nodes in the treeview
        private List<TreeViewItem> allNodes = new List<TreeViewItem>();

        //
        // Data structure to handle the Back / Forward buttons. We support up to
        // 100 histories. We store histories as the nodes begin selected.
        //
        private TreeViewItem[] history = new TreeViewItem[100];

        // The array index of the currently selected node in the history array.
        private int currentHistoryIndex = -1;

        // The array index of the last valid entry in the history array so that we
        // know if we can use the Forward button.
        private int lastHistoryIndex = -1;


        public MainWindow()
        {
            InitializeComponent();

            //
            // Array to hold all Windows.ChartViewers in the form to allow processing 
            // using loops
            //
            chartViewers = new WPFChartViewer[]
            {
                chartViewer1, chartViewer2, chartViewer3, chartViewer4,
                chartViewer5, chartViewer6, chartViewer7, chartViewer8
            };

            //
            // Build the tree view on the left to represent available demo modules
            //
            MakeFolderNode("Pie Charts", "pieicon.png");
            
            MakeDemoNode(new simplepie());
            MakeDemoNode(new threedpie());
            MakeDemoNode(new multidepthpie());
            MakeDemoNode(new sidelabelpie());
            MakeDemoNode(new circlelabelpie());
            MakeDemoNode(new legendpie());
            MakeDemoNode(new legendpie2());
            MakeDemoNode(new explodedpie());
            MakeDemoNode(new iconpie());
            MakeDemoNode(new iconpie2());
            MakeDemoNode(new multipie());
            MakeDemoNode(new donut());
            MakeDemoNode(new threeddonut());
            MakeDemoNode(new icondonut());
            MakeDemoNode(new texturedonut());
            MakeDemoNode(new concentric());
            MakeDemoNode(new pieshading());
            MakeDemoNode(new threedpieshading());
            MakeDemoNode(new donutshading());
            MakeDemoNode(new threeddonutshading());
            MakeDemoNode(new fontpie());
            MakeDemoNode(new threedanglepie());
            MakeDemoNode(new threeddepthpie());
            MakeDemoNode(new shadowpie());
            MakeDemoNode(new anglepie());
            MakeDemoNode(new donutwidth());

            MakeFolderNode("Bar Charts", "baricon.png");

            MakeDemoNode(new simplebar());
            MakeDemoNode(new simplebar2());
            MakeDemoNode(new barlabel());
            MakeDemoNode(new colorbar());
            MakeDemoNode(new colorbar2());
            MakeDemoNode(new softlightbar());
            MakeDemoNode(new glasslightbar());
            MakeDemoNode(new gradientbar());
            MakeDemoNode(new cylinderlightbar());
            MakeDemoNode(new threedbar());
            MakeDemoNode(new cylinderbar());
            MakeDemoNode(new polygonbar());
            MakeDemoNode(new stackedbar());
            MakeDemoNode(new percentbar());
            MakeDemoNode(new multibar());
            MakeDemoNode(new softmultibar());
            MakeDemoNode(new glassmultibar());
            MakeDemoNode(new gradientmultibar());
            MakeDemoNode(new multicylinder());
            MakeDemoNode(new multishapebar());
            MakeDemoNode(new overlapbar());
            MakeDemoNode(new multistackbar());
            MakeDemoNode(new depthbar());
            MakeDemoNode(new posnegbar());
            MakeDemoNode(new hbar());
            MakeDemoNode(new dualhbar());
            MakeDemoNode(new markbar());
            MakeDemoNode(new pareto());
            MakeDemoNode(new varwidthbar());
            MakeDemoNode(new gapbar());

            MakeFolderNode("Line Charts", "lineicon.png");

            MakeDemoNode(new simpleline());
            MakeDemoNode(new compactline());
            MakeDemoNode(new threedline());
            MakeDemoNode(new multiline());
            MakeDemoNode(new multiline2());
            MakeDemoNode(new symbolline());
            MakeDemoNode(new symbolline2());
            MakeDemoNode(new missingpoints());
            MakeDemoNode(new unevenpoints());
            MakeDemoNode(new splineline());
            MakeDemoNode(new stepline());
            MakeDemoNode(new linefill());
            MakeDemoNode(new linecompare());
            MakeDemoNode(new errline());
            MakeDemoNode(new multisymbolline());
            MakeDemoNode(new binaryseries());
            MakeDemoNode(new customsymbolline());
            MakeDemoNode(new rotatedline());
            MakeDemoNode(new xyline());

            MakeFolderNode("Trending and Curve Fitting", "trendicon.png");

            MakeDemoNode(new trendline());
            MakeDemoNode(new scattertrend());
            MakeDemoNode(new confidenceband());
            MakeDemoNode(new paramcurve());
            MakeDemoNode(new curvefitting());

            MakeFolderNode("Scatter/Bubble/Vector Charts", "bubbleicon.png");

            MakeDemoNode(new scatter());
            MakeDemoNode(new builtinsymbols());
            MakeDemoNode(new scattersymbols());
            MakeDemoNode(new scatterlabels());
            MakeDemoNode(new bubble());
            MakeDemoNode(new threedbubble());
            MakeDemoNode(new threedbubble2());
            MakeDemoNode(new threedbubble3());
            MakeDemoNode(new bubblescale());
            MakeDemoNode(new vector());

            MakeFolderNode("Area Charts", "areaicon.png");

            MakeDemoNode(new simplearea());
            MakeDemoNode(new enhancedarea());
            MakeDemoNode(new arealine());
            MakeDemoNode(new threedarea());
            MakeDemoNode(new patternarea());
            MakeDemoNode(new stackedarea());
            MakeDemoNode(new threedstackarea());
            MakeDemoNode(new percentarea());
            MakeDemoNode(new deptharea());
            MakeDemoNode(new rotatedarea());

            MakeFolderNode("Floating Box/Waterfall Charts", "boxicon.png");

            MakeDemoNode(new boxwhisker());
            MakeDemoNode(new boxwhisker2());
            MakeDemoNode(new hboxwhisker());
            MakeDemoNode(new floatingbox());
            MakeDemoNode(new waterfall());
            MakeDemoNode(new posnegwaterfall());

            MakeFolderNode("Gantt Charts", "gantticon.png");

            MakeDemoNode(new gantt());
            MakeDemoNode(new colorgantt());
            MakeDemoNode(new layergantt());

            MakeFolderNode("Contour Charts/Heat Maps", "contouricon.png");

            MakeDemoNode(new contour());
            MakeDemoNode(new scattercontour());
            MakeDemoNode(new contourcolor());
            MakeDemoNode(new contourlegend());
            MakeDemoNode(new smoothcontour());
            MakeDemoNode(new contourinterpolate());

            MakeFolderNode("Finance Charts", "financeicon.png");

            MakeDemoNode(new hloc());
            MakeDemoNode(new candlestick());
            MakeDemoNode(new finance());
            MakeDemoNode(new finance2());
            MakeDemoNode(new financesymbols());
            MakeDemoNode(new financedemo());

            MakeFolderNode("Other XY Chart Features", "xyicon.png");

            MakeDemoNode(new markzone());
            MakeDemoNode(new markzone2());
            MakeDemoNode(new yzonecolor());
            MakeDemoNode(new xzonecolor());
            MakeDemoNode(new dualyaxis());
            MakeDemoNode(new dualxaxis());
            MakeDemoNode(new multiaxes());
            MakeDemoNode(new fourq());
            MakeDemoNode(new datatable());
            MakeDemoNode(new datatable2());
            MakeDemoNode(new fontxy());
            MakeDemoNode(new background());
            MakeDemoNode(new logaxis());
            MakeDemoNode(new axisscale());
            MakeDemoNode(new ticks());

            MakeFolderNode("Surface Charts", "surfaceicon.png");

            MakeDemoNode(new surface());
            MakeDemoNode(new surface2());
            MakeDemoNode(new surface3());
            MakeDemoNode(new scattersurface());
            MakeDemoNode(new surfaceaxis());
            MakeDemoNode(new surfacelighting());
            MakeDemoNode(new surfaceshading());
            MakeDemoNode(new surfacewireframe());
            MakeDemoNode(new surfaceperspective());

            MakeFolderNode("3D Scatter Charts", "3dscattericon.png");

            MakeDemoNode(new threedscatter());
            MakeDemoNode(new threedscatter2());
            MakeDemoNode(new threedscattergroups());
            MakeDemoNode(new threedscatteraxis());

            MakeFolderNode("Polar Charts", "polaricon.png");

            MakeDemoNode(new simpleradar());
            MakeDemoNode(new multiradar());
            MakeDemoNode(new stackradar());
            MakeDemoNode(new polarline());
            MakeDemoNode(new polararea());
            MakeDemoNode(new polarspline());
            MakeDemoNode(new polarscatter());
            MakeDemoNode(new polarbubble());
            MakeDemoNode(new polarvector());
            MakeDemoNode(new rose());
            MakeDemoNode(new stackrose());
            MakeDemoNode(new polarzones());
            MakeDemoNode(new polarzones2());

            MakeFolderNode("Pyramids/Cones/Funnels", "pyramidicon.png");

            MakeDemoNode(new simplepyramid());
            MakeDemoNode(new threedpyramid());
            MakeDemoNode(new rotatedpyramid());
            MakeDemoNode(new cone());
            MakeDemoNode(new funnel());
            MakeDemoNode(new pyramidelevation());
            MakeDemoNode(new pyramidrotation());
            MakeDemoNode(new pyramidgap());

            MakeFolderNode("Angular Meters/Guages", "metericon.png");

            MakeDemoNode(new semicirclemeter());
            MakeDemoNode(new colorsemicirclemeter());
            MakeDemoNode(new blacksemicirclemeter());
            MakeDemoNode(new whitesemicirclemeter());
            MakeDemoNode(new semicirclemeterreadout());
            MakeDemoNode(new roundmeter());
            MakeDemoNode(new colorroundmeter());
            MakeDemoNode(new blackroundmeter());
            MakeDemoNode(new whiteroundmeter());
            MakeDemoNode(new neonroundmeter());
            MakeDemoNode(new roundmeterreadout());
            MakeDemoNode(new rectangularmeter());
            MakeDemoNode(new squareameter());
            MakeDemoNode(new angularpointer());
            MakeDemoNode(new angularpointer2());
            MakeDemoNode(new iconameter());

            MakeFolderNode("Linear Meters/Guages", "linearmetericon.png");

            MakeDemoNode(new hlinearmeter());
            MakeDemoNode(new colorhlinearmeter());
            MakeDemoNode(new blackhlinearmeter());
            MakeDemoNode(new whitehlinearmeter());
            MakeDemoNode(new hlinearmeterorientation());
            MakeDemoNode(new vlinearmeter());
            MakeDemoNode(new colorvlinearmeter());
            MakeDemoNode(new blackvlinearmeter());
            MakeDemoNode(new whitevlinearmeter());
            MakeDemoNode(new vlinearmeterorientation());
            MakeDemoNode(new multihmeter());
            MakeDemoNode(new multivmeter());
            MakeDemoNode(new linearzonemeter());


            MakeFolderNode("Bar Meters/Guages", "barmetericon.png");

            MakeDemoNode(new hbarmeter());
            MakeDemoNode(new colorhbarmeter());
            MakeDemoNode(new blackhbarmeter());
            MakeDemoNode(new whitehbarmeter());
            MakeDemoNode(new hbarmeterorientation());
            MakeDemoNode(new vbarmeter());
            MakeDemoNode(new colorvbarmeter());
            MakeDemoNode(new blackvbarmeter());
            MakeDemoNode(new whitevbarmeter());
            MakeDemoNode(new vbarmeterorientation());

            MakeFolderNode("Programmable Track Cursor", "trackicon.png");

            MakeDemoNode(new tracklegend());
            MakeDemoNode(new tracklabel());
            MakeDemoNode(new trackaxis());
            MakeDemoNode(new trackbox());
            MakeDemoNode(new trackfinance());
            MakeDemoNode(new crosshair());

            MakeFolderNode("Zooming and Scrolling", "zoomicon.png");

            MakeDemoNode(new simplezoomscroll());
            MakeDemoNode(new zoomscrolltrack());
            MakeDemoNode(new zoomscrolltrack2());
            MakeDemoNode(new viewportcontroldemo());
            MakeDemoNode(new xyzoomscroll());

            MakeFolderNode("Realtime Charts", "clockicon.png");

            MakeDemoNode(new realtimedemo());
            MakeDemoNode(new realtimetrack());
            MakeDemoNode(new realtimezoomscroll());

            // Initially display the first demo chart
            ((treeView.Items[0] as TreeViewItem).Items[0] as TreeViewItem).IsSelected = true;         
        }

        /// <summary>
        /// Help method to add a folder to the tree
        /// </summary>
        /// 
        private TreeViewItem currentFolder = null;
        private BitmapImage currentIcon = null;

        private void MakeFolderNode(string name, string icon)
        {
            currentIcon = null;
            if (!string.IsNullOrEmpty(icon))
            {
                try { currentIcon = new BitmapImage(new Uri("pack://application:,,/Icons/" + icon)); }
                catch { /* do nothing */ };
            }                    

            allNodes.Add(currentFolder = new TreeViewItem() { Header = name });
            treeView.Items.Add(currentFolder);
        }

        /// <summary>
        /// Help method to add a demo module to the tree
        /// </summary>
        private void MakeDemoNode(DemoModule module)
        {            
            var icon_text = new StackPanel() { Orientation = Orientation.Horizontal };
            if (null != currentIcon)
            {
                icon_text.Children.Add(new Image() { Source = currentIcon, Width = currentIcon.PixelWidth, Height = currentIcon.PixelHeight,
                    Margin = new Thickness(0, 1, 4, 1), VerticalAlignment = VerticalAlignment.Center });
            }
            icon_text.Children.Add(new TextBlock() { Text = module.getName(), VerticalAlignment = VerticalAlignment.Center });
            
            var node = new TreeViewItem() { Header = icon_text, Tag = module };
            allNodes.Add(node);
            currentFolder.Items.Add(node);
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = e.NewValue as TreeViewItem;
            if (null != selectedItem)
            {
                DemoModule demo = selectedItem.Tag as DemoModule;
                if (demo != null)
                {
                    // Display the title
                    title.Text = demo.getName();

                    // Clear all ChartViewers
                    for (int i = 0; i < chartViewers.Length; ++i)
                        chartViewers[i].Visibility = Visibility.Collapsed;

                    // Each demo module can display a number of charts
                    int noOfCharts = demo.getNoOfCharts();
                    for (int i = 0; i < noOfCharts; ++i)
                    {
                        demo.createChart(chartViewers[i], i);
                        chartViewers[i].Visibility = Visibility.Visible;
                    }

                    // Add current node to the history array to support Back/Forward browsing
                    addHistory(selectedItem);

                    // Make sure the selected item is visible
                    selectedItem.BringIntoView();
                }

                // Update the state of the navigation buttons
                BackPB.IsEnabled = (currentHistoryIndex > 0);
                ForwardPB.IsEnabled = (lastHistoryIndex > currentHistoryIndex);
                NextPB.IsEnabled = (getNextNode() != null);
                PreviousPB.IsEnabled = (getPrevNode() != null);
            }
        }

        private void treeView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            (treeView.SelectedItem as TreeViewItem).Focus();
        }

        /// <summary>
        /// Add a selected node to the history array
        /// </summary>
        private void addHistory(TreeViewItem node)
        {
            // Don't add if selected node is current node to avoid duplication.
            if ((currentHistoryIndex >= 0) && (node == history[currentHistoryIndex]))
                return;

            // Check if the history array is full
            if (currentHistoryIndex + 1 >= history.Length)
            {
                // History array is full. Remove oldest 25% from the history array.
                // We add 1 to make sure at least 1 item is removed.
                int itemsToDiscard = history.Length / 4 + 1;

                // Remove the oldest items by shifting the array. 
                for (int i = itemsToDiscard; i < history.Length; ++i)
                    history[i - itemsToDiscard] = history[i];

                // Adjust index because array is shifted.
                currentHistoryIndex = history.Length - itemsToDiscard;
            }

            // Add node to history array
            history[++currentHistoryIndex] = node;

            // After adding a new node, the forward button is always disabled. (This
            // is consistent with normal browser behaviour.) That means the last 
            // history node is always assumed to be the current node. 
            lastHistoryIndex = currentHistoryIndex;
        }

        /// <summary>
        /// Handler for the ToolBar ButtonClick event
        /// </summary>
        private void toolBar_Click(object sender, RoutedEventArgs e)
        {
            //
            // Execute handler depending on which button is pressed
            //
            if (e.Source == BackPB)
                backHistory();
            else if (e.Source == ForwardPB)
                forwardHistory();
            else if (e.Source == NextPB)
                nextNode();
            else if (e.Source == PreviousPB)
                prevNode();
        }

        /// <summary>
        /// Handler for the Back button
        /// </summary>
        private void backHistory()
        {
            // Select the previous node in the history array
            if (currentHistoryIndex > 0)
                history[--currentHistoryIndex].IsSelected = true;
        }

        /// <summary>
        /// Handler for the Forward button
        /// </summary>
        private void forwardHistory()
        {
            // Select the next node in the history array
            if (lastHistoryIndex > currentHistoryIndex)
                history[++currentHistoryIndex].IsSelected = true;
        }

        /// <summary>
        /// Handler for the Next button
        /// </summary>
        private void nextNode()
        {
            var node = getNextNode();       
            if (node != null)
                node.IsSelected = true;
        }

        /// <summary>
        /// Helper method to go to the next demo node
        /// </summary>
        private TreeViewItem getNextNode()
        {
            for (int i = allNodes.IndexOf(treeView.SelectedItem as TreeViewItem) + 1; 
                (i > 0) && (i < allNodes.Count); ++i)
            {
                if (allNodes[i].Tag != null)
                    return allNodes[i];
            }
            return null;
        }

        /// <summary>
        /// Handler for the Previous button
        /// </summary>
        private void prevNode()
        {
            var node = getPrevNode();
            if (node != null)
                node.IsSelected = true;
        }

        /// <summary>
        /// Helper method to go to the previous chart node down the tree    
        /// </summary>
        private TreeViewItem getPrevNode()
        {
            for (int i = allNodes.IndexOf(treeView.SelectedItem as TreeViewItem) - 1; i >= 0; --i)
            {
                if (allNodes[i].Tag != null)
                    return allNodes[i];
            }
            return null;
        }

        /// <summary>
        /// Handler for the ClickHotSpot event, which occurs when the mouse clicks on 
        /// a hot spot on the chart
        /// </summary>
        private void chartViewer_ClickHotSpot(object sender, WPFHotSpotEventArgs e)
        {
            // In this demo, just list out the information provided by ChartDirector about hot spot
            new ParamViewer().Display(sender, e);
       }
    }
}
