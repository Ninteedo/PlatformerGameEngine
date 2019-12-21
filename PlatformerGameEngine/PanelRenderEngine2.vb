'Richard Holmes
'22/03/2019
'Panel Render Engine v2

Public Class PanelRenderEngine2

    Public renderPanel As Panel
    Public renderResolution As New Size(640, 480)      'how many pixels the game is played at, scaled on render to the size of the render panel

    Public spriteFolderLocation As String
    'Public actorFolderLocation As String
    Public levelFolderLocation As String

#Region "Rendering"

    Public Sub DoGameRender(ByRef actorList As Actor())
        'renders a list of actors
        'this uses double buffering to render the result to a buffer, which is eventually rendered to the user
        'this eliminates flickering caused by directly rendering one actor at a time

        Using canvas As New PaintEventArgs(renderPanel.CreateGraphics, New Rectangle(New Point(0, 0), renderPanel.Size))
            canvas.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
            Using context As BufferedGraphicsContext = BufferedGraphicsManager.Current
                context.MaximumBuffer = renderPanel.Size
                Using renderLayer As BufferedGraphics = context.Allocate(canvas.Graphics, canvas.ClipRectangle)
                    renderLayer.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor

                    'fills the background with white
                    renderLayer.Graphics.FillRectangle(New SolidBrush(Color.White), New RectangleF(New PointF(0, 0), renderPanel.Size))

                    If Not IsNothing(actorList) Then
                        'sorts actorList by layer ascending using an insertion sort
                        For i1 As Integer = 1 To UBound(actorList)
                            If actorList(i1 - 1).Layer > actorList(i1).Layer Then
                                'finds the index where the current actor should be
                                Dim i2 As Integer = 0
                                Do While actorList(i2).Layer < actorList(i1).Layer
                                    i2 += 1
                                Loop

                                'moves the actor to its new index, shifting actors in between along
                                Dim temp As Actor = actorList(i1).Clone()
                                actorList = RemoveItem(actorList, i1)
                                actorList = InsertItem(actorList, temp, i2)
                            End If
                        Next

                        'renders each actor in new sorted order
                        For actorIndex As Integer = 0 To UBound(actorList)
                            Dim currentActor As Actor = actorList(actorIndex)
                            If Not IsNothing(currentActor.Sprites) AndAlso currentActor.CurrentSprite <= UBound(currentActor.Sprites) And currentActor.CurrentSprite >= 0 Then
                                Dim renderSprite As Sprite = currentActor.Sprites(currentActor.CurrentSprite)
                                Dim actHitbox As RectangleF = currentActor.Hitbox
                                Dim renderArea As RectangleF = ScaleRect(actHitbox, RenderScale)
                                renderLayer.Graphics.DrawImage(renderSprite.Bitmap, renderArea)
                            End If
                        Next actorIndex
                    End If

                    renderLayer.Render()
                End Using
            End Using
        End Using
    End Sub

    Public Sub DoGameRenderNoSort(ByVal actorList As Actor())
        'renders the actor list, but the inputted actor list is not sorted like it is in the regular DoGameRender
        'used in the level editor because it causes the actors to move around when layers are changed

        DoGameRender(actorList)
    End Sub

    Public ReadOnly Property RenderScale As SizeF   'the render scaling used by the renderer
        Get
            Return New SizeF(renderPanel.Size.Width / renderResolution.Width, renderPanel.Size.Height / renderResolution.Height)
        End Get
    End Property

#End Region

End Class