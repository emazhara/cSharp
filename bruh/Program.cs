using System;
using System.IO;

namespace bruh
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Creating the first matrix:\n");
            Console.Write("1. Insert the matrix manually\n" +
                          "2. Load the matrix from file\n");
            var choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Matrix matrix1 = null;
            Matrix matrix2 = null;
            switch (choice)
            {
                case 1:
                    Console.Write("Insert the height of the matrix\n");
                    var height = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Insert the width of the matrix\n");
                    var width = Convert.ToInt32(Console.ReadLine());
                    var matrix = new double[height, width];
                    for (int i = 0; i < height; i++)
                    {
                        Console.Write("Insert string of the matrix number " + (i + 1) + " separating the numbers with whitespace\n");
                        var matrString = Console.ReadLine();
                        var stringNums = matrString.Split(" ");
                        if (stringNums.Length != width)
                            throw new Exception("Incorrect amount of numbers in the line\n");
                        for(int j = 0; j < width; j++){
                            matrix[i,j] = Convert.ToDouble(stringNums[j]);
                        }
                    }
                    Console.Clear();
                    Console.Write("Creating the matrix...\n");
                    matrix1 = new Matrix(height, width, matrix);
                    Console.Write("Matrix created successfully\n");
                    break;
                case 2:
                    Console.Write("Insert the filename\n");
                    var filename = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Loading matrix from the file...\n");
                    matrix1 = new Matrix(filename);
                    Console.Write("Matrix loaded successfully\n");
                    break;
                default:
                    Console.Write("Incorrect choice\n");
                    break;
            }
            Console.Write("Creating the second matrix:\n");
            Console.Write("1. Insert the matrix manually\n" +
                          "2. Load the matrix from file\n");
            var choice2 = Convert.ToInt32(Console.ReadLine());
            switch (choice2)
            {
                case 1:
                    Console.Write("Insert the height of the matrix\n");
                    var height = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Insert the width of the matrix\n");
                    var width = Convert.ToInt32(Console.ReadLine());
                    var matrix = new double[height, width];
                    for (int i = 0; i < height; i++)
                    {
                        Console.Write("Insert string of the matrix number " + (i + 1) + " separating the numbers with whitespace\n");
                        var matrString = Console.ReadLine();
                        var stringNums = matrString.Split(" ");
                        if (stringNums.Length != width)
                            throw new Exception("Incorrect amount of numbers in the line\n");
                        for(int j = 0; j < width; j++){
                            matrix[i,j] = Convert.ToDouble(stringNums[i]);
                        }
                    }
                    Console.Clear();
                    Console.Write("Creating the matrix...\n");
                    matrix2 = new Matrix(height, width, matrix);
                    Console.Write("Matrix created successfully\n");
                    break;
                case 2:
                    Console.Write("Insert the filename\n");
                    var filename = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Loading matrix from the file...\n");
                    matrix2 = new Matrix(filename);
                    Console.Write("Matrix loaded successfully\n");
                    break;
                default:
                    Console.Write("Incorrect choice\n");
                    break;
            }
            while (true)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                Console.Clear();
                Console.Write("Select the action:\n" +
                              "1. Multiply\n" +
                              "2. Save to file\n" +
                              "3. Display\n" +
                              "4. Exit\n");
                var choice3 = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice3)
                {
                    case 1:
                        Matrix result = matrix1.MatrixMul(matrix2);
                        Console.Write(result.ToString());
                        break;
                    case 2:
                        Console.Write("Select the matrix: num. 1 or num. 2\n");
                        var matrnumber = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Insert the filename\n");
                        var filename = Console.ReadLine();
                        switch (matrnumber)
                        {
                            case 1:
                                 matrix1.SaveMatrix(filename);
                                 Console.Write("Saved successfully\n");
                                 break;
                            case 2:
                                matrix2.SaveMatrix(filename);
                                Console.Write("Saved successfully\n");
                                break;
                            default:
                                Console.Write("Incorrect choice\n");
                                break;
                        }
                        break;
                    case 3:
                        Console.Write("Matrix num. 1:\n" + matrix1.ToString());
                        Console.Write("Matrix num. 2:\n" + matrix2.ToString());
                        break;
                    case 4:
                        return;
                    default:
                        Console.Write("Incorrect choice\n");
                        break;
                }
            }
        }
    }

    public class Matrix
    {
        private readonly int _height;
        private readonly int _width;
        private readonly double[,] _matrix;

        public Matrix(int height, int width, double[,] matrix)
        {
            this._height = height;
            this._width = width;
            this._matrix = matrix;
        }
        public Matrix(string filename)
        {
            try
            {
                var reader = new StreamReader(filename);
                var line = reader.ReadLine();
                var firstLine = line.Split(" ");
                _width = firstLine.Length;
                _height++;
                while (reader.ReadLine() != null) _height++;
                _matrix = new double[_height,_width];
                reader.Close();
                reader = new StreamReader(filename);
                int matrLine = 0;
                for (line = reader.ReadLine(); matrLine < _height; matrLine++)
                {
                    var numbers = line.Split(" ");
                    if (numbers.Length != _width)
                        throw new Exception("Incorrect amount of numbers in the line\n");
                    for(int i = 0; i < numbers.Length; i++){
                        this._matrix[matrLine,i] = Convert.ToDouble(numbers[i]);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        public Matrix MatrixMul(Matrix o)
        {
            if (this == null || o == null)
            {
                throw new Exception("Empty matrices\n");
            }
            if (this._width != o._height)
            {
                throw new Exception("Matrices cannot be multiplied due to the difference in the size\n");
            }
            var newMatr = new double[this._height, o._width];
            for (int i = 0; i < this._height; i++)
            {
                for (int j = 0; j < o._width; j++)
                {
                    for (int k = 0; k < this._width; k++)
                        newMatr[i,j] += this._matrix[i, k] * o._matrix[k, j];
                }
            }

            return new Matrix(this._height, o._width, newMatr);
        }

        public new string ToString()
        {
            string answer = "";
            for (int i = 0; i < this._height; i++)
            {
                for (int j = 0; j < this._width - 1; j++)
                {
                    answer = String.Concat(answer, this._matrix[i, j].ToString());
                    answer = String.Concat(answer, " ");
                }
                answer = String.Concat(answer, this._matrix[i, this._width - 1].ToString());
                answer = String.Concat(answer, "\n");
            }
            return answer;
        }

        public void SaveMatrix(string filename)
        {
            var writer = new StreamWriter(filename);
            writer.Write(this.ToString());
            writer.Close();
        }
    }
}