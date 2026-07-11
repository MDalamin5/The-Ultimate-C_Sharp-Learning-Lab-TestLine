using System.Globalization;

namespace AdvArrays;
public class advArray
{
    public static void Run()
    {
        int [][] matrix = new int[3][];
        matrix = [
            [1, 3],
            [4, 5],
            [8, 3]
        ];

        for(int i=0; i<matrix.Length; i++)
        {
            for(int j=0; j<matrix[i].Length; j++)
            {
                Console.Write($"{matrix[i][j]} ");
                
            }
            Console.WriteLine();
            
        }
    }
}