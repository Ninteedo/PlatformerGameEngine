'Richard Holmes
'22/03/2019
'Panel Render Engine

Public Class RenderEngine

    ReadOnly _renderPanel As Panel
    Public RenderResolution As New Size(640, 480)      'how many pixels the game is played at, scaled on render to the size of the render panel

#Region "Constructors"

    Public Sub New(renderPanel As Panel, Optional renderResolution As Size = Nothing)
        _renderPanel = renderPanel
        If Not renderResolution.IsEmpty Then
            Me.RenderResolution = renderResolution
        End If
    End Sub

#End Region

#Region "Rendering"

    Public Sub DoGameRender(ByRef actorList As Actor())
        'renders a list of Actors
        'this uses double buffering to render the result to a buffer, which is eventually rendered to the user
        'this eliminates flickering caused by directly rendering one actor at a time

        Using canvas As New PaintEventArgs(_renderPanel.CreateGraphics, New Rectangle(New Point(0, 0), _renderPanel.Size))
            canvas.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor     'disables anti-aliasing
            Using context As BufferedGraphicsContext = BufferedGraphicsManager.Current
                context.MaximumBuffer = _renderPanel.Size
                Using renderLayer As BufferedGraphics = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                    renderLayer.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor    'disables anti-aliasing

                    'fills the background with white
                    renderLayer.Graphics.FillRectangle(New SolidBrush(Color.White), New RectangleF(New PointF(0, 0), _renderPanel.Size))

                    If Not IsNothing(actorList) Then
                        'sorts actorList by layer ascending using an insertion sort
                        For i As Integer = 1 To UBound(actorList)
                            If actorList(i - 1).Layer > actorList(i).Layer Then
                                'finds the index where the current actor should be
                                Dim j As Integer = 0
                                Do While actorList(j).Layer < actorList(i).Layer
                                    j += 1
                                Loop

                                'moves the actor to its new index, shifting Actors in between along
                                Dim temp As Actor = actorList(i).Clone()
                                actorList = RemoveItem(actorList, i)
                                actorList = InsertItem(actorList, temp, j)
                            End If
                        Next

                        'renders each actor in new sorted order
                        For Each currentActor As Actor In actorList
                            If Not IsNothing(currentActor.Sprites) And currentActor.SpriteIndex <= UBound(currentActor.Sprites) And currentActor.SpriteIndex >= 0 Then
                                Dim renderSprite As Sprite = currentActor.Sprites(currentActor.SpriteIndex)
                                Dim actHitbox As RectangleF = currentActor.Hitbox 'New RectangleF(currentActor.Hitbox.Location, New SizeF(currentActor.Hitbox.Width + 0.5, currentActor.Hitbox.Height + 0.5))
                                Dim renderArea As RectangleF = ScaleRect(actHitbox, RenderScale)
                                renderArea.Offset(ScaleSize(RenderScale, currentActor.Scale * 0.25))
                                renderArea.Inflate(ScaleSize(RenderScale, currentActor.Scale * 0.25))
                                renderLayer.Graphics.DrawImage(renderSprite.Bitmap, renderArea)
                            End If
                        Next
                    End If

                    'renders the buffer to the panel
                    renderLayer.Render()
                End Using
            End Using
        End Using
    End Sub

    Public Sub DoGameRenderNoSort(ByVal actorList As Actor())
        'renders the actor list, but the inputted actor list is not sorted like it is in the regular DoGameRender
        'used in the level editor because it causes the Actors to move around when layers are changed

        DoGameRender(If(Not IsNothing(actorList), actorList.Clone(), Nothing))
    End Sub

    Public ReadOnly Property RenderScale As SizeF   'the render scaling used by the renderer
        Get
            Return New SizeF(_renderPanel.Size.Width / RenderResolution.Width, _renderPanel.Size.Height / RenderResolution.Height)
        End Get
    End Property

#End Region

End Class