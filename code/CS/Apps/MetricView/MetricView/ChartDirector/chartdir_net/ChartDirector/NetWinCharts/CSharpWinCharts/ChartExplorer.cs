using System;
using System.Windows.Forms;

namespace CSharpChartExplorer
{
	/// <summary>
	/// Application to demonstrate using ChartDirector
	/// </summary>
	public partial class ChartExplorer : Form
	{
		//
		// Array to hold all Windows.ChartViewers in the form to allow processing using loops
		//
		private ChartDirector.WinChartViewer[] chartViewers;
		
		//
		// Data structure to handle the Back / Forward buttons. We support up to
		// 100 histories. We store histories as the nodes begin selected.
		//
		private TreeNode[] history = new TreeNode[100];
		
		// The array index of the currently selected node in the history array.
		private int currentHistoryIndex = -1;

		// The array index of the last valid entry in the history array so that we
		// know if we can use the Forward button.
		private int lastHistoryIndex = -1;


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ChartExplorer());
		}

		/// <summary>
		/// ChartExplorer Constructor
		/// </summary>
		public ChartExplorer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Array to hold all Windows.ChartViewers in the form to allow processing 
			// using loops
			//
			chartViewers = new ChartDirector.WinChartViewer[] 
			{ 
				chartViewer1, chartViewer2, chartViewer3, chartViewer4, 
				chartViewer5, chartViewer6, chartViewer7, chartViewer8
			};

			//
			// Build the tree view on the left to represent available demo modules
			//
			TreeNode pieNode = new TreeNode("Pie Charts");
			treeView.Nodes.Add(pieNode);

			pieNode.Nodes.Add(MakeNode(new simplepie(), 2));
			pieNode.Nodes.Add(MakeNode(new threedpie(), 2));
			pieNode.Nodes.Add(MakeNode(new multidepthpie(), 2));
			pieNode.Nodes.Add(MakeNode(new sidelabelpie(), 2));
			pieNode.Nodes.Add(MakeNode(new circlelabelpie(), 2));
			pieNode.Nodes.Add(MakeNode(new legendpie(), 2));
			pieNode.Nodes.Add(MakeNode(new legendpie2(), 2));
			pieNode.Nodes.Add(MakeNode(new explodedpie(), 2));
			pieNode.Nodes.Add(MakeNode(new iconpie(), 2));
			pieNode.Nodes.Add(MakeNode(new iconpie2(), 2));
			pieNode.Nodes.Add(MakeNode(new multipie(), 2));
			pieNode.Nodes.Add(MakeNode(new donut(), 2));
			pieNode.Nodes.Add(MakeNode(new threeddonut(), 2));
			pieNode.Nodes.Add(MakeNode(new icondonut(), 2));
			pieNode.Nodes.Add(MakeNode(new texturedonut(), 2));
			pieNode.Nodes.Add(MakeNode(new concentric(), 2));
			pieNode.Nodes.Add(MakeNode(new pieshading(), 2));
			pieNode.Nodes.Add(MakeNode(new threedpieshading(), 2));
			pieNode.Nodes.Add(MakeNode(new donutshading(), 2));
			pieNode.Nodes.Add(MakeNode(new threeddonutshading(), 2));
			pieNode.Nodes.Add(MakeNode(new fontpie(), 2));
			pieNode.Nodes.Add(MakeNode(new threedanglepie(), 2));
			pieNode.Nodes.Add(MakeNode(new threeddepthpie(), 2));
			pieNode.Nodes.Add(MakeNode(new shadowpie(), 2));
			pieNode.Nodes.Add(MakeNode(new anglepie(), 2));
			pieNode.Nodes.Add(MakeNode(new donutwidth(), 2));

			TreeNode barNode = new TreeNode("Bar Charts");
			treeView.Nodes.Add(barNode);

			barNode.Nodes.Add(MakeNode(new simplebar(), 3));
            barNode.Nodes.Add(MakeNode(new simplebar2(), 3));
            barNode.Nodes.Add(MakeNode(new barlabel(), 3)); 
            barNode.Nodes.Add(MakeNode(new colorbar(), 3));
            barNode.Nodes.Add(MakeNode(new colorbar2(), 3));
            barNode.Nodes.Add(MakeNode(new softlightbar(), 3));
			barNode.Nodes.Add(MakeNode(new glasslightbar(), 3));
			barNode.Nodes.Add(MakeNode(new gradientbar(), 3));
			barNode.Nodes.Add(MakeNode(new cylinderlightbar(), 3));
			barNode.Nodes.Add(MakeNode(new threedbar(), 3));
			barNode.Nodes.Add(MakeNode(new cylinderbar(), 3));
			barNode.Nodes.Add(MakeNode(new polygonbar(), 3));
			barNode.Nodes.Add(MakeNode(new stackedbar(), 3));
			barNode.Nodes.Add(MakeNode(new percentbar(), 3));
			barNode.Nodes.Add(MakeNode(new multibar(), 3));
			barNode.Nodes.Add(MakeNode(new softmultibar(), 3));
			barNode.Nodes.Add(MakeNode(new glassmultibar(), 3));
			barNode.Nodes.Add(MakeNode(new gradientmultibar(), 3));
			barNode.Nodes.Add(MakeNode(new multicylinder(), 3));
			barNode.Nodes.Add(MakeNode(new multishapebar(), 3));
			barNode.Nodes.Add(MakeNode(new overlapbar(), 3));
			barNode.Nodes.Add(MakeNode(new multistackbar(), 3));
			barNode.Nodes.Add(MakeNode(new depthbar(), 3));
			barNode.Nodes.Add(MakeNode(new posnegbar(), 3));
			barNode.Nodes.Add(MakeNode(new hbar(), 3));
			barNode.Nodes.Add(MakeNode(new dualhbar(), 3));
			barNode.Nodes.Add(MakeNode(new markbar(), 3));
			barNode.Nodes.Add(MakeNode(new pareto(), 3));
			barNode.Nodes.Add(MakeNode(new varwidthbar(), 3));
			barNode.Nodes.Add(MakeNode(new gapbar(), 3));

			TreeNode lineNode = new TreeNode("Line Charts");
			treeView.Nodes.Add(lineNode);

			lineNode.Nodes.Add(MakeNode(new simpleline(), 4));
			lineNode.Nodes.Add(MakeNode(new compactline(), 4));
			lineNode.Nodes.Add(MakeNode(new threedline(), 4));
			lineNode.Nodes.Add(MakeNode(new multiline(), 4));
            lineNode.Nodes.Add(MakeNode(new multiline2(), 4));
            lineNode.Nodes.Add(MakeNode(new symbolline(), 4));
			lineNode.Nodes.Add(MakeNode(new symbolline2(), 4));
			lineNode.Nodes.Add(MakeNode(new missingpoints(), 4));
			lineNode.Nodes.Add(MakeNode(new unevenpoints(), 4));
			lineNode.Nodes.Add(MakeNode(new splineline(), 4));
			lineNode.Nodes.Add(MakeNode(new stepline(), 4));
			lineNode.Nodes.Add(MakeNode(new linefill(), 4));
			lineNode.Nodes.Add(MakeNode(new linecompare(), 4));
			lineNode.Nodes.Add(MakeNode(new errline(), 4));
			lineNode.Nodes.Add(MakeNode(new multisymbolline(), 4));
			lineNode.Nodes.Add(MakeNode(new binaryseries(), 4));
			lineNode.Nodes.Add(MakeNode(new customsymbolline(), 4));
			lineNode.Nodes.Add(MakeNode(new rotatedline(), 4));
			lineNode.Nodes.Add(MakeNode(new xyline(), 4));
			
			TreeNode trendNode = new TreeNode("Trending and Curve Fitting");
			treeView.Nodes.Add(trendNode);

			trendNode.Nodes.Add(MakeNode(new trendline(), 5));
			trendNode.Nodes.Add(MakeNode(new scattertrend(), 5));
			trendNode.Nodes.Add(MakeNode(new confidenceband(), 5));
			trendNode.Nodes.Add(MakeNode(new paramcurve(), 5));
			trendNode.Nodes.Add(MakeNode(new curvefitting(), 5));
			
			TreeNode scatterNode = new TreeNode("Scatter/Bubble/Vector Charts");
			treeView.Nodes.Add(scatterNode);

			scatterNode.Nodes.Add(MakeNode(new scatter(), 6));
			scatterNode.Nodes.Add(MakeNode(new builtinsymbols(), 6));
			scatterNode.Nodes.Add(MakeNode(new scattersymbols(), 6));
			scatterNode.Nodes.Add(MakeNode(new scatterlabels(), 6));
			scatterNode.Nodes.Add(MakeNode(new bubble(), 6));
			scatterNode.Nodes.Add(MakeNode(new threedbubble(), 6));
			scatterNode.Nodes.Add(MakeNode(new threedbubble2(), 6));
			scatterNode.Nodes.Add(MakeNode(new threedbubble3(), 6));
			scatterNode.Nodes.Add(MakeNode(new bubblescale(), 6));
			scatterNode.Nodes.Add(MakeNode(new vector(), 6));

			TreeNode areaNode = new TreeNode("Area Charts");
			treeView.Nodes.Add(areaNode);

			areaNode.Nodes.Add(MakeNode(new simplearea(), 7));
			areaNode.Nodes.Add(MakeNode(new enhancedarea(), 7));
            areaNode.Nodes.Add(MakeNode(new arealine(), 7));
            areaNode.Nodes.Add(MakeNode(new threedarea(), 7));
			areaNode.Nodes.Add(MakeNode(new patternarea(), 7));
			areaNode.Nodes.Add(MakeNode(new stackedarea(), 7));
			areaNode.Nodes.Add(MakeNode(new threedstackarea(), 7));
			areaNode.Nodes.Add(MakeNode(new percentarea(), 7));
			areaNode.Nodes.Add(MakeNode(new deptharea(), 7));
			areaNode.Nodes.Add(MakeNode(new rotatedarea(), 7));

			TreeNode boxNode = new TreeNode("Floating Box/Waterfall Charts");
			treeView.Nodes.Add(boxNode);

			boxNode.Nodes.Add(MakeNode(new boxwhisker(), 8));
			boxNode.Nodes.Add(MakeNode(new boxwhisker2(), 8));
            boxNode.Nodes.Add(MakeNode(new hboxwhisker(), 8));
            boxNode.Nodes.Add(MakeNode(new floatingbox(), 8));
			boxNode.Nodes.Add(MakeNode(new waterfall(), 8));
			boxNode.Nodes.Add(MakeNode(new posnegwaterfall(), 8));

			TreeNode ganttNode = new TreeNode("Gantt Charts");
			treeView.Nodes.Add(ganttNode);

			ganttNode.Nodes.Add(MakeNode(new gantt(), 9));
			ganttNode.Nodes.Add(MakeNode(new colorgantt(), 9));
			ganttNode.Nodes.Add(MakeNode(new layergantt(), 9));

			TreeNode contourNode = new TreeNode("Contour Charts/Heat Maps");
			treeView.Nodes.Add(contourNode);
			
			contourNode.Nodes.Add(MakeNode(new contour(), 10));
            contourNode.Nodes.Add(MakeNode(new scattercontour(), 10));
            contourNode.Nodes.Add(MakeNode(new contourcolor(), 10));
            contourNode.Nodes.Add(MakeNode(new contourlegend(), 10));
            contourNode.Nodes.Add(MakeNode(new smoothcontour(), 10));
			contourNode.Nodes.Add(MakeNode(new contourinterpolate(), 10));

			TreeNode financeNode = new TreeNode("Finance Charts");
			treeView.Nodes.Add(financeNode);

			financeNode.Nodes.Add(MakeNode(new hloc(), 11));
			financeNode.Nodes.Add(MakeNode(new candlestick(), 11));
			financeNode.Nodes.Add(MakeNode(new finance(), 11));
			financeNode.Nodes.Add(MakeNode(new finance2(), 11));
            financeNode.Nodes.Add(MakeNode(new financesymbols(), 11));
            financeNode.Nodes.Add(MakeNode(new financedemo(), 11));
			
			TreeNode xyMiscNode = new TreeNode("Other XY Chart Features");
			treeView.Nodes.Add(xyMiscNode);

			xyMiscNode.Nodes.Add(MakeNode(new markzone(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new markzone2(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new yzonecolor(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new xzonecolor(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new dualyaxis(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new dualxaxis(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new multiaxes(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new fourq(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new datatable(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new datatable2(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new fontxy(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new background(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new logaxis(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new axisscale(), 12));
			xyMiscNode.Nodes.Add(MakeNode(new ticks(), 12));

			TreeNode surfaceNode = new TreeNode("Surface Charts");
			treeView.Nodes.Add(surfaceNode);

			surfaceNode.Nodes.Add(MakeNode(new surface(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surface2(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surface3(), 13));
			surfaceNode.Nodes.Add(MakeNode(new scattersurface(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surfaceaxis(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surfacelighting(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surfaceshading(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surfacewireframe(), 13));
			surfaceNode.Nodes.Add(MakeNode(new surfaceperspective(), 13));

            TreeNode threeDScatterNode = new TreeNode("3D Scatter Charts");
            treeView.Nodes.Add(threeDScatterNode);

            threeDScatterNode.Nodes.Add(MakeNode(new threedscatter(), 14));
            threeDScatterNode.Nodes.Add(MakeNode(new threedscatter2(), 14));
            threeDScatterNode.Nodes.Add(MakeNode(new threedscattergroups(), 14));
            threeDScatterNode.Nodes.Add(MakeNode(new threedscatteraxis(), 14));

			TreeNode polarNode = new TreeNode("Polar Charts");
			treeView.Nodes.Add(polarNode);

			polarNode.Nodes.Add(MakeNode(new simpleradar(), 15));
			polarNode.Nodes.Add(MakeNode(new multiradar(), 15));
			polarNode.Nodes.Add(MakeNode(new stackradar(), 15));
			polarNode.Nodes.Add(MakeNode(new polarline(), 15));
			polarNode.Nodes.Add(MakeNode(new polararea(), 15));
			polarNode.Nodes.Add(MakeNode(new polarspline(), 15));
			polarNode.Nodes.Add(MakeNode(new polarscatter(), 15));
			polarNode.Nodes.Add(MakeNode(new polarbubble(), 15));
			polarNode.Nodes.Add(MakeNode(new polarvector(), 15));
			polarNode.Nodes.Add(MakeNode(new rose(), 15));
			polarNode.Nodes.Add(MakeNode(new stackrose(), 15));
			polarNode.Nodes.Add(MakeNode(new polarzones(), 15));
			polarNode.Nodes.Add(MakeNode(new polarzones2(), 15));
											
			TreeNode pyramidNode = new TreeNode("Pyramids/Cones/Funnels");
			treeView.Nodes.Add(pyramidNode);
			
			pyramidNode.Nodes.Add(MakeNode(new simplepyramid(), 16));
			pyramidNode.Nodes.Add(MakeNode(new threedpyramid(), 16));
			pyramidNode.Nodes.Add(MakeNode(new rotatedpyramid(), 16));
			pyramidNode.Nodes.Add(MakeNode(new cone(), 16));
			pyramidNode.Nodes.Add(MakeNode(new funnel(), 16));
			pyramidNode.Nodes.Add(MakeNode(new pyramidelevation(), 16));
			pyramidNode.Nodes.Add(MakeNode(new pyramidrotation(), 16));
			pyramidNode.Nodes.Add(MakeNode(new pyramidgap(), 16));
										   
			TreeNode angularMeterNode = new TreeNode("Angular Meters/Guages");
            treeView.Nodes.Add(angularMeterNode);

            angularMeterNode.Nodes.Add(MakeNode(new semicirclemeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new colorsemicirclemeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new blacksemicirclemeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new whitesemicirclemeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new semicirclemeterreadout(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new roundmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new colorroundmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new blackroundmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new whiteroundmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new neonroundmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new roundmeterreadout(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new rectangularmeter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new squareameter(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new angularpointer(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new angularpointer2(), 17));
            angularMeterNode.Nodes.Add(MakeNode(new iconameter(), 17));

            TreeNode linearMeterNode = new TreeNode("Linear Meters/Guages");
            treeView.Nodes.Add(linearMeterNode);

            linearMeterNode.Nodes.Add(MakeNode(new hlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new colorhlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new blackhlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new whitehlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new hlinearmeterorientation(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new vlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new colorvlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new blackvlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new whitevlinearmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new vlinearmeterorientation(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new multihmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new multivmeter(), 21));
            linearMeterNode.Nodes.Add(MakeNode(new linearzonemeter(), 21));


            TreeNode barMeterNode = new TreeNode("Bar Meters/Guages");
            treeView.Nodes.Add(barMeterNode);

            barMeterNode.Nodes.Add(MakeNode(new hbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new colorhbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new blackhbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new whitehbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new hbarmeterorientation(), 22));
            barMeterNode.Nodes.Add(MakeNode(new vbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new colorvbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new blackvbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new whitevbarmeter(), 22));
            barMeterNode.Nodes.Add(MakeNode(new vbarmeterorientation(), 22));

            TreeNode trackCursorNode = new TreeNode("Programmable Track Cursor");
            treeView.Nodes.Add(trackCursorNode);

            trackCursorNode.Nodes.Add(MakeNode(new tracklegend(), 18));
            trackCursorNode.Nodes.Add(MakeNode(new tracklabel(), 18));
            trackCursorNode.Nodes.Add(MakeNode(new trackaxis(), 18));
            trackCursorNode.Nodes.Add(MakeNode(new trackbox(), 18));
            trackCursorNode.Nodes.Add(MakeNode(new trackfinance(), 18));
            trackCursorNode.Nodes.Add(MakeNode(new crosshair(), 18));
            
			TreeNode zoomScrollNode = new TreeNode("Zooming and Scrolling");
            treeView.Nodes.Add(zoomScrollNode);

            zoomScrollNode.Nodes.Add(MakeNode(new simplezoomscroll(), 19));
            zoomScrollNode.Nodes.Add(MakeNode(new zoomscrolltrack(), 19));
            zoomScrollNode.Nodes.Add(MakeNode(new zoomscrolltrack2(), 19));
            zoomScrollNode.Nodes.Add(MakeNode(new viewportcontroldemo(), 19));
            zoomScrollNode.Nodes.Add(MakeNode(new xyzoomscroll(), 19));

			TreeNode realTimeNode = new TreeNode("Realtime Charts");
			treeView.Nodes.Add(realTimeNode);

			realTimeNode.Nodes.Add(MakeNode(new realtimedemo(), 20));
            realTimeNode.Nodes.Add(MakeNode(new realtimetrack(), 20));
            realTimeNode.Nodes.Add(MakeNode(new realtimezoomscroll(), 20));
            
			treeView.SelectedNode = getNextChartNode(treeView.Nodes[0]);
		}

		/// <summary>
		/// Help method to add a demo module to the tree
		/// </summary>
		private TreeNode MakeNode(DemoModule module, int icon)
		{
			TreeNode node = new TreeNode(module.getName(), icon, icon);
			node.Tag = module;	// The demo module is attached to the node as the Tag property.
			return node;
		}
		
		/// <summary>
		/// Handler for the TreeView BeforeExpand event
		/// </summary>
		private void treeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// Change the node to use the open folder icon when the node expands
			e.Node.SelectedImageIndex = e.Node.ImageIndex = 1;
		}

		/// <summary>
		/// Handler for the TreeView BeforeCollapse event
		/// </summary>
		private void treeView_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// Change the node to use the clode folder icon when the node collapse
			e.Node.SelectedImageIndex = e.Node.ImageIndex = 0;
		}

		/// <summary>
		/// Handler for the TreeView AfterSelect event
		/// </summary>
		private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// Check if a demo module node is selected. Demo module is attached to the node
			// as the Tag property
			DemoModule demo = (DemoModule)e.Node.Tag;
			if (demo != null)
			{
				// Display the title
				title.Text = demo.getName();

				// Clear all ChartViewers
				for (int i = 0; i < chartViewers.Length; ++i)
					chartViewers[i].Visible = false;

				// Each demo module can display a number of charts
				int noOfCharts = demo.getNoOfCharts();
				for (int i = 0; i < noOfCharts; ++i)
				{
					demo.createChart(chartViewers[i], i);
					chartViewers[i].Visible = true;
				}
						
				// Now perform flow layout of the charts (viewers) 
				layoutCharts();

				// Add current node to the history array to support Back/Forward browsing
				addHistory(e.Node);
			}

			// Update the state of the buttons, status bar, etc.
			updateControls();
		}

		/// <summary>
		/// Helper method to perform a flow layout (left to right, top to bottom) of
		/// the chart.
		/// </summary>
		private void layoutCharts()
		{
			// Margin between the charts
			int margin = 5;

			// The first chart is always at the position as seen on the visual designer
			ChartDirector.WinChartViewer viewer =  chartViewers[0];
			viewer.Top = line.Bottom + margin;

			// The next chart is at the left of the first chart
			int currentX = viewer.Left + viewer.Width + margin;
			int currentY = viewer.Top;

			// The line height of a line of charts is that of the tallest chart in the line
			int lineHeight = viewer.Height;
			
			// Now layout subsequent charts (other than the first chart)
			for (int i = 1; i < chartViewers.Length; ++i)
			{
				viewer = chartViewers[i];
				
				// Layout only visible charts
				if (!viewer.Visible)
					continue;

				// Check for enough space on the left before it hits the panel border
				if (currentX + viewer.Width > rightPanel.Width)
				{
					// Not enough space, so move to the next line

					// Start of line is to align with the left of the first chart
					currentX = chartViewers[0].Left;

					// Adjust vertical by lineHeight plus a margin
					currentY += lineHeight + margin;

					// Reset the lineHeight
					lineHeight = 0;
				}
				
				// Put the chart at the current position
				viewer.Left = currentX;
				viewer.Top = currentY;

				// Advance the current position to the left prepare for the next chart
				currentX += viewer.Width + margin;

				// Update the lineHeight to reflect the tallest chart so far encountered
				// in the same line
				if (lineHeight < viewer.Height)
					lineHeight = viewer.Height;
			}
		}

		/// <summary>
		/// Add a selected node to the history array
		/// </summary>
		private void addHistory(TreeNode node)
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
		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//
			// Execute handler depending on which button is pressed
			//
			if (e.Button == BackPB)
				backHistory();
			else if (e.Button == ForwardPB)
				forwardHistory();
			else if (e.Button == NextPB)
				nextNode();
			else if (e.Button == PreviousPB)
				prevNode();
			else if (e.Button == ViewSourcePB)
				viewSource();
			else if (e.Button == HelpPB)
				help();
		}
		
		/// <summary>
		/// Handler for the Back button
		/// </summary>
		private void backHistory()
		{
			// Select the previous node in the history array
			if (currentHistoryIndex > 0)
				treeView.SelectedNode = history[--currentHistoryIndex];
		}

		/// <summary>
		/// Handler for the Forward button
		/// </summary>
		private void forwardHistory()
		{
			// Select the next node in the history array
			if (lastHistoryIndex > currentHistoryIndex)
				treeView.SelectedNode = history[++currentHistoryIndex];
		}

		/// <summary>
		/// Handler for the Next button
		/// </summary>
		private void nextNode()
		{
			// Getnext chart node of the current selected node by going down the tree
			TreeNode node = getNextChartNode(treeView.SelectedNode);
			
			// Display the node if available
			if (node != null)
				treeView.SelectedNode = node;
		}
		
		/// <summary>
		/// Helper method to go to the next chart node down the tree
		/// </summary>
		private TreeNode getNextChartNode(TreeNode node)
		{
			// Get the next node of by going down the tree
			node = getNextNode(node);
			
			// Skip nodes that are not associated with charts (e.g the folder nodes)
			while ((node != null) && (node.Tag == null))
				node = getNextNode(node);

			return node;
		}
			
		/// <summary>
		/// Helper method to go to the next node down the tree
		/// </summary>
		private TreeNode getNextNode(TreeNode node)
		{
			if (node == null)
				return null;
			
			// If the current node is a folder, the next node is the first child.
			if (node.FirstNode != null)
				return node.FirstNode;
			
			while (node != null)
			{
				// If there is a next sibling node, it is the next node.
				if (node.NextNode != null)
					return node.NextNode;

				// If there is no sibling node, the next node is the next sibling 
				// of the parent node. So we go back to the parent and loop again.
				node = node.Parent;
			}

			// No next node - must be already the last node.
            return null;
		}

		/// <summary>
		/// Handler for the Previous button
		/// </summary>
		private void prevNode()
		{
			// Get previous chart node of the current selected node by going up the tree
			TreeNode node = getPrevChartNode(treeView.SelectedNode);

			// Display the node if available
			if (node != null)
				treeView.SelectedNode = node;
		}
		
		/// <summary>
		/// Helper method to go to the previous chart node down the tree
		/// </summary>
		private TreeNode getPrevChartNode(TreeNode node)
		{
			// Get the prev node of by going up the tree
			node = getPrevNode(node);
			
			// Skip nodes that are not associated with charts (e.g the folder nodes)
			while ((node != null) && (node.Tag == null))
				node = getPrevNode(node);

			return node;
		}
		
		/// <summary>
		/// Helper method to go to the previous node up the tree
		/// </summary>
		private TreeNode getPrevNode(TreeNode node)
		{
			if (node == null)
				return null;
			
			// If there is no previous sibling node, the previous node must be its
			// parent. 
			if (node.PrevNode == null)
				return node.Parent;
		
			// If there is a previous sibling node, the previous node is the last
			// child of the previous sibling (if it has any child at all).
			node = node.PrevNode;
			while (node.LastNode != null)
				node = node.LastNode;

			return node;
		}

		/// <summary>
		/// Handler for the View Source button
		/// </summary>
		private void viewSource()
		{
			// Get the path name of the help file
			string helpFilePath = getHelpPath();
			if ((helpFilePath != null) && (currentHistoryIndex >= 0))
			{
                DemoModule m = (DemoModule)(history[currentHistoryIndex].Tag);
				Help.ShowHelp(this, helpFilePath, HelpNavigator.Topic, m.GetType().Name + ".htm");
			}
		}

		/// <summary>
		/// Handler for the View Doc button
		/// </summary>
		private void help()
		{
			// Get the path name of the help file
			string helpFilePath = getHelpPath();
			if (helpFilePath != null)
				Help.ShowHelp(this, helpFilePath);
		}

		/// <summary>
		/// Helper method to get the path name of the help file
		/// </summary>
		private string getHelpPath()
		{
			string helpfile = "netchartdir.chm";

			// To allow this program to run more robustly, we search for various
			// place for the help file.
			string [] placeToSearch = new string[] 
			{
				"",					// search the current directory
				"../../../../doc/",	// the install directory of the help file relative 
									// to the "bin/Debug" subdirectory of the VS.NET project
									// when installed by the ChartDirector installer.
				"../../",			// the project directory of VS.NET (relative to "bin/Debug")
				"../../../"			// the solution directory of VS.NET (relative to "bin/Debug")
			};

			// Return the first directory that contains the help file
			for (int i = 0; i < placeToSearch.Length; ++i)
			{
				if (System.IO.File.Exists(placeToSearch[i] + helpfile))
					return placeToSearch[i] + helpfile;
			}

			// Help file not found ???
			MessageBox.Show("Cannot locate help file " + helpfile + ".", "Help Error",
				MessageBoxButtons.OK, MessageBoxIcon.Error);

			return null;
		}

		/// <summary>
		/// Helper method to update the various controls
		/// </summary>
		private void updateControls()
		{
			//
			// Enable the various buttons there is really something they can do.
			//
			BackPB.Enabled = (currentHistoryIndex > 0);
			ForwardPB.Enabled = (lastHistoryIndex > currentHistoryIndex);
			NextPB.Enabled = (getNextChartNode(treeView.SelectedNode) != null);
			PreviousPB.Enabled = (getPrevChartNode(treeView.SelectedNode) != null);

			// The status bar always reflects the selected demo module
			if ((null != treeView.SelectedNode) && (null != treeView.SelectedNode.Tag))
			{
				DemoModule m = (DemoModule)treeView.SelectedNode.Tag;
				statusBarPanel.Text = " Module " + m.GetType().Name + ": " + m.getName();
			}
			else
				statusBarPanel.Text = "";
		}

		/// <summary>
		/// Handler for the panel layout event
		/// </summary>
		private void rightPanel_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			// Perform flow layout of the charts 
			if (chartViewers != null)
				layoutCharts();
		}

		/// <summary>
		/// Handler for the ClickHotSpot event, which occurs when the mouse clicks on 
		/// a hot spot on the chart
		/// </summary>
		private void chartViewer_ClickHotSpot(object sender, ChartDirector.WinHotSpotEventArgs e)
		{
			// In this demo, just list out the information provided by ChartDirector about hot spot
			new ParamViewer().Display(sender, e);
		}
	}
}
