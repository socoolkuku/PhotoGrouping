<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImgBrowser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImgBrowser))
        Me.imglMain = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwMain = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWidth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.mnuImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnBrowseSource = New System.Windows.Forms.Button()
        Me.txtFolderPath = New System.Windows.Forms.TextBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.tvwFolder = New System.Windows.Forms.TreeView()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lbxTag = New System.Windows.Forms.ListBox()
        Me.imglUI = New System.Windows.Forms.ImageList(Me.components)
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblMain = New System.Windows.Forms.Label()
        Me.bkwMain = New System.ComponentModel.BackgroundWorker()
        Me.pbrFile = New System.Windows.Forms.ProgressBar()
        Me.pbrThumb = New System.Windows.Forms.ProgressBar()
        Me.btnIndex = New System.Windows.Forms.Button()
        Me.bkwIndex = New System.ComponentModel.BackgroundWorker()
        Me.pbrIndex = New System.Windows.Forms.ProgressBar()
        Me.btnRenameTag = New System.Windows.Forms.Button()
        Me.btnRemoveTag = New System.Windows.Forms.Button()
        Me.btnAddTag = New System.Windows.Forms.Button()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'imglMain
        '
        Me.imglMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imglMain.ImageSize = New System.Drawing.Size(100, 100)
        Me.imglMain.TransparentColor = System.Drawing.Color.Transparent
        '
        'lvwMain
        '
        Me.lvwMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwMain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colPath, Me.colWidth, Me.colHeight})
        Me.lvwMain.ContextMenuStrip = Me.mnuImage
        Me.lvwMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwMain.LargeImageList = Me.imglMain
        Me.lvwMain.Location = New System.Drawing.Point(3, 27)
        Me.lvwMain.Name = "lvwMain"
        Me.lvwMain.Size = New System.Drawing.Size(540, 435)
        Me.lvwMain.SmallImageList = Me.imglMain
        Me.lvwMain.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwMain.TabIndex = 0
        Me.lvwMain.TileSize = New System.Drawing.Size(300, 300)
        Me.lvwMain.UseCompatibleStateImageBehavior = False
        '
        'colName
        '
        Me.colName.Text = "Name"
        '
        'colPath
        '
        Me.colPath.Text = "Path"
        '
        'colWidth
        '
        Me.colWidth.Text = "Width"
        '
        'colHeight
        '
        Me.colHeight.Text = "Height"
        '
        'mnuImage
        '
        Me.mnuImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnuImage.Name = "mnuImage"
        Me.mnuImage.Size = New System.Drawing.Size(61, 4)
        '
        'btnBrowseSource
        '
        Me.btnBrowseSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseSource.Location = New System.Drawing.Point(12, 7)
        Me.btnBrowseSource.Name = "btnBrowseSource"
        Me.btnBrowseSource.Size = New System.Drawing.Size(40, 23)
        Me.btnBrowseSource.TabIndex = 8
        Me.btnBrowseSource.Text = "..."
        Me.btnBrowseSource.UseVisualStyleBackColor = True
        '
        'txtFolderPath
        '
        Me.txtFolderPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPath.Location = New System.Drawing.Point(58, 7)
        Me.txtFolderPath.Name = "txtFolderPath"
        Me.txtFolderPath.Size = New System.Drawing.Size(197, 26)
        Me.txtFolderPath.TabIndex = 9
        '
        'btnLoad
        '
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.Location = New System.Drawing.Point(261, 6)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 28)
        Me.btnLoad.TabIndex = 10
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'tvwFolder
        '
        Me.tvwFolder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwFolder.Location = New System.Drawing.Point(-4, 0)
        Me.tvwFolder.Name = "tvwFolder"
        Me.tvwFolder.Size = New System.Drawing.Size(211, 432)
        Me.tvwFolder.TabIndex = 12
        '
        'splitMain
        '
        Me.splitMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splitMain.Location = New System.Drawing.Point(12, 41)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.TabControl1)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.lblCount)
        Me.splitMain.Panel2.Controls.Add(Me.lblMain)
        Me.splitMain.Panel2.Controls.Add(Me.lvwMain)
        Me.splitMain.Size = New System.Drawing.Size(760, 464)
        Me.splitMain.SplitterDistance = 210
        Me.splitMain.TabIndex = 13
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ImageList = Me.imglUI
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(211, 466)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.tvwFolder)
        Me.TabPage1.ImageKey = "folders"
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(203, 433)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Folder"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnRenameTag)
        Me.TabPage2.Controls.Add(Me.btnRemoveTag)
        Me.TabPage2.Controls.Add(Me.btnAddTag)
        Me.TabPage2.Controls.Add(Me.lbxTag)
        Me.TabPage2.ImageKey = "tags"
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(203, 433)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Tag"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lbxTag
        '
        Me.lbxTag.AllowDrop = True
        Me.lbxTag.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbxTag.FormattingEnabled = True
        Me.lbxTag.ItemHeight = 20
        Me.lbxTag.Location = New System.Drawing.Point(-4, 0)
        Me.lbxTag.Name = "lbxTag"
        Me.lbxTag.Size = New System.Drawing.Size(211, 364)
        Me.lbxTag.TabIndex = 0
        '
        'imglUI
        '
        Me.imglUI.ImageStream = CType(resources.GetObject("imglUI.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglUI.TransparentColor = System.Drawing.Color.Transparent
        Me.imglUI.Images.SetKeyName(0, "folders")
        Me.imglUI.Images.SetKeyName(1, "tags")
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(446, 4)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(97, 18)
        Me.lblCount.TabIndex = 1
        Me.lblCount.Text = "No file"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMain
        '
        Me.lblMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMain.Location = New System.Drawing.Point(3, 4)
        Me.lblMain.Name = "lblMain"
        Me.lblMain.Size = New System.Drawing.Size(437, 18)
        Me.lblMain.TabIndex = 1
        Me.lblMain.Text = "None"
        '
        'bkwMain
        '
        Me.bkwMain.WorkerReportsProgress = True
        Me.bkwMain.WorkerSupportsCancellation = True
        '
        'pbrFile
        '
        Me.pbrFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbrFile.Location = New System.Drawing.Point(672, 7)
        Me.pbrFile.Name = "pbrFile"
        Me.pbrFile.Size = New System.Drawing.Size(100, 10)
        Me.pbrFile.TabIndex = 15
        '
        'pbrThumb
        '
        Me.pbrThumb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbrThumb.Location = New System.Drawing.Point(672, 16)
        Me.pbrThumb.Name = "pbrThumb"
        Me.pbrThumb.Size = New System.Drawing.Size(100, 10)
        Me.pbrThumb.TabIndex = 16
        '
        'btnIndex
        '
        Me.btnIndex.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIndex.Location = New System.Drawing.Point(342, 6)
        Me.btnIndex.Name = "btnIndex"
        Me.btnIndex.Size = New System.Drawing.Size(75, 28)
        Me.btnIndex.TabIndex = 17
        Me.btnIndex.Text = "Index"
        Me.btnIndex.UseVisualStyleBackColor = True
        '
        'bkwIndex
        '
        Me.bkwIndex.WorkerReportsProgress = True
        Me.bkwIndex.WorkerSupportsCancellation = True
        '
        'pbrIndex
        '
        Me.pbrIndex.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbrIndex.Location = New System.Drawing.Point(672, 24)
        Me.pbrIndex.Name = "pbrIndex"
        Me.pbrIndex.Size = New System.Drawing.Size(100, 10)
        Me.pbrIndex.TabIndex = 18
        '
        'btnRenameTag
        '
        Me.btnRenameTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRenameTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRenameTag.Image = Global.ImageBrowser.My.Resources.Resources.edit
        Me.btnRenameTag.Location = New System.Drawing.Point(60, 379)
        Me.btnRenameTag.Name = "btnRenameTag"
        Me.btnRenameTag.Size = New System.Drawing.Size(48, 48)
        Me.btnRenameTag.TabIndex = 3
        Me.btnRenameTag.UseVisualStyleBackColor = True
        '
        'btnRemoveTag
        '
        Me.btnRemoveTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveTag.Image = Global.ImageBrowser.My.Resources.Resources.delete
        Me.btnRemoveTag.Location = New System.Drawing.Point(149, 379)
        Me.btnRemoveTag.Name = "btnRemoveTag"
        Me.btnRemoveTag.Size = New System.Drawing.Size(48, 48)
        Me.btnRemoveTag.TabIndex = 2
        Me.btnRemoveTag.UseVisualStyleBackColor = True
        '
        'btnAddTag
        '
        Me.btnAddTag.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddTag.Image = Global.ImageBrowser.My.Resources.Resources.plus
        Me.btnAddTag.Location = New System.Drawing.Point(6, 379)
        Me.btnAddTag.Name = "btnAddTag"
        Me.btnAddTag.Size = New System.Drawing.Size(48, 48)
        Me.btnAddTag.TabIndex = 1
        Me.btnAddTag.UseVisualStyleBackColor = True
        '
        'ImgBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 518)
        Me.Controls.Add(Me.pbrIndex)
        Me.Controls.Add(Me.btnIndex)
        Me.Controls.Add(Me.pbrThumb)
        Me.Controls.Add(Me.pbrFile)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.txtFolderPath)
        Me.Controls.Add(Me.btnBrowseSource)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ImgBrowser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Image Browser"
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imglMain As System.Windows.Forms.ImageList
    Friend WithEvents lvwMain As System.Windows.Forms.ListView
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnBrowseSource As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWidth As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeight As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtFolderPath As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents tvwFolder As System.Windows.Forms.TreeView
    Friend WithEvents splitMain As System.Windows.Forms.SplitContainer
    Friend WithEvents bkwMain As System.ComponentModel.BackgroundWorker
    Friend WithEvents pbrFile As System.Windows.Forms.ProgressBar
    Friend WithEvents pbrThumb As System.Windows.Forms.ProgressBar
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lbxTag As System.Windows.Forms.ListBox
    Friend WithEvents btnAddTag As System.Windows.Forms.Button
    Friend WithEvents btnRemoveTag As System.Windows.Forms.Button
    Friend WithEvents btnRenameTag As System.Windows.Forms.Button
    Friend WithEvents btnIndex As System.Windows.Forms.Button
    Friend WithEvents bkwIndex As System.ComponentModel.BackgroundWorker
    Friend WithEvents pbrIndex As System.Windows.Forms.ProgressBar
    Friend WithEvents mnuImage As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblMain As System.Windows.Forms.Label
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents imglUI As System.Windows.Forms.ImageList
End Class
