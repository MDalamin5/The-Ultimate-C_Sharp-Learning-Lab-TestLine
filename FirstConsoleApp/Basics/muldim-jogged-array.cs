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


        // JoggedArray

        int [][] jogged = new int[4][];

        /*
        1, 2, 3
        1,
        2, 4
        3, 4, 5, 6
        */
        //init array column via different size
        jogged[0] = new int[3];
        jogged[1] = new int[1];
        jogged[2] = new int[2];
        jogged[3] = new int[4];

        //array input take from user.
        for(int i=0; i<jogged.Length; i++)
        {
            for(int j=0; j<jogged[i].Length; j++)
            {
                Console.Write($"Please Enter the value of: Jogged[{i}][{j}]: ");
                
                int userInput = Convert.ToInt32(Console.ReadLine());
                jogged[i][j] = userInput;
            }
        }

        // jogged[0] = [1, 2, 4];
        // jogged[1] = [2];
        // jogged[2] = [2, 4];
        // jogged[3] = [5, 6, 7, 7];

        


        for(int i=0; i<jogged.Length; i++)
        {
            for(int j=0; j<jogged[i].Length; j++)
            {
                Console.Write($"{jogged[i][j]} ");
                
            }
            Console.WriteLine();
            
        }
    }
}