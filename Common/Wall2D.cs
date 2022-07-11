using System.Numerics;

namespace Common;

public interface IWall2D
{
   /// <summary>
   /// 墙的起点
   /// </summary>
   public Vector2 VertexFrom { get; set; }
   
   /// <summary>
   /// 墙的终点
   /// </summary>
   public Vector2 VertexTo { get; set; } 
   
   /// <summary>
   /// 强的标准向量
   /// </summary>
   public Vector2 VertexNormal{get; set; }

   /// <summary>
   /// 获取中点
   /// </summary>
   /// <returns></returns>
   public Vector2 GetCenter();
}

public class Wall2D : IWall2D
{
   /// <summary>
   /// 墙的起点
   /// </summary>
   public Vector2 VertexFrom { get; set; }

   /// <summary>
   /// 墙的终点
   /// </summary>
   public Vector2 VertexTo { get; set; }

   /// <summary>
   /// 墙的标准向量
   /// </summary>
   public Vector2 VertexNormal { get; set; }

   /// <summary>
   /// 获取中点
   /// </summary>
   /// <returns></returns>
   public Vector2 GetCenter()
   {
      return (VertexTo - VertexFrom) / 2.0f;
   }

   /// <summary>
   /// 计算标准向量
   /// </summary>
   protected void CalculateNormal()
   {
      var temp = Vector2.Normalize(VertexTo - VertexFrom);
      VertexNormal = new Vector2(temp.Y, -temp.X);
   }

   public Wall2D()
   {
   }

   public Wall2D(Vector2 vertexFrom, Vector2 vertexTo)
   {
      VertexFrom = vertexFrom;
      VertexTo = vertexTo;
   }

   public Wall2D(Vector2 vertexFrom, Vector2 vertexTo, Vector2 vertexNormal)
   {
      VertexFrom = vertexFrom;
      VertexTo = vertexTo;
      VertexNormal = vertexNormal;
   }

   public virtual void Render(bool renderNormals = false)
   {

   }
}
