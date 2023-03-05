using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridRenderer : Graphic
{
    //thickness指的是边框的薄厚
    public float thickness;
    public Vector2Int gridSize;
    private float width;
    private float height;
    private float cellWidth;
    private float cellHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        //设置宽高(自适应头盔)
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        //网格的多少
        cellWidth = width / gridSize.x;
        cellHeight = height / gridSize.y;
        //这个for循环绘制所有的网格
        //后面count用来找第几个点
        int count = 0;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DrawCell(vh, x, y, count);
                count++;
            }
        }
    }

    void DrawCell(VertexHelper vh, int x, int y, int index)
    {
        //迭代后的
        float xPos = cellWidth * x;
        float yPos = cellHeight * y;
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos, yPos+cellHeight);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos+cellWidth, yPos+cellHeight);
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos+cellWidth, yPos);
        vh.AddVert(vertex);
        //内部の顶点


        // 0--1 
        // |  |
        // 2--3

        // vh.AddTriangle(0,1,2);
        // vh.AddTriangle(2,3,0);
        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);
        vertex.position = new Vector3(xPos+(distance), yPos+(distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos+(distance), yPos+(cellHeight-distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos+(cellWidth-distance), yPos+(cellHeight-distance));
        vh.AddVert(vertex);
        vertex.position = new Vector3(xPos+(cellWidth-distance), yPos+(distance));
        vh.AddVert(vertex);
        //一组内部顶点
        int offset = index * 8;
        vh.AddTriangle(offset+0, offset+1,offset+ 5);
        vh.AddTriangle(offset+5, offset+4, offset+0);
        vh.AddTriangle(offset+1, offset+2, offset+6);
        vh.AddTriangle(offset+6, offset+5, offset+1);
        vh.AddTriangle(offset+2, offset+3, offset+7);
        vh.AddTriangle(offset+7, offset+6, offset+2);
        vh.AddTriangle(offset+3, offset+0, offset+4);
        vh.AddTriangle(offset+4, offset+7, offset+3);
    }
}