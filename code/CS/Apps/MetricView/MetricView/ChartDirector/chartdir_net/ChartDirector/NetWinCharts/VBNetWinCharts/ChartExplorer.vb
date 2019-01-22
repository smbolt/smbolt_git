Imports ChartDirector

Public Class ChartExplorer
    Inherits System.Windows.Forms.Form

    '
    ' Array to hold all WinChartViewers in the form to allow processing using loops
    '
    Private chartViewers As ChartDirector.WinChartViewer()

    '
    ' Data structure to handle the Back / Forward buttons. We support up to
    ' 100 histories. We store histories as the nodes begin selected.
    '
    Private history(99) As TreeNode

    ' The array index of the currently selected node in the history array.
    Private currentHistoryIndex As Integer = -1

    ' The array index of the last valid entry in the history array so that we
    ' know if we can use the Forward button.
    Private lastHistoryIndex As Integer = -1

    Private Sub ChartExplorer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Array to hold all WinChartViewers in the form to allow processing 
        ' using loops
        '
        chartViewers = New ChartDirector.WinChartViewer() _
        { _
            chartViewer1, chartViewer2, chartViewer3, chartViewer4, _
            chartViewer5, chartViewer6, chartViewer7, chartViewer8 _
        }

        '
        ' Build the tree view on the left to represent available demo modules
        '
        Dim pieNode As TreeNode = New TreeNode("Pie Charts")
        treeView.Nodes.Add(pieNode)

        pieNode.Nodes.Add(MakeNode(New simplepie(), 2))
        pieNode.Nodes.Add(MakeNode(New threedpie(), 2))
        pieNode.Nodes.Add(MakeNode(New multidepthpie(), 2))
        pieNode.Nodes.Add(MakeNode(New sidelabelpie(), 2))
        pieNode.Nodes.Add(MakeNode(New circlelabelpie(), 2))
        pieNode.Nodes.Add(MakeNode(New legendpie(), 2))
        pieNode.Nodes.Add(MakeNode(New legendpie2(), 2))
        pieNode.Nodes.Add(MakeNode(New explodedpie(), 2))
        pieNode.Nodes.Add(MakeNode(New iconpie(), 2))
        pieNode.Nodes.Add(MakeNode(New iconpie2(), 2))
        pieNode.Nodes.Add(MakeNode(New multipie(), 2))
        pieNode.Nodes.Add(MakeNode(New donut(), 2))
        pieNode.Nodes.Add(MakeNode(New threeddonut(), 2))
        pieNode.Nodes.Add(MakeNode(New icondonut(), 2))
        pieNode.Nodes.Add(MakeNode(New texturedonut(), 2))
        pieNode.Nodes.Add(MakeNode(New concentric(), 2))
        pieNode.Nodes.Add(MakeNode(New pieshading(), 2))
        pieNode.Nodes.Add(MakeNode(New threedpieshading(), 2))
        pieNode.Nodes.Add(MakeNode(New donutshading(), 2))
        pieNode.Nodes.Add(MakeNode(New threeddonutshading(), 2))
        pieNode.Nodes.Add(MakeNode(New fontpie(), 2))
        pieNode.Nodes.Add(MakeNode(New threedanglepie(), 2))
        pieNode.Nodes.Add(MakeNode(New threeddepthpie(), 2))
        pieNode.Nodes.Add(MakeNode(New shadowpie(), 2))
        pieNode.Nodes.Add(MakeNode(New anglepie(), 2))
        pieNode.Nodes.Add(MakeNode(New donutwidth(), 2))

        Dim barNode As TreeNode = New TreeNode("Bar Charts")
        treeView.Nodes.Add(barNode)

        barNode.Nodes.Add(MakeNode(New simplebar(), 3))
        barNode.Nodes.Add(MakeNode(New simplebar2(), 3))
        barNode.Nodes.Add(MakeNode(New barlabel(), 3))
        barNode.Nodes.Add(MakeNode(New colorbar(), 3))
        barNode.Nodes.Add(MakeNode(New colorbar2(), 3))
        barNode.Nodes.Add(MakeNode(New softlightbar(), 3))
        barNode.Nodes.Add(MakeNode(New glasslightbar(), 3))
        barNode.Nodes.Add(MakeNode(New gradientbar(), 3))
        barNode.Nodes.Add(MakeNode(New cylinderlightbar(), 3))
        barNode.Nodes.Add(MakeNode(New threedbar(), 3))
        barNode.Nodes.Add(MakeNode(New cylinderbar(), 3))
        barNode.Nodes.Add(MakeNode(New polygonbar(), 3))
        barNode.Nodes.Add(MakeNode(New stackedbar(), 3))
        barNode.Nodes.Add(MakeNode(New percentbar(), 3))
        barNode.Nodes.Add(MakeNode(New multibar(), 3))
        barNode.Nodes.Add(MakeNode(New softmultibar(), 3))
        barNode.Nodes.Add(MakeNode(New glassmultibar(), 3))
        barNode.Nodes.Add(MakeNode(New gradientmultibar(), 3))
        barNode.Nodes.Add(MakeNode(New multicylinder(), 3))
        barNode.Nodes.Add(MakeNode(New multishapebar(), 3))
        barNode.Nodes.Add(MakeNode(New overlapbar(), 3))
        barNode.Nodes.Add(MakeNode(New multistackbar(), 3))
        barNode.Nodes.Add(MakeNode(New depthbar(), 3))
        barNode.Nodes.Add(MakeNode(New posnegbar(), 3))
        barNode.Nodes.Add(MakeNode(New hbar(), 3))
        barNode.Nodes.Add(MakeNode(New dualhbar(), 3))
        barNode.Nodes.Add(MakeNode(New markbar(), 3))
        barNode.Nodes.Add(MakeNode(New pareto(), 3))
        barNode.Nodes.Add(MakeNode(New varwidthbar(), 3))
        barNode.Nodes.Add(MakeNode(New gapbar(), 3))

        Dim lineNode As TreeNode = New TreeNode("Line Charts")
        treeView.Nodes.Add(lineNode)

        lineNode.Nodes.Add(MakeNode(New simpleline(), 4))
        lineNode.Nodes.Add(MakeNode(New compactline(), 4))
        lineNode.Nodes.Add(MakeNode(New threedline(), 4))
        lineNode.Nodes.Add(MakeNode(New multiline(), 4))
        lineNode.Nodes.Add(MakeNode(New multiline2(), 4))
        lineNode.Nodes.Add(MakeNode(New symbolline(), 4))
        lineNode.Nodes.Add(MakeNode(New symbolline2(), 4))
        lineNode.Nodes.Add(MakeNode(New missingpoints(), 4))
        lineNode.Nodes.Add(MakeNode(New unevenpoints(), 4))
        lineNode.Nodes.Add(MakeNode(New splineline(), 4))
        lineNode.Nodes.Add(MakeNode(New stepline(), 4))
        lineNode.Nodes.Add(MakeNode(New linefill(), 4))
        lineNode.Nodes.Add(MakeNode(New linecompare(), 4))
        lineNode.Nodes.Add(MakeNode(New errline(), 4))
        lineNode.Nodes.Add(MakeNode(New multisymbolline(), 4))
        lineNode.Nodes.Add(MakeNode(New binaryseries(), 4))
        lineNode.Nodes.Add(MakeNode(New customsymbolline(), 4))
        lineNode.Nodes.Add(MakeNode(New rotatedline(), 4))
        lineNode.Nodes.Add(MakeNode(New xyline(), 4))

        Dim trendNode As TreeNode = New TreeNode("Trending and Curve Fitting")
        treeView.Nodes.Add(trendNode)

        trendNode.Nodes.Add(MakeNode(New trendline(), 5))
        trendNode.Nodes.Add(MakeNode(New scattertrend(), 5))
        trendNode.Nodes.Add(MakeNode(New confidenceband(), 5))
        trendNode.Nodes.Add(MakeNode(New paramcurve(), 5))
        trendNode.Nodes.Add(MakeNode(New curvefitting(), 5))

        Dim scatterNode As TreeNode = New TreeNode("Scatter/Bubble/Vector Charts")
        treeView.Nodes.Add(scatterNode)

        scatterNode.Nodes.Add(MakeNode(New scatter(), 6))
        scatterNode.Nodes.Add(MakeNode(New builtinsymbols(), 6))
        scatterNode.Nodes.Add(MakeNode(New scattersymbols(), 6))
        scatterNode.Nodes.Add(MakeNode(New scatterlabels(), 6))
        scatterNode.Nodes.Add(MakeNode(New bubble(), 6))
        scatterNode.Nodes.Add(MakeNode(New threedbubble(), 6))
        scatterNode.Nodes.Add(MakeNode(New threedbubble2(), 6))
        scatterNode.Nodes.Add(MakeNode(New threedbubble3(), 6))
        scatterNode.Nodes.Add(MakeNode(New bubblescale(), 6))
        scatterNode.Nodes.Add(MakeNode(New vector(), 6))

        Dim areaNode As TreeNode = New TreeNode("Area Charts")
        treeView.Nodes.Add(areaNode)

        areaNode.Nodes.Add(MakeNode(New simplearea(), 7))
        areaNode.Nodes.Add(MakeNode(New enhancedarea(), 7))
        areaNode.Nodes.Add(MakeNode(New arealine(), 7))
        areaNode.Nodes.Add(MakeNode(New threedarea(), 7))
        areaNode.Nodes.Add(MakeNode(New patternarea(), 7))
        areaNode.Nodes.Add(MakeNode(New stackedarea(), 7))
        areaNode.Nodes.Add(MakeNode(New threedstackarea(), 7))
        areaNode.Nodes.Add(MakeNode(New percentarea(), 7))
        areaNode.Nodes.Add(MakeNode(New deptharea(), 7))
        areaNode.Nodes.Add(MakeNode(New rotatedarea(), 7))

        Dim boxNode As TreeNode = New TreeNode("Floating Box/Waterfall Charts")
        treeView.Nodes.Add(boxNode)

        boxNode.Nodes.Add(MakeNode(New boxwhisker(), 8))
        boxNode.Nodes.Add(MakeNode(New boxwhisker2(), 8))
        boxNode.Nodes.Add(MakeNode(New hboxwhisker(), 8))
        boxNode.Nodes.Add(MakeNode(New floatingbox(), 8))
        boxNode.Nodes.Add(MakeNode(New waterfall(), 8))
        boxNode.Nodes.Add(MakeNode(New posnegwaterfall(), 8))

        Dim ganttNode As TreeNode = New TreeNode("Gantt Charts")
        treeView.Nodes.Add(ganttNode)

        ganttNode.Nodes.Add(MakeNode(New gantt(), 9))
        ganttNode.Nodes.Add(MakeNode(New colorgantt(), 9))
        ganttNode.Nodes.Add(MakeNode(New layergantt(), 9))

        Dim contourNode As TreeNode = New TreeNode("Contour Charts/Heat Maps")
        treeView.Nodes.Add(contourNode)

        contourNode.Nodes.Add(MakeNode(New contour(), 10))
        contourNode.Nodes.Add(MakeNode(New contourinterpolate(), 10))
        contourNode.Nodes.Add(MakeNode(New contourcolor(), 10))
        contourNode.Nodes.Add(MakeNode(New contourlegend(), 10))
        contourNode.Nodes.Add(MakeNode(New smoothcontour(), 10))
        contourNode.Nodes.Add(MakeNode(New scattercontour(), 10))


        Dim financeNode As TreeNode = New TreeNode("Finance Charts")
        treeView.Nodes.Add(financeNode)

        financeNode.Nodes.Add(MakeNode(New hloc(), 11))
        financeNode.Nodes.Add(MakeNode(New candlestick(), 11))
        financeNode.Nodes.Add(MakeNode(New finance(), 11))
        financeNode.Nodes.Add(MakeNode(New finance2(), 11))
        financeNode.Nodes.Add(MakeNode(New financesymbols(), 11))
        financeNode.Nodes.Add(MakeNode(New financedemo(), 11))

        Dim xyMiscNode As TreeNode = New TreeNode("Other XY Chart Features")
        treeView.Nodes.Add(xyMiscNode)

        xyMiscNode.Nodes.Add(MakeNode(New markzone(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New markzone2(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New yzonecolor(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New xzonecolor(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New dualyaxis(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New dualxaxis(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New multiaxes(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New fourq(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New datatable(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New datatable2(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New fontxy(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New background(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New logaxis(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New axisscale(), 12))
        xyMiscNode.Nodes.Add(MakeNode(New ticks(), 12))

        Dim surfaceNode As TreeNode = New TreeNode("Surface Charts")
        treeView.Nodes.Add(surfaceNode)

        surfaceNode.Nodes.Add(MakeNode(New surface(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surface2(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surface3(), 13))
        surfaceNode.Nodes.Add(MakeNode(New scattersurface(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surfaceaxis(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surfacelighting(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surfaceshading(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surfacewireframe(), 13))
        surfaceNode.Nodes.Add(MakeNode(New surfaceperspective(), 13))

        Dim threeDScatterNode As TreeNode = New TreeNode("3D Scatter Charts")
        treeView.Nodes.Add(threeDScatterNode)

        threeDScatterNode.Nodes.Add(MakeNode(New threedscatter(), 14))
        threeDScatterNode.Nodes.Add(MakeNode(New threedscatter2(), 14))
        threeDScatterNode.Nodes.Add(MakeNode(New threedscattergroups(), 14))
        threeDScatterNode.Nodes.Add(MakeNode(New threedscatteraxis(), 14))

        Dim polarNode As TreeNode = New TreeNode("Polar Charts")
        treeView.Nodes.Add(polarNode)

        polarNode.Nodes.Add(MakeNode(New simpleradar(), 15))
        polarNode.Nodes.Add(MakeNode(New multiradar(), 15))
        polarNode.Nodes.Add(MakeNode(New stackradar(), 15))
        polarNode.Nodes.Add(MakeNode(New polarline(), 15))
        polarNode.Nodes.Add(MakeNode(New polararea(), 15))
        polarNode.Nodes.Add(MakeNode(New polarspline(), 15))
        polarNode.Nodes.Add(MakeNode(New polarscatter(), 15))
        polarNode.Nodes.Add(MakeNode(New polarbubble(), 15))
        polarNode.Nodes.Add(MakeNode(New polarvector(), 15))
        polarNode.Nodes.Add(MakeNode(New rose(), 15))
        polarNode.Nodes.Add(MakeNode(New stackrose(), 15))
        polarNode.Nodes.Add(MakeNode(New polarzones(), 15))
        polarNode.Nodes.Add(MakeNode(New polarzones2(), 15))

        Dim pyramidNode As TreeNode = New TreeNode("Pyramids/Cones/Funnels")
        treeView.Nodes.Add(pyramidNode)

        pyramidNode.Nodes.Add(MakeNode(New simplepyramid(), 16))
        pyramidNode.Nodes.Add(MakeNode(New threedpyramid(), 16))
        pyramidNode.Nodes.Add(MakeNode(New rotatedpyramid(), 16))
        pyramidNode.Nodes.Add(MakeNode(New cone(), 16))
        pyramidNode.Nodes.Add(MakeNode(New funnel(), 16))
        pyramidNode.Nodes.Add(MakeNode(New pyramidelevation(), 16))
        pyramidNode.Nodes.Add(MakeNode(New pyramidrotation(), 16))
        pyramidNode.Nodes.Add(MakeNode(New pyramidgap(), 16))

        Dim angularMeterNode As TreeNode = New TreeNode("Angular Meters/Guages")
        treeView.Nodes.Add(angularMeterNode)

        angularMeterNode.Nodes.Add(MakeNode(New semicirclemeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New colorsemicirclemeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New blacksemicirclemeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New whitesemicirclemeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New semicirclemeterreadout(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New roundmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New colorroundmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New blackroundmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New whiteroundmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New neonroundmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New roundmeterreadout(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New rectangularmeter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New squareameter(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New angularpointer(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New angularpointer2(), 17))
        angularMeterNode.Nodes.Add(MakeNode(New iconameter(), 17))

        Dim linearMeterNode As TreeNode = New TreeNode("Linear Meters/Guages")
        treeView.Nodes.Add(linearMeterNode)

        linearMeterNode.Nodes.Add(MakeNode(New hlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New colorhlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New blackhlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New whitehlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New hlinearmeterorientation(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New vlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New colorvlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New blackvlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New whitevlinearmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New vlinearmeterorientation(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New multihmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New multivmeter(), 21))
        linearMeterNode.Nodes.Add(MakeNode(New linearzonemeter(), 21))

        Dim barMeterNode As TreeNode = New TreeNode("Bar Meters/Guages")
        treeView.Nodes.Add(barMeterNode)

        barMeterNode.Nodes.Add(MakeNode(New hbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New colorhbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New blackhbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New whitehbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New hbarmeterorientation(), 22))
        barMeterNode.Nodes.Add(MakeNode(New vbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New colorvbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New blackvbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New whitevbarmeter(), 22))
        barMeterNode.Nodes.Add(MakeNode(New vbarmeterorientation(), 22))

        Dim trackCursorNode As TreeNode = New TreeNode("Programmable Track Cursor")
        treeView.Nodes.Add(trackCursorNode)

        trackCursorNode.Nodes.Add(MakeNode(New tracklegend(), 18))
        trackCursorNode.Nodes.Add(MakeNode(New tracklabel(), 18))
        trackCursorNode.Nodes.Add(MakeNode(New trackaxis(), 18))
        trackCursorNode.Nodes.Add(MakeNode(New trackbox(), 18))
        trackCursorNode.Nodes.Add(MakeNode(New trackfinance(), 18))
        trackCursorNode.Nodes.Add(MakeNode(New crosshair(), 18))

        Dim zoomScrollNode As TreeNode = New TreeNode("Zooming and Scrolling")
        treeView.Nodes.Add(zoomScrollNode)

        zoomScrollNode.Nodes.Add(MakeNode(New simplezoomscroll(), 19))
        zoomScrollNode.Nodes.Add(MakeNode(New zoomscrolltrack(), 19))
        zoomScrollNode.Nodes.Add(MakeNode(New zoomscrolltrack2(), 19))
        zoomScrollNode.Nodes.Add(MakeNode(New viewportcontroldemo(), 19))
        zoomScrollNode.Nodes.Add(MakeNode(New xyzoomscroll(), 19))

        Dim realTimeNode As TreeNode = New TreeNode("Realtime Charts")
        treeView.Nodes.Add(realTimeNode)

        realTimeNode.Nodes.Add(MakeNode(New realtimedemo(), 20))
        realTimeNode.Nodes.Add(MakeNode(New realtimetrack(), 20))
        realTimeNode.Nodes.Add(MakeNode(New realtimezoomscroll(), 20))

        treeView.SelectedNode = getNextChartNode(treeView.Nodes(0))
    End Sub

    ' <summary>
    ' Help method to add a demo module to the tree
    ' </summary>
    Private Function MakeNode(ByVal m As DemoModule, ByVal icon As Integer) As TreeNode
        Dim node As TreeNode = New TreeNode(m.getName(), icon, icon)
        node.Tag = m ' The demo module is attached to the node as the Tag property.
        Return node
    End Function

    ' <summary>
    ' Handler for the TreeView BeforeExpand event
    ' </summary>
    Private Sub treeView_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles treeView.BeforeExpand
        e.Node.SelectedImageIndex = e.Node.ImageIndex = 1
    End Sub

    ' <summary>
    ' Handler for the TreeView BeforeCollapse event
    ' </summary>
    Private Sub treeView_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles treeView.BeforeCollapse
        e.Node.SelectedImageIndex = e.Node.ImageIndex = 0
    End Sub

    ' <summary>
    ' Handler for the TreeView AfterSelect event
    ' </summary>
    Private Sub treeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeView.AfterSelect
        ' Check if a demo module node is selected. Demo module is attached to the node
        ' as the Tag property
        Dim demo As DemoModule = e.Node.Tag
        If Not IsNothing(demo) Then
            ' Display the title
            title.Text = demo.getName()

            ' Clear all ChartViewers
            Dim i As Integer
            For i = 0 To chartViewers.Length - 1
                chartViewers(i).Visible = False
            Next

            ' Each demo module can display a number of charts
            Dim noOfCharts As Integer = demo.getNoOfCharts()
            For i = 0 To noOfCharts - 1
                demo.createChart(chartViewers(i), CStr(i))
                chartViewers(i).Visible = True
            Next

            ' Now perform flow layout of the charts (viewers) 
            layoutCharts()

            ' Add current node to the history array to support Back/Forward browsing
            addHistory(e.Node)
        End If

        ' Update the state of the buttons, status bar, etc.
        updateControls()
    End Sub

    ' <summary>
    ' Helper method to perform a flow layout (left to right, top to bottom) of
    ' the chart.
    ' </summary>
    Private Sub layoutCharts()
        ' Margin between the charts
        Dim margin As Integer = 5

        ' The first chart is always at the position as seen on the visual designer
        Dim viewer As ChartDirector.WinChartViewer = chartViewers(0)
        viewer.Top = line.Bottom + margin

        ' The next chart is at the left of the first chart
        Dim currentX As Integer = viewer.Left + viewer.Width + margin
        Dim currentY As Integer = viewer.Top

        ' The line height of a line of charts is that of the tallest chart in the line
        Dim lineHeight As Integer = viewer.Height

        ' Now layout subsequent charts (other than the first chart)
        Dim i As Integer
        For i = 1 To chartViewers.Length - 1
            viewer = chartViewers(i)

            ' Layout only visible charts
            If viewer.Visible Then
                ' Check for enough space on the left before it hits the panel border
                If (currentX + viewer.Width > rightPanel.Width) Then
                    ' Not enough space, so move to the next line
                    ' Start of line is to align with the left of the first chart
                    currentX = chartViewers(0).Left

                    ' Adjust vertical by lineHeight plus a margin
                    currentY += lineHeight + margin

                    ' Reset the lineHeight
                    lineHeight = 0
                End If

                ' Put the chart at the current position
                viewer.Left = currentX
                viewer.Top = currentY

                ' Advance the current position to the left prepare for the next chart
                currentX += viewer.Width + margin

                ' Update the lineHeight to reflect the tallest chart so far encountered
                ' in the same line
                If (lineHeight < viewer.Height) Then
                    lineHeight = viewer.Height
                End If
            End If
        Next
    End Sub

    ' <summary>
    ' Add a selected node to the history array
    ' </summary>
    Private Sub addHistory(ByVal node As TreeNode)

        ' Don't add if selected node is current node to avoid duplication.
        If currentHistoryIndex >= 0 AndAlso node Is history(currentHistoryIndex) Then
            Return
        End If

        ' Check if the history array is full
        If currentHistoryIndex + 1 >= history.Length Then
            ' History array is full. Remove oldest 25% from the history array.
            ' We add 1 to make sure at least 1 item is removed.
            Dim itemsToDiscard As Integer = history.Length / 4 + 1

            ' Remove the oldest items by shifting the array. 
            Dim i As Integer
            For i = itemsToDiscard To history.Length - 1
                history(i - itemsToDiscard) = history(i)
            Next

            ' Adjust index because array is shifted.
            currentHistoryIndex = history.Length - itemsToDiscard
        End If

        ' Add node to history array
        currentHistoryIndex = currentHistoryIndex + 1
        history(currentHistoryIndex) = node

        ' After adding a new node, the forward button is always disabled. (This
        ' is consistent with normal browser behaviour.) That means the last 
        ' history node is always assumed to be the current node. 
        lastHistoryIndex = currentHistoryIndex
    End Sub

    ' <summary>
    ' Handler for the ToolBar ButtonClick event
    ' </summary>
    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        '
        ' Execute handler depending on which button is pressed
        '
        If e.Button Is BackPB Then
            backHistory()
        ElseIf e.Button Is ForwardPB Then
            forwardHistory()
        ElseIf e.Button Is NextPB Then
            nextNode()
        ElseIf e.Button Is PreviousPB Then
            prevNode()
        ElseIf e.Button Is ViewSourcePB Then
            viewSource()
        ElseIf e.Button Is HelpPB Then
            help()
        End If
    End Sub

    ' <summary>
    ' Handler for the Back button
    ' </summary>
    Private Sub backHistory()
        ' Select the previous node in the history array
        If currentHistoryIndex > 0 Then
            currentHistoryIndex = currentHistoryIndex - 1
            treeView.SelectedNode = history(currentHistoryIndex)
        End If
    End Sub

    ' <summary>
    ' Handler for the Forward button
    ' </summary>
    Private Sub forwardHistory()
        ' Select the next node in the history array
        If lastHistoryIndex > currentHistoryIndex Then
            currentHistoryIndex = currentHistoryIndex + 1
            treeView.SelectedNode = history(currentHistoryIndex)
        End If
    End Sub

    ' <summary>
    ' Handler for the Next button
    ' </summary>
    Private Sub nextNode()
        ' Getnext chart node of the current selected node by going down the tree
        Dim node As TreeNode = getNextChartNode(treeView.SelectedNode)

        ' Display the node if available
        If Not IsNothing(node) Then
            treeView.SelectedNode = node
        End If
    End Sub

    ' <summary>
    ' Helper method to go to the next chart node down the tree
    ' </summary>
    Private Function getNextChartNode(ByVal node As TreeNode) As TreeNode
        ' Get the next node of by going down the tree
        node = getNextNode(node)

        ' Skip nodes that are not associated with charts (e.g the folder nodes)
        Do While Not IsNothing(node) AndAlso IsNothing(node.Tag)
            node = getNextNode(node)
        Loop

        Return node
    End Function

    ' <summary>
    ' Helper method to go to the next node down the tree
    ' </summary>
    Private Function getNextNode(ByVal node As TreeNode) As TreeNode
        If IsNothing(node) Then
            Return Nothing
        End If

        ' If the current node is a folder, the next node is the first child.
        If Not IsNothing(node.FirstNode) Then
            Return node.FirstNode
        End If

        Do While Not IsNothing(node)
            ' If there is a next sibling node, it is the next node.
            If Not IsNothing(node.NextNode) Then
                Return node.NextNode
            End If
            ' If there is no sibling node, the next node is the next sibling 
            ' of the parent node. So we go back to the parent and loop again.
            node = node.Parent
        Loop

        ' No next node - must be already the last node.
        Return Nothing
    End Function

    ' <summary>
    ' Handler for the Previous button
    ' </summary>
    Private Sub prevNode()
        ' Get previous chart node of the current selected node by going up the tree
        Dim node As TreeNode = getPrevChartNode(treeView.SelectedNode)

        ' Display the node if available
        If Not IsNothing(node) Then
            treeView.SelectedNode = node
        End If
    End Sub

    ' <summary>
    ' Helper method to go to the previous chart node down the tree
    ' </summary>
    Private Function getPrevChartNode(ByVal node As TreeNode) As TreeNode
        ' Get the prev node of by going up the tree
        node = getPrevNode(node)

        ' Skip nodes that are not associated with charts (e.g the folder nodes)
        Do While Not IsNothing(node) AndAlso IsNothing(node.Tag)
            node = getPrevNode(node)
        Loop

        Return node
    End Function

    ' <summary>
    ' Helper method to go to the previous node up the tree
    ' </summary>
    Private Function getPrevNode(ByVal node As TreeNode) As TreeNode
        If IsNothing(node) Then
            Return Nothing
        End If

        ' If there is no previous sibling node, the previous node must be its
        ' parent. 
        If IsNothing(node.PrevNode) Then
            Return node.Parent
        End If

        ' If there is a previous sibling node, the previous node is the last
        ' child of the previous sibling (if it has any child at all).
        node = node.PrevNode
        Do While Not IsNothing(node.LastNode)
            node = node.LastNode
        Loop

        Return node
    End Function

    ' <summary>
    ' Handler for the View Source button
    ' </summary>
    Private Sub viewSource()
        ' Get the path name of the help file
        Dim helpFilePath As String = getHelpPath()
        If Not IsNothing(helpFilePath) And currentHistoryIndex >= 0 Then
            Dim m As DemoModule = history(currentHistoryIndex).Tag
            System.Windows.Forms.Help.ShowHelp(Me, helpFilePath, HelpNavigator.Topic, TypeName(m) & ".htm")
        End If
    End Sub

    ' <summary>
    ' Handler for the View Doc button
    ' </summary>
    Private Sub help()
        ' Get the path name of the help file
        Dim helpFilePath As String = getHelpPath()
        If Not IsNothing(helpFilePath) Then
            System.Windows.Forms.Help.ShowHelp(Me, helpFilePath)
        End If
    End Sub

    ' <summary>
    ' Helper method to get the path name of the help file
    ' </summary>
    Private Function getHelpPath() As String
        Dim helpfile As String = "netchartdir.chm"

        ' To allow this program to run more robustly, we search for various
        ' place for the help file.
        ' (1) search the current directory
        ' (2) the install directory of the help file relative to the "bin/Debug" subdirectory of
        '     the VS.NET project when installed by the ChartDirector installer.
        ' (3) the project directory of VS.NET (relative to "bin/Debug")
        ' (4) the solution directory of VS.NET (relative to "bin/Debug")
        Dim placeToSearch As String() = New String() {"", "../../../../doc/", "../../", "../../../"}

        ' Return the first directory that contains the help file
        Dim i As Integer
        For i = 0 To placeToSearch.Length - 1
            If System.IO.File.Exists(placeToSearch(i) & helpfile) Then
                Return placeToSearch(i) & helpfile
            End If
        Next

        ' Help file not found ???
        MessageBox.Show("Cannot locate help file " + helpfile + ".", "Help Error", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)

        Return Nothing
    End Function

    ' <summary>
    ' Helper method to update the various controls
    ' </summary>
    Private Sub updateControls()
        '
        ' Enable the various buttons there is really something they can do.
        '
        BackPB.Enabled = (currentHistoryIndex > 0)
        ForwardPB.Enabled = (lastHistoryIndex > currentHistoryIndex)
        NextPB.Enabled = Not IsNothing(getNextChartNode(treeView.SelectedNode))
        PreviousPB.Enabled = Not IsNothing(getPrevChartNode(treeView.SelectedNode))

        ' The status bar always reflects the selected demo module
        If Not IsNothing(treeView.SelectedNode) AndAlso Not IsNothing(treeView.SelectedNode.Tag) Then
            Dim m As DemoModule = treeView.SelectedNode.Tag
            statusBarPanel.Text = " Module " & DirectCast(m, Object).GetType().Name & ": " & m.getName()
        Else
            statusBarPanel.Text = ""
        End If
    End Sub

    ' <summary>
    ' Handler for the panel layout event
    ' </summary>
    Private Sub rightPanel_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles rightPanel.Layout
        ' Perform flow layout of the charts 
        If Not IsNothing(chartViewers) Then
            layoutCharts()
        End If
    End Sub

    ' <summary>
    ' Handler for the ClickHotSpot event, which occurs when the mouse clicks on 
    ' a hot spot on the chart
    ' </summary>
    Private Sub chartViewer_ClickHotSpot(ByVal sender As Object, ByVal e As ChartDirector.WinHotSpotEventArgs) _
        Handles chartViewer1.ClickHotSpot, chartViewer2.ClickHotSpot, chartViewer3.ClickHotSpot, chartViewer4.ClickHotSpot, _
        chartViewer5.ClickHotSpot, chartViewer6.ClickHotSpot, chartViewer7.ClickHotSpot, chartViewer8.ClickHotSpot
        'In this demo, just list out the information provided by ChartDirector about hot spot
        Dim f As New ParamViewer()
        f.Display(sender, e)
    End Sub

End Class
