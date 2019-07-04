using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lesson6
{
    class MyMatrix
    {
        int _MatrixSize = 100;
        ulong[,] _Matrix;

        public ulong[,] Matrix { get => _Matrix; set => _Matrix = value; }
        public int MatrixSize { get => _MatrixSize; }

        public MyMatrix()
        {
            Matrix = CreateMatrix();
        }

        public MyMatrix(int matrixSize)
        {
            Matrix = CreateMatrix(matrixSize);
            _MatrixSize = matrixSize;
        }

        public MyMatrix(int matrixSize, int MaxItem)
        {
            _MatrixSize = matrixSize;
            Matrix = CreateMatrix(matrixSize);
            this.FillRandom(MaxItem);
        }

        private ulong[,] CreateMatrix()
        {
            var array = new ulong[MatrixSize, MatrixSize];

            return array;
        }

        private ulong[,] CreateMatrix(int size)
        {
            var array = new ulong[size, size];

            return array;
        }

        public void FillRandom(int MaxItem = 11)
        {
            var rnd = new Random();

            for (int i = 0; i < MatrixSize; i++)
            {
                for (int j = 0; j < MatrixSize; j++)
                {
                    this._Matrix[i, j] = (ulong)rnd.Next(MaxItem);
                }
            }
        }

        internal MyMatrix MyltiplyParallele(MyMatrix B)
        {
            if (!this.CanMultiply(B))
                throw new Exception("Матрицы нельзя перемножить");

            MyMatrix res = new MyMatrix(this.Rows());
            Task[] tasks = new Task[this.Rows()];
            for (int i = 0; i < this.Rows(); i++)
            {
                int ii = i;
                tasks[i] = Task.Factory.StartNew(() => MultiplyRow(B, res, ii));
            }
            Task.WaitAll(tasks);

            return res;
        }

        internal MyMatrix Myltiply(MyMatrix B)
        {
            if (!this.CanMultiply(B))
                throw new Exception("Матрицы нельзя перемножить");

            MyMatrix res = new MyMatrix(this.Rows());
            for (int i = 0; i < this.Rows(); i++)
            {
                for (int j = 0; j < B.Columns(); j++)
                {
                    for (int k = 0; k < B.Rows(); k++)
                    {
                        try
                        {
                            res._Matrix[i, j] += this._Matrix[i, k] * B._Matrix[k, j];
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }
                    }
                }
            }

            return res;
        }

        private void MultiplyRow(MyMatrix B, MyMatrix res, int i)
        {
            for (int j = 0; j < B.Columns(); j++)
            {
                for (int k = 0; k < B.Rows(); k++)
                {
                    try
                    {
                        res._Matrix[i, j] += this._Matrix[i, k] * B._Matrix[k, j];
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }

        public int Rows()
        {
            return _Matrix.GetLength(0);
        }

        public int Columns()
        {
            return this._Matrix.GetLength(1);
        }

        public bool CanMultiply(MyMatrix MatrixB)
        {
            return (this._Matrix.GetLength(1) == MatrixB._Matrix.GetLength(0) &&
                this._Matrix.Rank == 2 && MatrixB._Matrix.Rank == 2);
        }
    }
}
